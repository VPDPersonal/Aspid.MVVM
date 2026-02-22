#if !ASPID_MVVM_EDITOR_DISABLED
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// UIElements visual element for the <see cref="ScriptableViewModel"/> inspector.
    /// </summary>
    public class ScriptableViewModelVisualElement : ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor>
    {
        public ScriptableViewModelVisualElement(ScriptableViewModelEditor editor) 
            : base(editor) { }

        protected override string GetScriptName() =>
            $"{Editor.TargetAsViewModel.name} ({base.GetScriptName()})";
    }
}
#endif