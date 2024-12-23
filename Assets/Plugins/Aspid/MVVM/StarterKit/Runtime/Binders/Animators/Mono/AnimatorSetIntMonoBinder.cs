using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Animator/Animator Binder - Set Int")]
    public class AnimatorSetIntMonoBinder : AnimatorSetParameterMonoBinder<int>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<int, int> _converter;
#else
        private IConverterInt _converter;
#endif
        
        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, CachedComponent.GetInteger(ParameterName))) return;
            
            CachedComponent.SetInteger(ParameterName, value);
        }
    }
}