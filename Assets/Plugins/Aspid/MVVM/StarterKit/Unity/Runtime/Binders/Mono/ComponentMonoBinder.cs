using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract class ComponentMonoBinder<TComponent> : MonoBinder
        where TComponent : Component
    {
        [Header("Component")]
        [SerializeField] private TComponent _component;
        
        private bool _isCached;

        protected TComponent CachedComponent
        {
            get
            {
                if (_isCached) return _component;
                
                _isCached = true;
                
                if (_component) return _component;
                return _component = GetComponent<TComponent>();
            }
        }

        protected virtual void OnValidate()
        {
            if (Application.isPlaying) return;
            if (_component) return;
            
            _component = GetComponent<TComponent>();
        }
    }
}