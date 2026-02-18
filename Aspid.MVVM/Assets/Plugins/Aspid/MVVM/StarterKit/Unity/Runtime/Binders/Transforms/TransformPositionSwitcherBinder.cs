#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TransformPositionSwitcherBinder : SwitcherBinder<Transform, Vector3, Converter>
    {
        [SerializeField] private Space _space;
       
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            BindMode mode) 
            : this(target, trueValue, falseValue, Space.World, converter: null, mode) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Space space,
            BindMode mode) 
            : this(target, trueValue, falseValue, space, converter: null, mode) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Converter? converter,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, Space.World, converter, mode) { }
        
        public TransformPositionSwitcherBinder(
            Transform target,
            Vector3 trueValue,
            Vector3 falseValue, 
            Space space = Space.World,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode)
        {
            _space = space;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetPosition(value, _space);
    }
}