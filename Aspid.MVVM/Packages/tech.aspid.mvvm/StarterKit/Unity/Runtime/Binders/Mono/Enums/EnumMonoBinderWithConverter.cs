using System;
using UnityEngine;
using Aspid.FastTools.Enums;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that resolves the bound <see cref="System.Enum"/>
    /// to a <typeparamref name="TValue"/> via <see cref="EnumValues{T}"/>,
    /// optionally converts it via <see cref="IConverter{TFrom, TTo}"/>, and applies it to the component.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> whose property is set.</typeparam>
    /// <typeparam name="TValue">The type of value resolved from the enum lookup table.</typeparam>
    public abstract partial class EnumMonoBinderWithConverter<TComponent, TValue> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [Tooltip("Lookup table mapping each enum value to the resolved target value.")]
        [SerializeField] private EnumValues<TValue> _enumValues;

        [Tooltip("Optional converter applied to the resolved value before it is set.")]
        [SerializeReference] private IConverter<TValue, TValue> _converter;

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
