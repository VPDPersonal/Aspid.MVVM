#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom?, string?>
    {
        [SerializeField] private string? _format;
        
        public GenericToString() { }
        
        public GenericToString(string? format)
        {
            _format = format;
        }

        public string? Convert(TFrom? value)
        {
            if (value is null) return null;
            return string.IsNullOrEmpty(_format) ? ToStringValue(value) : string.Format(_format, value);
        }

        protected virtual string ToStringValue(TFrom value) => 
            value!.ToString();
    }
}