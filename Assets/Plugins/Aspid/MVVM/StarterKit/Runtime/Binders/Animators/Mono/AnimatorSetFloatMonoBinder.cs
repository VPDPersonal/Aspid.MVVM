using UnityEngine;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Animator/Animator Binder - Set Float")]
    public partial class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, CachedComponent.GetFloat(ParameterName))) return;
            
            CachedComponent.SetFloat(ParameterName, value);
        }

        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(value);

        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(value);

        [BinderLog]
        public void SetValue(double value) =>
            base.SetValue((float)value);
    }
}