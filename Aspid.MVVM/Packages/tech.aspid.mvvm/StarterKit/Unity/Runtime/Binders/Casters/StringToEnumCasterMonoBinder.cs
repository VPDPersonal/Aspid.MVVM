using System;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that
    /// converts a bound <see cref="string"/> to <typeparamref name="TEnum"/> with a configurable converter and forwards
    /// the result to a target <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// By default, uses <see cref="StringToEnumConverter{TEnum}"/>. Close the type over a concrete enum to make it addable as a component.
    /// </remarks>
    /// <typeparam name="TEnum">The enum type the string is converted into.</typeparam>
    public abstract partial class StringToEnumCasterMonoBinder<TEnum> : MonoBinder, IBinder<string>
        where TEnum : struct, Enum
    {
        [Tooltip("Converter from the bound string to the enum.")]
        [SerializeReference] private IConverter<string, TEnum> _converter = new StringToEnumConverter<TEnum>();

        [Tooltip("Invoked with the converted value.")]
        [SerializeField] private UnityEvent<TEnum> _casted;

        private void OnValidate() =>
            _converter ??= new StringToEnumConverter<TEnum>();

        /// <summary>
        /// Converts <paramref name="value"/> with the configured converter and invokes the target <see cref="UnityEvent{T}"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <remarks>
        /// With no converter assigned, logs an error and forwards nothing.
        /// </remarks>
        [BinderLog]
        public void SetValue(string value)
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
