using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ParseHtmlStringConverter"/> and, through it, the shared shape a
    /// converter reports a failure in.
    /// </summary>
    [TestFixture]
    public sealed class ParseHtmlStringConverterTests
    {
        [TestCase("#FF0000", 1f, 0f, 0f)]
        [TestCase("#00FF00", 0f, 1f, 0f)]
        [TestCase("red", 1f, 0f, 0f)]
        public void Convert_ParsesAnHtmlColor(string html, float r, float g, float b) =>
            Assert.AreEqual(new Color(r, g, b), new ParseHtmlStringConverter().Convert(html));

        [Test]
        public void Convert_UnparseableString_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("ParseHtmlStringConverter.*an HTML color"));

            var converter = new ParseHtmlStringConverter(Color.magenta);

            Assert.AreEqual(Color.magenta, converter.Convert("not a colour"));
            converter.Convert("still not");
            converter.Convert("nor this");
        }

        // A blank string is no value rather than a failed parse, so it answers with the fallback and
        // reports nothing.
        [Test]
        public void Convert_Null_ReturnsTheFallbackSilently()
        {
            Assert.AreEqual(Color.magenta, new ParseHtmlStringConverter(Color.magenta).Convert(null));
            LogAssert.NoUnexpectedReceived();
        }

        // The default fallback is transparent black, which "#00000000" also parses to — so before the
        // failure was reported these two cases were indistinguishable in the scene.
        [Test]
        public void Convert_FailureAndTransparentBlack_AreNowDistinguishable()
        {
            var converter = new ParseHtmlStringConverter();

            Assert.AreEqual(new Color(0, 0, 0, 0), converter.Convert("#00000000"));
            LogAssert.NoUnexpectedReceived();

            LogAssert.Expect(LogType.Error, new Regex("ParseHtmlStringConverter.*an HTML color"));
            Assert.AreEqual(new Color(0, 0, 0, 0), converter.Convert("nope"));
        }

        [Test]
        public void ConvertBack_AnyColor_WritesTheHexWithItsAlpha() =>
            Assert.AreEqual("#FF0000FF", new ParseHtmlStringConverter().ConvertBack(Color.red));

        // The alpha pair is written even when it is opaque: dropping it would make the reverse
        // direction lossy for every translucent color a two-way binding pushes back.
        [Test]
        public void ConvertBack_TranslucentColor_RoundTripsThroughConvert()
        {
            var converter = new ParseHtmlStringConverter();
            var parsed = converter.Convert(converter.ConvertBack(new Color(0.2f, 0.4f, 0.6f, 0.3f)));

            Assert.AreEqual(0.2f, parsed.r, 0.01f);
            Assert.AreEqual(0.3f, parsed.a, 0.01f);
        }

        // The hex formatting lives in one place, so this direction writes exactly what
        // ColorToHtmlStringConverter writes when it is asked for the alpha pair.
        [Test]
        public void ConvertBack_WritesWhatColorToHtmlStringConverterWrites()
        {
            var color = new Color(0.2f, 0.4f, 0.6f, 0.3f);

            Assert.AreEqual(
                new ColorToHtmlStringConverter(includeAlpha: true).Convert(color),
                new ParseHtmlStringConverter().ConvertBack(color));
        }
    }
}
