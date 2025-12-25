#if !ASPID_MVVM_EDITOR_DISABLED
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class ScriptableViewModelVisualElement : ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor>
    {
        public ScriptableViewModelVisualElement(ScriptableViewModelEditor editor) 
            : base(editor) { }

        protected override string GetScriptName() =>
            $"{Editor.TargetAsViewModel.name} ({base.GetScriptName()})";
    }
}
#endif