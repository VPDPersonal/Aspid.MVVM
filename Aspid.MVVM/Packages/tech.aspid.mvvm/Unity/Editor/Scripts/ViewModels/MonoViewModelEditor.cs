#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="MonoViewModelEditor{TMonoViewModel, TEditor}"/> for <see cref="MonoViewModel"/>.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public sealed class MonoViewModelEditor : MonoViewModelEditor<MonoViewModel, MonoViewModelEditor>
    {
        /// <summary>
        /// Creates the default ViewModel Inspector element.
        /// </summary>
        /// <returns>The Inspector element.</returns>
        protected override ViewModelVisualElement<MonoViewModel, MonoViewModelEditor> BuildVisualElement() =>
            new MonoViewModelVisualElement(this);
    }

    /// <summary>
    /// <see cref="ViewModelEditor{T, TEditor}"/> for <see cref="MonoViewModel"/> Inspectors.
    /// </summary>
    /// <typeparam name="TMonoViewModel">The inspected ViewModel type.</typeparam>
    /// <typeparam name="TEditor">The derived editor type.</typeparam>
    public abstract class MonoViewModelEditor<TMonoViewModel, TEditor> : ViewModelEditor<TMonoViewModel, TEditor>
        where TMonoViewModel : MonoViewModel
        where TEditor : MonoViewModelEditor<TMonoViewModel, TEditor> { }
}
#endif