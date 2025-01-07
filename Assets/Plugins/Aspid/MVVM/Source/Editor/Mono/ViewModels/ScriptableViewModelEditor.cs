using UnityEditor;

namespace Aspid.MVVM.Mono
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableViewModel), editorForChildClasses: true)]
    public class ScriptableViewModelEditor : ViewModelEditor<ScriptableViewModel> { }
}