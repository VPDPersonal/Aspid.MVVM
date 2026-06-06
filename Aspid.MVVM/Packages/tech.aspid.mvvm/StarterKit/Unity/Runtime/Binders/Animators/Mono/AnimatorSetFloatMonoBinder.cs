using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="AnimatorSetParameterMonoBinder{T}"/> that also implements <see cref="INumberBinder"/>,
    /// setting a float Animator parameter and accepting <see cref="int"/>, <see cref="long"/>, and <see cref="double"/> values.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Float")]
    public partial class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>, INumberBinder
    {
        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the bound float value before setting the Animator parameter.")]
        [SerializeReference] private Converter _converter;

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

        /// <summary>
        /// Forwards <paramref name="value"/> cast to <see cref="float"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(value);

        /// <summary>
        /// Forwards <paramref name="value"/> cast to <see cref="float"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(value);

        /// <summary>
        /// Forwards <paramref name="value"/> cast to <see cref="float"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(double value) =>
            base.SetValue((float)value);
    }
}