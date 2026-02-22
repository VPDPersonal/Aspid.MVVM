#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// UIElements visual element for the <see cref="ScriptableView"/> inspector.
    /// </summary>
    public class ScriptableViewVisualElement : ViewVisualElement<ScriptableView, ScriptableViewEditor>
    {
        public ScriptableViewVisualElement(ScriptableViewEditor editor) : base(editor) { }

        protected override string GetScriptName() =>
            $"{Editor.TargetAsView.name} ({base.GetScriptName()})";
    }
}
#endif