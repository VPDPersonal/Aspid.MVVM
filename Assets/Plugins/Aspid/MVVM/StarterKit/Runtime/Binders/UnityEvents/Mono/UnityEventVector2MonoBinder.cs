using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Vector2")]
    public sealed partial class UnityEventVector2MonoBinder : MonoBinder, IBinder<Vector2>
    {
        public event UnityAction<Vector2> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector2> _set;
        
        [BinderLog]
        public void SetValue(Vector2 value) 
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}