using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Color))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Color")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/UnityEvent/UnityEvent Binder Color")]
    public sealed partial class UnityEventColorMonoBinder : MonoBinder, IColorBinder
    {
        public event UnityAction<Color> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Color> _set;
        
        [BinderLog]
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}