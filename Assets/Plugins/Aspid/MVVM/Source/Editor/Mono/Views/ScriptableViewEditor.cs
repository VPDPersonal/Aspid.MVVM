#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

namespace Aspid.MVVM.Mono
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableView), editorForChildClasses: true)]
    public class ScriptableViewEditor : ViewEditor<ScriptableView> { }
}
#endif