#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableViewModel), editorForChildClasses: true)]
    public class ScriptableViewModelEditor : ViewModelEditor<ScriptableViewModel, ScriptableViewModelEditor>
    {
        protected override ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor> BuildVisualElement() => 
            new ScriptableViewModelVisualElement(this);
    }
}
#endif