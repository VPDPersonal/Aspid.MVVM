using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the collection converters and the dropdown-options converter that closes the last
    /// empty picker.
    /// </summary>
    [TestFixture]
    internal sealed class CollectionConverterTests
    {
        private static readonly string[] _three = { "a", "b", "c" };

        [Test]
        public void Count_CountsTheItems() =>
            Assert.AreEqual(3, new CollectionCountConverter<string>().Convert(_three));

        [Test]
        public void Count_NullIsZero() =>
            Assert.AreEqual(0, new CollectionCountConverter<string>().Convert(null));

        [Test]
        public void EmptyToBool_ReportsEmptiness()
        {
            Assert.IsTrue(new CollectionEmptyToBoolConverter<string>().Convert(Array.Empty<string>()));
            Assert.IsTrue(new CollectionEmptyToBoolConverter<string>().Convert(null));
            Assert.IsFalse(new CollectionEmptyToBoolConverter<string>().Convert(_three));
        }

        // An iterator carries no count of its own, so the counted-collection member cannot take it at
        // all: it arrives through the sequence member and is walked.
        [Test]
        public void Count_WalkedSequence_CountsTheItems() =>
            Assert.AreEqual(3, SequenceCountAsInt().Convert(Walk("a", "b", "c")));

        [Test]
        public void Count_WalkedSequence_EmptyIsZero() =>
            Assert.AreEqual(0, SequenceCountAsInt().Convert(Walk<string>()));

        [Test]
        public void Count_WalkedSequence_NullIsZero() =>
            Assert.AreEqual(0, SequenceCountAsInt().Convert(null));

        // One number, and the binding decides which numeric member carries it.
        [Test]
        public void Count_WidensToTheBoundNumericType()
        {
            Assert.AreEqual(3L, CollectionCountAsLong().Convert(_three));
            Assert.AreEqual(3f, CollectionCountAsFloat().Convert(_three), 1e-5f);
            Assert.AreEqual(3d, CollectionCountAsDouble().Convert(_three), 1e-12);
        }

        [Test]
        public void Count_WalkedSequence_WidensToTheBoundNumericType()
        {
            Assert.AreEqual(3L, SequenceCountAsLong().Convert(Walk("a", "b", "c")));
            Assert.AreEqual(3f, SequenceCountAsFloat().Convert(Walk("a", "b", "c")), 1e-5f);
            Assert.AreEqual(3d, SequenceCountAsDouble().Convert(Walk("a", "b", "c")), 1e-12);
        }

        [Test]
        public void Count_List_ArrivingAsASequence_IsStillCounted() =>
            Assert.AreEqual(3, SequenceCountAsInt().Convert(new List<string> { "a", "b", "c" }));

        // A sequence that knows its own size is asked for it rather than walked. Both fakes throw on
        // being enumerated, so a rewrite that lost the fast path fails here instead of merely walking
        // a long sequence on every push.
        [Test]
        public void Count_SequenceWithACount_TakesItRatherThanWalking()
        {
            Assert.AreEqual(3, SequenceCountAsInt().Convert(new CountedOnly(3)));
            Assert.AreEqual(3, SequenceCountAsInt().Convert(new MutableCountedOnly(3)));
        }

        [Test]
        public void EmptyToBool_WalkedSequence_ReportsEmptiness()
        {
            Assert.IsTrue(SequenceEmptiness().Convert(Walk<string>()));
            Assert.IsTrue(SequenceEmptiness().Convert(null));
            Assert.IsFalse(SequenceEmptiness().Convert(Walk("a", "b", "c")));
        }

        [Test]
        public void EmptyToBool_WalkedSequence_InvertsWhenAsked()
        {
            Assert.IsFalse(SequenceEmptiness(isInvert: true).Convert(Walk<string>()));
            Assert.IsTrue(SequenceEmptiness(isInvert: true).Convert(Walk("a")));
        }

        // Emptiness is one item's worth of question however long the sequence is, and the enumerator
        // is disposed on the way out — which an iterator holding a handle or a pooled buffer needs.
        [Test]
        public void EmptyToBool_WalkedSequence_PullsOneItemAndDisposes()
        {
            var probe = new Probe();

            Assert.IsFalse(SequenceEmptiness().Convert(Counted(100, probe)));
            Assert.AreEqual(1, probe.Pulls);
            Assert.IsTrue(probe.Disposed);
        }

        [Test]
        public void EmptyToBool_List_ArrivingAsASequence_IsStillTested() =>
            Assert.IsFalse(SequenceEmptiness().Convert(new List<string> { "a" }));

        [Test]
        public void EmptyToBool_SequenceWithACount_TakesItRatherThanWalking()
        {
            Assert.IsFalse(SequenceEmptiness().Convert(new CountedOnly(3)));
            Assert.IsTrue(SequenceEmptiness().Convert(new CountedOnly(0)));
            Assert.IsFalse(SequenceEmptiness().Convert(new MutableCountedOnly(3)));
            Assert.IsTrue(SequenceEmptiness().Convert(new MutableCountedOnly(0)));
        }

        [Test]
        public void JoinToString_JoinsWithTheSeparator() =>
            Assert.AreEqual("a, b, c", new CollectionJoinToStringConverter<string>(", ").Convert(_three));

        // The item slot takes any converter, so the per-item text is not limited to a composite format.
        [Test]
        public void JoinToString_ItemConverter_WritesEachItem() =>
            Assert.AreEqual(
                "[a], [b], [c]",
                new CollectionJoinToStringConverter<string>(", ", item: new GenericToStringConverter<string>("[{0}]"))
                    .Convert(_three));

        [Test]
        public void JoinToString_TrimsAndReportsTheOverflow() =>
            Assert.AreEqual("a, b +1 more", new CollectionJoinToStringConverter<string>(", ", maxItems: 2).Convert(_three));

        [Test]
        public void JoinToString_EmptyUsesTheEmptyText() =>
            Assert.AreEqual("—", new CollectionJoinToStringConverter<string>(", ", 0, "—").Convert(Array.Empty<string>()));

        [Test]
        public void JoinToString_NullUsesTheEmptyText() =>
            Assert.AreEqual("—", new CollectionJoinToStringConverter<string>(", ", 0, "—").Convert(null));

        // The builder is reused between calls, so a second call must not see the first one's text.
        [Test]
        public void JoinToString_ReusedBuilderDoesNotLeakBetweenCalls()
        {
            var converter = new CollectionJoinToStringConverter<string>(", ");

            Assert.AreEqual("a, b, c", converter.Convert(_three));
            Assert.AreEqual("x", converter.Convert(new[] { "x" }));
        }

        [Test]
        public void ElementAt_TakesTheIndex() =>
            Assert.AreEqual("b", new CollectionElementAtConverter<string>(1).Convert(_three));

        [Test]
        public void ElementAt_CountsFromTheEndWhenAsked() =>
            Assert.AreEqual("c", new CollectionElementAtConverter<string>(0, fromEnd: true).Convert(_three));

        [Test]
        public void ElementAt_OutsideTheListReportsItAndGivesTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("CollectionElementAtConverter.*outside the list"));

            Assert.AreEqual("?", new CollectionElementAtConverter<string>(9, false, "?").Convert(_three));
        }

        [Test]
        public void ElementAt_NegativeIndex_IsRejectedByTheConstructor() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new CollectionElementAtConverter<string>(-1));

        [Test]
        public void Contains_LooksForTheItem()
        {
            Assert.IsTrue(new CollectionContainsToBoolConverter<string>("b").Convert(_three));
            Assert.IsFalse(new CollectionContainsToBoolConverter<string>("z").Convert(_three));
            Assert.IsFalse(new CollectionContainsToBoolConverter<string>("b").Convert(null));
        }

        // The match slot takes any converter, so "contains" is not limited to equality with one value.
        [Test]
        public void Contains_MatchConverter_DecidesTheMatch()
        {
            var anyEmpty = new CollectionContainsToBoolConverter<string>(new StringEmptyToBoolConverter());

            Assert.IsFalse(anyEmpty.Convert(_three));
            Assert.IsTrue(anyEmpty.Convert(new[] { "a", "" }));
        }

        [Test]
        public void Contains_NullMatchInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() =>
                _ = new CollectionContainsToBoolConverter<string>(match: null!));

        // The Inspector shape: a converter deserialized with its match slot cleared.
        [Test]
        public void Contains_MissingMatch_CountsAsNoMatchAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("the match converter is required"));

            var converter = new CollectionContainsToBoolConverter<string>("b");
            typeof(CollectionContainsToBoolConverter<string>)
                .GetField("_match", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .SetValue(converter, null);

            Assert.IsFalse(converter.Convert(_three));
        }

        [TestCase(Aggregate.Sum, 6f)]
        [TestCase(Aggregate.Average, 2f)]
        [TestCase(Aggregate.Min, 1f)]
        [TestCase(Aggregate.Max, 3f)]
        public void Aggregate_Reduces(Aggregate operation, float expected) =>
            Assert.AreEqual(expected, Floats(operation).Convert(new[] { 1f, 2f, 3f }), 1e-5f);

        [Test]
        public void Aggregate_EmptyUsesTheEmptyResult() =>
            Assert.AreEqual(-1f, Floats(Aggregate.Min, -1f).Convert(Array.Empty<float>()), 1e-5f);

        [Test]
        public void Aggregate_NullUsesTheEmptyResult() =>
            Assert.AreEqual(-1f, Floats(Aggregate.Min, -1f).Convert(null), 1e-5f);

        [Test]
        public void Aggregate_ReducesLongs() =>
            Assert.AreEqual(6L, Longs(Aggregate.Sum).Convert(new[] { 1L, 2L, 3L }));

        [Test]
        public void Aggregate_ReducesDoubles() =>
            Assert.AreEqual(6d, Doubles(Aggregate.Sum).Convert(new[] { 1d, 2d, 3d }), 1e-12);

        // The reason the cross-type overloads are there: an integer collection averages to a
        // fraction, which only a float or double binding can carry.
        [Test]
        public void Aggregate_AverageOfIntsKeepsTheFraction() =>
            Assert.AreEqual(2.5f, IntsToFloat(Aggregate.Average).Convert(new[] { 1, 2, 3, 4 }), 1e-5f);

        [Test]
        public void Aggregate_AverageOfIntsTruncatesWhenBoundToInt() =>
            Assert.AreEqual(2, IntsToInt(Aggregate.Average).Convert(new[] { 1, 2, 3, 4 }));

        // The sum is carried in double, so a total past int range narrows by saturating rather than
        // wrapping around.
        [Test]
        public void Aggregate_SumBeyondIntRangeSaturates() =>
            Assert.AreEqual(int.MaxValue, DoublesToInt(Aggregate.Sum).Convert(new[] { 1e18d, 1e18d }));

        [Test]
        public void Aggregate_UndeclaredOperationIsReported()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a declared Aggregate"));

            Assert.AreEqual(-1f, Floats((Aggregate)99, -1f).Convert(new[] { 1f, 2f, 3f }), 1e-5f);
        }

        [Test]
        public void EnumToDropdownOptions_BuildsOnePerMember()
        {
            var options = new List<TMPro.TMP_Dropdown.OptionData>(
                new EnumToDropdownOptionDataConverter().Convert(Difficulty2.Easy));

            Assert.AreEqual(2, options.Count);
            Assert.AreEqual("Easy", options[0].text);
        }

        [Test]
        public void EnumToDropdownOptions_UsesTheInspectorName()
        {
            var options = new List<TMPro.TMP_Dropdown.OptionData>(
                new EnumToDropdownOptionDataConverter().Convert(Difficulty2.Easy));

            Assert.AreEqual("Very hard", options[1].text);
        }

        [Test]
        public void EnumToDropdownOptions_AuthoredLabelWins()
        {
            var converter = new EnumToDropdownOptionDataConverter(
                new[]
                {
                    new EnumToDropdownOptionDataConverter.Entry("Easy", "Casual"),
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            Assert.AreEqual("Casual", options[0].text);
        }

        // An entry naming a member the enum does not declare is authored in and never reached: the
        // dropdown comes out without the label, and nothing in the Inspector says why. The option
        // list is cached per type, but the report is not: a designer opening the scene after the
        // list was built would otherwise never see it.
        [Test]
        public void EnumToDropdownOptions_EntryNamingNoMember_IsReportedEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(
                    LogType.Error,
                    new Regex("EnumToDropdownOptionDataConverter.*not a member of Difficulty2"));

            var converter = new EnumToDropdownOptionDataConverter(
                new[]
                {
                    new EnumToDropdownOptionDataConverter.Entry("Simple", "Casual"),
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            converter.Convert(Difficulty2.Easy);
            converter.Convert(Difficulty2.Brutal);

            Assert.AreEqual("Easy", options[0].text);
        }

        // The scan answers with the first entry that names the member, so the second one is authored
        // in and unreachable.
        [Test]
        public void EnumToDropdownOptions_DuplicateEntry_TakesTheFirstAndReportsTheSecond()
        {
            LogAssert.Expect(
                LogType.Error,
                new Regex("EnumToDropdownOptionDataConverter.*listed more than once"));

            var converter = new EnumToDropdownOptionDataConverter(
                new[]
                {
                    new EnumToDropdownOptionDataConverter.Entry("Easy", "First"),
                    new EnumToDropdownOptionDataConverter.Entry("Easy", "Second"),
                });

            var options = new List<TMPro.TMP_Dropdown.OptionData>(converter.Convert(Difficulty2.Easy));

            Assert.AreEqual("First", options[0].text);
        }

        // The option set depends on the type, not the value, so rebuilding per push would allocate
        // an OptionData per member on every notification.
        [Test]
        public void EnumToDropdownOptions_ReusesTheListWhileTheTypeIsUnchanged()
        {
            var converter = new EnumToDropdownOptionDataConverter();

            Assert.AreSame(converter.Convert(Difficulty2.Easy), converter.Convert(Difficulty2.Brutal));
        }

        [Test]
        public void MaterialInstance_ReturnsACopyAndReusesIt()
        {
            var shader = Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default");
            var material = new Material(shader) { name = "Shared" };
            var converter = new MaterialInstanceConverter();

            try
            {
                var first = converter.Convert(material);

                Assert.AreNotSame(material, first);
                Assert.AreSame(first, converter.Convert(material));
                Assert.IsNotNull(first);
                Assert.IsTrue(first.name.Contains("Instance"));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(material);
            }
        }

        [Test]
        public void MaterialInstance_CanBeTurnedOff()
        {
            var shader = Shader.Find("Unlit/Color") ?? Shader.Find("Sprites/Default");
            var material = new Material(shader);

            try
            {
                Assert.AreSame(material, new MaterialInstanceConverter(instantiate: false).Convert(material));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(material);
            }
        }

        [Test]
        public void MaterialInstance_NullPassesThrough() =>
            Assert.IsNull(new MaterialInstanceConverter().Convert(null));

        // The count and emptiness members over a sequence are explicit, so every call above goes
        // through the interface the binding would use rather than the public method.
        private static IConverter<IEnumerable<string>, int> SequenceCountAsInt() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, long> SequenceCountAsLong() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, float> SequenceCountAsFloat() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, double> SequenceCountAsDouble() =>
            new CollectionCountConverter<string>();

        private static IConverter<IReadOnlyCollection<string>, long> CollectionCountAsLong() =>
            new CollectionCountConverter<string>();

        private static IConverter<IReadOnlyCollection<string>, float> CollectionCountAsFloat() =>
            new CollectionCountConverter<string>();

        private static IConverter<IReadOnlyCollection<string>, double> CollectionCountAsDouble() =>
            new CollectionCountConverter<string>();

        private static IConverter<IEnumerable<string>, bool> SequenceEmptiness(bool isInvert = false) =>
            new CollectionEmptyToBoolConverter<string>(isInvert);

        // An iterator, so the result is an IEnumerable and nothing more — no count is available to
        // take and the walking branch is the one under test.
        private static IEnumerable<T> Walk<T>(params T[] items)
        {
            foreach (var item in items)
                yield return item;
        }

        // The same, with the pulls counted and the disposal recorded. The items themselves are of no
        // interest — how many of them are asked for is the assertion.
        private static IEnumerable<string> Counted(int length, Probe probe)
        {
            try
            {
                for (var i = 0; i < length; i++)
                {
                    probe.Pulls++;
                    yield return "item";
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

        // A collection that answers Count and throws on being walked: the fast path is not an
        // optimisation to be checked by eye but a contract, so it is asserted as one.
        private sealed class CountedOnly : IReadOnlyCollection<string>
        {
            public CountedOnly(int count) => Count = count;

            public int Count { get; }

            public IEnumerator<string> GetEnumerator() => throw new AssertionException("the sequence was walked");

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        // The same, stopping at ICollection instead — the interface a collection written before
        // IReadOnlyCollection existed offers, and the second fast path.
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

        // All sixteen of the aggregate converter's interfaces are implemented explicitly, so every
        // call above goes through the interface the binding would use.
        private static IConverter<IEnumerable<float>, float> Floats(Aggregate operation, double emptyResult = 0d) =>
            new CollectionAggregateConverter(operation, emptyResult);

        private static IConverter<IEnumerable<long>, long> Longs(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<double>, double> Doubles(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<int>, float> IntsToFloat(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<int>, int> IntsToInt(Aggregate operation) =>
            new CollectionAggregateConverter(operation);

        private static IConverter<IEnumerable<double>, int> DoublesToInt(Aggregate operation) =>
            new CollectionAggregateConverter(operation);
    }
}
