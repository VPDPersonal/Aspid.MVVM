namespace Aspid.UI.MVVM.ViewModels
{
    /// <summary>
    /// Interface for a ViewModel that supports data binding.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Adds a binder for the specified ViewModel property.
        /// </summary>
        /// <param name="binder">The binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the binder will be bound.</param>
        public IRemoveBinderFromViewModel? AddBinder(IBinder binder, string propertyName);
    }
}