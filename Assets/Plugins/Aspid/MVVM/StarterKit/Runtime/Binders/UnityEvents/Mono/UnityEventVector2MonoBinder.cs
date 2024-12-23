using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Vector2")]
    public sealed partial class UnityEventVector2MonoBinder : MonoBinder, IBinder<Vector2>
    {
        public event UnityAction<Vector2> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReference] 
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _converter;
#else
        private IConverterVector2 _converter;
#endif
        
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