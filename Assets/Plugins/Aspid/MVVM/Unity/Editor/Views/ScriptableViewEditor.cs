#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableView), editorForChildClasses: true)]
    public class ScriptableViewEditor : ViewEditor<ScriptableView> { }
}
#endif