namespace Aspid.MVVM
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
        /// A <see cref="BindResult"/> object that contains information about the binding operation.
        /// The <see cref="BindResult.IsBound"/> property indicates whether the binder was successfully bound.
        /// If the binding was successful, the <see cref="BindResult.BinderRemover"/> property provides an interface
        /// for removing the binder from the ViewModel. If the binding failed (e.g., the property is read-only),
        /// <see cref="BindResult.BinderRemover"/> will be null.
        /// </returns>
        public BindResult AddBinder(IBinder binder, string propertyName);
    }
}