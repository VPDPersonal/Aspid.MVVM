#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableView), editorForChildClasses: true)]
    public class ScriptableViewEditor : ViewEditor<ScriptableView, ScriptableViewEditor>
    {
        protected override ViewVisualElement<ScriptableView, ScriptableViewEditor> BuildVisualElement() => 
            new ScriptableViewVisualElement(this);
    }
}
#endif