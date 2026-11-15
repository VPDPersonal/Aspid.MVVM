using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ScriptableObject"/> for ViewModels that refresh bindings after Inspector changes.
    /// </summary>
    [ViewModel]
    public abstract partial class ScriptableViewModel : ScriptableObject, IDisposable
    {
        [NonSerialized] private bool _isDisposed;

        /// <summary>
        /// Called when Unity validates serialized data. Override to validate additional fields.
        /// </summary>
        /// <remarks>
        /// Call the base implementation to queue binding notifications for the next Editor update on the main thread.
        /// </remarks>
        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            ViewModelValidationDispatcher.Enqueue(
                target: this,
                notify: static target => ((ScriptableViewModel)target).NotifyAll());
#endif
        }
        /// <summary>
        /// Called when Unity destroys the object to release its resources.
        /// </summary>
        /// <remarks>Overrides must call the base implementation.</remarks>
        protected virtual void OnDestroy() =>
            Dispose();

        /// <summary>
        /// Releases owned resources once without destroying the Unity object.
        /// </summary>
        /// <remarks>
        /// Repeated and reentrant calls do nothing, including after a cleanup failure.
        /// Override <see cref="OnDispose"/> to release resources.
        /// </remarks>
        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            OnDispose();
        }

        /// <summary>
        /// Called on the first disposal attempt. Override to release owned resources and subscriptions.
        /// </summary>
        /// <remarks>Cleanup exceptions propagate to the caller; subsequent disposal attempts do not retry cleanup.</remarks>
        protected virtual void OnDispose() { }
    }
}
