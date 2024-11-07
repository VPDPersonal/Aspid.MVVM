namespace Aspid.UI.MVVM.ViewModels
{
    /// <summary>
    /// Abstract class for a ViewModel implementing the <see cref="IViewModel"/> interface.
    /// Provides methods for adding and removing binders for binding with properties.
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addBinderMarker = new("MonoViewModel.AddBinder"); 
        private static readonly Unity.Profiling.ProfilerMarker _removeBinderMarker = new("MonoViewModel.RemoveBinder");
#endif
        
        /// <summary>
        /// Adds a Binder for the specified ViewModel property.
        /// </summary>
        /// <param name="binder">The Binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the Binder will be bound.</param>
        public void AddBinder(IBinder binder, string propertyName)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                AddBinderInternal(binder, propertyName);
            }
        }

        /// <summary>
        /// Abstract method for internal adding binder to ViewModel. 
        /// </summary>
        /// <param name="binder">The Binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the Binder will be bound.</param>
        protected abstract void AddBinderInternal(IBinder binder, string propertyName);

        /// <summary>
        /// Removes a Binder for the specified ViewModel property.
        /// </summary>
        /// <param name="binder">The Binder to be removed.</param>
        /// <param name="propertyName">The name of the property from which the Binder will be unbound.</param>
        public void RemoveBinder(IBinder binder, string propertyName)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_removeBinderMarker.Auto())
#endif
            {
                RemoveBinderInternal(binder, propertyName);
            }
        }

        /// <summary>
        /// Abstract method for internal removing binder from ViewModel. 
        /// </summary>
        /// <param name="binder">The Binder to be removed.</param>
        /// <param name="propertyName">The name of the property from which the Binder will be unbound.</param>
        protected abstract void RemoveBinderInternal(IBinder binder, string propertyName);
    }
}