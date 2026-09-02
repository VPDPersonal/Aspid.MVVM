#nullable enable
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DictionaryLookupConverter{TKey,TValue}"/> — the table scan, the
    /// fallback, and the default-equality quirks (null keys, NaN) it inherits.
    /// </summary>
    [TestFixture]
    public sealed class DictionaryLookupConverterTests
    {
        [TestCase("Fire", "Red")]
        [TestCase("Ice", "Blue")]
        public void Lookup_KeyInTheTable_ReturnsItsValue(string key, string expected) =>
            Assert.AreEqual(expected, Elements().Convert(key));

        [TestCase("Shock")]
        [TestCase("")]
        public void Lookup_KeyNotInTheTable_ReturnsTheFallback(string key) =>
            Assert.AreEqual("Grey", Elements().Convert(key));

        // String keys are compared with the type's own equality, which is ordinal and case-sensitive.
        // An id that arrives from a save file or a server in the wrong case finds nothing.
        [TestCase("fire")]
        [TestCase("FIRE")]
        [TestCase("Fire ")]
        public void Lookup_StringKeys_AreMatchedCaseAndWhitespaceSensitively(string key) =>
            Assert.AreEqual("Grey", Elements().Convert(key));

        [Test]
        public void Lookup_EmptyTable_ReturnsTheFallback() =>
            Assert.AreEqual(
                "Grey",
                new DictionaryLookupConverter<string, string>(System.Array.Empty<LookupEntry<string, string>>(), "Grey")
                    .Convert("Fire"));

        // A null map is coerced to an empty one by the constructor rather than throwing on the first
        // push, which is what an unassigned array field deserializes as.
        [Test]
        public void Lookup_NullTable_ReturnsTheFallback() =>
            Assert.AreEqual("Grey", new DictionaryLookupConverter<string, string>(null, "Grey").Convert("Fire"));

        [Test]
        public void Lookup_DefaultConstructed_ReturnsTheTypeDefault() =>
            Assert.IsNull(new DictionaryLookupConverter<string, string>().Convert("Fire"));

        // The scan stops at the first match, so a table with a key listed twice answers with the upper
        // row. Authoring order decides, and the duplicate is reported.
        [Test]
        public void Lookup_DuplicateKeys_ReportsItAndAnswersWithTheFirstRow()
        {
            var converter = new DictionaryLookupConverter<string, string>(
                new[]
                {
                    Row("Fire", "Red"),
                    Row("Fire", "Orange"),
                },
                "Grey");

            LogAssert.Expect(LogType.Error, new Regex("DictionaryLookupConverter.*listed more than once"));

            Assert.AreEqual("Red", converter.Convert("Fire"));
        }

        // A row whose value happens to equal default(TValue) is a listed key, not a missing one. An
        // implementation that used the value to signal "not found" would answer -1 here.
        [Test]
        public void Lookup_ValueEqualToTheTypeDefault_IsReturnedRatherThanTheFallback()
        {
            var converter = new DictionaryLookupConverter<string, int>(new[] { Row("None", 0) }, -1);

            Assert.AreEqual(0, converter.Convert("None"));
            Assert.AreEqual(-1, converter.Convert("Fire"));
        }

        // The default comparer says two nulls are equal, so a row left with its key unassigned is not
        // inert: it answers every push of a null id, ahead of the fallback the author expected.
        [Test]
        public void Lookup_NullKey_IsMatchedByARowWithAnUnassignedKey()
        {
            var converter = new DictionaryLookupConverter<string, string>(
                new[]
                {
                    Row<string, string>(null, "Unset"),
                    Row("Fire", "Red"),
                },
                "Grey");

            Assert.AreEqual("Unset", converter.Convert(null));
        }

        [Test]
        public void Lookup_NullKey_WithNoSuchRow_ReturnsTheFallback() =>
            Assert.AreEqual("Grey", Elements().Convert(null));

        // The default comparer calls Equals, not ==, and float.Equals reports two NaNs as equal. A
        // NaN key is therefore matchable — the opposite of what the operator would do, and the reason
        // a NaN pushed by a broken calculation lands on a row instead of the fallback.
        [Test]
        public void Lookup_NaNKey_IsMatchedByARowKeyedByNaN()
        {
            var converter = new DictionaryLookupConverter<float, string>(
                new[]
                {
                    Row(float.NaN, "Broken"),
                    Row(1f, "One"),
                },
                "Grey");

            Assert.AreEqual("Broken", converter.Convert(float.NaN));
            Assert.AreEqual("One", converter.Convert(1f));
        }

        [TestCase(0, "None")]
        [TestCase(2, "Silver")]
        public void Lookup_IntKeys_AreMatchedByValue(int key, string expected) =>
            Assert.AreEqual(
                expected,
                new DictionaryLookupConverter<int, string>(new[] { Row(0, "None"), Row(2, "Silver") }, "?")
                    .Convert(key));

        private static DictionaryLookupConverter<string, string> Elements() =>
            new(new[] { Row("Fire", "Red"), Row("Ice", "Blue") }, "Grey");

        private static LookupEntry<TKey, TValue> Row<TKey, TValue>(TKey key, TValue value) =>
            new(key, value);
    }
}
