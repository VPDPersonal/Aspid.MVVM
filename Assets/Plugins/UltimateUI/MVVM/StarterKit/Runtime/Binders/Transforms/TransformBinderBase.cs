using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public abstract class TransformBinderBase : Binder
    {
        protected readonly Transform Transform;

        protected TransformBinderBase(Transform transform)
        {
            Transform = transform;
        }
    }
}