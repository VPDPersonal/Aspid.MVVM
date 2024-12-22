#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters
{
    [Serializable]
    public sealed class ObjectNullToBoolConverter : IConverterObjectToBool
    {
        [SerializeField] private bool _isInvert;

        public ObjectNullToBoolConverter()
            : this(false) { }
        
        public ObjectNullToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        public bool Convert(object? value)
        {
            var to = value is null;
            return _isInvert ? !to : to;
        }
    }
}