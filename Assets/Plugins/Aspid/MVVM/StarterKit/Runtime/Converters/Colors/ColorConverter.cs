#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Converters.Colors
{
    [Serializable]
    public sealed class ColorConverter : IConverterStringToColor
    {
        [SerializeField] private bool _isThrowException;
        [SerializeField] private Color _defaultColor = new(0, 0, 0, 0);
        
        public Color Convert(string? value)
        {
            if (ColorUtility.TryParseHtmlString(value, out var color)) return color;
            if (!_isThrowException) return _defaultColor;

            throw new ArgumentException($"Invalid value: {value}");
        }
    }
}