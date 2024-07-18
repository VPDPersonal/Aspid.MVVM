using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public abstract class TransformBinderBase : MonoBinder
    {
        private Transform _transform;
        
        protected Transform CachedTransform => _transform ? _transform : _transform = transform;
    }
}