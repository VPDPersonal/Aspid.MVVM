using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AnimatorSetParameterMonoBinder{T}"/> that also implements <see cref="IIntBinder"/>,
    /// setting an integer Animator parameter and accepting <see cref="long"/>, <see cref="float"/>, and <see cref="double"/> values via truncating cast.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Int")]
    public class AnimatorSetIntMonoBinder : AnimatorSetParameterMonoBinder<int>, IIntBinder
    {
        [Tooltip("Converts the bound integer value before setting the Animator parameter.")]
        [SerializeReference] private IConverter<int, int> _converter;

        /// <summary>
        /// Applies <paramref name="value"/> (optionally converted) to the integer Animator parameter.
        /// Skips the call if the parameter already holds the same value.
        /// </summary>
        /// <param name="value">The integer value to apply.</param>
        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (value == CachedComponent.GetInteger(ParameterName)) return;

            CachedComponent.SetInteger(ParameterName, value);
        }
    }
}