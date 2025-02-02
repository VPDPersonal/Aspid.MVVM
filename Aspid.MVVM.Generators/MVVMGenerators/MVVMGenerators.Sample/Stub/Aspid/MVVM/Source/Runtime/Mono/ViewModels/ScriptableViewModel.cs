using UnityEngine;

namespace Aspid.MVVM.Mono
{
    public abstract class ScriptableViewModel : ScriptableObject, IViewModel
    {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addBinderMarker = new("ScriptableViewModel.AddBinder"); 
#endif

        protected virtual void OnValidate() =>
            this.InvokeAllChangedEventsEditor();

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
        public BindResult AddBinder(IBinder binder, string propertyName)
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
        /// A <see cref="BindResult"/> object that contains information about the binding operation.
        /// The <see cref="BindResult.IsBound"/> property indicates whether the binder was successfully bound.
        /// If the binding was successful, the <see cref="BindResult.BinderRemover"/> property provides an interface
        /// for removing the binder from the ViewModel. If the binding failed (e.g., the property is read-only),
        /// <see cref="BindResult.BinderRemover"/> will be null.
        /// </returns>
        protected abstract BindResult AddBinderInternal(IBinder binder, string propertyName);
    }
}