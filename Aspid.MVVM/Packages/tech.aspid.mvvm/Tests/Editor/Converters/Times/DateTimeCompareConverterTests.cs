using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="DateTimeCompareConverter"/> — the six <see cref="ComparisonMode"/>
    /// branches against a fixed moment, the UTC-normalized comparison, and the invalid-ticks guard.
    /// </summary>
    [TestFixture]
    internal sealed class DateTimeCompareConverterTests
    {
        private static readonly DateTime _reference = new(2024, 1, 1);

        [TestCase(ComparisonMode.GreaterThan, "2024-01-02", true)]
        [TestCase(ComparisonMode.GreaterThan, "2023-12-31", false)]
        [TestCase(ComparisonMode.LessThan, "2023-12-31", true)]
        [TestCase(ComparisonMode.Equal, "2024-01-01", true)]
        [TestCase(ComparisonMode.NotEqual, "2024-01-01", false)]
        [TestCase(ComparisonMode.GreaterThanOrEqual, "2024-01-01", true)]
        [TestCase(ComparisonMode.LessThanOrEqual, "2024-01-01", true)]
        public void Convert_ComparesAgainstTheFixedMoment(ComparisonMode comparison, string moment, bool expected) =>
            Assert.AreEqual(expected, new DateTimeCompareConverter(comparison, _reference).Convert(DateTime.Parse(moment)));

        // Two moments with known kinds name absolute instants and are compared in UTC. A Local moment
        // is converted through ToUniversalTime before the comparison, so a value stamped Local and a
        // reference stamped Utc that name the very same instant compare equal — not the wrong answer
        // a raw-ticks comparison of two different Kind values would give.
        [Test]
        public void Convert_KnownKinds_AreComparedInUtc()
        {
            var utcReference = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 7, 0, 0), DateTimeKind.Utc);
            var converter = new DateTimeCompareConverter(ComparisonMode.Equal, utcReference);

            var sameInstantAsLocal = DateTime.SpecifyKind(utcReference.ToLocalTime(), DateTimeKind.Local);

            Assert.IsTrue(converter.Convert(sameInstantAsLocal));
        }

        // As soon as either kind is Unspecified the instant is unknowable, so raw ticks are compared
        // instead of a UTC-normalized reading.
        [Test]
        public void Convert_UnspecifiedKind_ComparesRawTicks()
        {
            var reference = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
            var converter = new DateTimeCompareConverter(ComparisonMode.Equal, reference);

            Assert.IsTrue(converter.Convert(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified)));
        }

        [Test]
        public void Convert_UndeclaredComparison_ReportsAndReturnsFalse()
        {
            LogAssert.Expect(LogType.Error, new Regex("DateTimeCompareConverter.*not a declared ComparisonMode"));

            Assert.IsFalse(new DateTimeCompareConverter((ComparisonMode)99, _reference).Convert(_reference));
        }

        [Test]
        public void Convert_UndeclaredReferenceSource_ReportsAndReturnsFalse()
        {
            LogAssert.Expect(LogType.Error, new Regex("DateTimeCompareConverter.*not a declared ReferenceSource"));

            Assert.IsFalse(new DateTimeCompareConverter(ComparisonMode.Equal, (ReferenceSource)99).Convert(_reference));
        }

        // The ticks are Inspector-editable as a raw long, so a value outside DateTime's representable
        // range must be caught rather than throwing out of the binder.
        [Test]
        public void Convert_FixedMomentTicksOutsideTheCalendar_ReportsAndReturnsFalse()
        {
            var converter = new DateTimeCompareConverter(ComparisonMode.Equal, _reference);
            SetTicks(converter, long.MaxValue);

            LogAssert.Expect(LogType.Error, new Regex("outside the representable range"));

            Assert.IsFalse(converter.Convert(_reference));
        }

        private static void SetTicks(DateTimeCompareConverter converter, long ticks)
        {
            var field = typeof(DateTimeCompareConverter).GetField(
                "_referenceTicks",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(field, "DateTimeCompareConverter has no field _referenceTicks");
            field!.SetValue(converter, ticks);
        }
    }
}
