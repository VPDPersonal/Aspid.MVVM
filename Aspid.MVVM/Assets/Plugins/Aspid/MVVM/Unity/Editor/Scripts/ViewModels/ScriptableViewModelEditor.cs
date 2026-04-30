#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Default Unity Editor for <see cref="ScriptableViewModel"/> assets.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableViewModel), editorForChildClasses: true)]
    public sealed class ScriptableViewModelEditor : ScriptableViewModelEditor<ScriptableViewModel, ScriptableViewModelEditor>
    {
        protected override ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor> BuildVisualElement() =>
            new ScriptableViewModelVisualElement(this);
    }

    /// <summary>
    /// Abstract base editor for <see cref="ScriptableViewModel"/> and its derived types.
    /// </summary>
    /// <typeparam name="TScriptableViewModel">The concrete <see cref="ScriptableViewModel"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The derived editor type (self-referencing).</typeparam>
    public abstract class ScriptableViewModelEditor<TScriptableViewModel, TEditor> : ViewModelEditor<TScriptableViewModel, TEditor>
        where TScriptableViewModel : ScriptableViewModel
        where TEditor : ViewModelEditor<TScriptableViewModel, TEditor> { }
}
#endif