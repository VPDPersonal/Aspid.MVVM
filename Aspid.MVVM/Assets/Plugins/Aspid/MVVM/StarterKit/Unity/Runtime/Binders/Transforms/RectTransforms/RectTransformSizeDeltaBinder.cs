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
    public class RectTransformSizeDeltaBinder : TargetVector3Binder<RectTransform>
    {
        [SerializeField] private SizeDeltaMode _sizeMode;

        protected sealed override Vector3 Property
        {
            get => Target.sizeDelta;
            set => Target.SetSizeDelta(value, _sizeMode);
        }

        public RectTransformSizeDeltaBinder(
            RectTransform target,
            BindMode mode)
            : this(target, SizeDeltaMode.SizeDelta, converter: null, mode) { }

        public RectTransformSizeDeltaBinder(
            RectTransform target,
            SizeDeltaMode sizeMode,
            BindMode mode = BindMode.OneWay)
            : this(target, sizeMode, converter: null, mode) { }

        public RectTransformSizeDeltaBinder(
            RectTransform target,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, SizeDeltaMode.SizeDelta, converter, mode) { }

        public RectTransformSizeDeltaBinder(
            RectTransform target, 
            SizeDeltaMode sizeMode = SizeDeltaMode.SizeDelta, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _sizeMode = sizeMode;
        }
    }
}