using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.ViewModels
{
    /// <summary>
    /// Abstract base class for implementing the ViewModel, which provides methods for managing <see cref="IBinder"/>.
    /// Inherits from <see cref="MonoBehaviour"/> and implements the <see cref="IViewModel"/> interface.
    /// This class does not contain its own implementation and serves to unify ViewModels as <see cref="MonoBehaviour"/> objects.
    /// </summary>
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        /// <summary>
        /// Adds a Binder for the specified ViewModel property.
        /// The derived class must implement this method.
        /// </summary>
        /// <param name="binder">The Binder to be added.</param>
        /// <param name="propertyName">The name of the property to which the Binder will be bound.</param>
        public abstract void AddBinder(IBinder binder, string propertyName);

        /// <summary>
        /// Removes a Binder for the specified ViewModel property.
        /// The derived class must implement this method.
        /// </summary>
        /// <param name="binder">The Binder to be removed.</param>
        /// <param name="propertyName">The name of the property from which the Binder will be unbound.</param>
        public abstract void RemoveBinder(IBinder binder, string propertyName);
    }
}