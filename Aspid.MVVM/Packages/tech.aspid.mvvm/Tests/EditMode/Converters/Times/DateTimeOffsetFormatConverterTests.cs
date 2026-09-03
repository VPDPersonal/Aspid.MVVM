using System;
using NUnit.Framework;
using System.Threading;
using System.Globalization;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DateTimeOffsetFormatConverter"/> — the offset override, the fourteen-hour
    /// clamp, the offset source, and the fallback for a broken format string.
    /// </summary>
    /// <remarks>
    /// The culture is pinned to invariant for the whole fixture, because a custom format's <c>:</c> is
    /// the culture's time separator rather than a literal.
    /// </remarks>
    [TestFixture]
    public sealed class DateTimeOffsetFormatConverterTests
    {
        // 10:30 at +03:00 is 07:30 UTC: the offset, the shown hour and the UTC hour are three
        // different numbers, so a converter that dropped or re-read the offset cannot still look right.
        private static readonly DateTimeOffset _moment =
            new DateTimeOffset(2024, 12, 25, 10, 30, 0, TimeSpan.FromHours(3));

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

        // The offset the moment arrived with is what a DateTime cannot carry, so it is the first
        // thing to check survives.
        [TestCase("zzz", "+03:00")]
        [TestCase("HH:mm", "10:30")]
        [TestCase("dd.MM.yyyy HH:mm", "25.12.2024 10:30")]
        public void Convert_KeepsTheOffsetItArrivedWith(string format, string expected) =>
            Assert.AreEqual(expected, new DateTimeOffsetFormatConverter(format).Convert(_moment));

        // +05:45 is a real offset that is not a whole hour, so a converter holding the override in
        // hours rather than minutes cannot pass this.
        [TestCase("zzz", "+05:45")]
        [TestCase("HH:mm", "13:15")]
        public void Convert_AnOverride_ShowsTheMomentAtThatOffset(string format, string expected) =>
            Assert.AreEqual(
                expected,
                new DateTimeOffsetFormatConverter(format, offsetOverride: TimeSpan.FromMinutes(345)).Convert(_moment));

        [TestCase(20)]
        [TestCase(-20)]
        public void Constructor_AnOverrideBeyondFourteenHours_Throws(int hours) =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new DateTimeOffsetFormatConverter("zzz", offsetOverride: TimeSpan.FromHours(hours)));

        // The three sources are one field rather than two switches, so an override cannot be asked
        // for alongside local time. (A machine that happens to sit at +05:45 could not tell the two
        // apart, which is why the override is an offset no common zone uses.)
        [Test]
        public void Convert_AnOverride_ShowsTheOverrideRatherThanLocalTime() =>
            Assert.AreEqual(
                "+05:45",
                new DateTimeOffsetFormatConverter("zzz", offsetOverride: TimeSpan.FromMinutes(345)).Convert(_moment));

        // Showing the moment in the player's zone must move the clock and the offset together — the
        // instant named is the same one, whatever zone the machine running this is in.
        [Test]
        public void Convert_LocalOffsetSource_NamesTheSameInstant()
        {
            var text = new DateTimeOffsetFormatConverter("o", OffsetSource.Local).Convert(_moment);

            Assert.AreEqual(_moment.UtcDateTime, DateTimeOffset.Parse(text, CultureInfo.InvariantCulture).UtcDateTime);
        }

        // The offset source is a serialized enum, so a value outside it can only come from a broken
        // asset — which is reported and shown at the offset the moment arrived with, rather than at
        // an arbitrary one.
        [Test]
        public void Convert_AnUndeclaredOffsetSource_ShowsTheOffsetAsGiven()
        {
            LogAssert.Expect(LogType.Error, new Regex("DateTimeOffsetFormatConverter.*not a declared"));

            Assert.AreEqual(
                _moment.ToString("zzz", CultureInfo.InvariantCulture),
                new DateTimeOffsetFormatConverter("zzz", (OffsetSource)99).Convert(_moment));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Convert_AnEmptyFormat_UsesTheDefaultRendering(string format) =>
            Assert.AreEqual(
                _moment.ToString(CultureInfo.InvariantCulture),
                new DateTimeOffsetFormatConverter(format).Convert(_moment));

        // An unterminated quote is one of the few patterns .NET rejects outright; the binder has to
        // keep drawing rather than throw out of a property changed callback.
        [Test]
        public void Convert_ABrokenFormat_FallsBackToTheDefaultRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a DateTimeOffset format"));

            Assert.AreEqual(
                _moment.ToString(CultureInfo.InvariantCulture),
                new DateTimeOffsetFormatConverter("'unterminated").Convert(_moment));
        }

        [Test]
        public void Convert_ABrokenFormat_LogsOnEveryConversion()
        {
            for (var index = 0; index < 3; index++)
                LogAssert.Expect(LogType.Error, new Regex("is not a DateTimeOffset format"));

            var converter = new DateTimeOffsetFormatConverter("'unterminated");
            converter.Convert(_moment);
            converter.Convert(_moment);
            converter.Convert(_moment);
        }

        // A typo in a pattern is not a FormatException: .NET emits an unknown specifier as a literal,
        // so the fallback never runs and nothing is logged. The label shows the typo instead, which
        // is the failure mode to expect a report of — not a silent default rendering.
        [Test]
        public void Convert_AnUnknownSpecifier_IsEmittedAsALiteral() =>
            Assert.AreEqual("qq", new DateTimeOffsetFormatConverter("qq").Convert(_moment));
    }
}
