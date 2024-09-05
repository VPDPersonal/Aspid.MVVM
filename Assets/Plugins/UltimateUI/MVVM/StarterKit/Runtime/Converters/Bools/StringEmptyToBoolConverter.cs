using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Converters.Bools
{
    [Serializable]
    public sealed class StringEmptyToBoolConverter : IConverterStringToBool
    {
        [SerializeField] private bool _isInvert;

        public StringEmptyToBoolConverter(bool isInvert = false)
        {
            _isInvert = isInvert;
        }

        public bool Convert(string value) =>
            string.IsNullOrEmpty(value);
    }
}