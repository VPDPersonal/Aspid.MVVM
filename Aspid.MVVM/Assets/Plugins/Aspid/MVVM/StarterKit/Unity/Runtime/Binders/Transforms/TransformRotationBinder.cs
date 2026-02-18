#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterQuaternion;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TransformRotationBinder : TargetBinder<Transform, Quaternion, Converter>,
        INumberBinder,
        IRotationBinder
    {
        [SerializeField] private Space _space;
        
        protected sealed override Quaternion Property
        {
            get => Target.GetRotation(_space);
            set => Target.SetRotation(value, _space);
        }

        public TransformRotationBinder(Transform target, BindMode mode) 
            : this(target, Space.World, converter: null, mode) { }

        public TransformRotationBinder(Transform target, Space space, BindMode mode) 
            : this(target, space, converter: null, mode) { }

        public TransformRotationBinder(Transform target, Converter? converter, BindMode mode = BindMode.OneWay) 
            : this(target, Space.World, converter, mode) { }

        public TransformRotationBinder(
            Transform target,
            Space space = Space.World, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)    
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _space = space;
        }

        public void SetValue(int value) =>
            SetValue((float)value);
        
        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(double value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            base.SetValue(Quaternion.Euler(new Vector3(value, value, value)));
    }
}