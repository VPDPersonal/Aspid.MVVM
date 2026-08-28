#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Slider, Vector2}"/> that sets <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/>.
    /// Also implements <see cref="IVector2Binder"/>, allowing a scalar numeric value to be applied as equal min and max.
    /// </summary>
    /// <include file="XmlExampleDoc-Slider-MinMax-1.1.0.xml" path="doc//member[@name='SliderMinMaxBinder']/*" />
    [Serializable]
    public partial class SliderMinMaxBinder : TargetBinder<Slider, Vector2>, IVector2Binder
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(Target.minValue, Target.maxValue);
            set => Target.SetMinMax(value, _valueMode);
        }
        
        /// <param name="target">The <see cref="Slider"/> to bind.</param>
        /// <param name="valueMode">Determines which endpoint(s) of the slider range are updated.</param>
        /// <param name="converter">The converter applied to values before they are set on the slider, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public SliderMinMaxBinder(
            Slider target,
            SliderValueMode valueMode = SliderValueMode.Range,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _valueMode = valueMode;
        }
    }
}
