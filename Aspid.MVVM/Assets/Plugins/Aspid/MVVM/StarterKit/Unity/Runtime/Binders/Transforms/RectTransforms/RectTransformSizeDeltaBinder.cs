#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class RectTransformSizeDeltaBinder : TargetBinder<RectTransform>, IBinder<Vector2>, INumberBinder
    {
        [SerializeField] private SizeDeltaMode _sizeMode;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
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
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _sizeMode = sizeMode;
            _converter = converter;
        }

        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetSizeDelta(value, _sizeMode);
        }

        public void SetValue(int value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(long value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(double value) =>
            SetValue((float)value);
    }
}