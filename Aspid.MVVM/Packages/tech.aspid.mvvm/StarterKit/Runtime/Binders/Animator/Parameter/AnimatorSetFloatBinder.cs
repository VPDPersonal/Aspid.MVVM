#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterBinder{T}"/> that sets a <see langword="float"/> parameter.
    /// </summary>
    /// <remarks>
    /// Also accepts the other numeric types. A value the parameter already holds is not written again.
    /// </remarks>
    [Serializable]
    public class AnimatorSetFloatBinder : AnimatorSetParameterBinder<float>, IFloatBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<float, float>? _converter;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected AnimatorSetFloatBinder() { }

        /// <param name="target">The animator to bind.</param>
        /// <param name="parameterName">The parameter to set.</param>
        /// <param name="converter">
        /// The converter applied to the bound value, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="parameterName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AnimatorSetFloatBinder(
            Animator target,
            string parameterName,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, parameterName, mode)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, Target.GetFloat(ParameterName))) return;

            Target.SetFloat(ParameterName, value);
        }
    }
}
