using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale")]
    public partial class TransformScaleBinder : TransformBinderBase, IVectorBinder, IBinderNumber
    {
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);
        
        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedTransform.localScale = value;

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(Vector3.one * value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue(Vector3.one * value);

        [BinderLog]
        public void SetValue(float value) =>
            SetValue(Vector3.one * value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue(Vector3.one * (float)value);
    }
}