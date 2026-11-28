#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for ViewModels that refresh bindings after Inspector changes.
    /// </summary>
    [ViewModel]
    public abstract partial class MonoViewModel : MonoBehaviour, IDisposable
    {
        [NonSerialized] private bool _isDisposed;

        /// <summary>
        /// Called when Unity validates serialized data. Override to validate additional fields.
        /// </summary>
        /// <remarks>Call the base implementation to queue binding notifications on the Editor's main thread.</remarks>
        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            ViewModelValidationDispatcher.Enqueue(
                target: this,
                notify: static target => ((MonoViewModel)target).NotifyAll());
#endif
        }

        /// <summary>
        /// Called when Unity destroys the object to release its resources.
        /// </summary>
        /// <remarks>
        /// <para>Overrides must call the base implementation.</para>
        /// <para>Use <see cref="ViewModelExtensions.DestroyViewModel(IViewModel)"/> for components that never became active.</para>
        /// </remarks>
        protected virtual void OnDestroy() =>
            Dispose();

        /// <summary>
        /// Releases owned resources once without destroying the Unity object.
        /// </summary>
        /// <remarks>Cleanup is attempted once, even if <see cref="OnDispose"/> throws.</remarks>
        public void Dispose()
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker());
#endif
            {
                if (_isDisposed) return;
                _isDisposed = true;
                
                OnDispose();
            }
        }

        /// <summary>
        /// Called on the first disposal attempt. Override to release owned resources.
        /// </summary>
        protected virtual void OnDispose() { }
    }
}
