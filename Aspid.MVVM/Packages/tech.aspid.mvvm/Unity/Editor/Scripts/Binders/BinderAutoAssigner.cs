#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections;
using Aspid.MVVM.Validation;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Auto-assigns a <see cref="MonoBinder"/> to a parent view's binder slot, but only when the target is
    /// unambiguous and the assignment is non-destructive.
    /// </summary>
    /// <remarks>
    /// Two entry points share one resolution core (<see cref="TryResolveSingleTarget"/>):
    /// <list type="bullet">
    /// <item>
    /// an on-demand command in the binder's component context menu (<c>⋮ ▸ Auto-Assign View And Id</c>),
    /// always available and greyed out when it cannot assign;
    /// </item>
    /// <item>
    /// an opt-in reaction to "component added" (toggle under <c>Tools/Aspid 🐍/Experimental</c>, OFF by default).
    /// </item>
    /// </list>
    /// "Valid" reuses <see cref="BinderEditorUtilities.GetIds{T}"/> — the same matching the inspector dropdowns
    /// use (type + instance compatibility) — plus a non-destructiveness filter: array slots accept an append,
    /// single slots only accept assignment when empty. The binder is assigned if (and only if) exactly one such
    /// slot exists across the whole parent chain.
    /// </remarks>
    [InitializeOnLoad]
    internal static class BinderAutoAssigner
    {
        private const string EnabledKey = "Aspid.MVVM.BinderAutoAssign.Enabled";
        private const string ToggleMenuPath = "Tools/Aspid 🐍/Experimental/Auto-Assign Binder On Add";
        private const string ContextMenuPath = "CONTEXT/MonoBinder/Auto-Assign View And Id";

        /// <summary>
        /// Policy: array slots mean "many binders per member", so appending is non-destructive even when
        /// the array already holds binders. Flip to <c>false</c> to only auto-fill empty arrays.
        /// </summary>
        private const bool AppendToNonEmptyArrays = true;

        private static bool Enabled
        {
            get => EditorPrefs.GetBool(EnabledKey, false);
            set => EditorPrefs.SetBool(EnabledKey, value);
        }

        static BinderAutoAssigner()
        {
            ObjectChangeEvents.changesPublished -= OnChangesPublished;
            ObjectChangeEvents.changesPublished += OnChangesPublished;
        }

        #region On-Add Toggle Menu
        [MenuItem(ToggleMenuPath)]
        private static void ToggleEnabled() => Enabled = !Enabled;

        [MenuItem(ToggleMenuPath, isValidateFunction: true)]
        private static bool ToggleEnabledValidate()
        {
            Menu.SetChecked(ToggleMenuPath, Enabled);
            return true;
        }
        #endregion

        #region Context Menu Command
        // On-demand entry point: right-click a binder component (⋮) → "Auto-Assign View And Id".
        // Always available, independent of the on-add toggle; the validate pass greys it out when
        // there is nothing safe to assign.
        [MenuItem(ContextMenuPath)]
        private static void AutoAssignContext(MenuCommand command)
        {
            if (command.context is MonoBinder binder)
                TryAutoAssign(binder);
        }

        [MenuItem(ContextMenuPath, isValidateFunction: true)]
        private static bool AutoAssignContextValidate(MenuCommand command) =>
            command.context is MonoBinder binder && CanAutoAssign(binder);
        #endregion

        private static void OnChangesPublished(ref ObjectChangeEventStream stream)
        {
            if (!Enabled) return;
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;

            for (var i = 0; i < stream.length; i++)
            {
                // Adding a component to a GameObject is published as a structure change.
                if (stream.GetEventType(i) != ObjectChangeKind.ChangeGameObjectStructure) continue;

                stream.GetChangeGameObjectStructureEvent(i, out var data);
                if (EditorUtility.InstanceIDToObject(data.instanceId) is not GameObject go) continue;

                foreach (var binder in go.GetComponents<MonoBinder>())
                    TryAutoAssign(binder);
            }
        }

        /// <summary>
        /// Whether <paramref name="binder"/> can be auto-assigned right now: it is currently unassigned and
        /// exactly one valid, non-destructive target slot exists across its parent chain.
        /// </summary>
        /// <param name="binder">The binder to test.</param>
        /// <returns><c>true</c> when a subsequent <see cref="TryAutoAssign"/> would assign; otherwise <c>false</c>.</returns>
        public static bool CanAutoAssign(MonoBinder binder) =>
            TryResolveSingleTarget(binder, out _, out _);

        /// <summary>
        /// Tries to bind <paramref name="binder"/> to exactly one parent-view slot.
        /// No-op when the binder is already assigned, or when zero / more than one valid slot exists.
        /// </summary>
        /// <param name="binder">The binder to resolve.</param>
        /// <returns><c>true</c> when an assignment was made; otherwise <c>false</c>.</returns>
        public static bool TryAutoAssign(MonoBinder binder)
        {
            if (!TryResolveSingleTarget(binder, out var view, out var id)) return false;
            return Assign(binder, (IMonoBinderValidable)binder, view, id);
        }

        /// <summary>
        /// Resolves the single valid <c>(view, id)</c> target for <paramref name="binder"/>. Fails (returns
        /// <c>false</c>) when the binder is missing, in play mode, already assigned, or when the number of
        /// valid non-destructive slots across the parent chain is not exactly one.
        /// </summary>
        private static bool TryResolveSingleTarget(MonoBinder binder, out IView view, out string id)
        {
            view = null!;
            id = null!;

            if (!binder) return false;
            if (EditorApplication.isPlayingOrWillChangePlaymode) return false;

            var validable = (IMonoBinderValidable)binder;

            // Non-destructive: never override an existing assignment. Also keeps the call idempotent,
            // so it is safe to fire from several hooks.
            if (validable.View is not null) return false;
            if (!string.IsNullOrWhiteSpace(validable.Id)) return false;

            // Global single-match across the WHOLE parent chain. To prefer "nearest view wins" instead,
            // stop iterating views as soon as one of them yields any candidate.
            var seen = new HashSet<string>();
            IView? targetView = null;
            string? targetId = null;
            var count = 0;

            foreach (var viewData in BinderEditorUtilities.GetViews(binder))
            {
                var candidateView = viewData.View;
                if (candidateView is null) continue;

                // GetIds == the exact set the View/Id dropdowns offer (type + instance match).
                // Reusing it guarantees auto-assign never picks something the manual UI would not.
                foreach (var idData in BinderEditorUtilities.GetIds(binder, candidateView))
                {
                    if (!IsSlotAssignable(candidateView, idData.Id)) continue;

                    var key = (candidateView as Object)?.GetInstanceID() + "|" + idData.Id;
                    if (!seen.Add(key)) continue;

                    if (++count > 1) return false; // ambiguous -> abstain

                    targetView = candidateView;
                    targetId = idData.Id;
                }
            }

            if (count != 1) return false; // zero matches -> abstain

            view = targetView!;
            id = targetId!;
            return true;
        }

        private static bool IsSlotAssignable(IView view, string id)
        {
            var field = view.GetRequireBinderFieldById(id);
            if (field is null) return false;

            if (field.FieldType.IsArray)
            {
                if (AppendToNonEmptyArrays) return true;

                if (field.GetValue(field.FieldContainerObj) is not IEnumerable array) return true;
                return !array.Cast<object>().Any(item => item as Object != null);
            }

            // Single slot: assignable only when empty, so we never clobber an existing binder.
            return field.GetValue(field.FieldContainerObj) as Object == null;
        }

        private static bool Assign(MonoBinder binder, IMonoBinderValidable validable, IView view, string id)
        {
            var viewComponent = view as Component;
            if (viewComponent == null) return false;

            // Separate undo step from "Add Component". Collapsing the two would require coordinating with
            // the add operation and is out of scope here.
            Undo.RecordObjects(new Object[] { binder, viewComponent }, "Auto-Assign Binder");

            // Write through the same path the dropdown uses, so the View's serialized binder list
            // (_bindersList) stays in sync via the validator.
            validable.SetView(view);
            validable.SetId(id);
            ViewAndMonoBinderSyncValidator.SetBinderIfNotExistInView(validable);

            return true;
        }
    }
}
#endif
