using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
#if UNITY_2023_1_OR_NEWER
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private IConverter<T, string> _converter;
        
        [SerializeField] private UnityEvent<string> _casted;
        
        [BinderLog]
        public void SetValue(T value)
        {
            if (_converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(GenericToStringCasterMonoBinder<T>)}", context: this);
                return;
            }
            
            _casted.Invoke(_converter.Convert(value));
        }
    }
#else
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [SerializeField] private UnityEvent<string> _casted;
        
        protected abstract IConverter<T, string> Converter { get; }
        
        [BinderLog]
        public void SetValue(T value)
        {
            if (Converter is null)
            {
                Debug.LogError($"No converter assigned to {nameof(GenericToStringCasterMonoBinder<T>)}", context: this);
                return;
            }

	        _casted.Invoke(Converter.Convert(value));
        }
    }
#endif
}