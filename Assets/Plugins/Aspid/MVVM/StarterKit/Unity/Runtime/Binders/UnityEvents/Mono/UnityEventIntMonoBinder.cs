using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(int))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Int")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/UnityEvent/UnityEvent Binder - Int")]
    public sealed partial class UnityEventIntMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction<int> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<int> _set;

        [BinderLog]
        public void SetValue(int value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((int)value);

        [BinderLog]
        public void SetValue(float value) =>
            SetValue((int)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((int)value);
    }
}