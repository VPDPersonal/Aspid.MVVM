using System;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RelativeTimeConverter"/> — the multi-unit ladder, the unit separator,
    /// the past/future format placeholders, and instance reuse.
    /// </summary>
    /// <remarks>
    /// The converter reads the clock through <see cref="DateTime.Now"/> with no seam to inject one, so
    /// the assertions are written to hold whatever the clock says.
    /// </remarks>
    [TestFixture]
    public sealed class RelativeTimeConverterTests
    {
        [Test]
        public void Convert_NowIsNow() =>
            Assert.AreEqual("now", new RelativeTimeConverter().Convert(DateTime.Now));

        [Test]
        public void Convert_DefaultConstructor_DescribesThePast() =>
            Assert.AreEqual("5m ago", new RelativeTimeConverter().Convert(DateTime.Now.AddMinutes(-5)));

        // A spare second guards against the clock ticking the hours component down mid-test.
        [Test]
        public void Convert_DefaultConstructor_DescribesTheFuture() =>
            Assert.AreEqual("in 2h", new RelativeTimeConverter().Convert(DateTime.Now.AddHours(2).AddSeconds(1)));

        // A moment in the past only grows more past while the test runs, so its unit amounts cannot
        // shift under the assertion: one hour and five minutes ago stays 1h 5m to the second below.
        [Test]
        public void Convert_MaxUnitsOfTwo_WritesTwoUnits() =>
            Assert.AreEqual(
                "1h 5m ago",
                new RelativeTimeConverter(2).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        [Test]
        public void Convert_MaxUnitsOfOne_WritesOnlyTheLargest() =>
            Assert.AreEqual(
                "1h ago",
                new RelativeTimeConverter(1).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // A zero unit is passed over rather than written as "0m", so asking for three units of a
        // duration with an empty minutes component gives hours and seconds.
        [Test]
        public void Convert_MaxUnitsAboveOne_PassesOverAZeroUnit() =>
            Assert.AreEqual(
                "1h 5s ago",
                new RelativeTimeConverter(3).Convert(DateTime.Now.AddHours(-1).AddSeconds(-5)));

        // Asking for more units than the duration has does not pad it out with zeros or leave a
        // trailing separator.
        [Test]
        public void Convert_MaxUnitsAboveWhatTheDurationHas_WritesWhatThereIs() =>
            Assert.AreEqual(
                "1h 5m ago",
                new RelativeTimeConverter(4).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // Four is the whole ladder — day, hour, minute, second — and asking for more must not walk
        // off the end of it.
        [Test]
        public void Convert_MaxUnitsBeyondFour_WritesAtMostFour() =>
            Assert.AreEqual(
                "1d 2h 3m 4s ago",
                new RelativeTimeConverter(10)
                    .Convert(DateTime.Now.AddDays(-1).AddHours(-2).AddMinutes(-3).AddSeconds(-4)));

        // Zero and negative units cannot be written, so the constructor says so rather than quietly
        // picking a count the caller did not ask for.
        [TestCase(0)]
        [TestCase(-3)]
        public void Constructor_MaxUnitsBelowOne_Throws(int maxUnits) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new RelativeTimeConverter(maxUnits));

        // The largest unit accumulates rather than rolling into months, so forty days is 40d and not
        // "1mo 9d" — the unit names stop at days deliberately.
        [Test]
        public void Convert_TheLargestUnit_AccumulatesInDays() =>
            Assert.AreEqual("40d ago", new RelativeTimeConverter(2).Convert(DateTime.Now.AddDays(-40)));

        // The smaller units are components of what is left over, so twenty-five hours reads 1d 1h
        // rather than 1d 25h.
        [Test]
        public void Convert_TheSmallerUnits_AreComponentsOfTheRemainder() =>
            Assert.AreEqual("1d 1h ago", new RelativeTimeConverter(2).Convert(DateTime.Now.AddHours(-25)));

        // A moment ahead is the one case needing a spare second: the clock moves between the moment
        // being built and the converter reading it, and without the second the minutes component
        // would tick down to 4 mid-test.
        [Test]
        public void Convert_MaxUnitsOfTwo_UsesTheFutureFormat() =>
            Assert.AreEqual(
                "in 2h 5m",
                new RelativeTimeConverter(2).Convert(DateTime.Now.AddHours(2).AddMinutes(5).AddSeconds(1)));

        [TestCase(" ", "1h 5m ago")]
        [TestCase("", "1h5m ago")]
        [TestCase(", ", "1h, 5m ago")]
        [TestCase(" & ", "1h & 5m ago")]
        public void Convert_TheUnitSeparator_GoesBetweenTheUnits(string separator, string expected) =>
            Assert.AreEqual(
                expected,
                WithSeparator(2, separator).Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // One unit written under a multi-unit setting has nothing to separate, and a separator
        // appended before the check would leave "1h ago" reading " 1h ago" or "1h  ago".
        [Test]
        public void Convert_TheUnitSeparator_IsNotWrittenForASingleUnit() =>
            Assert.AreEqual("1h ago", WithSeparator(2, " & ").Convert(DateTime.Now.AddHours(-1)));

        // More than one unit has nothing single to put in {0}, so the whole quantity goes there and
        // {1} arrives empty — the documented cost of asking for two units with a format written for
        // one. Braces around each placeholder make an empty one visible.
        [Test]
        public void Convert_MaxUnitsAboveOne_PutsTheWholeQuantityInTheFirstPlaceholder() =>
            Assert.AreEqual(
                "[1h 5m][]",
                WithPastFormat(2, "[{0}][{1}]").Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        [Test]
        public void Convert_MaxUnitsOfOne_SplitsTheAmountAndTheUnitAcrossThePlaceholders() =>
            Assert.AreEqual(
                "[1][h]",
                WithPastFormat(1, "[{0}][{1}]").Convert(DateTime.Now.AddHours(-1).AddMinutes(-5)));

        // The StringBuilder is kept between calls to save the allocation, so a converter reused by a
        // binder — which is every binder — must not accumulate the previous reading.
        [Test]
        public void Convert_ReusedAcrossCalls_DoesNotAccumulateThePreviousReading()
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
