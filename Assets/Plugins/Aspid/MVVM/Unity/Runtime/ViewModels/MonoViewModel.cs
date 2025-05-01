using System;
using UnityEngine;

namespace Aspid.MVVM.Unity
{
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