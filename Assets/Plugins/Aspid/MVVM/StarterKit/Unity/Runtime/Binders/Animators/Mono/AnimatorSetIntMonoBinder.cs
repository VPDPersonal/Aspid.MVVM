using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder - Set Int")]
    [AddComponentContextMenu(typeof(Animator),"Add Animator Binder/Animator Binder - Set Int")]
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