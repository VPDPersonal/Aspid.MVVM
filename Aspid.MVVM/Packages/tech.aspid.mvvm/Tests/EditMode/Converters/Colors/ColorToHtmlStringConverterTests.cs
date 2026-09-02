using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorToHtmlStringConverter"/> — writing the hex, and undoing it through
    /// the shared parse path <see cref="ParseHtmlStringConverter"/> also uses.
    /// </summary>
    [TestFixture]
    public sealed class ColorToHtmlStringConverterTests
    {
        [Test]
        public void ColorToHtmlString_WritesTheHex() =>
            Assert.AreEqual("#FF0000", new ColorToHtmlStringConverter().Convert(Color.red));

        [Test]
        public void ColorToHtmlString_RoundTripsThroughParseHtmlString()
        {
            var text = new ColorToHtmlStringConverter(includeAlpha: true).Convert(new Color(0.2f, 0.4f, 0.6f, 0.8f));
            var parsed = new ParseHtmlStringConverter().Convert(text);

            Assert.AreEqual(0.2f, parsed.r, 0.01f);
            Assert.AreEqual(0.8f, parsed.a, 0.01f);
        }

        [TestCase("#FF0000")]
        [TestCase("red")]
        public void ColorToHtmlString_ConvertBack_ParsesAnHtmlColor(string html) =>
            Assert.AreEqual(Color.red, new ColorToHtmlStringConverter().ConvertBack(html));

        [Test]
        public void ColorToHtmlString_ConvertBack_RoundTripsItsOwnOutput()
        {
            var converter = new ColorToHtmlStringConverter(includeAlpha: true);
            var parsed = converter.ConvertBack(converter.Convert(new Color(0.2f, 0.4f, 0.6f, 0.8f)));

            Assert.AreEqual(0.2f, parsed.r, 0.01f);
            Assert.AreEqual(0.8f, parsed.a, 0.01f);
        }

        // A blank string is no value rather than a failed parse, in this direction too.
        [TestCase((string)null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ColorToHtmlString_ConvertBack_BlankString_ReturnsTheFallbackSilently(string html)
        {
            var converter = new ColorToHtmlStringConverter(includeAlpha: true, convertBackFallback: Color.magenta);

            Assert.AreEqual(Color.magenta, converter.ConvertBack(html));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void ColorToHtmlString_ConvertBack_UnparseableString_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("ColorToHtmlStringConverter.*an HTML color"));

            var converter = new ColorToHtmlStringConverter(includeAlpha: true, convertBackFallback: Color.magenta);

            Assert.AreEqual(Color.magenta, converter.ConvertBack("not a colour"));
            converter.ConvertBack("still not");
            converter.ConvertBack("nor this");
        }

        // The two converters share one parse path, so the reverse direction answers exactly as
        // ParseHtmlStringConverter.Convert does — down to the default fallback.
        [Test]
        public void ColorToHtmlString_ConvertBack_WithoutAFallback_AnswersLikeParseHtmlString()
        {
            LogAssert.Expect(LogType.Error, new Regex("ParseHtmlStringConverter.*an HTML color"));
            var expected = new ParseHtmlStringConverter().Convert("nope");

            LogAssert.Expect(LogType.Error, new Regex("ColorToHtmlStringConverter.*an HTML color"));
            Assert.AreEqual(expected, new ColorToHtmlStringConverter().ConvertBack("nope"));
        }
    }
}
