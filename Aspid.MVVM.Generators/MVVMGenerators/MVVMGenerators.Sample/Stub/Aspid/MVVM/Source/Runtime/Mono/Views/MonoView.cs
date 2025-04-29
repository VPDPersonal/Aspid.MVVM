#nullable disable
using System;
using Aspid.MVVM.Generation;
using UnityEngine;

namespace Aspid.MVVM.Mono
{
    /// <summary>
    /// Abstract class for a View, inheriting from <see cref="MonoBehaviour"/>, that implements the <see cref="IView"/> interface.
    /// Provides methods for initializing and deinitializing the View with an <see cref="IViewModel"/> for binding.
    /// </summary>
    [View]
    public abstract partial class MonoView : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// Destroys the GameObject of the View.
        /// May be overridden by a derived class.
        /// </summary>
        public virtual void Dispose() => Destroy(gameObject);
    }
}