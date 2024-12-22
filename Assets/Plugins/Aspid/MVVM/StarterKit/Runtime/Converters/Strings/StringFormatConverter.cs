using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class StringFormatConverter : IConverterString
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