using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale")]
    public partial class TransformScaleMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IVectorBinder, INumberBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterVector3ToVector3 Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value) =>  
            transform.localScale = Converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(Vector3.one * value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue(Vector3.one * value);
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(Vector3.one * value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(Vector3.one * (float)value);
    }
}