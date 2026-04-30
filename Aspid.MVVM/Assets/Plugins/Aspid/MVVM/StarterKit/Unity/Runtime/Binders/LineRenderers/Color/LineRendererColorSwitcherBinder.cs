#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherColorBinder{LineRenderer}"/> that switches the <see cref="LineRenderer.startColor"/> and <see cref="LineRenderer.endColor"/>
    /// color between two <see cref="Color"/> values depending on the configured <see cref="LineRendererColorMode"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-LineRenderer-Color-1.1.0.xml" path="doc//member[@name='LineRendererColorSwitcherBinder']/*" />
    [Serializable]
    public sealed class LineRendererColorSwitcherBinder : SwitcherColorBinder<LineRenderer>
    {
        [Tooltip("The color endpoint(s) to set when a value is applied.")]
        [SerializeField] private LineRendererColorMode _colorMode;

        /// <summary>
        /// Initializes a new instance of <see cref="LineRendererColorSwitcherBinder"/> targeting the specified <see cref="LineRenderer"/>.
        /// </summary>
        /// <param name="target">The <see cref="LineRenderer"/> to bind.</param>
        /// <param name="trueValue">The color applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The color applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="colorMode">The color endpoint(s) to set when a value is applied.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use no conversion.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        public LineRendererColorSwitcherBinder(
            LineRenderer target,
            Color trueValue,
            Color falseValue,
            LineRendererColorMode colorMode = LineRendererColorMode.StartAndEnd,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _colorMode = colorMode;
        }

        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            Target.SetColor(value, _colorMode);
    }
}