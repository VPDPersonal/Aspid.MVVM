#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public class MonoViewModelEditor : ViewModelEditor<MonoViewModel, MonoViewModelEditor>
    {
        protected override ViewModelVisualElement<MonoViewModel, MonoViewModelEditor> BuildVisualElement() => 
            new(this);
    }
}
#endif