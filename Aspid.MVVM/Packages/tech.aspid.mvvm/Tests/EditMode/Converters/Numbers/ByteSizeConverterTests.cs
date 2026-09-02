using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ByteSizeConverter"/> — the binary and decimal unit ladders and the
    /// whole-byte tier.
    /// </summary>
    [TestFixture]
    public sealed class ByteSizeConverterTests
    {
        [TestCase(500L, true, "500 B")]
        [TestCase(1024L, true, "1.0 KiB")]
        [TestCase(1536L, true, "1.5 KiB")]
        [TestCase(1_048_576L, true, "1.0 MiB")]
        public void Convert_BinaryUnits_UsesPowersOf1024(long value, bool binary, string expected) =>
            Assert.AreEqual(expected, new ByteSizeConverter(binary).Convert(value));

        [TestCase(500L, "500 B")]
        [TestCase(1000L, "1.0 KB")]
        [TestCase(1_000_000L, "1.0 MB")]
        public void Convert_DecimalUnits_UsesPowersOf1000(long value, string expected) =>
            Assert.AreEqual(expected, new ByteSizeConverter(binaryUnits: false).Convert(value));

        // The whole-byte tier has no decimal to show, even when decimals are requested.
        [Test]
        public void Convert_WholeBytes_HaveNoDecimals() =>
            Assert.AreEqual("500 B", new ByteSizeConverter(true, decimals: 3).Convert(500L));

        [Test]
        public void Convert_NegativeValue_KeepsTheSignInFront() =>
            Assert.AreEqual("-1.0 KiB", new ByteSizeConverter(true).Convert(-1024L));

        // long.MinValue has no positive counterpart of its own width, so it is negated as a double.
        [Test]
        public void Convert_LongMinValue_DoesNotThrow() =>
            Assert.DoesNotThrow(() => new ByteSizeConverter(true).Convert(long.MinValue));

        // The decimals decide the unit as much as the count does: 1 048 530 bytes is just under a
        // mebibyte, but written with one decimal it reads as one, and "1024.0 KiB" is not a size
        // anyone writes.
        [Test]
        public void Convert_RoundingUpToTheNextUnit_MovesToTheUnitAbove() =>
            Assert.AreEqual("1.0 MiB", new ByteSizeConverter(true).Convert(1_048_530L));

        // With enough decimals the same count stays where it is: nothing rounds up.
        [Test]
        public void Convert_RoundingThatStaysBelowTheUnit_KeepsIt() =>
            Assert.AreEqual("1023.955 KiB", new ByteSizeConverter(true, decimals: 3).Convert(1_048_530L));

        [Test]
        public void Convert_BeyondTheLargestUnit_StaysOnIt() =>
            Assert.AreEqual("1024.0 TiB", new ByteSizeConverter(true).Convert(1L << 50));

        // The int, float and double overloads are explicit, so they are reached through the interface
        // rather than the class.
        [Test]
        public void Convert_IntInput_ScalesLikeTheLong() =>
            Assert.AreEqual(
                "1.0 KiB",
                ((IConverter<int, string>)new ByteSizeConverter(true)).Convert(1024));

        // Bytes are whole, so a fractional count loses its fraction instead of rounding up.
        [Test]
        public void Convert_FractionalInput_TruncatesTowardZero() =>
            Assert.AreEqual(
                "1.5 KiB",
                ((IConverter<double, string>)new ByteSizeConverter(true)).Convert(1536.9d));
    }
}
