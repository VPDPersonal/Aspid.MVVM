#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public sealed class MonoViewModelEditor : MonoViewModelEditor<MonoViewModel, MonoViewModelEditor>
    {
        protected override ViewModelVisualElement<MonoViewModel, MonoViewModelEditor> BuildVisualElement() => 
            new MonoViewModelVisualElement(this);
    }
    
    // TODO Aspid.MVVM Unity – Write summary
    public abstract class MonoViewModelEditor<TMonoViewModel, TEditor> : ViewModelEditor<TMonoViewModel, TEditor>
        where TMonoViewModel : MonoViewModel
        where TEditor : MonoViewModelEditor<TMonoViewModel, TEditor> { }
}
#endif