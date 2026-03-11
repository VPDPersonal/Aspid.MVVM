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
    /// <see cref="AnimatorSetParameterMonoBinder{T}"/> that sets a float parameter on a <see cref="Animator"/>
    /// when the bound ViewModel value changes. Also implements <see cref="INumberBinder"/> to accept
    /// <see cref="int"/>, <see cref="long"/>, and <see cref="double"/> values.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Float")]
    public partial class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>, INumberBinder
    {
        [SerializeReferenceDropdown]
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