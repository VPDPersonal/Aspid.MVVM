using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extends <see cref="IBinder{T}"/> with the ability to accept hex/HTML color strings,
    /// converting them to <see cref="Color"/> before applying.
    /// </summary>
    public interface IColorBinder : IBinder<Color>, IBinder<string>
    {
        /// <summary>
        /// Parses <paramref name="value"/> as an HTML color string and applies the resulting <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The hex or HTML color string to parse (e.g. <c>#FF0000</c> or <c>red</c>).</param>
        void IBinder<string>.SetValue(string value)
        {
            ColorUtility.TryParseHtmlString(value, out var color);
            SetValue(color);
        }
    }
}