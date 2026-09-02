using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="OrdinalConverter"/> — the last-digit suffix rule, its 11-13 exception,
    /// and the wider numeric overloads.
    /// </summary>
    [TestFixture]
    public sealed class OrdinalConverterTests
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
        // The exception recurs at every hundred, not just the first: a "value < 20" guard would get
        // 1011-1013 wrong while looking correct on the cases above.
        [TestCase(1011, "1011th")]
        [TestCase(1012, "1012th")]
        [TestCase(1013, "1013th")]
        [TestCase(121, "121st")]
        [TestCase(122, "122nd")]
        [TestCase(123, "123rd")]
        public void Convert_AppendsTheEnglishSuffix(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(value));

        [Test]
        public void Convert_Zero_TakesTheDefaultSuffix() =>
            Assert.AreEqual("0th", new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(0));

        // A negative number keeps its sign in front and takes the suffix of its magnitude.
        [TestCase(-21, "-21st")]
        [TestCase(-11, "-11th")]
        public void Convert_NegativeValue_KeepsTheSignAndTakesTheMagnitudesSuffix(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(value));

        // Only the last two digits are ever negated, so the one input with no positive counterpart
        // of its own width never needs one.
        [Test]
        public void Convert_IntMinValue_DoesNotThrow() =>
            Assert.DoesNotThrow(() => new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(int.MinValue));

        [Test]
        public void Convert_IntMaxValue_Formats() =>
            Assert.AreEqual("2147483647th", new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(int.MaxValue));

        // The culture reaches only the negative sign: .NET writes ASCII digits for a positive number
        // whichever culture is picked, and the suffix stays English either way.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_TheCultureChangesNothingForAPositiveNumber() =>
            Assert.AreEqual("1234th", new OrdinalConverter(CultureInfoMode.CurrentCulture).Convert(1234));

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
