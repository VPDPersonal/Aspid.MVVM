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
    /// <see cref="SwitcherBinder{Slider, Vector2, Converter}"/> that switches <see cref="Slider.minValue"/> and <see cref="Slider.maxValue"/> between two ranges based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Slider-MinMax-1.1.0.xml" path="doc//member[@name='SliderMinMaxSwitcherBinder']/*" />
    [Serializable]
    public sealed class SliderMinMaxSwitcherBinder : SwitcherBinder<Slider, Vector2, Converter>
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode;
        
        /// <summary>
        /// Initializes a new instance of <see cref="SliderMinMaxSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Slider"/> to bind.</param>
        /// <param name="trueValue">The range applied when the bound property is <see langword="true"/>.</param>
        /// <param name="falseValue">The range applied when the bound property is <see langword="false"/>.</param>
        /// <param name="valueMode">Determines which endpoint(s) of the slider range are updated.</param>
        /// <param name="converter">The converter applied to values before they are set on the slider, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode.</param>
        public SliderMinMaxSwitcherBinder(
            Slider target,
            Vector2 trueValue,
            Vector2 falseValue,
            SliderValueMode valueMode = SliderValueMode.Range,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _valueMode = valueMode;
        }

        /// <summary>
        /// Called when applying the selected range to the slider using the configured <see cref="SliderValueMode"/>.
        /// </summary>
        protected override void SetValue(Vector2 value) =>
            Target.SetMinMax(value, _valueMode);
    }
}