using System;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="SequenceConverters{T}"/> — the only composition primitive the
    /// StarterKit ships, and the one an Inspector chain is built from.
    /// </summary>
    /// <remarks>
    /// The empty-slot and unconstructed cases are the ones that matter: the Inspector's
    /// <c>&lt;None&gt;</c> entry is a valid selection, and the type picker builds instances without
    /// running a constructor. Both are marked <c>[Ignore]</c> here and asserted for real once the
    /// guards land.
    /// </remarks>
    [TestFixture]
    internal sealed class SequenceConvertersTests
    {
        [Test]
        public void Convert_AppliesConvertersInDeclarationOrder() =>
            Assert.AreEqual(8, new SequenceConverters<int>(new Add(1), new Multiply(2)).Convert(3));

        [Test]
        public void Convert_OrderMatters() =>
            Assert.AreEqual(7, new SequenceConverters<int>(new Multiply(2), new Add(1)).Convert(3));

        [Test]
        public void Convert_EmptyChain_ReturnsInputUnchanged() =>
            Assert.AreEqual(3, new SequenceConverters<int>().Convert(3));

        [Test]
        public void Convert_SingleConverter_BehavesLikeThatConverter() =>
            Assert.AreEqual(4, new SequenceConverters<int>(new Add(1)).Convert(3));

        // An empty picker slot serialises as a null element.
        [Test]
        public void Convert_NullElement_IsSkipped() =>
            Assert.AreEqual(4, new SequenceConverters<int>(new Add(1), null).Convert(3));

        [Test]
        public void Convert_NullArray_ReturnsInputUnchanged() =>
            Assert.AreEqual(3, new SequenceConverters<int>(null).Convert(3));

        // The type picker constructs through Activator, so a parameterless constructor is required.
        [Test]
        public void ParameterlessConstructor_ProducesAUsableConverter()
        {
            var converter = (SequenceConverters<int>)Activator.CreateInstance(typeof(SequenceConverters<int>));
            Assert.AreEqual(3, converter.Convert(3));
        }

        private sealed class Add : IConverter<int, int>
        {
            private readonly int _amount;

            public Add(int amount) => _amount = amount;

            public int Convert(int value) => value + _amount;
        }

        private sealed class Multiply : IConverter<int, int>
        {
            private readonly int _factor;

            public Multiply(int factor) => _factor = factor;

            public int Convert(int value) => value * _factor;
        }
    }
}
