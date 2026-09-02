using System;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CollectionCountToStringConverter{T}"/> — the count word it delegates
    /// to a <see cref="PluralizeConverter"/>, the empty caption, and the walked-sequence path.
    /// </summary>
    [TestFixture]
    public sealed class CollectionCountToStringConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [TestCase(1, "1 one")]
        [TestCase(2, "2 many")]
        [TestCase(5, "5 many")]
        public void Count_English_PicksTheFormForTheCount(int count, string expected) =>
            Assert.AreEqual(expected, English().Convert(Items(count)));

        // The constructor's zero text defaults to null, which means "no separate empty caption" — so a
        // converter built in code writes an empty collection like any other count rather than
        // suppressing the number.
        [Test]
        public void Count_NoZeroTextAuthored_EmptyCollectionIsFormattedLikeAnyOtherCount() =>
            Assert.AreEqual("0 many", English().Convert(Array.Empty<string>()));

        [Test]
        public void Count_Null_IsCountedAsZero() =>
            Assert.AreEqual("0 many", English().Convert(null));

        // The reason the class exists rather than a Count converter chained into Pluralize: the empty
        // caption is a phrase of its own, with no count in front of it.
        [Test]
        public void Count_ZeroTextAuthored_IsWrittenWithoutTheCount()
        {
            var converter = new CollectionCountToStringConverter<string>(
                new PluralizeConverter(new EnglishPluralRule("one", "many")),
                zeroText: "Empty");

            Assert.AreEqual("Empty", converter.Convert(Array.Empty<string>()));
        }

        [Test]
        public void Count_ZeroTextAuthored_NullCollection_AlsoWritesIt() =>
            Assert.AreEqual(
                "Empty",
                new CollectionCountToStringConverter<string>(
                        new PluralizeConverter(new EnglishPluralRule("one", "many")), "Empty")
                    .Convert(null));

        // The parameterless constructor is what an Inspector-authored converter starts as, and its
        // defaults differ from the code path above: a zero text of "Empty" is already filled in.
        [TestCase(0, "Empty")]
        [TestCase(1, "1 item")]
        [TestCase(2, "2 items")]
        public void Count_DefaultConstructed_UsesTheInspectorDefaults(int count, string expected) =>
            Assert.AreEqual(expected, new CollectionCountToStringConverter<string>().Convert(Items(count)));

        // A grammar of three words reaches the phrase intact; the grammar itself, teens and all, is
        // covered in PluralRuleTests rather than through this converter.
        [TestCase(1, "1 one")]
        [TestCase(2, "2 few")]
        [TestCase(5, "5 many")]
        [TestCase(11, "11 many")]
        public void Count_Slavic_PicksTheFormForTheCount(int count, string expected) =>
            Assert.AreEqual(expected, Slavic().Convert(Items(count)));

        // Zero ends in zero, so it is not a teen and not a one — it lands on the many form, which is
        // the correct Russian reading and the only answer available when no empty caption is authored.
        [Test]
        public void Count_Slavic_ZeroWithoutAZeroText_UsesTheManyForm() =>
            Assert.AreEqual("0 many", Slavic().Convert(Array.Empty<string>()));

        // Only the size is read, so two different collections of the same size are one and the same
        // question to this converter — worth stating, since the items are never looked at.
        [Test]
        public void Count_DifferentCollectionOfTheSameSize_IsTheSamePhrase()
        {
            var converter = English();

            Assert.AreEqual(converter.Convert(new string[3]), converter.Convert(_three));
        }

        // One instance answers a whole sequence of counts, so nothing may be carried over between
        // pushes — zero in particular must not be confused with "nothing answered yet".
        [Test]
        public void Count_CountChanges_RewordsThePhrase()
        {
            var converter = English();

            Assert.AreEqual("0 many", converter.Convert(Array.Empty<string>()));
            Assert.AreEqual("1 one", converter.Convert(Items(1)));
            Assert.AreEqual("3 many", converter.Convert(Items(3)));
            Assert.AreEqual("1 one", converter.Convert(Items(1)));
        }

        // A word the grammar reaches for and the Inspector does not carry is reported by the grammar
        // itself, so the report names that rather than this converter — and the phrase keeps the count.
        [Test]
        public void Count_UnauthoredWord_ReportsItAndLeavesTheWordOut()
        {
            ExpectMissingWordError();

            Assert.AreEqual("2 ", Unworded().Convert(Items(2)));
        }

        // Nothing about the broken phrase is remembered between pushes, so the console says so every
        // time rather than once per count.
        [Test]
        public void Count_UnauthoredWord_ReportsItOnEveryPush()
        {
            var converter = Unworded();

            ExpectMissingWordError();
            ExpectMissingWordError();

            converter.Convert(Items(2));
            converter.Convert(Items(2));
        }

        // An authored empty caption returns before the grammar is consulted, so a converter missing
        // the word for one still answers an empty collection quite happily, and says nothing until
        // something is put in it.
        [Test]
        public void Count_UnauthoredWord_EmptyCollectionWithAZeroText_ReturnsTheZeroTextInstead() =>
            Assert.AreEqual(
                "Empty",
                new CollectionCountToStringConverter<string>(
                        new PluralizeConverter(new EnglishPluralRule("one", string.Empty)), "Empty")
                    .Convert(Array.Empty<string>()));

        // An iterator carries no count of its own, so the counted-collection member cannot take it at
        // all: it arrives through the sequence member and is walked to be counted.
        [TestCase(1, "1 one")]
        [TestCase(2, "2 many")]
        public void Count_WalkedSequence_PicksTheFormForTheCount(int count, string expected) =>
            Assert.AreEqual(expected, EnglishSequence().Convert(Streamed(Items(count))));

        [Test]
        public void Count_WalkedSequence_EmptyIsWordedLikeAnyOtherCount() =>
            Assert.AreEqual("0 many", EnglishSequence().Convert(Streamed<string>()));

        [Test]
        public void Count_WalkedSequence_Null_IsCountedAsZero() =>
            Assert.AreEqual("0 many", EnglishSequence().Convert(null));

        // The empty caption is a phrase of its own on this member too, not only on the counted one.
        [Test]
        public void Count_WalkedSequence_ZeroTextAuthored_IsWrittenWithoutTheCount() =>
            Assert.AreEqual("Empty", CaptionedSequence().Convert(Streamed<string>()));

        [Test]
        public void Count_List_ArrivingAsASequence_IsStillWorded() =>
            Assert.AreEqual("2 many", EnglishSequence().Convert(new List<string> { "a", "b" }));

        // A sequence that knows its own size is asked for it rather than walked; the fake throws on
        // being enumerated, so the fast path is asserted rather than assumed.
        [Test]
        public void Count_SequenceWithACount_TakesItRatherThanWalking()
        {
            Assert.AreEqual("3 many", EnglishSequence().Convert(new CountedOnlyCollection(3)));
            Assert.AreEqual("3 many", EnglishSequence().Convert(new MutableCountedOnlyCollection(3)));
        }

        // Only Count is read off the collection, so the items themselves are of no interest here.
        private static string[] Items(int count) => new string[count];

        private static CollectionCountToStringConverter<string> English() =>
            new(new PluralizeConverter(new EnglishPluralRule("one", "many")));

        private static CollectionCountToStringConverter<string> Slavic() =>
            new(new PluralizeConverter(new EastSlavicPluralRule("one", "few", "many")));

        private static CollectionCountToStringConverter<string> Unworded() =>
            new(new PluralizeConverter(new EnglishPluralRule("one", string.Empty)));

        private static void ExpectMissingWordError() =>
            LogAssert.Expect(LogType.Error, new Regex("PluralRule.*no word is authored"));

        // The sequence member is explicit, so these calls go through the interface a binding would
        // use rather than the public method.
        private static IConverter<IEnumerable<string>, string> EnglishSequence() => English();

        private static IConverter<IEnumerable<string>, string> CaptionedSequence() =>
            new CollectionCountToStringConverter<string>(
                new PluralizeConverter(new EnglishPluralRule("one", "many")), "Empty");

        private static IEnumerable<T> Streamed<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }
    }
}
