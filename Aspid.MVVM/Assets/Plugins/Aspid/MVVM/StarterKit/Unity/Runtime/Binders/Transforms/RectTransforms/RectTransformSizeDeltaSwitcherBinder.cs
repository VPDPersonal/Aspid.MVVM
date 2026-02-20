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
    public sealed class RectTransformSizeDeltaSwitcherBinder : SwitcherBinder<RectTransform, Vector3, Converter>
    {
        [SerializeField] private SizeDeltaMode _sizeMode;

        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, converter: null, mode) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode sizeMode,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, sizeMode, converter: null, mode) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, SizeDeltaMode.SizeDelta, converter, mode) { }
        
        public RectTransformSizeDeltaSwitcherBinder(
            RectTransform target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SizeDeltaMode sizeMode = SizeDeltaMode.SizeDelta,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _sizeMode = sizeMode;
        }

        protected override void SetValue(Vector3 value) =>
            Target.SetSizeDelta(value, _sizeMode);
    }
}