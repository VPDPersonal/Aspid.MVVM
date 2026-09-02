using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumToValueConverter{TEnum, T}"/> — the map-and-fallback happy path,
    /// the duplicate-key report, and the empty-map default.
    /// </summary>
    [TestFixture]
    public sealed class EnumToValueConverterTests
    {
        [Test]
        public void Convert_MapsAndFallsBack()
        {
            var converter = new EnumToValueConverter<Weather, Color>(
                new EnumToValueConverter<Weather, Color>.Entry[]
                {
                    new(Weather.Clear, Color.yellow),
                    new(Weather.Rain, Color.blue),
                },
                fallback: Color.gray);

            Assert.AreEqual(Color.yellow, converter.Convert(Weather.Clear));
            Assert.AreEqual(Color.blue, converter.Convert(Weather.Rain));
            Assert.AreEqual(Color.gray, converter.Convert(Weather.Snow));
        }

        [Test]
        public void Convert_EmptyMap_ReturnsTheFallback() =>
            Assert.AreEqual(
                Color.gray,
                new EnumToValueConverter<Weather, Color>(null, Color.gray).Convert(Weather.Clear));

        [Test]
        public void Convert_DefaultConstructed_HasAnEmptyMapAndReturnsTheDefaultFallback() =>
            Assert.AreEqual(0, new EnumToValueConverter<Medal, int>().Convert(Medal.Bronze));

        [Test]
        public void Convert_KeyListedTwice_ReportsItAndAnswersWithTheFirstEntry()
        {
            var converter = new EnumToValueConverter<Medal, string>(
                new[]
                {
                    new EnumToValueConverter<Medal, string>.Entry(Medal.Bronze, "first"),
                    new EnumToValueConverter<Medal, string>.Entry(Medal.Bronze, "second"),
                },
                fallback: "none");

            LogAssert.Expect(LogType.Error, new Regex("EnumToValueConverter.*listed more than once"));

            Assert.AreEqual("first", converter.Convert(Medal.Bronze));
        }

        // The array is copied on construction, so mutating the one the caller kept a reference to
        // must not reach into the converter's map afterwards.
        [Test]
        public void Convert_MapArray_IsCopiedOnConstruction()
        {
            var entries = new[] { new EnumToValueConverter<Medal, string>.Entry(Medal.Bronze, "before") };
            var converter = new EnumToValueConverter<Medal, string>(entries, fallback: "none");

            entries[0] = new EnumToValueConverter<Medal, string>.Entry(Medal.Bronze, "after");

            Assert.AreEqual("before", converter.Convert(Medal.Bronze));
        }
    }
}
