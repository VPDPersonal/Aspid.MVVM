using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(float))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Float")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/UnityEvent/UnityEvent Binder - Float")]
    public sealed partial class UnityEventFloatMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction<float> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<float> _set;

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}