#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetColorBinder{LineRenderer}"/> that sets the <see cref="LineRenderer.startColor"/> and <see cref="LineRenderer.endColor"/>
    /// color depending on the configured <see cref="LineRendererColorMode"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-LineRenderer-Color-1.1.0.xml" path="doc//member[@name='LineRendererColorBinder']/*" />
    [Serializable]
    public class LineRendererColorBinder : TargetColorBinder<LineRenderer>
    {
        [Tooltip("The color endpoint(s) to set when a value arrives from the ViewModel.")]
        [SerializeField] private LineRendererColorMode _colorMode;

        /// <inheritdoc/>
        protected sealed override Color Property
        {
            get => Target.GetColor(_colorMode);
            set => Target.SetColor(value, _colorMode);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LineRendererColorBinder"/> targeting the specified <see cref="LineRenderer"/>.
        /// </summary>
        /// <param name="target">The <see cref="LineRenderer"/> to bind.</param>
        /// <param name="colorMode">The color endpoint(s) to set when a value arrives from the ViewModel.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use no conversion.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        public LineRendererColorBinder(
            LineRenderer target,
            LineRendererColorMode colorMode = LineRendererColorMode.StartAndEnd,
            IConverter<Color, Color>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _colorMode = colorMode;
        }
    }
}