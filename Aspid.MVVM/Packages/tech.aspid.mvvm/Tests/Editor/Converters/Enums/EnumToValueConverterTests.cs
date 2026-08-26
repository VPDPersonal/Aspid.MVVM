using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumToValueConverter{TEnum, T}"/> — the duplicate-key report and the
    /// empty-map default.
    /// </summary>
    /// <remarks>
    /// The map-and-fallback happy path is covered by <c>EssentialConverterTests</c>; this fixture
    /// covers what that one does not: a key listed twice, and the parameterless constructor.
    /// </remarks>
    [TestFixture]
    internal sealed class EnumToValueConverterTests
    {
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
