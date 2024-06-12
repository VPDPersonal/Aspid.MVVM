using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Transforms
{
    public abstract class TransformBinderBase : MonoBinder
    {
        private Transform _transform;
        
        protected Transform CachedTransform => _transform ? _transform : _transform = transform;
    }
}