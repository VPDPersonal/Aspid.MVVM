using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> implementing <see cref="IBinder{T}"/> that converts a bound value
    /// of type <typeparamref name="T"/> to a <see cref="string"/> using a configurable converter and forwards
    /// the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of value received from the ViewModel.</typeparam>
    public abstract partial class ToStringCasterMonoBinder<T> : MonoBinder, IBinder<T>
    {
        [Tooltip("The converter used to transform the bound value to a string.")]
        [SerializeReference] private IConverter<T, string> _converter;
        
        [Tooltip("Invoked with the converted string value.")]
        [SerializeField] private UnityEvent<string> _casted;
        
        /// <summary>
        /// Converts <paramref name="value"/> to a <see cref="string"/> using the configured converter
        /// and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// If no converter is assigned, logs a Unity error and returns without invoking the event.
        /// </remarks>
        [BinderLog]
        public void SetValue(T value)
        {
            if (_converter is null)
            {
                this.LogError("no converter is assigned", "The value is not forwarded.");
                return;
            }
            
            _casted?.Invoke(_converter.Convert(value));
        }
    }
}