using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="GradientEvaluateConverter"/> — the input-range normalization, the
    /// missing-gradient fallback, and the degenerate-range guard.
    /// </summary>
    [TestFixture]
    internal sealed class GradientEvaluateConverterTests
    {
        private static Gradient BlackToWhite()
        {
            var gradient = new Gradient();
            gradient.SetKeys(
                new[] { new GradientColorKey(Color.black, 0f), new GradientColorKey(Color.white, 1f) },
                new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
            return gradient;
        }

        [Test]
        public void Convert_MapsTheInputRangeOntoTheGradient()
        {
            var converter = new GradientEvaluateConverter(BlackToWhite(), inputMin: 0f, inputMax: 100f);

            Assert.AreEqual(0.5f, converter.Convert(50f).r, 1e-4f);
        }

        [Test]
        public void Convert_NoGradient_ReportsAndReturnsWhite()
        {
            LogAssert.Expect(LogType.Error, new Regex("no gradient is assigned"));

            Assert.AreEqual(Color.white, new GradientEvaluateConverter(null!).Convert(0.5f));
        }

        // Every input would land on the same place in the ramp, so the gradient could only ever
        // answer with one color.
        [Test]
        public void Convert_DegenerateInputRange_ReportsAndReadsTheStart()
        {
            LogAssert.Expect(LogType.Error, new Regex("the input range is empty"));

            Assert.AreEqual(Color.black.r, new GradientEvaluateConverter(BlackToWhite(), 5f, 5f).Convert(5f).r, 1e-4f);
        }
        [Test]
        public void GradientEvaluate_Double_ReadsTheSameStop()
        {
            var gradient = new Gradient();
            Assert.AreEqual(
                new GradientEvaluateConverter(gradient).Convert(0.5f),
                ((IConverter<double, Color>)new GradientEvaluateConverter(gradient)).Convert(0.5d));
        }

    }
}
