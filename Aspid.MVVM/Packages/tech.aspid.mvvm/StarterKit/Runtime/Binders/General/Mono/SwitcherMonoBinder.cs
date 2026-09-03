using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that applies one of two preset values depending on a bound <see langword="bool"/>,
    /// passing the chosen value through an optional converter first.
    /// </summary>
    /// <typeparam name="T">The type of the values switched between.</typeparam>
    public abstract partial class SwitcherMonoBinder<T> : MonoBinder, IBinder<bool>
    {
        [Tooltip("Value applied when the bound bool is true.")]
        [SerializeField] private T _trueValue;

        [Tooltip("Value applied when the bound bool is false.")]
        [SerializeField] private T _falseValue;

        [Tooltip("Optional converter applied to the chosen value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<T, T> _converter;

        /// <summary>
        /// Chooses the true or false value, converts it via <see cref="GetConvertedValue"/> and forwards it to <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the chosen, converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(T value);

        /// <summary>
        /// Converts <paramref name="value"/> with the serialized converter, or returns it unchanged when none is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T GetConvertedValue(T value) => _converter is null
            ? value
            : _converter.Convert(value);
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that applies one of two preset values depending on a bound <see langword="bool"/>,
    /// passing the chosen value through an optional converter first.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> whose property is switched.</typeparam>
    /// <typeparam name="T">The type of the values switched between.</typeparam>
    public abstract partial class SwitcherMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<bool>
        where TComponent : Component
    {
        [Tooltip("Value applied when the bound bool is true.")]
        [SerializeField] private T _trueValue;

        [Tooltip("Value applied when the bound bool is false.")]
        [SerializeField] private T _falseValue;

        [Tooltip("Optional converter applied to the chosen value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<T, T> _converter;

        /// <summary>
        /// Chooses the true or false value, converts it via <see cref="GetConvertedValue"/> and forwards it to <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the chosen, converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(T value);

        /// <summary>
        /// Converts <paramref name="value"/> with the serialized converter, or returns it unchanged when none is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T GetConvertedValue(T value) => _converter is null
            ? value
            : _converter.Convert(value);
    }
}
