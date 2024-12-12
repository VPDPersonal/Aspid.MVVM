using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Animator/Animator Binder - Set Int")]
    public class AnimatorSetIntMonoBinder : AnimatorSetParameterMonoBinder<int>
    {
        [Header("Parameters")]
        [SerializeField] private string _parameterName;
     
        [Header("Converter")]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<int, int> _converter;
#else
        [SerializeReference] private IConverterIntToInt _converter;
#endif
        
        protected string ParameterName => _parameterName;
        
        protected sealed override void SetParameter(int value) =>
            CachedComponent.SetFloat(ParameterName, _converter?.Convert(value) ?? value);

        protected override bool CanExecute(int value) =>
            base.CanExecute(value) && CachedComponent.GetInteger(ParameterName) != value;
    }
}