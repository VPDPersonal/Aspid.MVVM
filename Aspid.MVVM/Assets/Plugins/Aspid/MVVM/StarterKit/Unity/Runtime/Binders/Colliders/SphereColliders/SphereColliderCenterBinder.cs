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
    public class SphereColliderCenterBinder : TargetVector3Binder<SphereCollider>
    {
        protected sealed override Vector3 Property
        {
            get => Target.center;
            set => Target.center = value;
        }

        public SphereColliderCenterBinder(SphereCollider target, BindMode mode)
            : this(target, converter: null, mode) { }

        public SphereColliderCenterBinder(
            SphereCollider target,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}