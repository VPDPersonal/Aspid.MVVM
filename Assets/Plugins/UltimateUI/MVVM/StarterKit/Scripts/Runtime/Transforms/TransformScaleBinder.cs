using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Transforms
{
    public class TransformScaleBinder : TransformBinderBase, IBinder<Vector3>
    {
        public void SetValue(Vector3 value) =>
            CachedTransform.localScale = value;
    }
}