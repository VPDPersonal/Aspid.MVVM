#if !ASPID_MVVM_EDITOR_DISABLED
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="MonoViewModelVisualElement{TViewModel, TEditor}"/> for <see cref="MonoViewModel"/>.
    /// </summary>
    public sealed class MonoViewModelVisualElement : MonoViewModelVisualElement<MonoViewModel, MonoViewModelEditor>
    {
        /// <inheritdoc/>
        public MonoViewModelVisualElement(MonoViewModelEditor editor) 
            : base(editor) { }
    }

    /// <summary>
    /// <see cref="ViewModelVisualElement{TViewModel, TEditor}"/> that includes the component index in its header.
    /// </summary>
    /// <typeparam name="TViewModel">The inspected component ViewModel type.</typeparam>
    /// <typeparam name="TEditor">The corresponding component ViewModel editor type.</typeparam>
    public abstract class MonoViewModelVisualElement<TViewModel, TEditor> : ViewModelVisualElement<TViewModel, TEditor>
        where TViewModel : MonoViewModel
        where TEditor : MonoViewModelEditor<TViewModel, TEditor>
    {
        /// <inheritdoc/>
        public MonoViewModelVisualElement(TEditor editor) 
            : base(editor) { }

        /// <summary>
        /// Returns the component display name with its duplicate index.
        /// </summary>
        /// <returns>The indexed display name.</returns>
        protected override string GetScriptName() =>
            Editor.TargetAsViewModel.GetDisplayNameWithIndex();
    }
}
#endif