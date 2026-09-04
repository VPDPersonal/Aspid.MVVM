using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that forwards one of two configured values depending on the bound
    /// <see langword="bool"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the values chosen between.</typeparam>
    public abstract partial class ConditionalMonoBinder<TValue> : MonoBinder, IBinder<bool>
    {
        [Tooltip("Forwarded when the bound value is true.")]
        [SerializeField] private TValue _whenTrue;

        [Tooltip("Forwarded when the bound value is false.")]
        [SerializeField] private TValue _whenFalse;

        [Tooltip("Invoked with the chosen value.")]
        [SerializeField] private UnityEvent<TValue> _value;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(bool value) =>
            _value?.Invoke(value ? _whenTrue : _whenFalse);
    }
}
