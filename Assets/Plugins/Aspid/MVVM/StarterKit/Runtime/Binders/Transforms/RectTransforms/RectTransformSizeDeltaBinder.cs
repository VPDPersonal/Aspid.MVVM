#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class RectTransformSizeDeltaBinder : TargetBinder<RectTransform>, IBinder<Vector2>, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private SizeDeltaMode _mode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public RectTransformSizeDeltaBinder(RectTransform target, Func<Vector2, Vector2> converter)
            : this(target, SizeDeltaMode.SizeDelta, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaBinder(RectTransform target, SizeDeltaMode mode, Func<Vector2, Vector2> converter)
            : this(target, mode, converter.ToConvert()) { }
        
        public RectTransformSizeDeltaBinder(
            RectTransform target, 
            SizeDeltaMode mode = SizeDeltaMode.SizeDelta, 
            Converter? converter = null)
            : base(target)
        {
            _mode = mode;
            _converter = converter;
        }

        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetSizeDelta(value, _mode);
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