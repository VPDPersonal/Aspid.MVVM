using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="PaddedNumberConverter"/> — the padding width, the custom pad character,
    /// the sign left outside the padding, the negative width that would throw, and the wider numeric
    /// overloads.
    /// </summary>
    [TestFixture]
    internal sealed class PaddedNumberConverterTests
    {
        [Test]
        public void Convert_PadsToTheWidth() =>
            Assert.AreEqual("007", new PaddedNumberConverter(3).Convert(7));

        [Test]
        public void Convert_LeavesAWideEnoughNumberAlone() =>
            Assert.AreEqual("1234", new PaddedNumberConverter(2).Convert(1234));

        [Test]
        public void Convert_CustomPadChar_IsUsedInsteadOfZero() =>
            Assert.AreEqual("__7", new PaddedNumberConverter(3, '_').Convert(7));

        // The sign sits outside the padding: -7 padded to three digits is "-07", not "0-7" or "-7 ".
        [Test]
        public void Convert_NegativeValue_KeepsTheSignOutsideThePadding() =>
            Assert.AreEqual("-07", new PaddedNumberConverter(2).Convert(-7));

        // PadLeft throws on a negative width, and a typed-in count reaches it straight from the
        // Inspector — the converter has to report it rather than take the binder down.
        [Test]
        public void Convert_NegativeDigits_ReportsAndPadsNothing()
        {
            LogAssert.Expect(LogType.Error, new Regex("PaddedNumberConverter.*digit count -1 is negative"));

            Assert.AreEqual("7", new PaddedNumberConverter(-1).Convert(7));
        }

        [Test]
        public void Convert_Long_PadsToTheWidth() =>
            Assert.AreEqual("007", ((IConverter<long, string>)new PaddedNumberConverter(3)).Convert(7L));

        // long.MinValue has no positive counterpart of its own width, and the widest input saturates
        // to exactly it — so the magnitude is taken unsigned rather than negated.
        [Test]
        public void Convert_LongMinValue_Formats() =>
            Assert.AreEqual(
                "-9223372036854775808",
                ((IConverter<long, string>)new PaddedNumberConverter(3)).Convert(long.MinValue));

        // The width counts digits, so a fractional input is truncated toward zero before it is padded.
        [TestCase(7.9d, "007")]
        [TestCase(-7.9d, "-007")]
        public void Convert_Double_TruncatesTowardZero(double value, string expected) =>
            Assert.AreEqual(expected, ((IConverter<double, string>)new PaddedNumberConverter(3)).Convert(value));

        [Test]
        public void Convert_Float_TruncatesTowardZero() =>
            Assert.AreEqual("007", ((IConverter<float, string>)new PaddedNumberConverter(3)).Convert(7.9f));
    }
}
