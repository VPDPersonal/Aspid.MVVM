using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="RomanNumeralConverter"/> — the subtractive numerals and the 1..3999
    /// boundary.
    /// </summary>
    [TestFixture]
    internal sealed class RomanNumeralConverterTests
    {
        [TestCase(1, "I")]
        [TestCase(4, "IV")]
        [TestCase(9, "IX")]
        [TestCase(40, "XL")]
        [TestCase(90, "XC")]
        [TestCase(400, "CD")]
        [TestCase(900, "CM")]
        [TestCase(1994, "MCMXCIV")]
        [TestCase(3999, "MMMCMXCIX")]
        public void Convert_WritesTheSubtractiveNumeral(int value, string expected) =>
            Assert.AreEqual(expected, new RomanNumeralConverter().Convert(value));

        // Outside 1..3999 there is no numeral to write, so the number is shown in digits instead.
        [TestCase(0, "0")]
        [TestCase(4000, "4000")]
        [TestCase(-5, "-5")]
        public void Convert_OutsideTheRange_FallsBackToDigits(int value, string expected) =>
            Assert.AreEqual(expected, new RomanNumeralConverter().Convert(value));
    }
}
