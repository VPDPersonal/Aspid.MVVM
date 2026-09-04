using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AnimatorSetParameterMonoBinder{T}"/> that sets a <see langword="int"/> parameter.
    /// </summary>
    /// <remarks>
    /// Also accepts the other numeric types. A value the parameter already holds is not written again.
    /// </remarks>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Int")]
    public class AnimatorSetIntMonoBinder : AnimatorSetParameterMonoBinder<int>, IIntBinder
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<int, int> _converter;

        /// <inheritdoc/>
        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (value == CachedComponent.GetInteger(ParameterName)) return;

            CachedComponent.SetInteger(ParameterName, value);
        }
    }
}
