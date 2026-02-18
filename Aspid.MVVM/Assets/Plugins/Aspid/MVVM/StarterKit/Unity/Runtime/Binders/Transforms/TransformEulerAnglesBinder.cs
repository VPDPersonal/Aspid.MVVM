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
    public class TransformEulerAnglesBinder : TargetVector3Binder<Transform>
    {
        [SerializeField] private Space _space;

        protected sealed override Vector3 Property
        {
            get => Target.GetEulerAngles(_space);
            set => Target.SetEulerAngles(value, _space);
        }

        public TransformEulerAnglesBinder(Transform target, BindMode mode)
            : this(target, Space.World, converter: null, mode) { }

        public TransformEulerAnglesBinder(Transform target, Space space, BindMode mode)
            : this(target, space, converter: null, mode) { }

        public TransformEulerAnglesBinder(
            Transform target,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, Space.World, converter, mode) { }

        public TransformEulerAnglesBinder(
            Transform target,
            Space space = Space.World,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _space = space;
        }
    }
}