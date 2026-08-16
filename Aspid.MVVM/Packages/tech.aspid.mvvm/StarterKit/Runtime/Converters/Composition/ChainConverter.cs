using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies two converters in sequence, converting through an intermediate type.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TMid">The intermediate type the first converter produces.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// Use <see cref="SequenceConverters{T}"/> instead when every converter in the chain shares one
    /// type. Both links are required here: the types on either side need not match, so a missing link
    /// leaves nothing meaningful to return.
    /// </remarks>
    [Serializable]
    public sealed class ChainConverter<TFrom, TMid, TTo> : IConverter<TFrom, TTo>
    {
        [SerializeReference] private IConverter<TFrom, TMid>? _first;

        [SerializeReference] private IConverter<TMid, TTo>? _second;

        [NonSerialized] private bool _loggedIncomplete;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainConverter{TFrom, TMid, TTo}"/> class with no links.
        /// </summary>
        public ChainConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainConverter{TFrom, TMid, TTo}"/> class.
        /// </summary>
        /// <param name="first">The converter applied to the input value.</param>
        /// <param name="second">The converter applied to the result of <paramref name="first"/>.</param>
        public ChainConverter(IConverter<TFrom, TMid> first, IConverter<TMid, TTo> second)
        {
            _first = first;
            _second = second;
        }

        /// <summary>
        /// Converts the specified value through both links.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The result of the second converter, or the default of <typeparamref name="TTo"/> when
        /// either link is missing.
        /// </returns>
        public TTo Convert(TFrom value)
        {
            if (_first is null || _second is null)
            {
                LogIncomplete();
                return default!;
            }

            return _second.Convert(_first.Convert(value));
        }

        private void LogIncomplete()
        {
            if (_loggedIncomplete) return;
            _loggedIncomplete = true;

            Debug.LogError(
                $"{nameof(ChainConverter<TFrom, TMid, TTo>)}: both links are required, and one is missing. "
                + "Returning the default value.");
        }
    }
}
