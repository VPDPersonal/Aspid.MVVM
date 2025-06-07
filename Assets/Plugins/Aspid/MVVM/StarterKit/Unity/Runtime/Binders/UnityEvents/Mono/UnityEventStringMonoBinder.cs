using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;
using System.Globalization;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(string))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - String")]
    [AddComponentContextMenu(typeof(Component),"Add UnityEvent Binder/UnityEvent Binder - String")]
    public sealed partial class UnityEventStringMonoBinder : MonoBinder, IBinder<string>, IAnyBinder, INumberBinder
    {
        public event UnityAction<string> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _set;
        
        [BinderLog]
        public void SetValue(string value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToString());
                
        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
                
        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
        
        [BinderLog]
        public void SetValue<T>(T value) =>
            SetValue(value.ToString());
    }
}