using System;
using UnityEngine;

namespace Aspid.MVVM.Unity
{
    [View]
    public abstract partial class MonoView : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// Destroys the GameObject of the View.
        /// May be overridden by a derived class.
        /// </summary>
        public virtual void Dispose()
        {
            Deinitialize();
            Destroy(gameObject);
        }
    }
}