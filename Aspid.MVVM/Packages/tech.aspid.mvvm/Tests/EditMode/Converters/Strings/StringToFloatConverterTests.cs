using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToFloatConverter"/> — reading, culture and the exact round trip.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToFloatConverterTests
    {
        [TestCase("1.5", 1.5f)]
        [TestCase("-0.25", -0.25f)]
        public void Convert_ReadsOrFallsBack(string value, float expected) =>
            Assert.AreEqual(expected, new StringToFloatConverter().Convert(value), 1e-5f);

        [Test]
        public void Convert_UnreadableTextFallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToFloatConverter"));
            Assert.AreEqual(0f, new StringToFloatConverter().Convert("abc"));
        }

        // A German player typing "1,5" means one and a half; reading it as invariant gives fifteen
        // or nothing at all.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_HonoursTheCulture() =>
            Assert.AreEqual(1.5f, new StringToFloatConverter(0f).Convert("1,5"), 1e-5f);

        [Test]
        public void ConvertBack_RoundTripsExactly()
        {
            const float value = 1.1f;
            var converter = new StringToFloatConverter();

            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)));
        }
    }
}
