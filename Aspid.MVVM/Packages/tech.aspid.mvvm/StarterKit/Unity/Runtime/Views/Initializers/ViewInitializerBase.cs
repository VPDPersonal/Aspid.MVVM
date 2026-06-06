using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract class ViewInitializerBase : MonoBehaviour
    { 
        [SerializeField] private bool _isDisposeViewOnDestroy = true;
        [SerializeField] private ViewInitializeComponent[] _viewComponents;
        
        private IView[] _views;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private Zenject.DiContainer _zenjectContainer;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [VContainer.Inject] 
        private VContainer.IObjectResolver _vcontainerContainer; 
#endif
        
        public IView[] Views
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    return GetViews();
                }
#endif

                return _views ??= GetViews();

                IView[] GetViews()
                {
                    var views = new IView[_viewComponents.Length];
                    
                    for (var i = 0; i < views.Length; i++)
                    {
                        var view = GetFromInitializeComponent(_viewComponents[i]);
                
                        if (view is IComponentInitializable viewInitializable)
                            viewInitializable.Initialize();
                    
                        views[i] = view;
                    }

                    return views;
                }
            }
        }
        
        public abstract IViewModel ViewModel { get; }
        
        public bool IsInitialized { get; protected set; }

        protected bool IsDisposeViewOnDestroy => _isDisposeViewOnDestroy;

        protected virtual void OnValidate()
        {
            if (_viewComponents is not null)
            {
                foreach (var viewComponent in _viewComponents)
                    viewComponent?.Validate();
            }
        }

        protected T GetFromInitializeComponent<T>(InitializeComponent<T> initializeComponent)
            where T : class
        {
#if ASPID_MVVM_ZENJECT_INTEGRATION
            initializeComponent.ZenjectContainer = _zenjectContainer;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
            initializeComponent.VContainerContainer = _vcontainerContainer;
#endif

            return initializeComponent.GetComponent();
        }
    }
}