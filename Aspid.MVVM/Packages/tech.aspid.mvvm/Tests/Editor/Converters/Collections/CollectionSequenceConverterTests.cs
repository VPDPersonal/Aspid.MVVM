using System;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Collections;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the collection sequence and lookup converters:
    /// <see cref="CollectionFirstConverter{T}"/>, <see cref="CollectionLastConverter{T}"/>,
    /// <see cref="CollectionTakeConverter{T}"/>, <see cref="CollectionCountToStringConverter{T}"/>
    /// and <see cref="DictionaryLookupConverter{TKey, TValue}"/>.
    /// </summary>
    /// <remarks>
    /// Each sequence converter branches on whatever collection interface carries the index or the
    /// count it needs and walks the sequence otherwise, so every behavioral assertion is made against
    /// both paths, and the walking path is pinned on how many items it pulls — the property a rewrite
    /// silently loses.
    /// <para>
    /// The list <see cref="CollectionTakeConverter{T}"/> hands out is shared to avoid allocating per
    /// push, and is asserted by reference — returning a fresh instance is still correct by value and
    /// only shows up as an allocation.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class CollectionSequenceConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };
        private static readonly int[] _five = { 1, 2, 3, 4, 5 };

        // ---------------------------------------------------------------------------------------
        // CollectionFirstConverter
        // ---------------------------------------------------------------------------------------

        [Test]
        public void First_List_ReturnsTheHead() =>
            Assert.AreEqual("a", new CollectionFirstConverter<string>("?").Convert(_three));

        // The same three items behind an enumerator with no indexer take the foreach branch instead.
        [Test]
        public void First_WalkedSequence_ReturnsTheHead() =>
            Assert.AreEqual("a", new CollectionFirstConverter<string>("?").Convert(Streamed("a", "b", "c")));

        // A queue is the shape the class documentation names: a real collection that stops at
        // IReadOnlyCollection and so never reaches the indexed branch.
        [Test]
        public void First_Queue_ReturnsTheHead() =>
            Assert.AreEqual("a", new CollectionFirstConverter<string>("?").Convert(new Queue<string>(_three)));

        [Test]
        public void First_EmptyList_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionFirstConverter<string>("?").Convert(Array.Empty<string>()));

        [Test]
        public void First_EmptyWalkedSequence_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionFirstConverter<string>("?").Convert(Streamed<string>()));

        [Test]
        public void First_Null_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionFirstConverter<string>("?").Convert(null));

        [Test]
        public void First_DefaultConstructed_EmptySequence_ReturnsTheTypeDefault() =>
            Assert.IsNull(new CollectionFirstConverter<string>().Convert(Array.Empty<string>()));

        // The fallback answers an empty sequence, not an empty item. A sequence whose head is null is
        // not empty, so the null travels to the binder — the caller that authored a fallback to avoid
        // exactly that still gets it.
        [Test]
        public void First_NullHead_ReturnsNullRatherThanTheFallback()
        {
            Assert.IsNull(new CollectionFirstConverter<string>("?").Convert(new[] { null, "b" }));
            Assert.IsNull(new CollectionFirstConverter<string>("?").Convert(Streamed(null, "b")));
        }

        // The point of the converter: the head is answered without the length of the sequence
        // mattering. One pull, on a hundred available items.
        [Test]
        public void First_WalkedSequence_PullsOnlyTheHead()
        {
            var probe = new Probe();

            Assert.AreEqual(1, new CollectionFirstConverter<int>(-1).Convert(Counted(100, probe)));
            Assert.AreEqual(1, probe.Pulls);
        }

        // Returning out of a foreach still runs the enumerator's Dispose, which a bare MoveNext would
        // skip — and an iterator holding a file handle or a pooled buffer frees it there.
        [Test]
        public void First_WalkedSequence_DisposesTheEnumeratorOnTheEarlyReturn()
        {
            var probe = new Probe();

            new CollectionFirstConverter<int>(-1).Convert(Counted(100, probe));

            Assert.IsTrue(probe.Disposed);
        }

        // ---------------------------------------------------------------------------------------
        // CollectionLastConverter
        // ---------------------------------------------------------------------------------------

        [Test]
        public void Last_List_ReturnsTheTail() =>
            Assert.AreEqual("c", new CollectionLastConverter<string>("?").Convert(_three));

        [Test]
        public void Last_WalkedSequence_ReturnsTheTail() =>
            Assert.AreEqual("c", new CollectionLastConverter<string>("?").Convert(Streamed("a", "b", "c")));

        [Test]
        public void Last_Queue_ReturnsTheTail() =>
            Assert.AreEqual("c", new CollectionLastConverter<string>("?").Convert(new Queue<string>(_three)));

        [Test]
        public void Last_EmptyList_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionLastConverter<string>("?").Convert(Array.Empty<string>()));

        // The walked branch never assigns when there is nothing to walk, so the fallback it started
        // from is what comes back. A branch seeded with default(T) would answer null here instead.
        [Test]
        public void Last_EmptyWalkedSequence_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionLastConverter<string>("?").Convert(Streamed<string>()));

        [Test]
        public void Last_Null_ReturnsTheFallback() =>
            Assert.AreEqual("?", new CollectionLastConverter<string>("?").Convert(null));

        [Test]
        public void Last_SingleItem_ReturnsIt()
        {
            Assert.AreEqual("a", new CollectionLastConverter<string>("?").Convert(new[] { "a" }));
            Assert.AreEqual("a", new CollectionLastConverter<string>("?").Convert(Streamed("a")));
        }

        // The mirror of the head case: a null tail overwrites the fallback the walk started from, so
        // "no last item" and "a last item that is null" are not the same answer.
        [Test]
        public void Last_NullTail_ReturnsNullRatherThanTheFallback()
        {
            Assert.IsNull(new CollectionLastConverter<string>("?").Convert(new[] { "a", null }));
            Assert.IsNull(new CollectionLastConverter<string>("?").Convert(Streamed("a", null)));
        }

        // The cost the documentation admits to: nothing short of walking the whole sequence can name
        // its last item. Pinned so that a later "optimisation" that stops early is caught.
        [Test]
        public void Last_WalkedSequence_PullsEveryItem()
        {
            var probe = new Probe();

            Assert.AreEqual(100, new CollectionLastConverter<int>(-1).Convert(Counted(100, probe)));
            Assert.AreEqual(100, probe.Pulls);
        }

        // ---------------------------------------------------------------------------------------
        // CollectionTakeConverter — what comes back
        // ---------------------------------------------------------------------------------------

        [Test]
        public void Take_List_KeepsTheItemsOffTheStart() =>
            CollectionAssert.AreEqual(new[] { 1, 2 }, new CollectionTakeConverter<int>(2).Convert(_five));

        [Test]
        public void Take_WalkedSequence_KeepsTheItemsOffTheStart() =>
            CollectionAssert.AreEqual(new[] { 1, 2 }, new CollectionTakeConverter<int>(2).Convert(Streamed(1, 2, 3, 4, 5)));

        // Off the end, but still in the sequence's own order — not reversed, which is the shape a
        // "last five lines of a log" view would be wrong in without anyone noticing for a while.
        [Test]
        public void Take_List_FromEnd_KeepsTheTailInItsOriginalOrder() =>
            CollectionAssert.AreEqual(new[] { 4, 5 }, new CollectionTakeConverter<int>(2, fromEnd: true).Convert(_five));

        [Test]
        public void Take_WalkedSequence_FromEnd_KeepsTheTailInItsOriginalOrder() =>
            CollectionAssert.AreEqual(
                new[] { 4, 5 },
                new CollectionTakeConverter<int>(2, fromEnd: true).Convert(Streamed(1, 2, 3, 4, 5)));

        [TestCase(false)]
        [TestCase(true)]
        public void Take_CountAboveTheLength_KeepsEverything(bool fromEnd)
        {
            CollectionAssert.AreEqual(_five, new CollectionTakeConverter<int>(50, fromEnd).Convert(_five));
            CollectionAssert.AreEqual(_five, new CollectionTakeConverter<int>(50, fromEnd).Convert(Streamed(1, 2, 3, 4, 5)));
        }

        // Zero is the value a count field is left at while it is being typed into, and a negative one
        // is what an old asset carries. Neither may index past the front, and neither may let the
        // whole sequence through — which the walking branch used to do, having nothing to stop on.
        [TestCase(0, false)]
        [TestCase(0, true)]
        [TestCase(-1, false)]
        [TestCase(-1, true)]
        public void Take_CountOfZeroOrLess_KeepsNothing(int count, bool fromEnd)
        {
            CollectionAssert.IsEmpty(Taking(count, fromEnd).Convert(_five));
            CollectionAssert.IsEmpty(Taking(count, fromEnd).Convert(Streamed(1, 2, 3, 4, 5)));
        }

        // Keeping none of the items is a legal setting; keeping a negative number of them is not.
        [Test]
        public void Take_NegativeCount_IsRejectedByTheConstructor() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new CollectionTakeConverter<int>(-1));

        [Test]
        public void Take_CountOfZero_IsAcceptedAndKeepsNothing() =>
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(0).Convert(_five));

        [TestCase(false)]
        [TestCase(true)]
        public void Take_Null_KeepsNothing(bool fromEnd) =>
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(2, fromEnd).Convert(null));

        [TestCase(false)]
        [TestCase(true)]
        public void Take_EmptySequence_KeepsNothing(bool fromEnd)
        {
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(2, fromEnd).Convert(Array.Empty<int>()));
            CollectionAssert.IsEmpty(new CollectionTakeConverter<int>(2, fromEnd).Convert(Streamed<int>()));
        }

        [Test]
        public void Take_DefaultConstructed_KeepsThreeOffTheStart() =>
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, new CollectionTakeConverter<int>().Convert(_five));

        // Off the start, the walk stops as soon as it has enough — the whole reason a "top three" over
        // a long feed is affordable. A converter that buffered everything first would read 100 here.
        [Test]
        public void Take_WalkedSequence_StopsPullingOnceItHasEnough()
        {
            var probe = new Probe();

            CollectionAssert.AreEqual(new[] { 1, 2 }, new CollectionTakeConverter<int>(2).Convert(Counted(100, probe)));
            Assert.AreEqual(2, probe.Pulls);
        }

        // Off the end it cannot: where the last two items begin is unknown until the sequence ends, so
        // the tail case pays for the whole walk. Asserted because it is the asymmetry that decides
        // whether this converter belongs on a long feed at all.
        [Test]
        public void Take_WalkedSequence_FromEnd_PullsEveryItem()
        {
            var probe = new Probe();

            CollectionAssert.AreEqual(
                new[] { 99, 100 },
                new CollectionTakeConverter<int>(2, fromEnd: true).Convert(Counted(100, probe)));

            Assert.AreEqual(100, probe.Pulls);
        }

        // ---------------------------------------------------------------------------------------
        // CollectionTakeConverter — the reused buffer
        // ---------------------------------------------------------------------------------------

        // The contract that makes this converter allocation-free on a binder that pushes every frame.
        [Test]
        public void Take_ReturnsTheSameListInstanceOnEveryCall()
        {
            var converter = new CollectionTakeConverter<int>(2);

            Assert.AreSame(converter.Convert(new[] { 1, 2, 3 }), converter.Convert(new[] { 7, 8, 9 }));
        }

        // The early return for a null or a non-positive count leaves through a different line, and it
        // has to hand back the same list rather than a fresh empty one.
        [Test]
        public void Take_ReturnsTheSameListInstanceAcrossTheNullAndEmptyPaths()
        {
            var converter = new CollectionTakeConverter<int>(2);

            Assert.AreSame(converter.Convert(null), converter.Convert(_five));
            Assert.AreSame(converter.Convert(_five), converter.Convert(Streamed(1, 2, 3)));
        }

        // The price of that reuse, stated as a test so nobody has to discover it: a result held past
        // the push it arrived on is a view of whatever came last, not a snapshot.
        [Test]
        public void Take_ResultHeldAcrossCalls_ShowsTheLatestConversion()
        {
            var converter = new CollectionTakeConverter<int>(2);
            var held = converter.Convert(new[] { 1, 2, 3 });

            converter.Convert(new[] { 7, 8, 9 });

            CollectionAssert.AreEqual(new[] { 7, 8 }, held);
        }

        // ...and a null push does not leave the old items standing: the buffer is cleared first.
        [Test]
        public void Take_ResultHeldAcrossANullConversion_IsEmptied()
        {
            var converter = new CollectionTakeConverter<int>(2);
            var held = converter.Convert(new[] { 1, 2, 3 });

            converter.Convert(null);

            CollectionAssert.IsEmpty(held);
        }

        // The buffer is per instance, not static. Two binders sharing one would overwrite each other
        // and the symptom would depend on which one pushed last.
        [Test]
        public void Take_SeparateInstances_DoNotShareABuffer()
        {
            var first = new CollectionTakeConverter<int>(2);
            var second = new CollectionTakeConverter<int>(2);

            var firstResult = first.Convert(new[] { 1, 2, 3 });
            var secondResult = second.Convert(new[] { 7, 8, 9 });

            Assert.AreNotSame(firstResult, secondResult);
            CollectionAssert.AreEqual(new[] { 1, 2 }, firstResult);
        }

        // ---------------------------------------------------------------------------------------
        // CollectionCountToStringConverter — English
        // ---------------------------------------------------------------------------------------

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

        // ---------------------------------------------------------------------------------------
        // CollectionCountToStringConverter — Slavic
        // ---------------------------------------------------------------------------------------

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

        // ---------------------------------------------------------------------------------------
        // CollectionCountToStringConverter — the wording it delegates
        // ---------------------------------------------------------------------------------------

        // Only the size is read, so two different collections of the same size are one and the same
        // question to this converter — worth stating, since the items are never looked at.
        [Test]
        public void Count_DifferentCollectionOfTheSameSize_IsTheSamePhrase()
        {
            var converter = English();

            Assert.AreEqual(converter.Convert(new string[3]), converter.Convert(_three));
        }

        // One instance answers a whole sequence of counts, so nothing may be carried over between
        // pushes — zero in particular, which used to be the state a phrase cache confused with
        // "nothing answered yet".
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

        private static CollectionCountToStringConverter<string> Unworded() =>
            new(new PluralizeConverter(new EnglishPluralRule("one", string.Empty)));

        private static void ExpectMissingWordError() =>
            LogAssert.Expect(LogType.Error, new Regex("PluralRule.*no word is authored"));

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

        // ---------------------------------------------------------------------------------------
        // CollectionCountToStringConverter — any sequence
        // ---------------------------------------------------------------------------------------

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
            Assert.AreEqual("3 many", EnglishSequence().Convert(new CountedOnly(3)));
            Assert.AreEqual("3 many", EnglishSequence().Convert(new MutableCountedOnly(3)));
        }

        // ---------------------------------------------------------------------------------------
        // DictionaryLookupConverter
        // ---------------------------------------------------------------------------------------

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
                new DictionaryLookupConverter<string, string>(Array.Empty<LookupEntry<string, string>>(), "Grey")
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

        // ---------------------------------------------------------------------------------------
        // Helpers
        // ---------------------------------------------------------------------------------------

        // Only Count is read off the collection, so the items themselves are of no interest here.
        private static string[] Items(int count) => new string[count];

        // A count the constructor refuses can still arrive from a serialized asset, so the fields are
        // written the way deserialization writes them.
        private static CollectionTakeConverter<int> Taking(int count, bool fromEnd)
        {
            var converter = new CollectionTakeConverter<int>();

            Field("_count").SetValue(converter, count);
            Field("_fromEnd").SetValue(converter, fromEnd);

            return converter;

            static FieldInfo Field(string name) =>
                typeof(CollectionTakeConverter<int>)
                    .GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!;
        }

        private static CollectionCountToStringConverter<string> English() =>
            new(new PluralizeConverter(new EnglishPluralRule("one", "many")));

        private static CollectionCountToStringConverter<string> Slavic() =>
            new(new PluralizeConverter(new EastSlavicPluralRule("one", "few", "many")));

        // The sequence member is explicit, so these calls go through the interface a binding would
        // use rather than the public method.
        private static IConverter<IEnumerable<string>, string> EnglishSequence() => English();

        private static IConverter<IEnumerable<string>, string> CaptionedSequence() =>
            new CollectionCountToStringConverter<string>(
                new PluralizeConverter(new EnglishPluralRule("one", "many")), "Empty");

        private static DictionaryLookupConverter<string, string> Elements() =>
            new(new[] { Row("Fire", "Red"), Row("Ice", "Blue") }, "Grey");

        private static LookupEntry<TKey, TValue> Row<TKey, TValue>(TKey key, TValue value) =>
            new(key, value);

        // An iterator, so the result is an IEnumerable and nothing more — the converters' indexed fast
        // path cannot see it and the walking branch is the one under test.
        private static IEnumerable<T> Streamed<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }

        // The same, with the pulls counted and the disposal recorded, for the assertions about how
        // much of a sequence a converter actually reads.
        private static IEnumerable<int> Counted(int length, Probe probe)
        {
            try
            {
                for (var i = 1; i <= length; i++)
                {
                    probe.Pulls++;
                    yield return i;
                }
            }
            finally
            {
                probe.Disposed = true;
            }
        }

        private sealed class Probe
        {
            public int Pulls;
            public bool Disposed;
        }

        // A collection that answers Count and throws on being walked, so the counting fast path is
        // asserted rather than assumed.
        private sealed class CountedOnly : IReadOnlyCollection<string>
        {
            public CountedOnly(int count) => Count = count;

            public int Count { get; }

            public IEnumerator<string> GetEnumerator() => throw new AssertionException("the sequence was walked");

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        // The same, stopping at ICollection instead — the second fast path.
        private sealed class MutableCountedOnly : ICollection<string>
        {
            public MutableCountedOnly(int count) => Count = count;

            public int Count { get; }

            public bool IsReadOnly => true;

            public void Add(string item) => throw new NotSupportedException();

            public void Clear() => throw new NotSupportedException();

            public bool Contains(string item) => throw new NotSupportedException();

            public void CopyTo(string[] array, int arrayIndex) => throw new NotSupportedException();

            public bool Remove(string item) => throw new NotSupportedException();

            public IEnumerator<string> GetEnumerator() => throw new AssertionException("the sequence was walked");

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
