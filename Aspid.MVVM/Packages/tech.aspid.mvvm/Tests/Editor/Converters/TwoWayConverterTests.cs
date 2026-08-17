using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="ITwoWayConverter{TFrom, TTo}"/> and the converters that implement it.
    /// </summary>
    /// <remarks>
    /// The contract each implementation signs is <c>ConvertBack(Convert(x)) == x</c>, so most of what
    /// follows is a round trip rather than a hand-computed expected value.
    /// </remarks>
    [TestFixture]
    internal sealed class TwoWayConverterTests
    {
        [TestCase(NumberOperation.Plus)]
        [TestCase(NumberOperation.Minus)]
        [TestCase(NumberOperation.Multiply)]
        [TestCase(NumberOperation.Division)]
        public void Arithmetic_RoundTripsDouble(NumberOperation operation)
        {
            var converter = TwoWay<double>(operation, coefficient: 4);

            Assert.AreEqual(0.75d, converter.ConvertBack(converter.Convert(0.75d)), delta: 1e-12);
        }

        // The audit's example: a x100 converter used to send 0.75 back as 7500, because the reverse
        // path applied the forward conversion a second time.
        [Test]
        public void Arithmetic_MultiplyUndoesRatherThanCompounds()
        {
            var converter = TwoWay<double>(NumberOperation.Multiply, coefficient: 100);

            Assert.AreEqual(75d, converter.Convert(0.75d), delta: 1e-12);
            Assert.AreEqual(0.75d, converter.ConvertBack(75d), delta: 1e-12);
        }

        [Test]
        public void Arithmetic_PlusUndoes() =>
            Assert.AreEqual(3d, TwoWay<double>(NumberOperation.Plus, 2).ConvertBack(5d), delta: 1e-12);

        [Test]
        public void Arithmetic_MinusUndoes() =>
            Assert.AreEqual(3d, TwoWay<double>(NumberOperation.Minus, 2).ConvertBack(1d), delta: 1e-12);

        [Test]
        public void Arithmetic_DivisionUndoes() =>
            Assert.AreEqual(6d, TwoWay<double>(NumberOperation.Division, 2).ConvertBack(3d), delta: 1e-12);

        [Test]
        public void Arithmetic_RoundTripsFloat()
        {
            var converter = TwoWay<float>(NumberOperation.Multiply, coefficient: 4);

            Assert.AreEqual(0.75f, converter.ConvertBack(converter.Convert(0.75f)), delta: 1e-6f);
        }

        [Test]
        public void Arithmetic_RoundTripsInt()
        {
            var converter = TwoWay<int>(NumberOperation.Plus, coefficient: 7);

            Assert.AreEqual(5, converter.ConvertBack(converter.Convert(5)));
        }

        [Test]
        public void Arithmetic_RoundTripsLong()
        {
            var converter = TwoWay<long>(NumberOperation.Minus, coefficient: 7);

            Assert.AreEqual(5L, converter.ConvertBack(converter.Convert(5L)));
        }

        [Test]
        public void Passthrough_RoundTrips() =>
            Assert.AreEqual(7, new PassthroughConverter<int>().ConvertBack(7));

        [Test]
        public void Sequence_UndoesEveryLinkInReverseOrder()
        {
            var sequence = new SequenceConverters<double>(
                new ArithmeticNumberConverter(NumberOperation.Plus, 3),
                new ArithmeticNumberConverter(NumberOperation.Multiply, 2));

            Assert.AreEqual(16d, sequence.Convert(5d), delta: 1e-12);
            Assert.AreEqual(5d, sequence.ConvertBack(16d), delta: 1e-12);
        }

        [Test]
        public void Sequence_EmptyChain_RoundTrips() =>
            Assert.AreEqual(5d, new SequenceConverters<double>().ConvertBack(5d), delta: 1e-12);

        // Undoing part of a chain would leave the value in neither space, so a single one-way link
        // makes the whole sequence one-way.
        [Test]
        public void Sequence_WithAOneWayLink_ReturnsTheValueUnchanged()
        {
            var sequence = new SequenceConverters<double>(
                new ArithmeticNumberConverter(NumberOperation.Plus, 3),
                new OneWayDouble());

            Assert.AreEqual(16d, sequence.ConvertBack(16d), delta: 1e-12);
        }

        [Test]
        public void Sequence_NullLinksAreSkippedInBothDirections()
        {
            var sequence = new SequenceConverters<double>(
                new ArithmeticNumberConverter(NumberOperation.Plus, 3),
                null);

            Assert.AreEqual(8d, sequence.Convert(5d), delta: 1e-12);
            Assert.AreEqual(5d, sequence.ConvertBack(8d), delta: 1e-12);
        }

        private static ITwoWayConverter<T, T> TwoWay<T>(NumberOperation operation, double coefficient) =>
            (ITwoWayConverter<T, T>)(object)new ArithmeticNumberConverter(operation, coefficient);

        private sealed class OneWayDouble : IConverter<double, double>
        {
            public double Convert(double value) => value;
        }
    }
}
