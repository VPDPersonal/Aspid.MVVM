using System;
using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="HashToColorConverter"/> — the stable per-name color, the blank-name
    /// fallback, and the saturation range.
    /// </summary>
    /// <remarks>
    /// The hash is FNV-1a rather than <c>string.GetHashCode</c>, which is randomised per process and
    /// would give the same name a different color on every launch.
    /// </remarks>
    [TestFixture]
    public sealed class HashToColorConverterTests
    {
        [Test]
        public void HashToColor_IsStableForTheSameName()
        {
            var converter = new HashToColorConverter();

            Assert.AreEqual(converter.Convert("Vladislav"), converter.Convert("Vladislav"));
            Assert.AreNotEqual(converter.Convert("Vladislav"), converter.Convert("Someone"));
        }

        [Test]
        public void HashToColor_BlankGivesTheFallback() =>
            Assert.AreEqual(Color.gray, new HashToColorConverter().Convert(null));

        [Test]
        public void HashToColor_AuthoredFallback_IsUsedForABlankName() =>
            Assert.AreEqual(
                Color.magenta,
                new HashToColorConverter(0.6f, fallback: Color.magenta).Convert(string.Empty));

        // HSVToRGB reads a saturation or value outside 0..1 as a color outside the gamut. [Range(0, 1)]
        // holds the Inspector, so the constructor refuses one.
        [TestCase(4f, 0.9f)]
        [TestCase(-0.1f, 0.9f)]
        [TestCase(0.6f, 1.5f)]
        public void Constructor_SaturationOrValueOutsideTheRange_Throws(float saturation, float value) =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashToColorConverter(saturation, value));
    }
}
