#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class ParseHtmlStringConverter : IConverterStringToColor
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