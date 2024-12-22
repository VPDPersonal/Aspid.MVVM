#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class StringEmptyToBoolConverter : IConverterStringToBool
    {
        [SerializeField] private bool _isInvert;

        public StringEmptyToBoolConverter()
            : this(false) { }
        
        public StringEmptyToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        public bool Convert(string value)
        {
            var to = string.IsNullOrEmpty(value);
            return _isInvert ? !to : to;
        }
    }
}