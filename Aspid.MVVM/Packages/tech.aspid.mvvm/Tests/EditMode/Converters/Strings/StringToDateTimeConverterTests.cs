using System;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Globalization;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToDateTimeConverter"/> — the exact format, the wrong-format
    /// fallback and the round trip.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToDateTimeConverterTests
    {
        [Test]
        public void Convert_ReadsAnExactFormat() =>
            Assert.AreEqual(
                new DateTime(2024, 12, 25),
                new StringToDateTimeConverter("dd.MM.yyyy").Convert("25.12.2024"));

        [Test]
        public void Convert_WrongFormatGivesTheFallback()
        {
            var fallback = new DateTime(2000, 1, 1);
            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter"));

            Assert.AreEqual(
                fallback,
                new StringToDateTimeConverter("dd.MM.yyyy", fallback).Convert("2024-12-25"));
        }

        [Test]
        public void Convert_BlankGivesTheFallback() =>
            Assert.AreEqual(default(DateTime), new StringToDateTimeConverter().Convert(null));

        // Both halves read the same format and culture fields, so what ConvertBack writes, Convert
        // reads — the property a two-way binding on an input field depends on.
        [Test]
        public void Convert_RoundTripsAnExactFormat()
        {
            var converter = new StringToDateTimeConverter("dd.MM.yyyy");
            var date = new DateTime(2024, 12, 25);

            Assert.AreEqual("25.12.2024", converter.ConvertBack(date));
            Assert.AreEqual(date, converter.Convert(converter.ConvertBack(date)));
        }

        // With no format authored the culture's general form is written, which carries the time down
        // to the second and no further.
        [Test]
        public void Convert_WithNoFormat_RoundTripsToTheSecond()
        {
            var converter = new StringToDateTimeConverter();
            var date = new DateTime(2024, 12, 25, 13, 45, 30);

            Assert.AreEqual(date, converter.Convert(converter.ConvertBack(date)));
        }

        // A format the reading half merely refuses is one the writing half throws on, and a binder
        // pushing back must not be the thing that stops.
        [Test]
        public void ConvertBack_AnUnusableFormat_IsReported()
        {
            var converter = new StringToDateTimeConverter("Q");
            var date = new DateTime(2024, 12, 25);

            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter.*not a DateTime format"));
            Assert.AreEqual(date.ToString(CultureInfo.CurrentCulture), converter.ConvertBack(date));
        }

        // A tick count an Inspector long is free to hold but the calendar is not. It is read only on
        // the path that reaches for the fallback, so a converter whose text parses never mentions it.
        [Test]
        public void Convert_FallbackTicksOutsideTheCalendar_AreReportedAndPinned()
        {
            var converter = new StringToDateTimeConverter(format: string.Empty);
            SetField(converter, "_fallbackTicks", -1L);

            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter.*outside the range"));

            Assert.AreEqual(DateTime.MinValue, converter.Convert(string.Empty));
        }

        [Test]
        public void Convert_FallbackTicksInsideTheCalendar_AreLeftAlone()
        {
            var fallback = new DateTime(2000, 1, 1);

            Assert.AreEqual(fallback, new StringToDateTimeConverter(string.Empty, fallback).Convert(string.Empty));
            LogAssert.NoUnexpectedReceived();
        }

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field!.SetValue(target, value);
        }
    }
}
