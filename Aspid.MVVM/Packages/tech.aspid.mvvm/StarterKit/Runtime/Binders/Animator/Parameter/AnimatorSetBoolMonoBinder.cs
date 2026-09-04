using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterMonoBinder{T}"/> that sets a <see langword="bool"/> parameter.
    /// </summary>
    /// <remarks>
    /// A value the parameter already holds is not written again.
    /// </remarks>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Bool")]
    public class AnimatorSetBoolMonoBinder : AnimatorSetParameterMonoBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <inheritdoc/>
        protected sealed override void SetParameter(bool value)
        {
            value = _converter?.Convert(value) ?? value;
            if (value == CachedComponent.GetBool(ParameterName)) return;

            CachedComponent.SetBool(ParameterName, value);
        }
    }
}
