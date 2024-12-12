using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Animator/Animator Binder - Set Float")]
    public class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>
    {
        [Header("Parameters")]
        [SerializeField] private string _parameterName;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<float, float> _converter;
#else
        [SerializeReference] private IConverterFloatToFloat _converter;
#endif
        
        protected string ParameterName => _parameterName;
        
        protected sealed override void SetParameter(float value) =>
            CachedComponent.SetFloat(ParameterName, _converter?.Convert(value) ?? value);

        protected override bool CanExecute(float value) =>
            base.CanExecute(value) && !Mathf.Approximately(CachedComponent.GetFloat(ParameterName), value);
    }
}