using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DateTimeFormatConverter"/> — the format string, the local-time
    /// conversion, and the fallback for a broken format.
    /// </summary>
    [TestFixture]
    public sealed class DateTimeFormatConverterTests
    {
        private static readonly DateTime _moment = new(2024, 12, 25, 10, 30, 0);

        [Test]
        public void Convert_UsesTheAuthoredFormat() =>
            Assert.AreEqual(
                "25.12.2024",
                new DateTimeFormatConverter("dd.MM.yyyy", culture: CultureInfoMode.InvariantCulture).Convert(_moment));

        [Test]
        public void Convert_ConvertsToLocalTimeWhenAsked()
        {
            var utc = DateTime.SpecifyKind(_moment, DateTimeKind.Utc);
            var converter = new DateTimeFormatConverter("o", toLocalTime: true, culture: CultureInfoMode.InvariantCulture);

            Assert.AreEqual(DateTimeKind.Local, DateTime.Parse(converter.Convert(utc), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).Kind);
        }

        [Test]
        public void Convert_ABrokenFormat_FallsBackToTheDefaultRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a DateTime format"));

            Assert.AreEqual(
                _moment.ToString(CultureInfo.InvariantCulture),
                new DateTimeFormatConverter("'unterminated", culture: CultureInfoMode.InvariantCulture).Convert(_moment));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Convert_AnEmptyFormat_UsesTheDefaultRendering(string format) =>
            Assert.AreEqual(
                _moment.ToString(CultureInfo.InvariantCulture),
                new DateTimeFormatConverter(format, culture: CultureInfoMode.InvariantCulture).Convert(_moment));
    }
}
