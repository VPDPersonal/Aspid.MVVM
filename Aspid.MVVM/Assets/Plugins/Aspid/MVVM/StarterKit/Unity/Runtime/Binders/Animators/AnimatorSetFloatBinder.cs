#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterBinder{T}"/> that sets a float parameter on an <see cref="Animator"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    /// <include file="XmlExampleDoc-Animator-1.1.0.xml" path="doc//member[@name='AnimatorSetFloatBinder']/*" />
    [Serializable]
    public class AnimatorSetFloatBinder : AnimatorSetParameterBinder<float>
    {
        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the bound float value before setting the Animator parameter.")]
        [SerializeReference] private Converter? _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetFloatBinder"/> with no converter.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> whose float parameter is set.</param>
        /// <param name="parameterName">The name of the float Animator parameter.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetFloatBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetFloatBinder"/>.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> whose float parameter is set.</param>
        /// <param name="parameterName">The name of the float Animator parameter.</param>
        /// <param name="converter">The converter used to transform the bound float value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetFloatBinder(
            Animator animator,
            string parameterName,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
        {
            _converter = converter;
        }

        /// <summary>
        /// Applies <paramref name="value"/> (optionally converted) to the float Animator parameter.
        /// Skips the call if the parameter already holds an approximately equal value.
        /// </summary>
        /// <param name="value">The float value to apply.</param>
        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, Target.GetFloat(ParameterName))) return;

            Target.SetFloat(ParameterName, value);
        }
    }
}