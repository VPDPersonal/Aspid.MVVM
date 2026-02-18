#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.RectOffset?, UnityEngine.RectOffset?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterRectOffset;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class LayoutGroupPaddingBinder : TargetBinder<LayoutGroup, RectOffset, Converter>, INumberBinder
    {
        [SerializeField] private PaddingMode _paddingMode;
        [NonSerialized] private RectOffset? _cachedRectOffset;
        
        protected sealed override RectOffset Property
        {
            get => Target.padding;
            set => Target.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
        }
        
        public LayoutGroupPaddingBinder(
            LayoutGroup target, 
            PaddingMode paddingMode,
            BindMode bindMode = BindMode.OneWay)
            : this(target, paddingMode, converter: null, bindMode) { }
        
        public LayoutGroupPaddingBinder(
            LayoutGroup target, 
            PaddingMode paddingMode,
            Converter? converter = null,
            BindMode bindMode = BindMode.OneWay)
            : base(target, converter, bindMode)
        {
            bindMode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _paddingMode = paddingMode;
        }
        
        public void SetValue(int value)
        {
            _cachedRectOffset ??= new RectOffset();
            _cachedRectOffset.left = value;
            _cachedRectOffset.right = value;
            _cachedRectOffset.top = value;
            _cachedRectOffset.bottom = value;
            
            base.SetValue(_cachedRectOffset);
        }
        
        public void SetValue(long value) =>
            SetValue((int)value);
        
        public void SetValue(float value) =>
            SetValue((int)value);
        
        public void SetValue(double value) =>
            SetValue((int)value);
    }
}