using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterMonoBinder{T}"/> that sets a <see langword="float"/> parameter.
    /// </summary>
    /// <remarks>
    /// Also accepts the other numeric types. A value the parameter already holds is not written again.
    /// </remarks>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Float")]
    public class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>, IFloatBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<float, float> _converter;

        /// <inheritdoc/>
        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, CachedComponent.GetFloat(ParameterName))) return;

            CachedComponent.SetFloat(ParameterName, value);
        }
    }
}
