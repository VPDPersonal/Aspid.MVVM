#nullable enable
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract class TargetVector3Binder<TTarget> : TargetBinder<TTarget, Vector3, Converter>,      
        IVectorBinder,
        INumberBinder
    {
        protected TargetVector3Binder(TTarget target, Converter? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
        
        public void SetValue(Vector2 value) =>
            base.SetValue(value);
        
        public void SetValue(int value) =>
            base.SetValue(new Vector3(value, value, value));
        
        public void SetValue(long value) =>
            base.SetValue(new Vector3(value, value, value));
        
        public void SetValue(float value) =>
            base.SetValue(new Vector3(value, value, value));
        
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}