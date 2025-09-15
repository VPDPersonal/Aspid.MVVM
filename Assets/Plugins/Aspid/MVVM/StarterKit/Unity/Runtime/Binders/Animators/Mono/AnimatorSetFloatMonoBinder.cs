using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder - Set Float")]
    [AddComponentContextMenu(typeof(Animator),"Add Animator Binder/Animator Binder - Set Float")]
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