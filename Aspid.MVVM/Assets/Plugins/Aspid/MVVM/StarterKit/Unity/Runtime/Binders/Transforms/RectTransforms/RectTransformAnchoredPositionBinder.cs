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
    public class RectTransformAnchoredPositionBinder : TargetVector3Binder<RectTransform>
    {
        [SerializeField] private Space _space;

        protected sealed override Vector3 Property
        {
            get => Target.GetAnchoredPosition(_space);
            set => Target.SetAnchoredPosition(value, _space);
        }

        public RectTransformAnchoredPositionBinder(
            RectTransform transform,
            BindMode mode)
            : this(transform, Space.World, converter: null, mode) { }

        public RectTransformAnchoredPositionBinder(
            RectTransform transform,
            Space space,
            BindMode mode)
            : this(transform, space, converter: null, mode) { }

        public RectTransformAnchoredPositionBinder(
            RectTransform transform,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(transform, Space.World, converter, mode) { }

        public RectTransformAnchoredPositionBinder(
            RectTransform target,
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