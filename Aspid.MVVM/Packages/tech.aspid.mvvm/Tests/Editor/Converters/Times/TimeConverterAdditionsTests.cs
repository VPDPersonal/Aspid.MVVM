using System;
using UnityEngine;
using NUnit.Framework;
using System.Threading;
using System.Reflection;
using System.Globalization;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the time converters added after <see cref="SecondsToTimeStringConverter"/>:
    /// <see cref="TimeSpanArithmeticConverter"/>, <see cref="TimeUntilConverter"/>,
    /// <see cref="DateTimeToUnixTimestampConverter"/>, <see cref="DateTimeOffsetFormatConverter"/>,
    /// and the multi-unit fields of <see cref="RelativeTimeConverter"/>.
    /// </summary>
    /// <remarks>
    /// Two converters here read the clock through <see cref="DateTime.Now"/> with no seam to inject one,
    /// so the assertions on them are written to hold whatever the clock says.
    /// <para>
    /// The culture is pinned to invariant for the whole fixture, because a custom format's <c>:</c> is
    /// the culture's time separator rather than a literal.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class TimeConverterAdditionsTests
    {
        // 10:30 at +03:00 is 07:30 UTC: the offset, the shown hour and the UTC hour are three
        // different numbers, so a converter that dropped or re-read the offset cannot still look right.
        private static readonly DateTimeOffset Moment =
            new DateTimeOffset(2024, 12, 25, 10, 30, 0, TimeSpan.FromHours(3));

        private static readonly DateTime UtcMoment = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private CultureInfo _previousCulture;

        [SetUp]
        public void UseInvariantCulture()
        {
            _previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [TearDown]
        public void RestoreCulture() =>
            Thread.CurrentThread.CurrentCulture = _previousCulture;

        [TestCase(NumberOperation.Plus, 30f, 60d, 90d)]
        [TestCase(NumberOperation.Minus, 30f, 60d, 30d)]
        // Subtracting past zero is allowed to go negative rather than clamping — a progress ring that
        // overran shows a negative duration, not a frozen one.
        [TestCase(NumberOperation.Minus, 90f, 60d, -30d)]
        // The total-minus-elapsed case the class exists for.
        [TestCase(NumberOperation.ReverseSubtract, 30f, 10d, 20d)]
        [TestCase(NumberOperation.ReverseSubtract, 10f, 60d, -50d)]
        // For Multiply and Division the operand is a plain factor, not a number of seconds.
        [TestCase(NumberOperation.Multiply, 2f, 60d, 120d)]
        [TestCase(NumberOperation.Multiply, 0f, 60d, 0d)]
        [TestCase(NumberOperation.Division, 2f, 60d, 30d)]
        [TestCase(NumberOperation.Division, -2f, 60d, -30d)]
        [TestCase(NumberOperation.Modulo, 60f, 90d, 30d)]
        [TestCase(NumberOperation.Modulo, 60f, 30d, 30d)]
        // Power and ReverseDivide read the duration as its number of seconds and the result back as
        // seconds, which is the only meaning they can have for a duration.
        [TestCase(NumberOperation.Power, 2f, 4d, 16d)]
        [TestCase(NumberOperation.ReverseDivide, 60f, 4d, 15d)]
        public void TimeSpanArithmetic_AppliesTheOperation(
            NumberOperation operation,
            float operand,
            double seconds,
            double expected) =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(expected),
                new TimeSpanArithmeticConverter(operation, operand).Convert(TimeSpan.FromSeconds(seconds)));

        // C#'s % keeps the sign of the left operand, so a naive implementation answers "-30s into the
        // cycle" for a duration behind the epoch it is measured from.
        [TestCase(60f)]
        [TestCase(-60f)]
        public void TimeSpanArithmetic_ModuloOfANegativeDuration_IsNonNegative(float operand) =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(30),
                new TimeSpanArithmeticConverter(NumberOperation.Modulo, operand).Convert(TimeSpan.FromSeconds(-90)));

        [Test]
        public void TimeSpanArithmetic_DefaultConstructed_LeavesTheDurationUnchanged() =>
            Assert.AreEqual(TimeSpan.FromSeconds(60), new TimeSpanArithmeticConverter().Convert(TimeSpan.FromSeconds(60)));

        // The point of holding the sum in ticks: a year is 31,536,000 seconds, and a float carries
        // about seven digits, so adding one second to it through a float number of seconds would
        // round the second away entirely.
        [Test]
        public void TimeSpanArithmetic_Plus_KeepsASecondAFloatOfSecondsWouldLose() =>
            Assert.AreEqual(
                TimeSpan.FromDays(365) + TimeSpan.FromSeconds(1),
                new TimeSpanArithmeticConverter(NumberOperation.Plus, 1f).Convert(TimeSpan.FromDays(365)));

        // The operand is a number of seconds, not a whole one.
        [Test]
        public void TimeSpanArithmetic_Plus_KeepsAFractionalOperand() =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(1.5),
                new TimeSpanArithmeticConverter(NumberOperation.Plus, 0.5f).Convert(TimeSpan.FromSeconds(1)));

        // Left to TimeSpan.FromTicks the overflow would wrap into a negative duration, which is wrong
        // in a way that still looks like a plausible reading on a label.
        [Test]
        public void TimeSpanArithmetic_Multiply_PastWhatADurationHolds_SaturatesAtMaxValue() =>
            Assert.AreEqual(
                TimeSpan.MaxValue,
                new TimeSpanArithmeticConverter(NumberOperation.Multiply, 1e10f).Convert(TimeSpan.FromDays(1000)));

        [Test]
        public void TimeSpanArithmetic_Multiply_PastWhatADurationHolds_SaturatesAtMinValue() =>
            Assert.AreEqual(
                TimeSpan.MinValue,
                new TimeSpanArithmeticConverter(NumberOperation.Multiply, 1e10f).Convert(TimeSpan.FromDays(-1000)));

        // A negative base to a fractional exponent is NaN, and casting NaN to long is an arbitrary
        // number of ticks rather than an error.
        [Test]
        public void TimeSpanArithmetic_Power_ProducingNaN_ReturnsZero() =>
            Assert.AreEqual(
                TimeSpan.Zero,
                new TimeSpanArithmeticConverter(NumberOperation.Power, 0.5f).Convert(TimeSpan.FromSeconds(-4)));

        // Math.Pow(0, -1) is positive infinity, which lands on the same saturation guard as an overflow.
        [Test]
        public void TimeSpanArithmetic_Power_ProducingInfinity_SaturatesAtMaxValue() =>
            Assert.AreEqual(
                TimeSpan.MaxValue,
                new TimeSpanArithmeticConverter(NumberOperation.Power, -1f).Convert(TimeSpan.Zero));

        // The double the sum is held in runs out of precision before a tick count does: a duration a
        // hundred microseconds short of TimeSpan.MaxValue comes back 23 ticks short of where it went
        // in, with a zero operand and without tripping the saturation guard.
        [Test]
        public void TimeSpanArithmetic_ADurationNearMaxValue_LosesTicksToTheDoublePipeline() =>
            Assert.AreEqual(
                9223372036854774784L,
                new TimeSpanArithmeticConverter(NumberOperation.Plus, 0f).Convert(new TimeSpan(long.MaxValue - 1000L)).Ticks);

        [Test]
        public void TimeSpanArithmetic_Division_ByAZeroOperand_ReturnsTheDuration()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by a zero operand"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(60),
                new TimeSpanArithmeticConverter(NumberOperation.Division, 0f).Convert(TimeSpan.FromSeconds(60)));
        }

        [Test]
        public void TimeSpanArithmetic_Modulo_ByAZeroOperand_ReturnsTheDuration()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by a zero operand"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(60),
                new TimeSpanArithmeticConverter(NumberOperation.Modulo, 0f).Convert(TimeSpan.FromSeconds(60)));
        }

        // A converter bound to a per-frame duration would otherwise log every frame, and Unity
        // captures a stack trace for each error.
        [Test]
        public void TimeSpanArithmetic_Division_ByAZeroOperand_LogsOncePerInstance()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by a zero operand"));

            var converter = new TimeSpanArithmeticConverter(NumberOperation.Division, 0f);
            converter.Convert(TimeSpan.FromSeconds(1));
            converter.Convert(TimeSpan.FromSeconds(2));
            converter.Convert(TimeSpan.FromSeconds(3));
        }

        // ReverseDivide divides by the bound duration rather than by the operand, and a zero duration
        // is an ordinary reading of a timer rather than a misconfiguration — so it returns quietly.
        // The test fails on an unexpected error log, which is the half being asserted here.
        [Test]
        public void TimeSpanArithmetic_ReverseDivide_ByAZeroDuration_ReturnsTheDurationWithoutLogging() =>
            Assert.AreEqual(
                TimeSpan.Zero,
                new TimeSpanArithmeticConverter(NumberOperation.ReverseDivide, 60f).Convert(TimeSpan.Zero));

        [Test]
        public void TimeSpanArithmetic_AnUndeclaredOperation_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new TimeSpanArithmeticConverter((NumberOperation)99, 1f).Convert(TimeSpan.FromSeconds(60)));

        [Test]
        public void TimeUntil_AMomentAlreadyPast_IsReportedAsZero() =>
            Assert.AreEqual(TimeSpan.Zero, new TimeUntilConverter().Convert(new DateTime(2000, 1, 1)));

        // Unclamped, a passed moment reads negative — what a "you are late by" label wants.
        [Test]
        public void TimeUntil_AMomentAlreadyPast_Unclamped_IsNegative() =>
            Assert.AreEqual(
                -TimeSpan.FromMinutes(30).TotalSeconds,
                new TimeUntilConverter(useUtcNow: false, clampToZero: false)
                    .Convert(DateTime.Now.AddMinutes(-30))
                    .TotalSeconds,
                delta: 1d);

        // The clamp must not touch a moment still ahead, which is the whole working range.
        [Test]
        public void TimeUntil_AFutureMoment_IsTheDistanceToIt() =>
            Assert.AreEqual(
                TimeSpan.FromMinutes(30).TotalSeconds,
                new TimeUntilConverter().Convert(DateTime.Now.AddMinutes(30)).TotalSeconds,
                delta: 1d);

        // Measuring a moment against the wrong clock is out by exactly the zone offset, which is the
        // failure the tooltip warns about and the only observable difference the flag makes.
        [Test]
        public void TimeUntil_UtcAndLocal_DifferByTheZoneOffset()
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            if (offset == TimeSpan.Zero) Assert.Ignore("This machine runs at UTC, so the two clocks cannot differ.");

            var target = new DateTime(2200, 1, 1);
            var utc = new TimeUntilConverter(useUtcNow: true, clampToZero: false).Convert(target);
            var local = new TimeUntilConverter(useUtcNow: false, clampToZero: false).Convert(target);

            Assert.AreEqual(offset.TotalSeconds, (utc - local).TotalSeconds, delta: 1d);
        }

        [TestCase(false, 1704067200L)]
        [TestCase(true, 1704067200000L)]
        public void DateTimeToUnixTimestamp_AUtcMoment_IsTheEpochCount(bool milliseconds, long expected) =>
            Assert.AreEqual(expected, new DateTimeToUnixTimestampConverter(milliseconds).Convert(UtcMoment));

        // Seconds are floored rather than rounded: half past the second is still that second.
        [TestCase(false, 1704067200L)]
        [TestCase(true, 1704067200500L)]
        public void DateTimeToUnixTimestamp_ASubSecondMoment_TruncatesToTheUnit(bool milliseconds, long expected) =>
            Assert.AreEqual(
                expected,
                new DateTimeToUnixTimestampConverter(milliseconds)
                    .Convert(new DateTime(2024, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc)));

        // Epoch counts run backwards too; a converter that used an unsigned intermediate would not.
        [Test]
        public void DateTimeToUnixTimestamp_APreEpochMoment_IsNegative() =>
            Assert.AreEqual(
                -315619200L,
                new DateTimeToUnixTimestampConverter().Convert(new DateTime(1960, 1, 1, 0, 0, 0, DateTimeKind.Utc)));

        // A moment with no Kind is read as local, matching what UnixTimestampToDateTimeConverter
        // hands back — reading it as UTC instead would shift every timestamp by the zone offset.
        // Midday mid-June is used because a DST transition never lands there and so cannot make the
        // local reading ambiguous.
        [Test]
        public void DateTimeToUnixTimestamp_AnUnspecifiedKind_IsReadAsLocal() =>
            Assert.AreEqual(
                new DateTimeToUnixTimestampConverter().Convert(new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Local)),
                new DateTimeToUnixTimestampConverter().Convert(new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Unspecified)));

        // This converter exists only because the pair's reverse runs in TwoWay and OneWayToSource
        // binders alone, so the two have to agree digit for digit.
        [TestCase(false)]
        [TestCase(true)]
        public void DateTimeToUnixTimestamp_MatchesTheReverseOfUnixTimestampToDateTime(bool milliseconds) =>
            Assert.AreEqual(
                new UnixTimestampToDateTimeConverter(milliseconds).ConvertBack(UtcMoment),
                new DateTimeToUnixTimestampConverter(milliseconds).Convert(UtcMoment));

        // The local leg is the one that can go wrong: the DateTime carries Kind Local and has to be
        // taken back to UTC before counting, or the round trip is out by the zone offset.
        [TestCase(true)]
        [TestCase(false)]
        public void DateTimeToUnixTimestamp_RoundTripsAUnixTimestamp(bool utc)
        {
            const long timestamp = 1_700_000_000L;
            var moment = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: utc).Convert(timestamp);

            Assert.AreEqual(timestamp, new DateTimeToUnixTimestampConverter().Convert(moment));
        }

        // The offset the moment arrived with is what a DateTime cannot carry, so it is the first
        // thing to check survives.
        [TestCase("zzz", "+03:00")]
        [TestCase("HH:mm", "10:30")]
        [TestCase("dd.MM.yyyy HH:mm", "25.12.2024 10:30")]
        public void DateTimeOffsetFormat_KeepsTheOffsetItArrivedWith(string format, string expected) =>
            Assert.AreEqual(expected, new DateTimeOffsetFormatConverter(format).Convert(Moment));

        // +05:45 is a real offset that is not a whole hour, so a converter holding the override in
        // hours rather than minutes cannot pass this.
        [TestCase("zzz", "+05:45")]
        [TestCase("HH:mm", "13:15")]
        public void DateTimeOffsetFormat_AnOverride_ShowsTheMomentAtThatOffset(string format, string expected) =>
            Assert.AreEqual(
                expected,
                new DateTimeOffsetFormatConverter(format, offsetOverride: TimeSpan.FromMinutes(345)).Convert(Moment));

        // ToOffset throws past ±14 hours and the offset is typed into the Inspector rather than
        // picked from a list, so a fat-fingered field has to show the wrong hour, not stop the binder.
        [TestCase(20, "+14:00")]
        [TestCase(-20, "-14:00")]
        public void DateTimeOffsetFormat_AnOverrideBeyondFourteenHours_IsClamped(int hours, string expected) =>
            Assert.AreEqual(
                expected,
                new DateTimeOffsetFormatConverter("zzz", offsetOverride: TimeSpan.FromHours(hours)).Convert(Moment));

        // Both switches on at once is the misconfiguration the tooltip resolves. (A machine that
        // happens to sit at +05:45 could not tell the two apart, which is why the override is an
        // offset no common zone uses.)
        [Test]
        public void DateTimeOffsetFormat_AnOverride_TakesPrecedenceOverLocalTime() =>
            Assert.AreEqual(
                "+05:45",
                new DateTimeOffsetFormatConverter("zzz", toLocalTime: true, offsetOverride: TimeSpan.FromMinutes(345))
                    .Convert(Moment));

        // Showing the moment in the player's zone must move the clock and the offset together — the
        // instant named is the same one, whatever zone the machine running this is in.
        [Test]
        public void DateTimeOffsetFormat_ToLocalTime_NamesTheSameInstant()
        {
            var text = new DateTimeOffsetFormatConverter("o", toLocalTime: true).Convert(Moment);

            Assert.AreEqual(Moment.UtcDateTime, DateTimeOffset.Parse(text, CultureInfo.InvariantCulture).UtcDateTime);
        }

        [TestCase("")]
        [TestCase("   ")]
        public void DateTimeOffsetFormat_AnEmptyFormat_UsesTheDefaultRendering(string format) =>
            Assert.AreEqual(
                Moment.ToString(CultureInfo.InvariantCulture),
                new DateTimeOffsetFormatConverter(format).Convert(Moment));

        // An unterminated quote is one of the few patterns .NET rejects outright; the binder has to
        // keep drawing rather than throw out of a property changed callback.
        [Test]
        public void DateTimeOffsetFormat_ABrokenFormat_FallsBackToTheDefaultRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a DateTimeOffset format"));

            Assert.AreEqual(
                Moment.ToString(CultureInfo.InvariantCulture),
                new DateTimeOffsetFormatConverter("'unterminated").Convert(Moment));
        }

        [Test]
        public void DateTimeOffsetFormat_ABrokenFormat_LogsOncePerInstance()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a DateTimeOffset format"));

            var converter = new DateTimeOffsetFormatConverter("'unterminated");
            converter.Convert(Moment);
            converter.Convert(Moment);
            converter.Convert(Moment);
        }

        // A typo in a pattern is not a FormatException: .NET emits an unknown specifier as a literal,
        // so the fallback never runs and nothing is logged. The label shows the typo instead, which
        // is the failure mode to expect a report of — not a silent default rendering.
        [Test]
        public void DateTimeOffsetFormat_AnUnknownSpecifier_IsEmittedAsALiteral() =>
            Assert.AreEqual("qq", new DateTimeOffsetFormatConverter("qq").Convert(Moment));

        // A moment in the past only grows more past while the test runs, so its unit amounts cannot
        // shift under the assertion: one hour and five minutes ago stays 1h 5m to the second below.
        [Test]
        public void RelativeTime_MaxUnitsOfTwo_WritesTwoUnits() =>
            Assert.AreEqual(
                "1h 5m ago",
                new RelativeTimeConverter(2).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        [Test]
        public void RelativeTime_MaxUnitsOfOne_WritesOnlyTheLargest() =>
            Assert.AreEqual(
                "1h ago",
                new RelativeTimeConverter(1).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // A zero unit is passed over rather than written as "0m", so asking for three units of a
        // duration with an empty minutes component gives hours and seconds.
        [Test]
        public void RelativeTime_MaxUnitsAboveOne_PassesOverAZeroUnit() =>
            Assert.AreEqual(
                "1h 5s ago",
                new RelativeTimeConverter(3).Convert(DateTime.Now.AddHours(-1).AddSeconds(-5)));

        // Asking for more units than the duration has does not pad it out with zeros or leave a
        // trailing separator.
        [Test]
        public void RelativeTime_MaxUnitsAboveWhatTheDurationHas_WritesWhatThereIs() =>
            Assert.AreEqual(
                "1h 5m ago",
                new RelativeTimeConverter(4).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // Four is the whole ladder — day, hour, minute, second — and asking for more must not walk
        // off the end of it.
        [Test]
        public void RelativeTime_MaxUnitsBeyondFour_WritesAtMostFour() =>
            Assert.AreEqual(
                "1d 2h 3m 4s ago",
                new RelativeTimeConverter(10)
                    .Convert(DateTime.Now.AddDays(-1).AddHours(-2).AddMinutes(-3).AddSeconds(-4)));

        // Zero and negative are what an Inspector field starts at or gets dragged to; both have to
        // land on the single-unit form rather than writing nothing at all.
        [TestCase(0)]
        [TestCase(-3)]
        public void RelativeTime_MaxUnitsBelowOne_FallsBackToTheSingleUnitForm(int maxUnits) =>
            Assert.AreEqual(
                "1h ago",
                new RelativeTimeConverter(maxUnits).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // The largest unit accumulates rather than rolling into months, so forty days is 40d and not
        // "1mo 9d" — the unit names stop at days deliberately.
        [Test]
        public void RelativeTime_TheLargestUnit_AccumulatesInDays() =>
            Assert.AreEqual("40d ago", new RelativeTimeConverter(2).Convert(DateTime.Now.AddDays(-40)));

        // The smaller units are components of what is left over, so twenty-five hours reads 1d 1h
        // rather than 1d 25h.
        [Test]
        public void RelativeTime_TheSmallerUnits_AreComponentsOfTheRemainder() =>
            Assert.AreEqual("1d 1h ago", new RelativeTimeConverter(2).Convert(DateTime.Now.AddHours(-25)));

        // A moment ahead is the one case needing a spare second: the clock moves between the moment
        // being built and the converter reading it, and without the second the minutes component
        // would tick down to 4 mid-test.
        [Test]
        public void RelativeTime_MaxUnitsOfTwo_UsesTheFutureFormat() =>
            Assert.AreEqual(
                "in 2h 5m",
                new RelativeTimeConverter(2).Convert(DateTime.Now.AddHours(2).AddMinutes(5).AddSeconds(1)));

        [TestCase(" ", "1h 5m ago")]
        [TestCase("", "1h5m ago")]
        [TestCase(", ", "1h, 5m ago")]
        [TestCase(" и ", "1h и 5m ago")]
        public void RelativeTime_TheUnitSeparator_GoesBetweenTheUnits(string separator, string expected) =>
            Assert.AreEqual(
                expected,
                WithSeparator(2, separator).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // One unit written under a multi-unit setting has nothing to separate, and a separator
        // appended before the check would leave "1h ago" reading " 1h ago" or "1h  ago".
        [Test]
        public void RelativeTime_TheUnitSeparator_IsNotWrittenForASingleUnit() =>
            Assert.AreEqual("1h ago", WithSeparator(2, " и ").Convert(DateTime.Now.AddHours(-1)));

        // More than one unit has nothing single to put in {0}, so the whole quantity goes there and
        // {1} arrives empty — the documented cost of asking for two units with a format written for
        // one. Braces around each placeholder make an empty one visible.
        [Test]
        public void RelativeTime_MaxUnitsAboveOne_PutsTheWholeQuantityInTheFirstPlaceholder() =>
            Assert.AreEqual(
                "[1h 5m][]",
                WithPastFormat(2, "[{0}][{1}]").Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        [Test]
        public void RelativeTime_MaxUnitsOfOne_SplitsTheAmountAndTheUnitAcrossThePlaceholders() =>
            Assert.AreEqual(
                "[1][h]",
                WithPastFormat(1, "[{0}][{1}]").Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // The StringBuilder is kept between calls to save the allocation, so a converter reused by a
        // binder — which is every binder — must not accumulate the previous reading.
        [Test]
        public void RelativeTime_ReusedAcrossCalls_DoesNotAccumulateThePreviousReading()
        {
            var converter = new RelativeTimeConverter(2);
            converter.Convert(DateTime.Now.AddDays(-1).AddHours(-2));

            Assert.AreEqual("1h 5m ago", converter.Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));
        }

        private static RelativeTimeConverter WithSeparator(int maxUnits, string separator) =>
            With(new RelativeTimeConverter(maxUnits), "_unitSeparator", separator);

        private static RelativeTimeConverter WithPastFormat(int maxUnits, string pastFormat) =>
            With(new RelativeTimeConverter(maxUnits), "_pastFormat", pastFormat);

        // The separator and the formats are Inspector state with no constructor overload, so the
        // tests set them the way the Inspector does. A renamed field throws here rather than leaving
        // the default in place, which would let the assertions above pass for the wrong reason.
        private static T With<T>(T converter, string field, object value)
            where T : class
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var info = converter.GetType().GetField(field, flags);
            if (info is null) throw new InvalidOperationException($"{converter.GetType().Name} has no {field} field.");

            info.SetValue(converter, value);
            return converter;
        }
    }
}
