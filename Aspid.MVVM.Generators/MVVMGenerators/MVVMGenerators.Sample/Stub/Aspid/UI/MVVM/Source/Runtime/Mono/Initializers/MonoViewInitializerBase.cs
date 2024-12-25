using UnityEngine;
using Aspid.MVVM.Views;
using Aspid.MVVM.ViewModels;
using Aspid.MVVM.Views.Extensions;
using Aspid.MVVM.ViewModels.Extensions;

namespace Aspid.MVVM.Mono.Initializers
{
    /// <summary>
    /// Abstract base class for initializing the View using the ViewModel.
    /// Inherits from <see cref="MonoBehaviour"/> and provides a mechanism for automatic initialization and resource cleanup.
    /// </summary>
    public abstract class MonoViewInitializerBase : MonoBehaviour
    {
        [SerializeField] private bool _isDisposeViewOnDestroy;
        [SerializeField] private bool _isDisposeViewModelOnDestroy;
        
        /// <summary>
        /// Abstract property that should return the View.
        /// </summary>
        protected abstract IView View { get; }
        
        /// <summary>
        /// Abstract property that should return the ViewModel.
        /// </summary>
        protected abstract IViewModel ViewModel { get; }

        /// <summary>
        /// Initializes the View by binding it to the ViewModel.
        /// </summary>
        protected void Initialize() => View.Initialize(ViewModel);

        protected virtual void OnDestroy()
        {
            if (_isDisposeViewOnDestroy) 
                View.DisposeView();
            
            if (_isDisposeViewModelOnDestroy) 
                ViewModel.DisposeViewModel();
        }
    }
}