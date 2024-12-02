namespace Aspid.MVVM.ViewModels
{
    /// <summary>
    /// Interface for a ViewModel that supports data binding.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Adds a binder to the specified ViewModel property.
        /// </summary>
        /// <param name="binder">The binder that will be associated with the ViewModel property.</param>
        /// <param name="propertyName">The name of the property to which the binder will be bound.</param>
        /// <returns>
        /// An interface for removing the binder from the ViewModel, or null if the binder could not be added
        /// or if the property is read-only.
        /// </returns>
        public IRemoveBinderFromViewModel? AddBinder(IBinder binder, string propertyName);
    }
}