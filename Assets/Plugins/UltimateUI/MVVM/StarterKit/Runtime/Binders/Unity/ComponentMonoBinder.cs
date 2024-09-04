using UnityEngine;
using UltimateUI.MVVM.Unity;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity
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

                if (_component && TryGetComponent(out _component))
                {
                    _isCached = true;
                    return _component;
                }

                return _component;
            }
        }
    }
}