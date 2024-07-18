using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Transforms
{
    public abstract class TransformBindingBase : MonoBinding
    {
        private Transform _transform;
        
        protected Transform CachedTransform => _transform ? _transform : _transform = transform;
    }
}