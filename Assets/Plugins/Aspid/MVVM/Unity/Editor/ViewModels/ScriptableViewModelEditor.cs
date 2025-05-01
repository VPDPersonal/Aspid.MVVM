#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

namespace Aspid.MVVM.Unity
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableViewModel), editorForChildClasses: true)]
    public class ScriptableViewModelEditor : ViewModelEditor<ScriptableViewModel> { }
}
#endif