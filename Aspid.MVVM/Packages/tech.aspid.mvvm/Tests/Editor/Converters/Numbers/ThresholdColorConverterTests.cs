using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="ThresholdColorConverter"/> — the highest qualifying stop, stop-order
    /// independence, interpolation between neighbours, and the no-stops fallback.
    /// </summary>
    /// <remarks>
    /// The parameterless constructor was made private; a converter is always built with its stops
    /// named explicitly.
    /// </remarks>
    [TestFixture]
    internal sealed class ThresholdColorConverterTests
    {
        private static ColorStop[] Stops() => new[]
        {
            new ColorStop(0.75f, Color.green),
            new ColorStop(0.25f, Color.yellow),
        };

        [Test]
        public void Convert_PicksTheHighestQualifyingStop()
        {
            var converter = new ThresholdColorConverter(Stops(), Color.red);

            Assert.AreEqual(Color.green, converter.Convert(0.8f));
            Assert.AreEqual(Color.yellow, converter.Convert(0.5f));
            Assert.AreEqual(Color.red, converter.Convert(0.1f));
        }

        // The stops are authored in whatever order the Inspector left them.
        [Test]
        public void Convert_DoesNotDependOnStopOrder()
        {
            var ascending = new ThresholdColorConverter(
                new[] { new ColorStop(0.25f, Color.yellow), new ColorStop(0.75f, Color.green) },
                Color.red);

            Assert.AreEqual(Color.green, ascending.Convert(0.8f));
        }

        [Test]
        public void Convert_Interpolate_BlendsTowardTheNextStopUp()
        {
            var converter = new ThresholdColorConverter(Stops(), Color.red, interpolate: true);

            var blended = converter.Convert(0.5f);

            Assert.Greater(blended.g, Color.yellow.g);
            Assert.Less(blended.g, Color.green.g);
        }

        [Test]
        public void Convert_NoStops_ReportsAndReturnsTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("no stops are authored"));

            Assert.AreEqual(Color.red, new ThresholdColorConverter(null, Color.red).Convert(0.5f));
        }
    }
}
