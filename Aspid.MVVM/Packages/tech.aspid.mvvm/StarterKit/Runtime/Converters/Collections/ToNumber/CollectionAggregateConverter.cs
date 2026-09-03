#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reduces a collection of numbers to one.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>: int and long results truncate and saturate, long values past 2^53 lose precision.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Number",
        Name = "Aggregate",
        Tooltip = "Reduces a collection of numbers to one")]
    public sealed class CollectionAggregateConverter :
        IConverter<IEnumerable<int>?, int>, IConverter<IEnumerable<int>?, long>,
        IConverter<IEnumerable<int>?, float>, IConverter<IEnumerable<int>?, double>,
        IConverter<IEnumerable<long>?, int>, IConverter<IEnumerable<long>?, long>,
        IConverter<IEnumerable<long>?, float>, IConverter<IEnumerable<long>?, double>,
        IConverter<IEnumerable<float>?, int>, IConverter<IEnumerable<float>?, long>,
        IConverter<IEnumerable<float>?, float>, IConverter<IEnumerable<float>?, double>,
        IConverter<IEnumerable<double>?, int>, IConverter<IEnumerable<double>?, long>,
        IConverter<IEnumerable<double>?, float>, IConverter<IEnumerable<double>?, double>
    {
        [Tooltip("What to compute.")]
        [SerializeField] private AggregateOperation _operation = AggregateOperation.Sum;

        [Tooltip("Returned for an empty collection.")]
        [SerializeField] private double _emptyResult;

        /// <remarks>Default: computing a sum.</remarks>
        public CollectionAggregateConverter() { }

        /// <param name="operation">What to compute.</param>
        /// <param name="emptyResult">Returned for an empty collection.</param>
        public CollectionAggregateConverter(
            AggregateOperation operation,
            double emptyResult = 0d)
        {
            _operation = operation;
            _emptyResult = emptyResult;
        }

        /// <summary>
        /// Reduces the specified collection.
        /// </summary>
        /// <param name="value">The numbers to reduce.</param>
        /// <returns>
        /// The result, always in <see cref="double"/>, or the empty result when there is nothing to
        /// reduce or the operation is not a declared <see cref="AggregateOperation"/>.
        /// </returns>
        public double Reduce(IEnumerable<double>? value)
        {
            if (value is null) return _emptyResult;

            var accumulator = Accumulator.Empty;
            foreach (var item in value)
                accumulator.Add(item);

            return Result(accumulator);
        }

        /// <inheritdoc cref="Reduce(IEnumerable{double})"/>
        public double Reduce(IEnumerable<int>? value)
        {
            if (value is null) return _emptyResult;

            var accumulator = Accumulator.Empty;
            foreach (var item in value)
                accumulator.Add(item);

            return Result(accumulator);
        }

        /// <inheritdoc cref="Reduce(IEnumerable{double})"/>
        public double Reduce(IEnumerable<long>? value)
        {
            if (value is null) return _emptyResult;

            var accumulator = Accumulator.Empty;
            foreach (var item in value)
                accumulator.Add(item);

            return Result(accumulator);
        }

        /// <inheritdoc cref="Reduce(IEnumerable{double})"/>
        public double Reduce(IEnumerable<float>? value)
        {
            if (value is null) return _emptyResult;

            var accumulator = Accumulator.Empty;
            foreach (var item in value)
                accumulator.Add(item);

            return Result(accumulator);
        }

        int IConverter<IEnumerable<int>?, int>.Convert(IEnumerable<int>? value) =>
            NumericSaturation.ToInt(Reduce(value));

        int IConverter<IEnumerable<long>?, int>.Convert(IEnumerable<long>? value) =>
            NumericSaturation.ToInt(Reduce(value));

        int IConverter<IEnumerable<float>?, int>.Convert(IEnumerable<float>? value) =>
            NumericSaturation.ToInt(Reduce(value));

        int IConverter<IEnumerable<double>?, int>.Convert(IEnumerable<double>? value) =>
            NumericSaturation.ToInt(Reduce(value));

        long IConverter<IEnumerable<int>?, long>.Convert(IEnumerable<int>? value) =>
            NumericSaturation.ToLong(Reduce(value));

        long IConverter<IEnumerable<long>?, long>.Convert(IEnumerable<long>? value) =>
            NumericSaturation.ToLong(Reduce(value));

        long IConverter<IEnumerable<float>?, long>.Convert(IEnumerable<float>? value) =>
            NumericSaturation.ToLong(Reduce(value));

        long IConverter<IEnumerable<double>?, long>.Convert(IEnumerable<double>? value) =>
            NumericSaturation.ToLong(Reduce(value));

        float IConverter<IEnumerable<int>?, float>.Convert(IEnumerable<int>? value) =>
            (float)Reduce(value);

        float IConverter<IEnumerable<long>?, float>.Convert(IEnumerable<long>? value) =>
            (float)Reduce(value);

        float IConverter<IEnumerable<float>?, float>.Convert(IEnumerable<float>? value) =>
            (float)Reduce(value);

        float IConverter<IEnumerable<double>?, float>.Convert(IEnumerable<double>? value) =>
            (float)Reduce(value);

        double IConverter<IEnumerable<int>?, double>.Convert(IEnumerable<int>? value) =>
            Reduce(value);

        double IConverter<IEnumerable<long>?, double>.Convert(IEnumerable<long>? value) =>
            Reduce(value);

        double IConverter<IEnumerable<float>?, double>.Convert(IEnumerable<float>? value) =>
            Reduce(value);

        double IConverter<IEnumerable<double>?, double>.Convert(IEnumerable<double>? value) =>
            Reduce(value);

        private double Result(in Accumulator accumulator) => accumulator.Count is 0
            ? _emptyResult
            : _operation switch
            {
                AggregateOperation.Sum => accumulator.Sum,
                AggregateOperation.Average => accumulator.Sum / accumulator.Count,
                AggregateOperation.Min => accumulator.Min,
                AggregateOperation.Max => accumulator.Max,
                _ => Undeclared()
            };

        private double Undeclared()
        {
            this.LogError(
                problem: $"the operation {_operation.Describe()} is not a declared {nameof(AggregateOperation)}",
                consequence: "Returning the authored empty result.");

            return _emptyResult;
        }

        private struct Accumulator
        {
            public int Count { get; private set; }

            public double Sum { get; private set; }

            public double Min { get; private set; }

            public double Max { get; private set; }

            public static Accumulator Empty => new()
            {
                Min = double.PositiveInfinity,
                Max = double.NegativeInfinity
            };

            public void Add(double item)
            {
                Count++;
                Sum += item;

                if (item < Min) Min = item;
                if (item > Max) Max = item;
            }
        }
    }
}
