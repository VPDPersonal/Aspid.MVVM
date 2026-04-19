using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Greeter
{
    [Serializable]
    public sealed class PaintNameConverter : IConverterString
    {
        [SerializeField] private Color _color;

        public string Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3) return value;

            var hex = ColorUtility.ToHtmlStringRGB(_color); 
            return value.Insert(startIndex: 3, $"<color=#{hex}>") + "</color>"; 
        }
    }
}