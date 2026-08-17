using System;
using System.Threading;
using NUnit.Framework;
using System.Reflection;
using System.Globalization;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="ThousandsSeparatorConverter"/>, <see cref="DecimalFormatConverter"/>
    /// and the last-two-digits rule in <see cref="OrdinalConverter"/>.
    /// </summary>
    /// <remarks>
    /// Three mistakes are pinned here: a separator authored to outlive the player's locale that reverts
    /// to the device one, or is written into the process-wide <see cref="NumberFormatInfo"/>; an amount
    /// routed through <see cref="double"/>, which is what <see cref="decimal"/> was chosen to prevent;
    /// and 11th/12th/13th, which a last-digit rule turns into "11st" — 111th and 1011th with them.
    /// The fixture is pinned to the invariant culture, and <c>TearDown</c> restores the thread either way.
    /// </remarks>
    [TestFixture]
    internal sealed class NumberFormatAdditionsTests
    {
        private CultureInfo _previous;

        [SetUp]
        public void UseInvariantCulture()
        {
            _previous = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [TearDown]
        public void RestoreCulture() =>
            Thread.CurrentThread.CurrentCulture = _previous;

        // The separator field ships empty, and empty is the one value that cannot mean "no
        // separator": it means "whatever the device is set to", which is exactly what a project
        // reaching for this converter is trying to escape.
        [Test]
        public void Thousands_EmptySeparator_FollowsTheDeviceCulture()
        {
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter().Convert(1234567L));

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("1.234.567", new ThousandsSeparatorConverter().Convert(1234567L));
        }

        [TestCase("_", "1_234_567")]
        [TestCase("'", "1'234'567")]
        [TestCase(" ", "1 234 567")]
        // Written escaped because the character a game most often reaches for here is invisible.
        [TestCase("\u2009", "1\u2009234\u2009567")]
        public void Thousands_AuthoredSeparator_ReplacesTheCultureOne(string separator, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter(separator).Convert(1234567L));

        // The reason the converter exists at all: a score authored with a thin space has to read the
        // same in a screenshot taken on a German machine as on an English one.
        [Test]
        public void Thousands_AuthoredSeparator_SurvivesTheDeviceCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("1_234_567", new ThousandsSeparatorConverter("_").Convert(1234567L));
        }

        [TestCase(0L, "0")]
        [TestCase(999L, "999")]
        [TestCase(1000L, "1_000")]
        [TestCase(-1234567L, "-1_234_567")]
        public void Thousands_BelowAGroupAndBelowZero(long value, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter("_").Convert(value));

        // Nothing on this path negates the input, so the value with no positive counterpart formats
        // instead of throwing — unlike OrdinalConverter further down.
        [Test]
        public void Thousands_LongMinValue_Formats() =>
            Assert.AreEqual(
                "-9_223_372_036_854_775_808",
                new ThousandsSeparatorConverter("_").Convert(long.MinValue));

        [TestCase(1234567, "1_234_567")]
        [TestCase(int.MinValue, "-2_147_483_648")]
        public void Thousands_IntOverload_GroupsTheSameWay(int value, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter("_").Convert(value));

        // A NumberFormatInfo taken from a culture is shared process-wide and read-only. Setting the
        // separator on it rather than on a clone would either throw here or silently re-format every
        // number the rest of the game prints.
        [Test]
        public void Thousands_AuthoredSeparator_LeavesTheSharedCultureFormatAlone()
        {
            new ThousandsSeparatorConverter("_", CultureInfoMode.InvariantCulture).Convert(1234567L);

            Assert.AreEqual(",", CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator);
        }

        // The clone is cached because a binder pushes on every notification rather than on every
        // change. Re-authoring the separator has to invalidate that cache; a stale clone would keep
        // writing the old separator for the rest of the session.
        [Test]
        public void Thousands_SeparatorReauthored_RebuildsTheCachedFormat()
        {
            var converter = new ThousandsSeparatorConverter("_");
            Assert.AreEqual("1_234_567", converter.Convert(1234567L));

            SetSeparator(converter, "'");

            Assert.AreEqual("1'234'567", converter.Convert(1234567L));
        }

        // The same cache, from the other side: repeated pushes must not drift.
        [Test]
        public void Thousands_RepeatedCalls_KeepFormattingTheSameWay()
        {
            var converter = new ThousandsSeparatorConverter("_");

            Assert.AreEqual("1_234_567", converter.Convert(1234567L));
            Assert.AreEqual("7_654_321", converter.Convert(7654321L));
            Assert.AreEqual("1_234_567", converter.Convert(1234567L));
        }

        // An authored separator replaces the culture's separator but not its grouping. India groups
        // the last three digits and then in pairs, so the authored character lands in Indian places —
        // the half of the tooltip's promise that the previous row cannot show.
        [Test]
        public void Thousands_AuthoredSeparator_KeepsTheCultureGrouping()
        {
            CultureInfo india;

            try
            {
                india = CultureInfo.GetCultureInfo("en-IN");
            }
            catch (CultureNotFoundException)
            {
                Assert.Ignore("en-IN is not present in this runtime's culture data.");
                return;
            }

            Assume.That(india.NumberFormat.NumberGroupSizes, Is.EqualTo(new[] { 3, 2 }));

            Thread.CurrentThread.CurrentCulture = india;

            Assert.AreEqual("12_34_567", new ThousandsSeparatorConverter("_").Convert(1234567L));
        }

        // The default format is N2, so the row also pins the rounding: .678 becomes .68 rather than
        // being cut to .67.
        [Test]
        public void Decimal_DefaultFormat_GroupsAndRoundsToTwoDecimals() =>
            Assert.AreEqual("12,345.68", new DecimalFormatConverter().Convert(12345.678m));

        [Test]
        public void Decimal_HonoursTheCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("12.345,68", new DecimalFormatConverter().Convert(12345.678m));
            Assert.AreEqual(
                "12,345.68",
                new DecimalFormatConverter("N2", CultureInfoMode.InvariantCulture).Convert(12345.678m));
        }

        // Why the converter refuses to reach decimal through double: this amount carries 24
        // significant digits and a double holds 15-17, so the double route prints
        // 123,456,789,012,345,685,803,008 — wrong from the eighteenth digit on.
        [Test]
        public void Decimal_KeepsDigitsADoubleWouldLose() =>
            Assert.AreEqual(
                "123,456,789,012,345,678,901,234",
                new DecimalFormatConverter("N0", CultureInfoMode.InvariantCulture)
                    .Convert(123456789012345678901234m));

        // A decimal carries its scale, so an authored 1.50 is not the same value as 1.5 and a price
        // keeps the cent column it was written with.
        [Test]
        public void Decimal_GeneralFormat_PreservesTheScale() =>
            Assert.AreEqual("1.50", General().Convert(1.50m));

        // The basket that drifts: in double, 0.1 + 0.2 is 0.30000000000000004.
        [Test]
        public void Decimal_GeneralFormat_ShowsNoBinaryDrift() =>
            Assert.AreEqual("0.3", General().Convert(0.1m + 0.2m));

        // Clearing the format field in the Inspector is not a crash — an empty numeric format string
        // is defined to mean the general format.
        [Test]
        public void Decimal_EmptyFormat_FallsBackToTheGeneralFormat() =>
            Assert.AreEqual("12345.678", General().Convert(12345.678m));

        // A one-character format is read as a standard specifier, and an unknown one throws. The
        // converter has no guard, unlike TimeSpanFormatConverter, which logs and falls back — so a
        // typo in the Inspector surfaces as an exception on the first push rather than as bad text.
        [Test]
        public void Decimal_UnknownStandardSpecifier_Throws() =>
            Assert.Throws<FormatException>(() => new DecimalFormatConverter("q").Convert(12.5m));

        // Two characters make it a custom format string instead, where an unrecognised character is
        // copied to the output verbatim: the very same typo now prints itself and loses the number
        // entirely, without throwing anything anyone could notice.
        [Test]
        public void Decimal_UnknownCustomFormat_PrintsItselfInsteadOfTheAmount() =>
            Assert.AreEqual("qq", new DecimalFormatConverter("qq").Convert(12.5m));

        // C is what the culture field is worth having for. The symbol and the side it sits on come
        // from culture data that differs between runtimes, so the assertion is limited to what is
        // invariant: the amount is there and something was added to it.
        [Test]
        public void Decimal_CurrencyFormat_AddsASymbolToTheAmount()
        {
            var text = new DecimalFormatConverter("C2", CultureInfoMode.InvariantCulture).Convert(12.5m);

            StringAssert.Contains("12.50", text);
            Assert.AreNotEqual("12.50", text);
        }

        [TestCase(0, "0th")]
        [TestCase(1, "1st")]
        [TestCase(2, "2nd")]
        [TestCase(3, "3rd")]
        [TestCase(4, "4th")]
        [TestCase(21, "21st")]
        [TestCase(22, "22nd")]
        [TestCase(23, "23rd")]
        [TestCase(100, "100th")]
        [TestCase(101, "101st")]
        public void Ordinal_LastDigitDecidesTheSuffix(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // The classic trap: 11, 12 and 13 end in 1, 2 and 3 and still take "th". The exception is not
        // about the teens but about the last two digits, so it comes back in every hundred — 111 and
        // 1011 are the cases a "value < 20" guard gets wrong while looking correct.
        [TestCase(11, "11th")]
        [TestCase(12, "12th")]
        [TestCase(13, "13th")]
        [TestCase(111, "111th")]
        [TestCase(112, "112th")]
        [TestCase(113, "113th")]
        [TestCase(211, "211th")]
        [TestCase(1011, "1011th")]
        [TestCase(1012, "1012th")]
        [TestCase(1013, "1013th")]
        public void Ordinal_TheTeensAndEveryHundredAfterThem_TakeTh(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // The other edge of the same exception: 14 is outside it despite being a teen, and 121 is
        // outside it despite ending in 21.
        [TestCase(14, "14th")]
        [TestCase(114, "114th")]
        [TestCase(121, "121st")]
        [TestCase(122, "122nd")]
        [TestCase(123, "123rd")]
        public void Ordinal_JustOutsideTheTeens_ReturnsToTheLastDigitRule(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // The suffix is picked from the magnitude while the sign survives the formatting, so a
        // negative rank keeps the suffix its positive counterpart would take. Worth pinning because
        // the class remark asserts no ordinal carries a sign — the code formats one.
        [TestCase(-1, "-1st")]
        [TestCase(-3, "-3rd")]
        [TestCase(-11, "-11th")]
        public void Ordinal_Negative_KeepsTheSuffixOfItsMagnitude(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // Math.Abs(int.MinValue) throws by contract, there being no positive counterpart, so the one
        // input that cannot be negated takes the binder down instead of printing. Recorded as it
        // behaves today, not endorsed: negating into a long would format it.
        [Test]
        public void Ordinal_IntMinValue_ThrowsInsteadOfFormatting() =>
            Assert.Throws<OverflowException>(() => new OrdinalConverter().Convert(int.MinValue));

        [Test]
        public void Ordinal_IntMaxValue_Formats() =>
            Assert.AreEqual("2147483647th", new OrdinalConverter().Convert(int.MaxValue));

        // The culture reaches the digits and nothing else, and .NET writes ASCII digits for an
        // integer whichever culture is picked — which is what makes the field decorative for any
        // ordinal that is not negative.
        [Test]
        public void Ordinal_TheCultureChangesNothingForAPositiveNumber()
        {
            Assert.AreEqual("1234th", new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(1234));

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("1234th", new OrdinalConverter(CultureInfoMode.CurrentCulture).Convert(1234));
        }

        private static DecimalFormatConverter General() =>
            new DecimalFormatConverter(string.Empty, CultureInfoMode.InvariantCulture);

        // The separator has a constructor, but re-authoring it on a live instance is Inspector state
        // with no setter, so the test writes it the way the Inspector does.
        private static void SetSeparator(ThousandsSeparatorConverter converter, string separator)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var field = typeof(ThousandsSeparatorConverter).GetField("_separator", flags);
            if (field is null) throw new InvalidOperationException("ThousandsSeparatorConverter has no _separator field.");

            field.SetValue(converter, separator);
        }
    }
}
