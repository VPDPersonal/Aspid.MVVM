using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that converts a bound <typeparamref name="TFrom"/> to <typeparamref name="TTo"/>
    /// with a serialized converter and forwards the result to a <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of value received from the ViewModel.</typeparam>
    /// <typeparam name="TTo">The type of value forwarded to the event.</typeparam>
    public abstract partial class CasterMonoBinder<TFrom, TTo> : MonoBinder, IBinder<TFrom>
    {
        [Tooltip("Converter from the bound value to the forwarded one.")]
        [SerializeReference] private IConverter<TFrom, TTo> _converter;

        [Tooltip("Invoked with the converted value.")]
        [SerializeField] private UnityEvent<TTo> _casted;

        /// <inheritdoc/>
        protected override void Reset()
        {
            base.Reset();
            _converter ??= CreateDefaultConverter();
        }

        private void OnValidate() =>
            _converter ??= CreateDefaultConverter();

        /// <summary>
        /// Converts <paramref name="value"/> and invokes the event. Logs an error and forwards nothing when no converter is set.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TFrom value)
        {
            if (_converter is null)
            {
                this.LogError(
                    problem: "no converter is assigned",
                    consequence: "The value is not forwarded.");

                return;
            }

            _casted?.Invoke(_converter.Convert(value));
        }

        /// <summary>
        /// Returns the converter assigned when the field is empty, or <see langword="null"/> to leave it empty.
        /// </summary>
        /// <returns>The default converter.</returns>
        protected virtual IConverter<TFrom, TTo> CreateDefaultConverter() => null;
    }
}
