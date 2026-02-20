#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableView), editorForChildClasses: true)]
    public sealed class ScriptableViewEditor : ScriptableViewEditor<ScriptableView, ScriptableViewEditor>
    {
        protected override ViewVisualElement<ScriptableView, ScriptableViewEditor> BuildVisualElement() => 
            new ScriptableViewVisualElement(this);
    }
    
    // TODO Aspid.MVVM Unity – Write summary
    public abstract class ScriptableViewEditor<TScriptableView, TEditor> : ViewEditor<TScriptableView, TEditor>
        where TScriptableView : ScriptableView
        where TEditor : ScriptableViewEditor<TScriptableView, TEditor> { }
}
#endif