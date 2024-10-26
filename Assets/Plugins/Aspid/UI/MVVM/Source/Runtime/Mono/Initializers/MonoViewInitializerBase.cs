using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.MVVM.Mono.Initializers
{
    public abstract class MonoViewInitializerBase : MonoBehaviour
    {
        [SerializeField] private bool _isDisposeOnDestroy;
        
        protected abstract IView View { get; }
        
        protected abstract IViewModel ViewModel { get; }

        protected void Initialize() => View.Initialize(ViewModel);

        protected virtual void OnDestroy()
        {
            var viewModel = View.DeinitializeView();
            if (_isDisposeOnDestroy) viewModel?.DisposeViewModel();
        }
    }
}