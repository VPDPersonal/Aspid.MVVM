using System;
using UnityEngine;

namespace Aspid.MVVM.Unity
{
    /// <summary>
    /// Represents a base class for views in a Unity context that are derived from <see cref="MonoBehaviour"/>.
    /// Implements <see cref="IDisposable"/> to allow cleanup of resources, including the destruction of the associated GameObject.
    /// </summary>
    [View]
    public abstract partial class MonoView : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// Destroys the GameObject of the View.
        /// May be overridden by a derived class.
        /// </summary>
        public virtual void Dispose() =>
            Destroy(gameObject);

        protected virtual void OnDestroy() =>
            Deinitialize();
    }
}