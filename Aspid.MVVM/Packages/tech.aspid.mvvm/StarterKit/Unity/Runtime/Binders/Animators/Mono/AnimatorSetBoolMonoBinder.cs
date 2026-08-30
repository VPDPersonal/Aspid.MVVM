using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterMonoBinder{T}"/> that sets a boolean parameter on an <see cref="Animator"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Bool")]
    public class AnimatorSetBoolMonoBinder : AnimatorSetParameterMonoBinder<bool>
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool> _converter;

        /// <summary>
        /// Applies <paramref name="value"/>, transformed by the configured converter if present, to the boolean
        /// Animator parameter. Skips the call if the parameter already holds the same value.
        /// </summary>
        /// <param name="value">The boolean value to apply.</param>
        protected sealed override void SetParameter(bool value)
        {
            value = _converter?.Convert(value) ?? value;
            if (value == CachedComponent.GetBool(ParameterName)) return;

            CachedComponent.SetBool(ParameterName, value);
        }
    }
}
