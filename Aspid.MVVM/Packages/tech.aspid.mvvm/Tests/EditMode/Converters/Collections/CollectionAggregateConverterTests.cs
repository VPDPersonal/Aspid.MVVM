using System;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionAggregateConverter"/> — each <see cref="Aggregate"/>
    /// operation across its numeric widths, the empty fallback, and the int-saturation on overflow.
    /// </summary>
    [TestFixture]
    public sealed class CollectionAggregateConverterTests
    {
        [TestCase(Aggregate.Sum, 6f)]
        [TestCase(Aggregate.Average, 2f)]
        [TestCase(Aggregate.Min, 1f)]
        [TestCase(Aggregate.Max, 3f)]
        public void Aggregate_Reduces(Aggregate operation, float expected) =>
            Assert.AreEqual(expected, Floats(operation).Convert(new[] { 1f, 2f, 3f }), 1e-5f);

        [Test]
        public void Aggregate_EmptyUsesTheEmptyResult() =>
            Assert.AreEqual(-1f, Floats(Aggregate.Min, -1f).Convert(Array.Empty<float>()), 1e-5f);

        [Test]
        public void Aggregate_NullUsesTheEmptyResult() =>
            Assert.AreEqual(-1f, Floats(Aggregate.Min, -1f).Convert(null), 1e-5f);

        [Test]
        public void Aggregate_ReducesLongs() =>
            Assert.AreEqual(6L, Longs(Aggregate.Sum).Convert(new[] { 1L, 2L, 3L }));

        [Test]
        public void Aggregate_ReducesDoubles() =>
            Assert.AreEqual(6d, Doubles(Aggregate.Sum).Convert(new[] { 1d, 2d, 3d }), 1e-12);

        // The reason the cross-type overloads are there: an integer collection averages to a
        // fraction, which only a float or double binding can carry.
        [Test]
        public void Aggregate_AverageOfIntsKeepsTheFraction() =>
            Assert.AreEqual(2.5f, IntsToFloat(Aggregate.Average).Convert(new[] { 1, 2, 3, 4 }), 1e-5f);

        [Test]
        public void Aggregate_AverageOfIntsTruncatesWhenBoundToInt() =>
            Assert.AreEqual(2, IntsToInt(Aggregate.Average).Convert(new[] { 1, 2, 3, 4 }));

        // The sum is carried in double, so a total past int range narrows by saturating rather than
        // wrapping around.
        [Test]
        public void Aggregate_SumBeyondIntRangeSaturates() =>
            Assert.AreEqual(int.MaxValue, DoublesToInt(Aggregate.Sum).Convert(new[] { 1e18d, 1e18d }));

        [Test]
        public void Aggregate_UndeclaredOperationIsReported()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a declared Aggregate"));

            Assert.AreEqual(-1f, Floats((Aggregate)99, -1f).Convert(new[] { 1f, 2f, 3f }), 1e-5f);
        }

        // All sixteen of the aggregate converter's interfaces are implemented explicitly, so every
        // call above goes through the interface the binding would use.
        private static IConverter<IEnumerable<float>, float> Floats(Aggregate operation, double emptyResult = 0d) =>
            new CollectionAggregateConverter(operation, emptyResult);

        private static IConverter<IEnumerable<long>, long> Longs(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<double>, double> Doubles(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<int>, float> IntsToFloat(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<int>, int> IntsToInt(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<double>, int> DoublesToInt(Aggregate operation) =>
            new CollectionAggregateConverter(operation);
    }
}
