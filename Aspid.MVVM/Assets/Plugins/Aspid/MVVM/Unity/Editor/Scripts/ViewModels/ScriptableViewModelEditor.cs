#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableViewModel), editorForChildClasses: true)]
    public sealed class ScriptableViewModelEditor : ScriptableViewModelEditor<ScriptableViewModel, ScriptableViewModelEditor>
    {
        protected override ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor> BuildVisualElement() => 
            new ScriptableViewModelVisualElement(this);
    }
    
    // TODO Aspid.MVVM Unity – Write summary
    public abstract class ScriptableViewModelEditor<TScriptableViewModel, TEditor> : ViewModelEditor<TScriptableViewModel, TEditor>
        where TScriptableViewModel : ScriptableViewModel
        where TEditor : ViewModelEditor<TScriptableViewModel, TEditor> { }
}
#endif