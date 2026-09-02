using UnityEngine;
using NUnit.Framework;
using System.Globalization;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DecimalFormatConverter"/> — the standard .NET format strings, the
    /// culture, the general format, and the fallback for a format .NET refuses.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class DecimalFormatConverterTests
    {
        [Test]
        public void Convert_DefaultFormat_WritesTwoDecimalsWithGrouping() =>
            Assert.AreEqual("1,234.50", new DecimalFormatConverter("N2").Convert(1234.5m));

        // The default format also rounds: .678 becomes .68 rather than being cut to .67.
        [Test]
        public void Convert_DefaultFormat_RoundsToTwoDecimals() =>
            Assert.AreEqual("12,345.68", new DecimalFormatConverter().Convert(12345.678m));

        [Test]
        public void Convert_FFormat_OmitsGrouping() =>
            Assert.AreEqual("1234.50", new DecimalFormatConverter("F2").Convert(1234.5m));

        [Test]
        public void Convert_CurrencyFormat_AddsASymbolToTheAmount()
        {
            var text = new DecimalFormatConverter("C2", CultureInfoMode.InvariantCulture).Convert(12.5m);

            StringAssert.Contains("12.50", text);
            Assert.AreNotEqual("12.50", text);
        }

        [Test]
        [SetCulture("de-DE")]
        public void Convert_HonoursTheCulture()
        {
            Assert.AreEqual("12.345,68", new DecimalFormatConverter().Convert(12345.678m));
            Assert.AreEqual(
                "12,345.68",
                new DecimalFormatConverter("N2", CultureInfoMode.InvariantCulture).Convert(12345.678m));
        }

        // Why the converter refuses to reach decimal through double: this amount carries 24
        // significant digits and a double holds 15-17, so the double route prints
        // 123,456,789,012,345,685,803,008 — wrong from the eighteenth digit on.
        [Test]
        public void Convert_KeepsDigitsADoubleWouldLose() =>
            Assert.AreEqual(
                "123,456,789,012,345,678,901,234",
                new DecimalFormatConverter("N0").Convert(123456789012345678901234m));

        // A decimal carries its scale, so an authored 1.50 is not the same value as 1.5 and a price
        // keeps the cent column it was written with.
        [Test]
        public void Convert_GeneralFormat_PreservesTheScale() =>
            Assert.AreEqual("1.50", GeneralFormat().Convert(1.50m));

        // The basket that drifts: in double, 0.1 + 0.2 is 0.30000000000000004.
        [Test]
        public void Convert_GeneralFormat_ShowsNoBinaryDrift() =>
            Assert.AreEqual("0.3", GeneralFormat().Convert(0.1m + 0.2m));

        // Clearing the format field in the Inspector is not a crash — an empty numeric format string
        // is defined to mean the general format.
        [Test]
        public void Convert_EmptyFormat_FallsBackToTheGeneralFormat() =>
            Assert.AreEqual("12345.678", GeneralFormat().Convert(12345.678m));

        // A typed-in format is not picked from a list, so a typo is not a compile error.
        [Test]
        public void Convert_UnusableFormat_FallsBackToTheGeneralRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("DecimalFormatConverter.*is not a numeric format"));

            Assert.AreEqual(
                (1234.5m).ToString(CultureInfo.CurrentCulture),
                new DecimalFormatConverter("Q").Convert(1234.5m));
        }

        // The report is not once-only: a format that breaks is broken on every push, and a converter
        // that says so once leaves the rest of the session looking healthy.
        [Test]
        public void Convert_UnusableFormat_ReportsEveryPush()
        {
            var converter = new DecimalFormatConverter("q");

            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("DecimalFormatConverter.*is not a numeric format"));

            Assert.AreEqual("12.5", converter.Convert(12.5m));
            Assert.AreEqual("12.5", converter.Convert(12.5m));
        }

        // Two characters make it a custom format string instead, where an unrecognised character is
        // copied to the output verbatim: the very same typo now prints itself and loses the number
        // entirely, without throwing anything anyone could notice.
        [Test]
        public void Convert_UnknownCustomFormat_PrintsItselfInsteadOfTheAmount() =>
            Assert.AreEqual("qq", new DecimalFormatConverter("qq").Convert(12.5m));

        private static DecimalFormatConverter GeneralFormat() =>
            new(string.Empty, CultureInfoMode.InvariantCulture);
    }
}
