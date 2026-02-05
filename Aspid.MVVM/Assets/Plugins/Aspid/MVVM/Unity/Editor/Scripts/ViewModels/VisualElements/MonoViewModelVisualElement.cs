#if !ASPID_MVVM_EDITOR_DISABLED
using Aspid.UnityFastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class MonoViewModelVisualElement : MonoViewModelVisualElement<MonoViewModel, MonoViewModelEditor>
    {
        public MonoViewModelVisualElement(MonoViewModelEditor editor) 
            : base(editor) { }
    }

    public abstract class MonoViewModelVisualElement<TViewModel, TEditor> : ViewModelVisualElement<TViewModel, TEditor>
        where TViewModel : MonoViewModel
        where TEditor : MonoViewModelEditor<TViewModel, TEditor>
    {
        public MonoViewModelVisualElement(TEditor editor) 
            : base(editor) { }

        protected override string GetScriptName() =>
            Editor.TargetAsViewModel.GetScriptNameWithIndex();
    }
}
#endif