#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterBinder{T}"/> that sets an integer parameter on a <see cref="Animator"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    /// <include file="XmlExampleDoc-Animator-1.1.0.xml" path="doc//member[@name='AnimatorSetIntBinder']/*" />
    [Serializable]
    public class AnimatorSetIntBinder : AnimatorSetParameterBinder<int>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetIntBinder"/> with no converter.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> whose integer parameter is set.</param>
        /// <param name="parameterName">The name of the integer Animator parameter.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetIntBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, converter: null, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetIntBinder"/>.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> whose integer parameter is set.</param>
        /// <param name="parameterName">The name of the integer Animator parameter.</param>
        /// <param name="converter">The converter used to transform the bound integer value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetIntBinder(
            Animator animator,
            string parameterName,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
        {
            _converter = converter;
        }

        /// <summary>
        /// Applies <paramref name="value"/> (optionally converted) to the integer Animator parameter.
        /// Skips the call if the parameter already holds an approximately equal value.
        /// </summary>
        /// <param name="value">The integer value to apply.</param>
        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, Target.GetInteger(ParameterName))) return;

            Target.SetInteger(ParameterName, value);
        }
    }
}