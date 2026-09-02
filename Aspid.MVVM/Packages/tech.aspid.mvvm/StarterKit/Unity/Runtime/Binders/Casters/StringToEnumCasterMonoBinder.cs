using System;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> that
    /// parses a bound <see cref="string"/> as <typeparamref name="TEnum"/> and forwards the result to a target
    /// <see cref="UnityEvent{T}"/>.
    /// </summary>
    /// <remarks>
    /// Names are matched case-insensitively, because a value that came from text rarely matches the C# casing, and a
    /// numeric string is refused — <see cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/> accepts any number,
    /// including one no member has.
    /// <para/>
    /// Logs failures by default, unlike the numeric casters.
    /// </remarks>
    /// <typeparam name="TEnum">The enum type the string is parsed into.</typeparam>
    public abstract partial class StringToEnumCasterMonoBinder<TEnum> : MonoBinder, IBinder<string>
        where TEnum : struct, Enum
    {
        [Tooltip("Invoked with the parsed value.")]
        [SerializeField] private UnityEvent<TEnum> _casted;

        [Tooltip("Value forwarded when the string names no member of the enum.")]
        [SerializeField] private TEnum _fallback;

        [Tooltip("Logs an error for every string that names no member.")]
        [SerializeField] private bool _logFailures = true;

        /// <summary>
        /// Parses <paramref name="value"/> as <typeparamref name="TEnum"/> and invokes the target
        /// <see cref="UnityEvent{T}"/> with the result, or with the fallback value when it names no member.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            if (EnumNameParse.TryName(value, out TEnum parsed))
            {
                _casted?.Invoke(parsed);
                return;
            }

            if (_logFailures)
                this.LogError(value.Expected($"a member of {typeof(TEnum).Name}"), $"Forwarding {_fallback} instead.");

            _casted?.Invoke(_fallback);
        }
    }
}
