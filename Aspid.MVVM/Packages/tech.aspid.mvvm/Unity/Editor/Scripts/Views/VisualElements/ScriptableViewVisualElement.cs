#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal class ScriptableViewVisualElement : ViewVisualElement<ScriptableView, ScriptableViewEditor>
    {
        public ScriptableViewVisualElement(ScriptableViewEditor editor) : base(editor) { }

        protected override string GetScriptName() =>
            Editor.TargetAsView.name;

        protected override string GetScriptSubtext() =>
            base.GetScriptName();
    }
}
#endif