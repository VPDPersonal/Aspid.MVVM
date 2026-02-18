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
    public sealed class CapsuleColliderCenterSwitcherBinder : SwitcherBinder<CapsuleCollider, Vector3, Converter>
    {
        public CapsuleColliderCenterSwitcherBinder(
            CapsuleCollider target,
            Vector3 trueValue, 
            Vector3 falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public CapsuleColliderCenterSwitcherBinder(
            CapsuleCollider target,
            Vector3 trueValue, 
            Vector3 falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(Vector3 value) =>
            Target.center = value;
    }
}