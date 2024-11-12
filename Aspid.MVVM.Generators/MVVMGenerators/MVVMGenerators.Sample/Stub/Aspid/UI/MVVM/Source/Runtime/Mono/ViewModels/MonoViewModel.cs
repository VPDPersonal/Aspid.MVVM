using System;
using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.ViewModels
{
    /// <summary>
    /// Abstract class for a ViewModel, inheriting from <see cref="MonoBehaviour"/>, that implements the <see cref="IViewModel"/> interface.
    /// Provides methods for adding and removing binders for binding with properties.
    /// </summary>
    public abstract class MonoViewModel : MonoBehaviour, IViewModel, IDisposable
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addBinderMarker = new("MonoViewModel.AddBinder"); 
#endif

        /// <summary>
        /// Adds a Binder for the specified ViewModel property.
        /// </summary>
        /// <param name="binder">The Binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the Binder will be bound.</param>
        public IRemoveBinderFromViewModel AddBinder(IBinder binder, string propertyName)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_addBinderMarker.Auto())
#endif
            {
                return AddBinderInternal(binder, propertyName);
            }
        }

        /// <summary>
        /// Abstract method for internal adding binder to ViewModel. 
        /// </summary>
        /// <param name="binder">The Binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the Binder will be bound.</param>
        protected abstract IRemoveBinderFromViewModel AddBinderInternal(IBinder binder, string propertyName);
        
        /// <summary>
        /// Destroys the Component of the ViewModel.
        /// May be overridden by a derived class.
        /// </summary>
        public virtual void Dispose() => Destroy(this);
    }
}