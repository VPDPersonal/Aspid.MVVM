using System;
using UnityEngine;
using Aspid.FastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that maps a bound enum ViewModel value to a concrete typed value
    /// using a configurable <see cref="EnumValues{T}"/> lookup table.
    /// </summary>
    /// <typeparam name="T">The type of value resolved from the enum lookup table.</typeparam>
    public abstract partial class EnumMonoBinder<T> : MonoBinder, IBinder<Enum>
    {
        [Tooltip("Lookup table mapping each enum value to the resolved target value.")]
        [SerializeField] private EnumValues<T> _enumValues;

        /// <summary>
        /// Resolves <paramref name="value"/> to a <typeparamref name="T"/> via the lookup table and forwards it to <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The bound enum value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));

        /// <summary>
        /// Applies the resolved <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The resolved value to apply.</param>
        protected abstract void SetValue(T value);
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that resolves the bound <see cref="System.Enum"/>
    /// to a <typeparamref name="TValue"/> via <see cref="EnumValues{T}"/> and applies it to the component.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> whose property is set.</typeparam>
    /// <typeparam name="TValue">The type of value resolved from the enum lookup table.</typeparam>
    public abstract partial class EnumMonoBinder<TComponent, TValue> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [Tooltip("Lookup table mapping each enum value to the resolved target value.")]
        [SerializeField] private EnumValues<TValue> _enumValues;

        /// <summary>
        /// Resolves <paramref name="value"/> to a <typeparamref name="TValue"/> via the lookup table and forwards it to <see cref="SetValue(TValue)"/>.
        /// </summary>
        /// <param name="value">The bound enum value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));

        /// <summary>
        /// Applies the resolved <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The resolved value to apply.</param>
        protected abstract void SetValue(TValue value);
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that resolves the bound <see cref="System.Enum"/>
    /// to a <typeparamref name="TValue"/> via <see cref="EnumValues{T}"/>,
    /// optionally converts it via <typeparamref name="TConverter"/>, and applies it to the component.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> whose property is set.</typeparam>
    /// <typeparam name="TValue">The type of value resolved from the enum lookup table.</typeparam>
    /// <typeparam name="TConverter">The converter type used to transform the resolved value before applying it.</typeparam>
    public abstract partial class EnumMonoBinder<TComponent, TValue, TConverter> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
        where TConverter : IConverter<TValue, TValue>
    {
        [Tooltip("Lookup table mapping each enum value to the resolved target value.")]
        [SerializeField] private EnumValues<TValue> _enumValues;

        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the resolved value before it is set.")]
        [SerializeReference] private TConverter _converter;

        /// <summary>
        /// Resolves <paramref name="value"/> to a <typeparamref name="TValue"/> via the lookup table,
        /// optionally converts it via the serialized converter, and forwards it to <see cref="SetValue(TValue)"/>.
        /// </summary>
        /// <param name="value">The bound enum value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value)
        {
            var enumValue = _converter is null
                ? _enumValues.GetValue(value)
                : _converter.Convert(_enumValues.GetValue(value));

            SetValue(enumValue);
        }

        /// <summary>
        /// Applies the resolved and optionally converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(TValue value);
    }
}