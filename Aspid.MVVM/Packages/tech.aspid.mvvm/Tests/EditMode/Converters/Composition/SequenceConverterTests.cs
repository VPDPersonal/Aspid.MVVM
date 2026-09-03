using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="SequenceConverter{T}"/> — the composition primitive an Inspector chain
    /// is built from.
    /// </summary>
    /// <remarks>
    /// The empty-slot and unconstructed cases are the ones that matter: the Inspector's
    /// <c>&lt;None&gt;</c> entry is a valid selection, and the type picker builds instances without
    /// running a constructor.
    /// </remarks>
    [TestFixture]
    public sealed class SequenceConverterTests
    {
        [Test]
        public void Convert_AppliesConvertersInDeclarationOrder() =>
            Assert.AreEqual(8, new SequenceConverter<int>(new AddConverter(1), new Multiply(2)).Convert(3));

        [Test]
        public void Convert_OrderMatters() =>
            Assert.AreEqual(7, new SequenceConverter<int>(new Multiply(2), new AddConverter(1)).Convert(3));

        [Test]
        public void Convert_EmptyChain_ReturnsInputUnchanged() =>
            Assert.AreEqual(3, new SequenceConverter<int>().Convert(3));

        [Test]
        public void Convert_SingleConverter_BehavesLikeThatConverter() =>
            Assert.AreEqual(4, new SequenceConverter<int>(new AddConverter(1)).Convert(3));

        // An empty picker slot serializes as a null element.
        [Test]
        public void Convert_NullElement_IsSkipped() =>
            Assert.AreEqual(4, new SequenceConverter<int>(new AddConverter(1), null).Convert(3));

        [Test]
        public void Convert_NullArray_ReturnsInputUnchanged() =>
            Assert.AreEqual(3, new SequenceConverter<int>((IConverter<int, int>[])null).Convert(3));

        // The type picker constructs through Activator, so a parameterless constructor is required.
        [Test]
        public void ParameterlessConstructor_ProducesAUsableConverter()
        {
            var converter = (SequenceConverter<int>)Activator.CreateInstance(typeof(SequenceConverter<int>));
            Assert.AreEqual(3, converter.Convert(3));
        }

        [Test]
        public void ConvertBack_UndoesEveryLinkInReverseOrder()
        {
            var sequence = new SequenceConverter<double>(
                new ArithmeticNumberConverter(NumberOperation.Add, 3),
                new ArithmeticNumberConverter(NumberOperation.Multiply, 2));

            Assert.AreEqual(16d, sequence.Convert(5d), delta: 1e-12);
            Assert.AreEqual(5d, sequence.ConvertBack(16d), delta: 1e-12);
        }

        [Test]
        public void ConvertBack_EmptyChain_RoundTrips() =>
            Assert.AreEqual(5d, new SequenceConverter<double>().ConvertBack(5d), delta: 1e-12);

        // Undoing part of a chain would leave the value in neither space, so a single one-way link
        // makes the whole sequence one-way — and says so rather than undoing the rest silently.
        [Test]
        public void ConvertBack_WithAOneWayLink_ReturnsTheValueUnchangedAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("OneWayDouble converts one way only"));

            var sequence = new SequenceConverter<double>(
                new ArithmeticNumberConverter(NumberOperation.Add, 3),
                new OneWayDouble());

            Assert.AreEqual(16d, sequence.ConvertBack(16d), delta: 1e-12);
        }

        [Test]
        public void Convert_NullLinksAreSkippedInBothDirections()
        {
            var sequence = new SequenceConverter<double>(
                new ArithmeticNumberConverter(NumberOperation.Add, 3),
                null);

            Assert.AreEqual(8d, sequence.Convert(5d), delta: 1e-12);
            Assert.AreEqual(5d, sequence.ConvertBack(8d), delta: 1e-12);
        }

        private sealed class OneWayDouble : IConverter<double, double>
        {
            public double Convert(double value) => value;
        }

        private sealed class Multiply : IConverter<int, int>
        {
            private readonly int _factor;

            public Multiply(int factor) => _factor = factor;

            public int Convert(int value) => value * _factor;
        }
    }
}
