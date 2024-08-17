using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms.Mono
{
    public abstract class TransformMonoBinderBase : MonoBinder
    {
        private bool _isCached;
        private Transform _transform;

        protected Transform CachedTransform
        {
            get
            {
                if (_isCached) return _transform;

                _transform = transform;
                _isCached = true;

                return _transform;
            }
        }
    }
}