using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Int")]
    public sealed partial class UnityEventIntMonoBinder : MonoBinder, IBinder<int>
    {
        public event UnityAction<int> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<int, int> _converter;
#else
        private IConverterInt _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<int> _set;

        [BinderLog]
        public void SetValue(int value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}