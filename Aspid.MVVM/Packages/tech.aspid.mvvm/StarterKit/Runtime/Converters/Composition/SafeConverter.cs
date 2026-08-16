using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Runs another converter and substitutes a fallback value if it throws.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// Dispatch to binders is a bare multicast: an exception raised inside a converter cuts the
    /// subscriber list and silently stops every binder queued behind it. Wrapping contains the damage
    /// to that converter.
    /// <para>
    /// It catches everything on purpose — a containment boundary the author opts into, not a filter
    /// for expected failures.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class SafeConverter<TFrom, TTo> : IConverter<TFrom, TTo>
    {
        [Tooltip("The converter to run. When empty, the fallback value is returned.")]
        [SerializeReference] private IConverter<TFrom, TTo>? _inner;

        [Tooltip("Returned when the wrapped converter throws or is empty.")]
        [SerializeField] private TTo _fallback = default!;

        [Tooltip("Report the first failure to the console.")]
        [SerializeField] private bool _logErrors = true;

        [NonSerialized] private bool _logged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeConverter{TFrom, TTo}"/> class with no wrapped converter.
        /// </summary>
        public SafeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeConverter{TFrom, TTo}"/> class.
        /// </summary>
        /// <param name="inner">The converter to run.</param>
        /// <param name="fallback">Returned when <paramref name="inner"/> throws or is <see langword="null"/>.</param>
        /// <param name="logErrors">If <see langword="true"/>, reports the first failure to the console.</param>
        public SafeConverter(IConverter<TFrom, TTo>? inner, TTo fallback = default!, bool logErrors = true)
        {
            _inner = inner;
            _fallback = fallback;
            _logErrors = logErrors;
        }

        /// <summary>
        /// Converts the specified value, substituting the fallback if the wrapped converter throws.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value, or the fallback.</returns>
        public TTo Convert(TFrom value)
        {
            if (_inner is null) return _fallback;

            try
            {
                return _inner.Convert(value);
            }
            catch (Exception exception)
            {
                LogFailure(exception);
                return _fallback;
            }
        }

        private void LogFailure(Exception exception)
        {
            if (!_logErrors || _logged) return;
            _logged = true;

            Debug.LogError($"{_inner!.GetType().Name} threw ({exception.Message}). Using the fallback value.");
        }
    }
}
