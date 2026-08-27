#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterBinder{T}"/> that sets a boolean parameter on an <see cref="Animator"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    /// <include file="XmlExampleDoc-Animator-1.1.0.xml" path="doc//member[@name='AnimatorSetBoolBinder']/*" />
    [Serializable]
    public class AnimatorSetBoolBinder : AnimatorSetParameterBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool>? _converter;

        /// <param name="animator">The <see cref="Animator"/> whose boolean parameter is set.</param>
        /// <param name="parameterName">The name of the boolean Animator parameter.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetBoolBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, converter: null, mode) { }

        /// <param name="animator">The <see cref="Animator"/> whose boolean parameter is set.</param>
        /// <param name="parameterName">The name of the boolean Animator parameter.</param>
        /// <param name="converter">
        /// An optional converter applied to the value before it is applied. Pass <see langword="null"/> to use the
        /// value unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetBoolBinder(
            Animator animator,
            string parameterName,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
        {
            _converter = converter;
        }

        /// <summary>
        /// Applies <paramref name="value"/>, transformed by the configured converter if present, to the boolean
        /// Animator parameter. Skips the call if the parameter already holds the same value.
        /// </summary>
        /// <param name="value">The boolean value to apply.</param>
        protected sealed override void SetParameter(bool value)
        {
            value = _converter?.Convert(value) ?? value;
            if (value == Target.GetBool(ParameterName)) return;

            Target.SetBool(ParameterName, value);
        }
    }
}
