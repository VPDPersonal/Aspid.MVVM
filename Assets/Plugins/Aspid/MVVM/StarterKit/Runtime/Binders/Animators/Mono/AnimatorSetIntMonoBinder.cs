using UnityEngine;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterInt;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder - Set Int")]
    public partial class AnimatorSetIntMonoBinder : AnimatorSetParameterMonoBinder<int>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected sealed override void SetParameter(int value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, CachedComponent.GetInteger(ParameterName))) return;
            
            CachedComponent.SetInteger(ParameterName, value);
        }

        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue((int)value);

        [BinderLog]
        public void SetValue(float value)=>
            base.SetValue((int)value);

        [BinderLog]
        public void SetValue(double value)=>
            base.SetValue((int)value);
    }
}