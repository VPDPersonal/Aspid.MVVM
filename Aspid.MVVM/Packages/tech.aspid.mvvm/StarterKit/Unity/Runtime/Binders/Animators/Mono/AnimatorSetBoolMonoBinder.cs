using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterMonoBinder{T}"/> that sets a boolean parameter on an <see cref="Animator"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Bool")]
    public class AnimatorSetBoolMonoBinder : AnimatorSetParameterMonoBinder<bool>
    {
        [Tooltip("When enabled, the bound boolean value is inverted before being applied to the Animator parameter.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Applies <paramref name="value"/> (optionally inverted) to the boolean Animator parameter.
        /// Skips the call if the parameter already holds the same value.
        /// </summary>
        /// <param name="value">The boolean value to apply.</param>
        protected sealed override void SetParameter(bool value)
        {
            value = _isInvert ? !value : value;
            if (value == CachedComponent.GetBool(ParameterName)) return;

            CachedComponent.SetBool(ParameterName, value);
        }
    }
}