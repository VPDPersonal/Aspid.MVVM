#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="ScriptableViewModelEditor{TScriptableViewModel, TEditor}"/> for <see cref="ScriptableViewModel"/>.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScriptableViewModel), editorForChildClasses: true)]
    public sealed class ScriptableViewModelEditor : ScriptableViewModelEditor<ScriptableViewModel, ScriptableViewModelEditor>
    {
        /// <summary>
        /// Creates the default ViewModel Inspector element.
        /// </summary>
        /// <returns>The Inspector element.</returns>
        protected override ViewModelVisualElement<ScriptableViewModel, ScriptableViewModelEditor> BuildVisualElement() =>
            new ScriptableViewModelVisualElement(this);
    }

    /// <summary>
    /// <see cref="ViewModelEditor{T, TEditor}"/> for <see cref="ScriptableViewModel"/> Inspectors.
    /// </summary>
    /// <typeparam name="TScriptableViewModel">The inspected ViewModel type.</typeparam>
    /// <typeparam name="TEditor">The derived editor type.</typeparam>
    public abstract class ScriptableViewModelEditor<TScriptableViewModel, TEditor> : ViewModelEditor<TScriptableViewModel, TEditor>
        where TScriptableViewModel : ScriptableViewModel
        where TEditor : ViewModelEditor<TScriptableViewModel, TEditor> { }
}
#endif