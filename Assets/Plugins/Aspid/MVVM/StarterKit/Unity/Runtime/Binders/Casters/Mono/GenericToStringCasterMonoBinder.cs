using System;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
#if UNITY_2023_1_OR_NEWER
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private IConverter<T, string> _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        [BinderLog]
        public void SetValue(T value)
        {
            if (_converter is null) throw new NullReferenceException(nameof(_converter));
            _casted.Invoke(_converter.Convert(value));
        }
    }
#else
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _casted;
        
        protected abstract IConverter<T, string> Converter { get; }
        
        [BinderLog]
        public void SetValue(T value)
        {
	        if (Converter is null) throw new NullReferenceException(nameof(Converter));
	        _casted.Invoke(Converter.Convert(value));
        }
    }
#endif
}