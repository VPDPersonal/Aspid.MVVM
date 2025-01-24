using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Color")]
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