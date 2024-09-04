using System;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Converters.Strings
{
    [Serializable]
    public sealed class StringEmptyToBoolConverter : IConverter<string, bool>
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