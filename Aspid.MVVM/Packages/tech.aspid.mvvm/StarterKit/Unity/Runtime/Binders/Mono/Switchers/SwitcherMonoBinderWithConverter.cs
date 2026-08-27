using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that switches a component property
    /// between two serialized options based on a bound boolean,
    /// with optional value conversion via <see cref="IConverter{TFrom, TTo}"/> before applying.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> whose property is switched.</typeparam>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    public abstract partial class SwitcherMonoBinderWithConverter<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<bool>
        where TComponent : Component
    {
        [Tooltip("Value applied when the bound boolean is true.")]
        [SerializeField] private T _trueValue;

        [Tooltip("Value applied when the bound boolean is false.")]
        [SerializeField] private T _falseValue;

        [Tooltip("Optional converter applied to the selected value before it is set.")]
        [SerializeReference] private IConverter<T, T> _converter;

        /// <summary>
        /// Selects the true or false value based on <paramref name="value"/>, converts it via <see cref="GetConvertedValue"/>,
        /// and forwards it to <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The bound boolean value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the selected and converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(T value);

        /// <summary>
        /// Converts <paramref name="value"/> using the serialized converter, or returns it unchanged if no converter is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T GetConvertedValue(T value) =>
            _converter is null ? value : _converter.Convert(value);
    }
}
