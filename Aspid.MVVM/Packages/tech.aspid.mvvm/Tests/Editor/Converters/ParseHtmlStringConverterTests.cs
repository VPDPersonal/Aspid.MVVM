using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="ParseHtmlStringConverter"/> and, through it, the shared
    /// <see cref="ConverterFailureMode"/> vocabulary.
    /// </summary>
    [TestFixture]
    internal sealed class ParseHtmlStringConverterTests
    {
        [TestCase("#FF0000", 1f, 0f, 0f)]
        [TestCase("#00FF00", 0f, 1f, 0f)]
        [TestCase("red", 1f, 0f, 0f)]
        public void Convert_ParsesAnHtmlColour(string html, float r, float g, float b) =>
            Assert.AreEqual(new Color(r, g, b), new ParseHtmlStringConverter().Convert(html));

        [Test]
        public void Convert_UnparseableString_ReturnsTheFallbackAndReportsOnce()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not an HTML colour"));

            var converter = new ParseHtmlStringConverter(Color.magenta);

            Assert.AreEqual(Color.magenta, converter.Convert("not a colour"));
            converter.Convert("still not");
            converter.Convert("nor this");
        }

        [Test]
        public void Convert_Null_ReturnsTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not an HTML colour"));

            Assert.AreEqual(Color.magenta, new ParseHtmlStringConverter(Color.magenta).Convert(null));
        }

        [Test]
        public void Convert_ThrowMode_Throws() =>
            Assert.Throws<ArgumentException>(
                () => new ParseHtmlStringConverter(Color.magenta, ConverterFailureMode.Throw).Convert("nope"));

        // The default fallback is transparent black, which "#00000000" also parses to — so before the
        // failure was reported these two cases were indistinguishable in the scene.
        [Test]
        public void Convert_FailureAndTransparentBlack_AreNowDistinguishable()
        {
            var converter = new ParseHtmlStringConverter();

            Assert.AreEqual(new Color(0, 0, 0, 0), converter.Convert("#00000000"));
            LogAssert.NoUnexpectedReceived();

            LogAssert.Expect(LogType.Error, new Regex("is not an HTML colour"));
            Assert.AreEqual(new Color(0, 0, 0, 0), converter.Convert("nope"));
        }

        // The input is a string and the output a colour, so there is no input to return.
        [Test]
        public void Convert_ReturnInputMode_BehavesAsReturnFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not an HTML colour"));

            var converter = new ParseHtmlStringConverter(Color.magenta, ConverterFailureMode.ReturnInput);

            Assert.AreEqual(Color.magenta, converter.Convert("nope"));
        }
    }
}
