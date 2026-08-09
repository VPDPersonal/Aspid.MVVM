using System;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The Settings tab (the rightmost square tab): the package's settings surfaced inside the window so the toolset is
    /// self-contained. The one surface showing BOTH storage scopes — the Unity-native pages each render only their
    /// own scope (Preferences the per-user controls, Project Settings the shared ones) from the same definitions.
    /// A thin host over the shared settings surface — <see cref="AspidSettingsUI.BuildSurfaceContent"/> composes the
    /// scope legend and every package area's section from one definition per area, and
    /// <see cref="AspidSettingsUI.BuildResetFooter"/> pins the per-scope reset under the scroll. The window's dotted
    /// canvas already sits behind this tab, so unlike the Unity-native pages it brings no canvas of its own.
    /// On top of the surface the tab runs the window's keyboard ring (the Project/Asset References idiom): ↑/↓ walk
    /// one flat focus ring over every actionable element, Enter activates the highlighted one (toggles a switch,
    /// cycles the gate, opens the add-folder picker, presses a button), ←/→ nudge the highlighted slider, Escape
    /// drops the highlight.
    /// </summary>
    internal sealed class SettingsView : VisualElement
    {
        private readonly ScrollView _scroll;

        // Keyboard navigation: one flat focus ring in visual order, shared with the other window tabs. Activate fires
        // on Enter; Adjust (sliders only) on ←/→ with the step sign; Remove (excluded-folder rows only) on Delete/Backspace.
        private readonly NavRing _ring;

        public SettingsView()
        {
            // Repaint the plain Unity fields into the window's dark palette so they don't float bright over the canvas.
            this.AsSurface();

            _scroll = new ScrollView(ScrollViewMode.Vertical) { style = { flexGrow = 1 } };
            AspidSettingsUI.BuildSurfaceContent(_scroll.contentContainer);
            Add(_scroll);

            // Pinned under the scroll, so the reset affordance stays reachable however long the tab grows.
            Add(AspidSettingsUI.BuildResetFooter());

            // The shared keyboard ring: the root holds focus (grabbed on attach) so keys reach it before anything is
            // highlighted. A focused row takes the focused class; the ring also covers the reset footer pinned OUTSIDE
            // the scroll, where ScrollTo throws on non-descendants — so the scroll is gated on containment.
            _ring = new NavRing(
                host: this,
                navTargetClass: AspidSettingsUI.NavTargetClass,
                focusedClass: AspidSettingsUI.NavTargetFocusedClass,
                scrollTo: element => { if (IsInScrollContent(element)) _scroll.ScrollTo(element); });

            // The surface's controls are stable for the tab's lifetime (the excluded-folders panel rebuilds only its
            // internal rows), so the ring is collected once, in tree (= visual) order.
            CollectNavTargets(this);
        }

        // ---------------------------------------------------------------------------------------------------------
        // Keyboard navigation
        // ---------------------------------------------------------------------------------------------------------

        private bool IsInScrollContent(VisualElement element)
        {
            for (var parent = element.parent; parent != null; parent = parent.parent)
            {
                if (parent == _scroll.contentContainer)
                    return true;
            }

            return false;
        }

        // Walks the tree in order and registers every actionable control by type, stopping the descent at each one so
        // its internals (a switch's thumb, a button's label) never become ring members of their own. Enter mirrors
        // what a click on the element does; the slider instead takes ←/→ (its natural axis). The excluded-folders
        // control contributes several members (its add header and each folder row) and replaces its row elements on
        // every list change, so the whole ring re-collects on its RowsRebuilt.
        private void CollectNavTargets(VisualElement element)
        {
            switch (element)
            {
                case AspidSwitch toggle:
                    _ring.Register(toggle, () => toggle.value = !toggle.value);
                    return;

                case EnumField dropdown:
                    _ring.Register(dropdown, () => CycleEnum(dropdown));
                    return;

                case SliderInt slider:
                    _ring.Register(
                        slider,
                        activate: null,
                        adjust: delta => slider.value = Mathf.Clamp(slider.value + delta, slider.lowValue, slider.highValue));
                    return;

                case SerializeReferenceExcludedFoldersField folders:
                    foreach (var (target, activate, remove) in folders.GetNavTargets())
                        _ring.Register(target, activate, remove: remove);

                    // -= then += keeps the subscription single across re-collections (this case re-runs on every
                    // rebuild of the ring).
                    folders.RowsRebuilt -= RebuildNavTargets;
                    folders.RowsRebuilt += RebuildNavTargets;
                    return;

                case Button button when button.ClassListContains(AspidSettingsUI.ActionClass):
                    _ring.Register(button, () => Submit(button));
                    return;
            }

            foreach (var child in element.Children())
                CollectNavTargets(child);
        }

        // Re-collects the whole ring after the excluded-folders rows are replaced. Rebuild restores the highlight to
        // the same position, clamped — so deleting a folder row with the keyboard lands the highlight on the next row
        // instead of dropping it.
        private void RebuildNavTargets() =>
            _ring.Rebuild(() => CollectNavTargets(this));

        // Enter on the gate dropdown steps to the next value (wrapping) instead of opening the popup — the popup is
        // a native menu the ring can't reach into, while cycling keeps the whole interaction on the keyboard.
        private static void CycleEnum(EnumField dropdown)
        {
            var values = Enum.GetValues(dropdown.value.GetType());
            var index = Array.IndexOf(values, dropdown.value);
            dropdown.value = (Enum)values.GetValue((index + 1) % values.Length);
        }

        // Presses a Button exactly as the keyboard would: Clickable listens for the submit navigation event.
        private static void Submit(Button button)
        {
            using var evt = new NavigationSubmitEvent { target = button };
            button.SendEvent(evt);
        }
    }
}
