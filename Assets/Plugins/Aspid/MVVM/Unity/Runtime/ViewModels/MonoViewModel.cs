using System;
using UnityEngine;

namespace Aspid.MVVM.Unity
{
    /// <summary>
    /// Represents a base class for ViewModels in a Unity context that are derived from <see cref="MonoBehaviour"/>.
    /// Implements <see cref="IDisposable"/> to allow cleanup of resources, including the destruction of the component.
    /// </summary>
    [ViewModel]
    public abstract partial class MonoViewModel : MonoBehaviour, IDisposable
    {
        protected virtual void OnValidate() =>
            this.InvokeAllChangedEventsDebug();
        
        /// <summary>
        /// Destroys the Component of the ViewModel.
        /// May be overridden by a derived class.
        /// </summary>
        public virtual void Dispose() => Destroy(this);
    }
}