namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract class for a ViewModel implementing the <see cref="IViewModel"/> interface.
    /// Provides methods for adding binders for binding with properties.
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addBinderMarker = new("MonoViewModel.AddBinder"); 
#endif
        /// <summary>
        /// Adds a binder to the specified ViewModel property.
        /// </summary>
        /// <param name="binder">The binder that will be associated with the ViewModel property.</param>
        /// <param name="propertyName">The name of the property to which the binder will be bound.</param>
        /// <returns>
        /// An interface for removing the binder from the ViewModel, or null if the binder could not be added
        /// or if the property is read-only.
        /// </returns>
        public IRemoveBinderFromViewModel? AddBinder(IBinder binder, string propertyName)
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                return AddBinderInternal(binder, propertyName);
            }
        }

        /// <summary>
        /// Abstract method for adding a binder to a ViewModel property internally.
        /// This method must be implemented in derived classes to provide specific behavior.
        /// </summary>
        /// <param name="binder">The binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the binder will be bound.</param>
        /// <returns>
        /// An interface for removing the binder from the ViewModel.
        /// Implementations can return null if binding is not possible or if the property is read-only.
        /// </returns>
        protected abstract IRemoveBinderFromViewModel? AddBinderInternal(IBinder binder, string propertyName);
    }
}