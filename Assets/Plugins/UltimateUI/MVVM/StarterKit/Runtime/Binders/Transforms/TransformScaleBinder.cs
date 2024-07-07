using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public partial class TransformScaleBinder : TransformBinderBase, IVectorBinder
    {
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedTransform.localScale = value;
    }

}