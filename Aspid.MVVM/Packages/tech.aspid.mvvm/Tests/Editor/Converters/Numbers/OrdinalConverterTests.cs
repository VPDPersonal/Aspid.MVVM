using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="OrdinalConverter"/> — the last-digit suffix rule, its 11-13 exception,
    /// and the wider numeric overloads.
    /// </summary>
    [TestFixture]
    internal sealed class OrdinalConverterTests
    {
        [TestCase(1, "1st")]
        [TestCase(2, "2nd")]
        [TestCase(3, "3rd")]
        [TestCase(4, "4th")]
        [TestCase(21, "21st")]
        [TestCase(22, "22nd")]
        // The teens break the last-digit rule: 11, 12 and 13 all take "th".
        [TestCase(11, "11th")]
        [TestCase(12, "12th")]
        [TestCase(13, "13th")]
        [TestCase(111, "111th")]
        public void Convert_AppendsTheEnglishSuffix(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(value));

        [Test]
        public void Convert_Zero_TakesTheDefaultSuffix() =>
            Assert.AreEqual("0th", new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(0));

        // A negative number keeps its sign in front and takes the suffix of its magnitude.
        [Test]
        public void Convert_NegativeValue_KeepsTheSignAndTakesTheMagnitudesSuffix() =>
            Assert.AreEqual("-21st", new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(-21));

        // Only the last two digits are ever negated, so the one input with no positive counterpart
        // of its own width never needs one.
        [Test]
        public void Convert_IntMinValue_DoesNotThrow() =>
            Assert.DoesNotThrow(() => new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(int.MinValue));

        // The long overload reaches ranks an int cannot hold; the suffix is still decided by the last
        // two digits.
        [Test]
        public void Convert_Long_TakesTheSuffixOfItsLastTwoDigits() =>
            Assert.AreEqual(
                "1000000000001st",
                ((IConverter<long, string>)new OrdinalConverter(CultureInfoMode.InvariantCulture))
                    .Convert(1000000000001L));

        // long.MinValue has no positive counterpart of its own width either, and the widest input
        // saturates to exactly it — so only the remainder is ever negated.
        [Test]
        public void Convert_LongMinValue_Formats() =>
            Assert.AreEqual(
                "-9223372036854775808th",
                ((IConverter<long, string>)new OrdinalConverter(CultureInfoMode.InvariantCulture))
                    .Convert(long.MinValue));

        // An ordinal is a rank, so a fractional input is truncated toward zero rather than rounded:
        // 21.9 is still the 21st, not the 22nd.
        [TestCase(21.9d, "21st")]
        [TestCase(-21.9d, "-21st")]
        public void Convert_Double_TruncatesTowardZero(double value, string expected) =>
            Assert.AreEqual(
                expected,
                ((IConverter<double, string>)new OrdinalConverter(CultureInfoMode.InvariantCulture)).Convert(value));

        [Test]
        public void Convert_Float_TruncatesTowardZero() =>
            Assert.AreEqual(
                "2nd",
                ((IConverter<float, string>)new OrdinalConverter(CultureInfoMode.InvariantCulture)).Convert(2.9f));
    }
}
