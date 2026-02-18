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
    public class TransformScaleBinder : TargetVector3Binder<Transform>
    {
        protected sealed override Vector3 Property
        {
            get => Target.localScale;
            set => Target.localScale = value;
        }

        public TransformScaleBinder(Transform target, BindMode mode = BindMode.OneWay)
            : this(target, converter: null, mode) { }

        public TransformScaleBinder(Transform target, Converter? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}