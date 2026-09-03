// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How a command binder reflects the command's <c>CanExecute</c> state on its target.
    /// </summary>
    public enum InteractableMode
    {
        /// <summary>
        /// The state is ignored.
        /// </summary>
        None,

        /// <summary>
        /// The target GameObject is shown or hidden.
        /// </summary>
        Visible,

        /// <summary>
        /// The target's <c>interactable</c> flag follows the state.
        /// </summary>
        Interactable,

        /// <summary>
        /// The state is handed to an assigned <see cref="ICanExecuteHandler"/>.
        /// </summary>
        Custom
    }
}
