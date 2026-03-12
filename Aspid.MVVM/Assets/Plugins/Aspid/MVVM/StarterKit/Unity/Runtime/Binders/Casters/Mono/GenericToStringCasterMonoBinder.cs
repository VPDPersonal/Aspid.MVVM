using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
#if UNITY_2023_1_OR_NEWER
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound value
    /// of type <typeparamref name="T"/> to a <see cref="string"/> using a configurable converter and forwards
    /// the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of value received from the ViewModel.</typeparam>
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Tooltip("The converter used to transform the bound value to a string.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private IConverter<T, string> _converter;
        
        [Tooltip("Invoked with the converted string value each time a new value arrives from the ViewModel.")]
        [SerializeField] private UnityEvent<string> _casted;
        
        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs an error via <c>UnityEngine.Debug.LogError</c> and returns
        /// without invoking the event.
        /// </remarks>
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
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound value
    /// of type <typeparamref name="T"/> to a <see cref="string"/> using a configurable converter and forwards
    /// the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of value received from the ViewModel.</typeparam>
    public abstract partial class GenericToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Tooltip("Invoked with the converted string value each time a new value arrives from the ViewModel.")]
        [SerializeField] private UnityEvent<string> _casted;
        
        /// <summary>
        /// Gets the converter used to transform the bound value to a <see cref="string"/>.
        /// </summary>
        protected abstract IConverter<T, string> Converter { get; }
        
        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs an error via <c>UnityEngine.Debug.LogError</c> and returns
        /// without invoking the event.
        /// </remarks>
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