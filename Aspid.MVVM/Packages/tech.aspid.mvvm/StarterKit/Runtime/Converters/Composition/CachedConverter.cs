using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Remembers the last conversion and reuses it while the input is unchanged.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// Binders push on every notification, not on every change, so an allocating converter allocates
    /// once per push even while the value stands still.
    /// <para>
    /// Only wrap a pure converter: one that also reads outside its input — a scene component's
    /// current position, for instance — keeps returning the value it had when the input last changed.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class CachedConverter<TFrom, TTo> : IConverter<TFrom, TTo>
    {
        [Tooltip("The converter to memoize. When empty, the default value is returned.")]
        [SerializeReference] private IConverter<TFrom, TTo>? _inner;

        [NonSerialized] private bool _hasCache;
        [NonSerialized] private TFrom? _lastInput;
        [NonSerialized] private TTo _lastOutput = default!;

        public CachedConverter() { }

        /// <param name="inner">The converter to memoize.</param>
        public CachedConverter(IConverter<TFrom, TTo>? inner)
        {
            _inner = inner;
        }

        /// <summary>
        /// Converts the specified value, reusing the previous result when the input is unchanged.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public TTo Convert(TFrom value)
        {
            if (_inner is null) return default!;
            if (_hasCache && EqualityComparer<TFrom>.Default.Equals(_lastInput!, value)) return _lastOutput;

            _lastInput = value;
            _lastOutput = _inner.Convert(value);
            _hasCache = true;

            return _lastOutput;
        }
    }
}
