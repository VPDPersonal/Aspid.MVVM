using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Converters.Strings
{
    [Serializable]
    public sealed class StringFormatConverter : IConverterStringToString
    {
        [SerializeField] private string _format;

        public StringFormatConverter()
        {
            _format = string.Empty;
        }
        
        public StringFormatConverter(string format)
        {
            _format = format;
        }

        public string Convert(string value) =>
            string.IsNullOrEmpty(_format) ? value : string.Format(_format, value);
    }
}