using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    public partial class TransformScaleBinder : TransformBinderBase, IVectorBinder, INumberBinder
    {
        public TransformScaleBinder(Transform transform) : base(transform) { }

        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        public void SetValue(Vector3 value) =>
            Transform.localScale = value;

        public void SetValue(int value) =>
            SetValue(Vector3.one * value);

        public void SetValue(long value) =>
            SetValue(Vector3.one * value);
        
        public void SetValue(float value) =>
            SetValue(Vector3.one * value);
        
        public void SetValue(double value) =>
            SetValue(Vector3.one * (float)value);
    }
}