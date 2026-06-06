#if !ASPID_MVVM_EDITOR_DISABLED
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Default Unity Editor for <see cref="MonoViewModel"/> components.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoViewModel), editorForChildClasses: true)]
    public sealed class MonoViewModelEditor : MonoViewModelEditor<MonoViewModel, MonoViewModelEditor>
    {
        protected override ViewModelVisualElement<MonoViewModel, MonoViewModelEditor> BuildVisualElement() =>
            new MonoViewModelVisualElement(this);
    }

    /// <summary>
    /// Abstract base editor for <see cref="MonoViewModel"/> and its derived types.
    /// </summary>
    /// <typeparam name="TMonoViewModel">The concrete <see cref="MonoViewModel"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The derived editor type (self-referencing).</typeparam>
    public abstract class MonoViewModelEditor<TMonoViewModel, TEditor> : ViewModelEditor<TMonoViewModel, TEditor>
        where TMonoViewModel : MonoViewModel
        where TEditor : MonoViewModelEditor<TMonoViewModel, TEditor> { }
}
#endif