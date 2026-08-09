using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reduces a collection of numbers to one.
    /// </summary>
    /// <remarks>
    /// A total or an average over an observable list, without a computed property that has to be
    /// invalidated whenever the list changes.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection Aggregate", Tooltip = "Reduces a collection of numbers to one")]
    public sealed class CollectionAggregateConverter : IConverter<IEnumerable<float>?, float>
    {
        [Tooltip("What to compute.")]
        [SerializeField] private Aggregate _operation = Aggregate.Sum;

        [Tooltip("Returned for an empty collection.")]
        [SerializeField] private float _emptyResult;

        /// <remarks>Default: computing a sum.</remarks>
        public CollectionAggregateConverter() { }

        /// <param name="operation">What to compute.</param>
        /// <param name="emptyResult">Returned for an empty collection.</param>
        public CollectionAggregateConverter(Aggregate operation, float emptyResult = 0f)
        {
            _operation = operation;
            _emptyResult = emptyResult;
        }

        /// <summary>
        /// Reduces the specified collection.
        /// </summary>
        /// <param name="value">The numbers to reduce.</param>
        /// <returns>The result, or the empty result when there is nothing to reduce.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public float Convert(IEnumerable<float>? value)
        {
            if (value is null) return _emptyResult;

            var count = 0;
            var sum = 0f;
            var min = float.PositiveInfinity;
            var max = float.NegativeInfinity;

            foreach (var item in value)
            {
                count++;
                sum += item;
                if (item < min) min = item;
                if (item > max) max = item;
            }

            if (count == 0) return _emptyResult;

            return _operation switch
            {
                Aggregate.Sum => sum,
                Aggregate.Average => sum / count,
                Aggregate.Min => min,
                Aggregate.Max => max,
                _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
            };
        }
    }
}
