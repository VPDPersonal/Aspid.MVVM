using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets an integer parameter on a Unity <see cref="Animator"/> when
    /// the bound ViewModel value changes. Also implements <see cref="INumberBinder"/> to accept
    /// <see cref="long"/>, <see cref="float"/>, and <see cref="double"/> values via truncating cast.
    /// </summary>
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Int")]
    public partial class AnimatorSetIntMonoBinder : AnimatorSetParameterMonoBinder<int>, INumberBinder
    {
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