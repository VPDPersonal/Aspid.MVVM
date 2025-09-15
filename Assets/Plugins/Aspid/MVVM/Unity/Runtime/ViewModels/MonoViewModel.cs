using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a base class for ViewModels in a Unity context that are derived from <see cref="MonoBehaviour"/>.
    /// Implements <see cref="IDisposable"/> to allow cleanup of resources, including the destruction of the component.
    /// </summary>
    [ViewModel]
    public abstract partial class MonoViewModel : MonoBehaviour, IDisposable
    {
        protected virtual void OnValidate() =>
            NotifyAll();

        public void Dispose() =>
            Destroy(this);
    }
}