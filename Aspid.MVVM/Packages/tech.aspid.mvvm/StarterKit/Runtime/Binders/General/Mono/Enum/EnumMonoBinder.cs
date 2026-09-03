using System;
using UnityEngine;
using Aspid.FastTools.Enums;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that resolves a bound <see cref="Enum"/> to a <typeparamref name="TValue"/>
    /// through an <see cref="EnumValues{TValue}"/> table, passing the result through an optional converter first.
    /// </summary>
    /// <typeparam name="TValue">The type of value resolved from the table.</typeparam>
    public abstract partial class EnumMonoBinder<TValue> : MonoBinder, IBinder<Enum>
    {
        [Tooltip("Value applied for each enum member.")]
        [SerializeField] private EnumValues<TValue> _enumValues;

        [Tooltip("Optional converter applied to the resolved value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<TValue, TValue> _converter;

        /// <summary>
        /// Resolves <paramref name="value"/> through the table, converts it and forwards it to <see cref="SetValue(TValue)"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value)
        {
            var resolved = _enumValues.GetValue(value);

            SetValue(_converter is null ? resolved : _converter.Convert(resolved));
        }

        /// <summary>
        /// Applies the resolved, converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(TValue value);
    }

    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that resolves a bound <see cref="Enum"/> to a <typeparamref name="TValue"/>
    /// through an <see cref="EnumValues{TValue}"/> table, passing the result through an optional converter first.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that receives the value.</typeparam>
    /// <typeparam name="TValue">The type of value resolved from the table.</typeparam>
    public abstract partial class EnumMonoBinder<TComponent, TValue> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [Tooltip("Value applied for each enum member.")]
        [SerializeField] private EnumValues<TValue> _enumValues;

        [Tooltip("Optional converter applied to the resolved value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<TValue, TValue> _converter;

        /// <summary>
        /// Resolves <paramref name="value"/> through the table, converts it and forwards it to <see cref="SetValue(TValue)"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value)
        {
            var resolved = _enumValues.GetValue(value);

            SetValue(_converter is null ? resolved : _converter.Convert(resolved));
        }

        /// <summary>
        /// Applies the resolved, converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(TValue value);
    }
}
