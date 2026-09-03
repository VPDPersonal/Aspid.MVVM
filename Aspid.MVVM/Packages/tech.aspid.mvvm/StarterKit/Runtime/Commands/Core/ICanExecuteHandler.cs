// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reacts to a command's <c>CanExecute</c> state on behalf of a command binder whose interactable mode is <c>Custom</c>.
    /// </summary>
    public interface ICanExecuteHandler
    {
        /// <summary>
        /// Reflects whether the bound command can currently execute.
        /// </summary>
        /// <param name="canExecute">The command's current <c>CanExecute</c> result.</param>
        public void SetCanExecute(bool canExecute);
    }
}
