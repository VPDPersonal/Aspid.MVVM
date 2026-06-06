#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Default Unity Editor for <see cref="ScriptableView"/> assets.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableView), editorForChildClasses: true)]
    internal sealed class ScriptableViewEditor : ScriptableViewEditor<ScriptableView, ScriptableViewEditor>
    {
        protected override ViewVisualElement<ScriptableView, ScriptableViewEditor> BuildVisualElement() =>
            new ScriptableViewVisualElement(this);
    }

    /// <summary>
    /// Abstract base editor for <see cref="ScriptableView"/> and its derived types.
    /// </summary>
    /// <typeparam name="TScriptableView">The concrete <see cref="ScriptableView"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The derived editor type (self-referencing).</typeparam>
    internal abstract class ScriptableViewEditor<TScriptableView, TEditor> : ViewEditor<TScriptableView, TEditor>
        where TScriptableView : ScriptableView
        where TEditor : ScriptableViewEditor<TScriptableView, TEditor> { }
}
#endif