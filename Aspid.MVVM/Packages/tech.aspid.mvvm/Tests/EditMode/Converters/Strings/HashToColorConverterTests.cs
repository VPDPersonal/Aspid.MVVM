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

        // The Range attribute only holds the Inspector; the constructor takes any float, and HSVToRGB
        // reads a saturation above one as a color outside the gamut.
        [Test]
        public void HashToColor_SaturationOutsideTheRange_IsHeldToIt() =>
            Assert.AreEqual(
                new HashToColorConverter(1f).Convert("Vladislav"),
                new HashToColorConverter(4f).Convert("Vladislav"));
    }
}
