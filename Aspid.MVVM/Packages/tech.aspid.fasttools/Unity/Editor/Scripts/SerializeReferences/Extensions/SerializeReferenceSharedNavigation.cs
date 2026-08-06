using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// IMGUI-side group navigation for shared references, mirroring the UIToolkit field's click-to-highlight: a click
    /// on the "Shared reference #N" message picks the group's next member in document order (wrapping), expands the
    /// collapsed parents hiding it, scrolls the inspector to it once it is painted, and pulses every member in the
    /// group colour. The UIToolkit field does not use this — its live-field registry (see
    /// <c>SerializeReferenceField</c>) navigates through the element tree instead; only the pulse timings and the
    /// document-order cycling are kept in lock-step between the two.
    /// </summary>
    internal static class SerializeReferenceSharedNavigation
    {
        // Mirrors the UIToolkit field's FlashAlpha / FlashDurationMs / FlashHoldFraction so the pulse reads the same
        // in both UIs: full tint for the first FlashHoldFraction, then a linear fade.
        private const float FlashAlpha = 0.25f;
        private const double FlashSeconds = 1.6;
        private const float FlashHoldFraction = 0.35f;

        // Keep the revealed member out of the viewport's very edge: scroll it to about a quarter down, so a line or
        // two of context stays visible above it.
        private const float RevealViewportFraction = 0.25f;

        // A member revealed by expanding its parents only gets a rect once it is painted; if no repaint reports it
        // within this window (e.g. the property vanished), the pending reveal is dropped.
        private const double RevealTimeoutSeconds = 1.0;

        // The pending reveal: the group member the next repaint should scroll to (set by NavigateFrom after it
        // expanded the member's parents, consumed by RevealIfPending when the member reports where it was painted).
        private static int _revealTarget;
        private static long _revealRid;
        private static string _revealPath;
        private static double _revealUntil;

        // The per-group navigation cursor, keyed by (target object, rid). Advancing from the cursor — not the clicked
        // field — lets repeated clicks on the SAME notice walk the whole group. Mirrors the UIToolkit field's cursor.
        private static readonly Dictionary<(int target, long rid), string> NavigationCursor = new();

        // The active pulse: every drawn member of (target, rid) except the clicked one tints in the group colour
        // until the deadline. Driven by an EditorApplication.update repaint loop while it lasts.
        private static int _flashTarget;
        private static long _flashRid;
        private static string _flashExceptPath;
        private static double _flashUntil;

        /// <summary>
        /// The "Shared reference #N" message was clicked: reveal the WHOLE group — expand the collapsed parents over
        /// every member (a member inside collapsed parents is not drawn at all) — then scroll to the next member in
        /// document order (cursor-based, so repeated clicks walk the group; see <see cref="RevealIfPending"/>) and
        /// start the group pulse.
        /// Call with the inspector's LIVE property (valid — the message click runs synchronously inside Draw), never
        /// a persistent copy: isExpanded is cached per SerializedObject instance at construction, so the ancestor
        /// expansion only reaches the inspector's foldouts when written through its own long-lived SerializedObject.
        /// </summary>
        public static void NavigateFrom(SerializedProperty property)
        {
            var target = property.serializedObject.targetObject;
            if (target == null) return;

            var rid = property.managedReferenceId;
            if (rid < 0) return;

            // The group's canonical document order (shared with the UIToolkit field) backs the cycling.
            var group = SerializeReferenceHelpers.GetSharedReferenceGroupPaths(property);
            if (group.Count < 2) return;

            var selfPath = property.propertyPath;

            // Reveal the WHOLE group, not just the scroll target: the pulse covers every drawn member, so every
            // member must be drawn. IMGUI re-reads isExpanded on every repaint, so one pass reaches any depth.
            foreach (var path in group)
                if (path != selfPath)
                    ExpandAncestors(property.serializedObject, path);

            // The scroll target: the next member in document order after the cursor; the clicked field is skipped.
            var key = (target.GetInstanceID(), rid);
            var start = NavigationCursor.TryGetValue(key, out var cursor) ? IndexOf(group, cursor) : -1;
            if (start < 0) start = IndexOf(group, selfPath);

            string nextPath = null;
            for (var step = 1; step <= group.Count && nextPath is null; step++)
            {
                var candidate = group[(start + step) % group.Count];
                if (candidate != selfPath) nextPath = candidate;
            }

            if (nextPath is null) return;
            NavigationCursor[key] = nextPath;

            StartFlash(target.GetInstanceID(), rid, selfPath);

            _revealTarget = target.GetInstanceID();
            _revealRid = rid;
            _revealPath = nextPath;
            _revealUntil = EditorApplication.timeSinceStartup + RevealTimeoutSeconds;
        }

        /// <summary>
        /// Reports where a shared field was painted this Repaint. When the field is the pending reveal's member —
        /// possibly just un-hidden by <see cref="NavigateFrom"/>'s expansion — the inspector scrolls to it. Call from
        /// the drawer with the field's FULL rect (header + children).
        /// </summary>
        public static void RevealIfPending(SerializedProperty property, Rect fieldRect)
        {
            if (Event.current.type != EventType.Repaint) return;
            if (_revealPath is null || EditorApplication.timeSinceStartup > _revealUntil) return;

            var target = property.serializedObject.targetObject;
            if (target == null || target.GetInstanceID() != _revealTarget) return;
            if (property.managedReferenceId != _revealRid) return;
            if (property.propertyPath != _revealPath) return;

            _revealPath = null;
            var screenRect = GUIUtility.GUIToScreenRect(fieldRect);

            // Scrolling mutates the host ScrollView mid-Repaint; defer it to the next editor tick.
            EditorApplication.delayCall += () => ScrollTo(screenRect);
        }

        /// <summary>
        /// True while the group pulse covers this field — a member of the pulsing (target, rid) group other than the
        /// clicked one — with the overlay's current fade alpha. The drawer paints the overlay over its full rect.
        /// </summary>
        public static bool TryGetFlashAlpha(SerializedProperty property, out float alpha)
        {
            alpha = 0f;

            var remaining = _flashUntil - EditorApplication.timeSinceStartup;
            if (remaining <= 0) return false;

            var target = property.serializedObject.targetObject;
            if (target == null || target.GetInstanceID() != _flashTarget) return false;
            if (property.managedReferenceId != _flashRid) return false;
            if (property.propertyPath == _flashExceptPath) return false;

            // Hold-then-fade matching the UIToolkit pulse's easing.
            var t = 1f - (float)(remaining / FlashSeconds);
            alpha = t < FlashHoldFraction
                ? FlashAlpha
                : FlashAlpha * (1f - (t - FlashHoldFraction) / (1f - FlashHoldFraction));
            return true;
        }

        private static int IndexOf(IReadOnlyList<string> paths, string path)
        {
            for (var i = 0; i < paths.Count; i++)
                if (paths[i] == path)
                    return i;

            return -1;
        }

        // Expands every '.'-prefix ancestor so the property is actually drawn; prefixes that are not real properties
        // (the bare ".Array" marker) resolve to null and are skipped. The member itself is left alone — revealing it
        // must not toggle its own foldout.
        private static void ExpandAncestors(SerializedObject serializedObject, string path)
        {
            for (var dot = path.IndexOf('.'); dot >= 0; dot = path.IndexOf('.', dot + 1))
            {
                using var ancestor = serializedObject.FindProperty(path[..dot]);
                if (ancestor != null) ancestor.isExpanded = true;
            }
        }

        private static void StartFlash(int target, long rid, string exceptPath)
        {
            _flashTarget = target;
            _flashRid = rid;
            _flashExceptPath = exceptPath;
            _flashUntil = EditorApplication.timeSinceStartup + FlashSeconds;

            // IMGUI only repaints on events, so the fade needs its own repaint driver for its lifetime.
            EditorApplication.update -= DriveFlashRepaints;
            EditorApplication.update += DriveFlashRepaints;
        }

        private static void DriveFlashRepaints()
        {
            InternalEditorUtility.RepaintAllViews();
            if (EditorApplication.timeSinceStartup < _flashUntil) return;

            EditorApplication.update -= DriveFlashRepaints;
            InternalEditorUtility.RepaintAllViews(); // one final repaint erases the last visible tint
        }

        // Scrolls the clicked window's ScrollView so the destination rect (screen space) sits a quarter down the
        // viewport. The inspector hosts IMGUI editors inside a UIToolkit ScrollView, so GUI.ScrollTo cannot reach
        // it — the scroll goes through the window's element tree instead. Screen-space rects and the root's
        // worldBound share the window's origin, so the conversion is a plain offset.
        private static void ScrollTo(Rect screenRect)
        {
            var window = EditorWindow.mouseOverWindow != null ? EditorWindow.mouseOverWindow : EditorWindow.focusedWindow;
            if (window == null) return;

            var scrollView = window.rootVisualElement?.Q<ScrollView>();
            if (scrollView == null) return;

            var viewport = scrollView.contentViewport.worldBound;
            var targetY = screenRect.y - window.position.y;

            // Already comfortably inside the viewport → the pulse alone is enough.
            if (targetY >= viewport.yMin + 4f && targetY + screenRect.height <= viewport.yMax - 4f) return;

            var offset = scrollView.scrollOffset;
            offset.y += targetY - (viewport.yMin + viewport.height * RevealViewportFraction);
            scrollView.scrollOffset = offset; // the ScrollView clamps to its content bounds
        }
    }
}
