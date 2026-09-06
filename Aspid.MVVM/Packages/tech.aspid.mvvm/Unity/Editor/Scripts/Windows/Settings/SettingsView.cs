using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // The Settings tab, composed from the Aspid.FastTools settings surface so both packages' windows read as one
    // family: a header with the storage-scope legend, one glass card per area, striped rows. The Build card edits the
    // scripting defines behind the profiler, binder log and editor checks; those trigger a recompile, so the switches
    // stage their values and a single Apply commits them. The Welcome card holds the per-user auto-show switch.
    //
    // Keyboard: the window's ring walks the switches and the action buttons, Enter flips or presses the highlighted one.
    internal sealed class SettingsView : VisualElement
    {
        private readonly ScrollView _scroll;
        private readonly NavRing _ring;

        private readonly AspidSwitch _profilerSwitch;
        private readonly AspidSwitch _binderLogSwitch;
        private readonly AspidSwitch _editorChecksSwitch;

        private readonly VisualElement _actions;

        public SettingsView()
        {
            this.AsSurface();

            _scroll = new ScrollView(ScrollViewMode.Vertical) { style = { flexGrow = 1 } };
            var content = _scroll.contentContainer;

            content.Add(BuildHeader());

            _profilerSwitch = CreateDefineSwitch(
                "Profiler",
                "Emit profiler markers for the Unity Profiler.\n" +
                "Shared setting: a scripting define in the committed Player Settings.");

            _binderLogSwitch = CreateDefineSwitch(
                "Binder log",
                "Log binder operations in detail; useful when a binding misbehaves.\n" +
                "Shared setting: a scripting define in the committed Player Settings.");

            _editorChecksSwitch = CreateDefineSwitch(
                "Editor checks",
                "Run additional validation in the Editor to catch binding errors early.\n" +
                "Shared setting: a scripting define in the committed Player Settings.");

            _actions = BuildActions();

            AspidSettingsUI.AddSection(content, "Build", card => card
                .AddChild(_profilerSwitch)
                .AddChild(_binderLogSwitch)
                .AddChild(_editorChecksSwitch)
                .AddChild(AspidSettingsUI.CreateRowNote("Applying rewrites the scripting defines and recompiles the project."))
                .AddChild(_actions));

            AspidSettingsUI.AddSection(content, "Welcome", WelcomeSettingsUI.BuildControls);

            Add(_scroll);
            LoadCurrentValues();

            _ring = new NavRing(
                host: this,
                navTargetClass: AspidSettingsUI.NavTargetClass,
                focusedClass: AspidSettingsUI.NavTargetFocusedClass,
                scrollTo: element => _scroll.ScrollTo(element));

            CollectNavTargets(this);
        }

        private bool HasPendingChanges =>
            _profilerSwitch.value != AspidMvvmSettings.IsEnabledProfiler ||
            _binderLogSwitch.value != AspidMvvmSettings.IsEnabledBinderLog ||
            _editorChecksSwitch.value != AspidMvvmSettings.IsEnabledCheckForEditor;

        private static VisualElement BuildHeader()
        {
            var heading = new AspidLabel("Settings", AspidLabelPreset.Default
                    .SetLabelTheme(ThemeStyle.Type.Lightness)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H4)
                    .SetLineTheme(ThemeStyle.Type.Dark))
                .AddClass(AspidSettingsUI.HeaderTitleClass);

            var description = new Label("Every Aspid.MVVM setting in one place. The stripe on each row shows where the value is stored.")
                .AddClass(AspidSettingsUI.HeaderDescriptionClass);

            return new VisualElement().AddClass(AspidSettingsUI.HeaderClass)
                .AddChild(heading)
                .AddChild(description)
                .AddChild(AspidSettingsUI.BuildScopeLegend());
        }

        private AspidSwitch CreateDefineSwitch(string label, string tooltip)
        {
            var toggle = new AspidSwitch(label) { tooltip = tooltip };
            toggle.WithScopeStripe(AspidSettingsUI.SharedScopeClass);
            toggle.RegisterValueChangedCallback(_ => UpdatePendingState());

            return toggle;
        }

        private VisualElement BuildActions()
        {
            var revert = new Button(Revert) { text = "Revert", tooltip = "Drop the staged changes." }
                .AddClass(AspidSettingsUI.ActionClass)
                .AddClass(AspidSettingsUI.ActionDangerClass);

            var apply = new Button(Apply) { text = "Apply", tooltip = "Write the scripting defines. The project recompiles." }
                .AddClass(AspidSettingsUI.ActionClass);

            return new VisualElement().AddClass(AspidSettingsUI.RowClass)
                .AddChild(new Label("Staged changes").AddClass(AspidSettingsUI.RowCaptionClass))
                .AddChild(revert)
                .AddChild(apply);
        }

        private void LoadCurrentValues()
        {
            _profilerSwitch.SetValueWithoutNotify(AspidMvvmSettings.IsEnabledProfiler);
            _binderLogSwitch.SetValueWithoutNotify(AspidMvvmSettings.IsEnabledBinderLog);
            _editorChecksSwitch.SetValueWithoutNotify(AspidMvvmSettings.IsEnabledCheckForEditor);

            UpdatePendingState();
        }

        // The action row is hidden while there is nothing to apply or revert, so the card stays quiet at rest.
        private void UpdatePendingState() =>
            _actions.style.display = HasPendingChanges ? DisplayStyle.Flex : DisplayStyle.None;

        private void Apply()
        {
            AspidMvvmSettings.IsEnabledProfiler = _profilerSwitch.value;
            AspidMvvmSettings.IsEnabledBinderLog = _binderLogSwitch.value;
            AspidMvvmSettings.IsEnabledCheckForEditor = _editorChecksSwitch.value;

            UpdatePendingState();
            Debug.Log("<color=#0d8c5e>Aspid.MVVM settings applied.</color>");
        }

        private void Revert() =>
            LoadCurrentValues();

        // Walks the tree in order and registers every actionable control by type, stopping the descent at each one so
        // its internals never become ring members of their own. The hidden action row's buttons stay registered: the
        // ring skips members whose ancestors are display:none.
        private void CollectNavTargets(VisualElement element)
        {
            switch (element)
            {
                case AspidSwitch toggle:
                    _ring.Register(toggle, () => toggle.value = !toggle.value);
                    return;

                case Button button when button.ClassListContains(AspidSettingsUI.ActionClass):
                    _ring.Register(button, () => Submit(button));
                    return;
            }

            foreach (var child in element.Children())
                CollectNavTargets(child);
        }

        // Presses a Button exactly as the keyboard would: Clickable listens for the submit navigation event.
        private static void Submit(Button button)
        {
            using var evt = new NavigationSubmitEvent { target = button };
            button.SendEvent(evt);
        }
    }
}
