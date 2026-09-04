#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterBinder{T}"/> that sets a <see langword="int"/> parameter.
    /// </summary>
    /// <remarks>
    /// Also accepts the other numeric types. A value the parameter already holds is not written again.
    /// </remarks>
    [Serializable]
    public class AnimatorSetIntBinder : AnimatorSetParameterBinder<int>, IIntBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<int, int>? _converter;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected AnimatorSetIntBinder() { }

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
        public AnimatorSetIntBinder(
            Animator target,
            string parameterName,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, parameterName, mode)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (value == Target.GetInteger(ParameterName)) return;

            Target.SetInteger(ParameterName, value);
        }
    }
}
