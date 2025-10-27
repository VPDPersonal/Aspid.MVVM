// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class ScriptableViewModelVisualElement : ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor>
    {
        public ScriptableViewModelVisualElement(ScriptableViewModelEditor editor) 
            : base(editor) { }

        protected override string GetScriptName() =>
            $"{Editor.TargetAsViewModel.name} ({base.GetScriptName()})";
    }
}