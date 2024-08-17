using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public class TransformScaleSwitcherBinder : TransformBinderBase, IBinder<bool>
    {
        protected readonly Vector3 TrueScale;
        protected readonly Vector3 FalseScale;

        public TransformScaleSwitcherBinder(Transform transform, Vector3 trueScale, Vector3 falseScale) 
            : base(transform)
        {
            TrueScale = trueScale;
            FalseScale = falseScale;
        }
        
        public void SetValue(bool value) =>
            Transform.localScale = GetLocalScale(value);

        protected Vector3 GetLocalScale(bool value) =>
            value ? TrueScale : FalseScale;
    }
}