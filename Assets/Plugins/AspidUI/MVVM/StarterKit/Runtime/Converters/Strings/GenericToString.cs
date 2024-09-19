using System;
using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Converters.Strings
{
    [Serializable]
    public class GenericToString<TFrom> : IConverter<TFrom, string>
    {
        [SerializeField] private string _format;
        
        public GenericToString() { }
        
        public GenericToString(string format)
        {
            _format = format;
        }

        public string Convert(TFrom value)
        {
            var convertedValue = value?.ToString();
            if (string.IsNullOrEmpty(convertedValue)) return convertedValue;
            
            return string.IsNullOrEmpty(_format) ? convertedValue : string.Format(_format, convertedValue);
        }
    }
}