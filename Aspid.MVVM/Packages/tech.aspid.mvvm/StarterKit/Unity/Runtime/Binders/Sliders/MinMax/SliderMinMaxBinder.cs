#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Slider, Vector2, Converter}"/> that sets <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/>.
    /// Also implements <see cref="INumberBinder"/>, allowing a scalar numeric value to be applied as equal min and max.
    /// </summary>
    /// <include file="XmlExampleDoc-Slider-MinMax-1.1.0.xml" path="doc//member[@name='SliderMinMaxBinder']/*" />
    [Serializable]
    public class SliderMinMaxBinder : TargetBinder<Slider, Vector2, Converter>, INumberBinder
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(Target.minValue, Target.maxValue);
            set => Target.SetMinMax(value, _valueMode);
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="SliderMinMaxBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Slider"/> to bind.</param>
        /// <param name="valueMode">Determines which endpoint(s) of the slider range are updated.</param>
        /// <param name="converter">The converter applied to values before they are set on the slider, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public SliderMinMaxBinder(
            Slider target,
            SliderValueMode valueMode = SliderValueMode.Range,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _valueMode = valueMode;
        }

        /// <summary>
        /// Sets both <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/> to
        /// the same value, then applies the configured <see cref="SliderValueMode"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets both slider endpoints.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets both slider endpoints.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        /// <summary>
        /// Casts the value to <see langword="float"/> and sets both slider endpoints.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}