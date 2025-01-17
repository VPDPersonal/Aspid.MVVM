using UnityEngine;

namespace Aspid.MVVM.Mono
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
        protected abstract IView[] Views { get; }
        
        /// <summary>
        /// Abstract property that should return the ViewModel.
        /// </summary>
        protected abstract IViewModel ViewModel { get; }

        /// <summary>
        /// Initializes the View by binding it to the ViewModel.
        /// </summary>
        protected void Initialize()
        {
            foreach (var view in Views)
                view.Initialize(ViewModel);
        }

        protected virtual void OnDestroy()
        {
            if (_isDisposeViewOnDestroy)
            {
                foreach (var view in Views)
                    view.DisposeView();
            }
            
            if (_isDisposeViewModelOnDestroy) 
                ViewModel.DisposeViewModel();
        }
    }
}