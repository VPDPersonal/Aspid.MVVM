#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class TextBinder : TargetBinder<TMP_Text, string, Converter>, INumberBinder
    {
        protected sealed override string? Property
        {
            get => Target.text;
            set => Target.text = value;
        }

        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;
        
        public TextBinder(TMP_Text target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public TextBinder(TMP_Text target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
        
        public void SetValue(int value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        public void SetValue(long value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        public void SetValue(float value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
        
        public void SetValue(double value) =>
            SetValue(value.ToCultureString(_cultureInfoMode));
    }
}
#endif