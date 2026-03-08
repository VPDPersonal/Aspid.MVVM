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
    /// <summary>
    /// Binder that switches the <see cref="SphereCollider.center"/> property on a <see cref="SphereCollider"/>
    /// between two values based on a bound boolean ViewModel value.
    /// </summary>
    [Serializable]
    public sealed class SphereColliderCenterSwitcherBinder : SwitcherBinder<SphereCollider, Vector3, Converter>
    {
        public SphereColliderCenterSwitcherBinder(
            SphereCollider target,
            Vector3 trueValue,
            Vector3 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        public SphereColliderCenterSwitcherBinder(
            SphereCollider target,
            Vector3 trueValue,
            Vector3 falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(Vector3 value) =>
            Target.center = value;
    }
}