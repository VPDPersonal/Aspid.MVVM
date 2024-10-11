using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Initializers;

namespace Aspid.UI.MVVM.StarterKit.Views.Initializers
{
    [AddComponentMenu("UI/View Initializers/Simple View Initializer")]
    public sealed class SimpleViewInitializer : MonoViewInitializerBase
    {
        [Header("View")]
        [SerializeField] private Component<IView> _view;
        
        [Header("View Model")]
        [SerializeField] private Component<IViewModel> _viewModel;
        
        protected override IView View => _view.Resolve switch 
        { 
            Resolve.Mono => (_view.Mono as IView)!,
            Resolve.References => _view.References,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override IViewModel ViewModel => _viewModel.Resolve switch
        {
            Resolve.Mono => (_viewModel.Mono as IViewModel)!,
            Resolve.References => _viewModel.References,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private void OnValidate()
        {
            switch (_view?.Resolve)
            {
                case Resolve.Mono: _view.References = null; break;
                case Resolve.References: _view.Mono = null; break;
            }
            
            switch (_viewModel?.Resolve)
            {
                case Resolve.Mono: _viewModel.References = null; break;
                case Resolve.References: _viewModel.Mono = null; break;
            }
        }

        private void Awake() => Initialize();
        
        private enum Resolve
        {
            Mono,
            References
        }
        
        [Serializable]
        private sealed class Component<TInterface> 
            where TInterface : class
        {
            public Resolve Resolve;
            public Component Mono;
            
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
            [SerializeReferenceDropdown]
#endif
            [SerializeReference] public TInterface References;
        }
    }
}