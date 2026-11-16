#if !ASPID_MVVM_EDITOR_DISABLED
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ViewModelVisualElement{TViewModel, TEditor}"/> with an asset name and type header.
    /// </summary>
    public class ScriptableViewModelVisualElement : ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor>
    {
        /// <inheritdoc/>
        public ScriptableViewModelVisualElement(ScriptableViewModelEditor editor) 
            : base(editor) { }

        /// <summary>
        /// Returns the asset name for the header.
        /// </summary>
        /// <returns>The asset name.</returns>
        protected override string GetScriptName() =>
            Editor.TargetAsViewModel.name;

        /// <summary>
        /// Returns the ViewModel type name for the header subtext.
        /// </summary>
        /// <returns>The ViewModel display name.</returns>
        protected override string GetScriptSubtext() =>
            base.GetScriptName();
    }
}
#endif