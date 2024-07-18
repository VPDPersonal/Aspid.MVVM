using System;
using UnityEngine;
using UltimateUI.MVVM.Views;
using UnityEngine.Serialization;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.Initializers;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.ViewBinders
{
    public sealed class SimpleViewInitializer : MonoViewInitializerBase
    {
        [Header("View")]
        [SerializeField] private Resolve _viewResolve;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [FormerlySerializedAs("_monoView")]
        [TriInspector.ShowIf(nameof(_viewResolve), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolve), Resolve.Mono)]
#endif
        [SerializeField] private MonoView _monoViewValidate;
        
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
        
        [FormerlySerializedAs("_bindOrder")]
        [Header("Bind Order")]
        [SerializeField] private InitializeOrder _initializeOrder;

        protected override IView View => _viewResolve switch 
        { 
            Resolve.Mono => _monoViewValidate,
            Resolve.References => _referenceView,
            _ => throw new ArgumentOutOfRangeException()
        };

        protected override IViewModel ViewModel => _viewModelResolve switch
        {
            Resolve.Mono => _monoViewModel,
            Resolve.References => _referencesViewModel,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private void Awake() => Initialize(InitializeOrder.Awake);

        private void OnEnable() => Initialize(InitializeOrder.OnEnable);
        
        private void Start() => Initialize(InitializeOrder.Start);
        
        private void Initialize(InitializeOrder initializeOrder)
        {
            if (initializeOrder == _initializeOrder)
                Initialize();
        }
        
        private enum Resolve
        {
            Mono,
            References
        }
        
        private enum InitializeOrder
        {
            Awake,
            Start,
            OnEnable,
        }
    }
}