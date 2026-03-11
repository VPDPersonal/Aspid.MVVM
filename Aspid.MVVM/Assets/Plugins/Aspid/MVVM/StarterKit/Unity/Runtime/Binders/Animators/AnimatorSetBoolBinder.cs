#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterBinder{T}"/> that sets a boolean parameter on a <see cref="Animator"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    [Serializable]
    public class AnimatorSetBoolBinder : AnimatorSetParameterBinder<bool>
    {
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetBoolBinder"/> with inversion disabled.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> whose boolean parameter is set.</param>
        /// <param name="parameterName">The name of the boolean Animator parameter.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetBoolBinder(Animator animator, string parameterName, BindMode mode)
            : this(animator, parameterName, isInvert: false, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AnimatorSetBoolBinder"/>.
        /// </summary>
        /// <param name="animator">The <see cref="Animator"/> whose boolean parameter is set.</param>
        /// <param name="parameterName">The name of the boolean Animator parameter.</param>
        /// <param name="isInvert">When <see langword="true"/>, the bound boolean value is inverted before being applied.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public AnimatorSetBoolBinder(
            Animator animator,
            string parameterName,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(animator, parameterName, mode)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Applies <paramref name="value"/> (optionally inverted) to the boolean Animator parameter.
        /// Skips the call if the parameter already holds the same value.
        /// </summary>
        /// <param name="value">The boolean value to apply.</param>
        protected sealed override void SetParameter(bool value)
        {
            value = _isInvert ? !value : value;
            if (value == Target.GetBool(ParameterName)) return;

            Target.SetBool(ParameterName, value);
        }
    }
}