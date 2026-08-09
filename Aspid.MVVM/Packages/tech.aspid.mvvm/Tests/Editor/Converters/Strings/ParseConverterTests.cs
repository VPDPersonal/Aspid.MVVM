using System;
using System.Threading;
using NUnit.Framework;
using System.Globalization;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the parsing converters — the direction from text back into a value.
    /// </summary>
    /// <remarks>
    /// Parsing currently lives inside <c>InputFieldBinder</c>, hard-coded: no culture, no fallback,
    /// and a failed parse silently swallows the event. These make those decisions authorable, and the
    /// failure rows below are the ones the binder gets wrong today.
    /// </remarks>
    [TestFixture]
    internal sealed class ParseConverterTests
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

        [TestCase("42", 42)]
        [TestCase("-7", -7)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        [TestCase("abc", 0)]
        [TestCase("1.5", 0)]
        public void StringToInt_ReadsOrFallsBack(string value, int expected) =>
            Assert.AreEqual(expected, new StringToIntConverter().Convert(value));

        [Test]
        public void StringToInt_UsesTheAuthoredFallback() =>
            Assert.AreEqual(-1, new StringToIntConverter(-1).Convert("nonsense"));

        [Test]
        public void StringToInt_RoundTrips()
        {
            var converter = new StringToIntConverter();

            Assert.AreEqual(42, converter.Convert(converter.ConvertBack(42)));
        }

        [Test]
        public void StringToLong_Reads() =>
            Assert.AreEqual(9_000_000_000L, new StringToLongConverter().Convert("9000000000"));

        [TestCase("1.5", 1.5f)]
        [TestCase("-0.25", -0.25f)]
        [TestCase("abc", 0f)]
        public void StringToFloat_ReadsOrFallsBack(string value, float expected) =>
            Assert.AreEqual(expected, new StringToFloatConverter().Convert(value), 1e-5f);

        // A German player typing "1,5" means one and a half; reading it as invariant gives fifteen
        // or nothing at all.
        [Test]
        public void StringToFloat_HonoursTheCulture()
        {
            var german = new StringToFloatConverter(0f, CultureInfoMode.CurrentCulture);
            var previous = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
                Assert.AreEqual(1.5f, german.Convert("1,5"), 1e-5f);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previous;
            }
        }

        [TestCase("true", true)]
        [TestCase("TRUE", true)]
        [TestCase("1", true)]
        [TestCase("yes", true)]
        [TestCase("on", true)]
        [TestCase("false", false)]
        [TestCase("0", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void StringToBool_ReadsTheUsualSpellings(string value, bool expected) =>
            Assert.AreEqual(expected, new StringToBoolParseConverter().Convert(value));

        [Test]
        public void StringToBool_TakesAuthoredSpellings() =>
            Assert.IsTrue(new StringToBoolParseConverter(new[] { "да" }).Convert("ДА"));

        [TestCase("Rain", Weather.Rain)]
        [TestCase("rain", Weather.Rain)]
        [TestCase("nonsense", Weather.Clear)]
        [TestCase("", Weather.Clear)]
        public void StringToEnum_ReadsTheMember(string value, Weather expected) =>
            Assert.AreEqual(expected, new StringToEnumConverter<Weather>(Weather.Clear).Convert(value));

        // Enum.TryParse accepts a bare number and hands back an undeclared member for it, which is
        // rarely what a name-shaped input means.
        [Test]
        public void StringToEnum_RejectsANumberThatNamesNoMember() =>
            Assert.AreEqual(Weather.Clear, new StringToEnumConverter<Weather>(Weather.Clear).Convert("99"));

        [Test]
        public void StringToEnum_RoundTrips()
        {
            var converter = new StringToEnumConverter<Weather>(Weather.Clear);

            Assert.AreEqual(Weather.Snow, converter.Convert(converter.ConvertBack(Weather.Snow)));
        }

        [Test]
        public void StringToDateTime_ReadsAnExactFormat() =>
            Assert.AreEqual(
                new DateTime(2024, 12, 25),
                new StringToDateTimeConverter("dd.MM.yyyy").Convert("25.12.2024"));

        [Test]
        public void StringToDateTime_WrongFormatGivesTheFallback()
        {
            var fallback = new DateTime(2000, 1, 1);

            Assert.AreEqual(
                fallback,
                new StringToDateTimeConverter("dd.MM.yyyy", fallback).Convert("2024-12-25"));
        }

        [Test]
        public void StringToDateTime_BlankGivesTheFallback() =>
            Assert.AreEqual(default(DateTime), new StringToDateTimeConverter().Convert(null));
    }
}
