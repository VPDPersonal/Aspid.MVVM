using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale")]
    public partial class TransformScaleMonoBinder : MonoBinder, IVectorBinder, INumberBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
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