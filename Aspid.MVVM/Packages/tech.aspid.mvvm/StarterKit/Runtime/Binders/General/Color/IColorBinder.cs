using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IBinder{T}">IBinder&lt;Color&gt;</see> that also accepts an HTML color string such as <c>#FF0000</c> or <c>red</c>.
    /// </summary>
    public interface IColorBinder : IBinder<Color>, IBinder<string>
    {
        /// <summary>
        /// Parses <paramref name="value"/> as an HTML color and applies it. An empty string applies <see langword="default"/>.
        /// </summary>
        /// <param name="value">The HTML color string.</param>
        void IBinder<string>.SetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                SetValue(default(Color));
                return;
            }

            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                this.LogError(
                    problem: $"'{value}' is not an HTML color",
                    consequence: "The value is ignored.");

                return;
            }

            SetValue(color);
        }
    }
}
