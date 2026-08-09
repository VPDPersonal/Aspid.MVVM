#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
// ReSharper disable PossibleNullReferenceException
// ReSharper disable NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
namespace Aspid.FastTools.Enums
{
    /// <summary>
    /// Allocation-free enumerator over the resolved entries of an <see cref="EnumValues{TValue}"/>
    /// (<typeparamref name="TKey"/> = <see cref="Enum"/>) or an <see cref="EnumValues{TEnum,TValue}"/>
    /// (<typeparamref name="TKey"/> = the enum type). Boxed only when consumed through the
    /// <see cref="IEnumerable{T}"/> interface (e.g. LINQ).
    /// </summary>
    /// <typeparam name="TKey">The key type the entries are yielded as.</typeparam>
    /// <typeparam name="TValue">The type of the value associated with each enum member.</typeparam>
    public struct EnumValuesEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private int _index;
        private readonly EnumValue<TValue>[] _values;

        readonly object IEnumerator.Current => Current;

        public KeyValuePair<TKey, TValue> Current { get; private set; }

        internal EnumValuesEnumerator(EnumValue<TValue>[] values)
        {
            _index = 0;
            _values = values;
            Current = default;
        }

        public bool MoveNext()
        {
            while (_index < _values.Length)
            {
                var value = _values[_index++];
                if (!value.IsResolved) continue;

                // For a value-type TKey this unboxes the stored key, copying it out of the
                // existing box; for TKey = Enum it is a plain reference cast — no allocation.
                Current = new KeyValuePair<TKey, TValue>((TKey)(object)value.Key, value.Value);
                return true;
            }

            return false;
        }

        void IEnumerator.Reset()
        {
            Current = default;
            _index = 0;
        }

        readonly void IDisposable.Dispose() { }
    }
}
