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
    public class TextBinder : TargetBinder<TMP_Text>, IBinder<string?>, INumberBinder
    {
        [SerializeField] private CultureInfoMode _cultureInfoMode = CultureInfoMode.CurrentCulture;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public TextBinder(TMP_Text target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public TextBinder(TMP_Text target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter; 
        }

        public void SetValue(string? value)
        {
            if (value is null) Target.text = null;
            else Target.text = _converter?.Convert(value) ?? value;
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