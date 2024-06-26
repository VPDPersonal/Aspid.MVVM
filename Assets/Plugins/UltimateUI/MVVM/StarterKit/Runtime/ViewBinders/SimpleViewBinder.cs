using System;
using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.ViewBinders;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.ViewBinders
{
    public sealed class SimpleViewBinder : MonoViewBinderBase
    {
        [Header("View")]
        [SerializeField] private Resolve _viewResolve;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolve), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolve), Resolve.Mono)]
#endif
        [SerializeField] private MonoView _monoView;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolve), Resolve.References)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolve), Resolve.References)]
#endif
        [SerializeReference] private IView _referenceView;
        
        [Header("View Model")]
        [SerializeField] private Resolve _viewModelResolve;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolve), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolve), Resolve.Mono)]
#endif
        [SerializeField] private MonoViewModel _monoViewModel;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolve), Resolve.References)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolve), Resolve.References)]
#endif
        [SerializeReference] private IViewModel _referencesViewModel;
        
        [Header("Bind Order")]
        [SerializeField] private BindOrder _bindOrder;

        protected override IView View => _viewResolve switch 
        { 
            Resolve.Mono => _monoView,
            Resolve.References => _referenceView,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override IViewModel ViewModel => _viewModelResolve switch
        {
            Resolve.Mono => _monoViewModel,
            Resolve.References => _referencesViewModel,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private void Awake() => Bind(BindOrder.Awake);

        private void OnEnable() => Bind(BindOrder.OnEnable);

        private void OnDisable()
        {
            if (_bindOrder == BindOrder.OnEnable)
                Unbind();
        }

        private void Start() => Bind(BindOrder.Start);

        private void OnDestroy()
        {
            if (_bindOrder != BindOrder.OnEnable)
                Unbind();
        }

        private void Bind(BindOrder bindOrder)
        {
            if (bindOrder == _bindOrder)
                Bind();
        }
        
        private enum Resolve
        {
            Mono,
            References
        }
        
        private enum BindOrder
        {
            Awake,
            Start,
            OnEnable,
        }
    }
}