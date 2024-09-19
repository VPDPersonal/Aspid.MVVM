using System;
using UnityEngine;

namespace AspidUI.MVVM.StarterKit.Converters.Strings
{
    [Serializable]
    public sealed class ObjectToStringConverter : IConverterObjectToString
    {
        [SerializeField] private string _format;
        
        public ObjectToStringConverter() { }
        
        public ObjectToStringConverter(string format)
        {
            _format = format;
        }

        public string Convert(object value)
        {
            var convertedValue = value?.ToString();
            if (string.IsNullOrEmpty(convertedValue)) return convertedValue;
            
            return string.IsNullOrEmpty(_format) ? convertedValue : string.Format(_format, convertedValue);
        }
    }
}