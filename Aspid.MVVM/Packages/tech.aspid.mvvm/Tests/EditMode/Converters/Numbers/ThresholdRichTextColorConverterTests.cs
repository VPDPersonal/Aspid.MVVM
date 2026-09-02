using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ThresholdRichTextColorConverter"/> — the stop pick, the no-stops
    /// fallback and the pluggable number converter.
    /// </summary>
    [TestFixture]
    public sealed class ThresholdRichTextColorConverterTests
    {
        // Explicit channels rather than Color.yellow, which is #FFEB04 in Unity rather than #FFFF00.
        private static readonly Color _pureYellow = new(1f, 1f, 0f);

        [Test]
        public void Convert_PicksTheHighestQualifyingStop()
        {
            var converter = new ThresholdRichTextColorConverter(
                new[]
                {
                    new ColorStop(0.75f, Color.green),
                    new ColorStop(0.25f, _pureYellow),
                },
                fallback: Color.red);

            Assert.AreEqual("<color=#00FF00>0.8</color>", converter.Convert(0.8f));
            Assert.AreEqual("<color=#FFFF00>0.5</color>", converter.Convert(0.5f));
            Assert.AreEqual("<color=#FF0000>0.1</color>", converter.Convert(0.1f));
        }

        // The stops are authored in whatever order the Inspector left them.
        [Test]
        public void Convert_DoesNotDependOnStopOrder()
        {
            var ascending = new ThresholdRichTextColorConverter(
                new[]
                {
                    new ColorStop(0.25f, _pureYellow),
                    new ColorStop(0.75f, Color.green),
                },
                fallback: Color.red);

            Assert.AreEqual("<color=#00FF00>0.8</color>", ascending.Convert(0.8f));
        }

        // The number slot takes any converter, so the text inside the tag is not limited to the
        // default numeric format.
        [Test]
        public void Convert_NumberConverter_WritesTheNumber()
        {
            var converter = new ThresholdRichTextColorConverter(
                new[] { new ColorStop(0.25f, _pureYellow) },
                fallback: Color.red,
                number: new NumberFormatConverter("F2", CultureInfoMode.InvariantCulture));

            Assert.AreEqual("<color=#FFFF00>0.50</color>", converter.Convert(0.5f));
        }

        // An empty stop table is a converter that can never pick anything, so it is reported rather
        // than quietly painting everything the fallback color. The number is still written and still
        // wrapped, so the tag proves the failure happened inside the color pick alone.
        [Test]
        public void Convert_NoStops_ReportsItAndUsesTheFallbackColor()
        {
            LogAssert.Expect(LogType.Error, new Regex("ThresholdRichTextColorConverter.*no stops are authored"));

            var converter = new ThresholdRichTextColorConverter(
                System.Array.Empty<ColorStop>(),
                fallback: Color.red,
                number: new NumberFormatConverter("F2", CultureInfoMode.InvariantCulture));

            Assert.AreEqual("<color=#FF0000>0.50</color>", converter.Convert(0.5f));
        }
    }
}
