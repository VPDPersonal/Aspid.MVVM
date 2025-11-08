// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class ScriptableViewVisualElement : ViewVisualElement<ScriptableView, ScriptableViewEditor>
    {
        public ScriptableViewVisualElement(ScriptableViewEditor editor) : base(editor) { }

        protected override string GetScriptName() =>
            $"{Editor.TargetAsSpecificView.name} ({base.GetScriptName()})";
    }
}