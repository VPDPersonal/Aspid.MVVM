using System;
using UnityEngine;
using AspidUI.MVVM.Views;
using AspidUI.MVVM.ViewModels;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.Unity.ViewModels;
using AspidUI.MVVM.Unity.Initializers;

namespace AspidUI.MVVM.StarterKit.Views.Initializers
{
    [AddComponentMenu("UI/View Initializers/Simple View Initializer")]
    public sealed class SimpleViewInitializer : MonoViewInitializerBase
    {
        [Header("View")]
        [SerializeField] private Component<MonoView, IView> _view;
        
        [Header("View Model")]
        [SerializeField] private Component<MonoViewModel, IViewModel> _viewModel;
        
        protected override IView View => _view.Resolve switch 
        { 
            Resolve.Mono => _view.Mono,
            Resolve.References => _view.References,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override IViewModel ViewModel => _viewModel.Resolve switch
        {
            Resolve.Mono => _viewModel.Mono,
            Resolve.References => _viewModel.References,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private void OnValidate()
        {
            switch (_view.Resolve)
            {
                case Resolve.Mono: _view.References = null; break;
                case Resolve.References: _view.Mono = null; break;
                default: throw new ArgumentOutOfRangeException();
            }
            
            switch (_viewModel.Resolve)
            {
                case Resolve.Mono: _viewModel.References = null; break;
                case Resolve.References: _viewModel.Mono = null; break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Awake() =>  Initialize();
        
        private enum Resolve
        {
            Mono,
            References
        }
        
        [Serializable]
        private sealed class Component<TMono, TInterface>
            where TMono : MonoBehaviour
            where TInterface : class
        {
            [field: SerializeField] 
            public Resolve Resolve { get; set; }
            
#if ASPID_UI_TRI_INSPECTOR_INTEGRATION
            [field: TriInspector.ShowIf(nameof(Resolve), Resolve.Mono)]
#elif ASPID_UI_ODIN_INSPECTOR_INTEGRATION
            [field: Sirenix.OdinInspector.ShowIf(nameof(Resolve), Resolve.Mono)]
#endif
            [field: SerializeField] 
            public TMono Mono { get; set; }
        
#if ASPID_UI_TRI_INSPECTOR_INTEGRATION
            [field: TriInspector.ShowIf(nameof(Resolve), Resolve.References)]
#elif ASPID_UI_ODIN_INSPECTOR_INTEGRATION
            [field: Sirenix.OdinInspector.ShowIf(nameof(Resolve), Resolve.References)]
#endif
            [field: SerializeReference]
            public TInterface References { get; set; }
        }
    }
}