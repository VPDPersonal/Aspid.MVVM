using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AnimatorSetParameterMonoBinder{T}"/> that also implements <see cref="IFloatBinder"/>,
    /// setting a float Animator parameter and accepting <see cref="int"/>, <see cref="long"/>, and <see cref="double"/> values.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Float")]
    public class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>, IFloatBinder
    {
        [Tooltip("Converts the bound float value before setting the Animator parameter.")]
        [SerializeReference] private IConverter<float, float> _converter;

        /// <summary>
        /// Applies <paramref name="value"/> (optionally converted) to the float Animator parameter.
        /// Skips the call if the parameter already holds an approximately equal value.
        /// </summary>
        /// <param name="value">The float value to apply.</param>
        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, CachedComponent.GetFloat(ParameterName))) return;

            CachedComponent.SetFloat(ParameterName, value);
        }
    }
}