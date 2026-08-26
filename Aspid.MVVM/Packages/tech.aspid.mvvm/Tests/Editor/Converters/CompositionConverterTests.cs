#nullable enable
using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Reflection;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the composition primitives — the wrappers that let converters be assembled in the
    /// Inspector instead of written as a new class each time.
    /// </summary>
    /// <remarks>
    /// Half of each fixture is about the half-configured state, because that is what a wrapper spends
    /// its life in: dropped into a field, then filled in. Each one degrades in a way its own summary
    /// commits to, and those are the assertions below.
    /// </remarks>
    [TestFixture]
    internal sealed class CompositionConverterTests
    {
        [Test]
        public void Compose_AppliesBothLinksInOrder() =>
            Assert.AreEqual(
                "8",
                new ComposeConverter<int, int, string>(new Add(1), new ToText()).Convert(7));

        [Test]
        public void Compose_MissingLink_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("both links are required"));

            // The Inspector shape: a wrapper deserialized before its links are filled in.
            var converter = Empty<ComposeConverter<int, int, string>>();
            Assert.IsNull(converter.Convert(7));
            converter.Convert(8);
            converter.Convert(9);
        }

        // A wrapper with nothing to wrap is a mistake in code and a half-filled field in the
        // Inspector. The constructors refuse it; the parameterless shape reports it on every push.
        [Test]
        public void NullInnerInTheConstructor_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new SafeConverter<int, int>(null!));
            Assert.Throws<ArgumentNullException>(() => _ = new CachedConverter<int, int>(null!));
            Assert.Throws<ArgumentNullException>(() => _ = new NullGuardConverter<string, string>(null!));
            Assert.Throws<ArgumentNullException>(() => _ = new ConditionalConverter<int>(null!, new Add(1), null));
        }

        [Test]
        public void Compose_NullLinkInTheConstructor_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new ComposeConverter<int, int, string>(new Add(1), null!));
            Assert.Throws<ArgumentNullException>(() => _ = new ComposeConverter<int, int, string>(null!, new ToText()));
        }

        [Test]
        public void Compose_UndoesBothLinksInReverseOrder()
        {
            var converter = new ComposeConverter<int, int, string>(new TwoWayAdd(1), new TwoWayText());

            Assert.AreEqual("8", converter.Convert(7));
            Assert.AreEqual(7, converter.ConvertBack("8"));
        }

        // Undoing one link and not the other would leave the value in neither space, so a single
        // one-way link makes the whole composition one-way — and says so.
        [Test]
        public void Compose_ConvertBack_WithAOneWayLink_ReturnsTheFallbackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("ToText converts one way only"));

            var converter = new ComposeConverter<int, int, string>(new TwoWayAdd(1), new ToText());
            Assert.AreEqual(0, converter.ConvertBack("8"));
        }

        [Test]
        public void Compose_ConvertBack_MissingLink_ReturnsTheFallbackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("both links are required"));

            Assert.AreEqual(0, Empty<ComposeConverter<int, int, string>>().ConvertBack("8"));
        }

        [Test]
        public void Inverse_RunsTheWrappedConverterTheOtherWayRound()
        {
            var converter = new InverseConverter<int, string>(new TwoWayText());

            Assert.AreEqual(7, converter.Convert("7"));
            Assert.AreEqual("7", converter.ConvertBack(7));
        }

        [Test]
        public void Inverse_NullInnerInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new InverseConverter<int, string>(null!));

        [Test]
        public void Inverse_MissingConverter_ReturnsTheDefaultAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("the converter is required"));

            var converter = Empty<InverseConverter<int, string>>();
            Assert.AreEqual(0, converter.Convert("7"));
            converter.Convert("8");
        }

        [Test]
        public void Conditional_TrueBranch_IsApplied() =>
            Assert.AreEqual(11, Conditional(new IsPositive(), new Add(1), new Add(100)).Convert(10));

        [Test]
        public void Conditional_FalseBranch_IsApplied() =>
            Assert.AreEqual(90, Conditional(new IsPositive(), new Add(1), new Add(100)).Convert(-10));

        // A configured branch with no predicate to select it is a half-finished setup, not a design.
        [Test]
        public void Conditional_NoPredicateButConfiguredBranch_PassesThroughAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("predicate that selects it is missing"));

            var converter = Conditional(null, new Add(1), new Add(100));
            Assert.AreEqual(10, converter.Convert(10));
            converter.Convert(11);
        }

        // A fully empty instance is the identity by design, so it stays quiet.
        [Test]
        public void Conditional_FullyEmpty_PassesTheValueThroughSilently() =>
            Assert.AreEqual(10, Conditional(null, null, null).Convert(10));

        [Test]
        public void Conditional_EmptyBranch_PassesTheValueThrough() =>
            Assert.AreEqual(10, Conditional(new IsPositive(), null, new Add(100)).Convert(10));

        [Test]
        public void Safe_SubstitutesTheFallbackWhenTheInnerThrows()
        {
            LogAssert.Expect(LogType.Error, new Regex("threw"));

            Assert.AreEqual(-1, new SafeConverter<int, int>(new Throws(), fallback: -1).Convert(7));
        }

        [Test]
        public void Safe_ReportsEveryFailure()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("threw"));

            var converter = new SafeConverter<int, int>(new Throws(), fallback: -1);
            converter.Convert(1);
            converter.Convert(2);
            converter.Convert(3);
        }

        // The report carries the exception in full — type, message, and stack — so the failure is
        // diagnosable from the console alone.
        [Test]
        public void Safe_ReportsTheFullException()
        {
            LogAssert.Expect(LogType.Error, new Regex("threw InvalidOperationException \\(boom\\)"));

            new SafeConverter<int, int>(new Throws(), fallback: -1).Convert(7);
        }

        [Test]
        public void Safe_NoInner_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("inner converter is required"));

            var converter = SetField(Empty<SafeConverter<int, int>>(), "_fallback", -1);
            Assert.AreEqual(-1, converter.Convert(7));
            converter.Convert(8);
        }

        [Test]
        public void Safe_PassesThroughWhenTheInnerSucceeds() =>
            Assert.AreEqual(8, new SafeConverter<int, int>(new Add(1), fallback: -1).Convert(7));

        [Test]
        public void Safe_UndoesTheInnerConverter()
        {
            var converter = new SafeConverter<int, int>(new TwoWayAdd(1), fallback: -1, convertBackFallback: -2);

            Assert.AreEqual(8, converter.Convert(7));
            Assert.AreEqual(7, converter.ConvertBack(8));
        }

        [Test]
        public void Safe_ConvertBack_SubstitutesTheFallbackWhenTheInnerThrows()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("threw InvalidOperationException \\(boom\\)"));

            var converter = new SafeConverter<int, int>(new ThrowsBack(), fallback: -1, convertBackFallback: -2);
            Assert.AreEqual(-2, converter.ConvertBack(8));
            converter.ConvertBack(9);
        }

        // A one-way inner cannot be undone, and the wrapper says which converter it is rather than
        // passing the View's value on as if it had been converted.
        [Test]
        public void Safe_ConvertBack_WithAOneWayInner_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("Add converts one way only"));

            var converter = new SafeConverter<int, int>(new Add(1), fallback: -1, convertBackFallback: -2);
            Assert.AreEqual(-2, converter.ConvertBack(8));
            converter.ConvertBack(9);
        }

        [Test]
        public void Safe_ConvertBack_NoInner_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("inner converter is required"));

            var converter = SetField(Empty<SafeConverter<int, int>>(), "_convertBackFallback", -2);
            Assert.AreEqual(-2, converter.ConvertBack(8));
            converter.ConvertBack(9);
        }

        [Test]
        public void NullGuard_NullInput_ReturnsTheConfiguredResult() =>
            Assert.AreEqual("—", new NullGuardConverter<string, string>(new ToUpper(), "—").Convert(null));

        [Test]
        public void NullGuard_NonNullInput_ReachesTheInner() =>
            Assert.AreEqual("ABC", new NullGuardConverter<string, string>(new ToUpper(), "—").Convert("abc"));

        // The wrapped converter would throw on null; the guard is what stops it.
        [Test]
        public void NullGuard_ProtectsAnInnerThatCannotTakeNull() =>
            Assert.AreEqual("—", new NullGuardConverter<string, string>(new ThrowsOnNull(), "—").Convert(null));

        [Test]
        public void NullGuard_NonNullInputWithNoInner_ReturnsTheNullResultAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("inner converter is required"));

            var converter = SetField(Empty<NullGuardConverter<string, string>>(), "_nullResult", "—");
            Assert.AreEqual("—", converter.Convert("abc"));
            converter.Convert("abc");
        }

        // A null input never reaches the inner, so a missing one is not a misconfiguration yet.
        [Test]
        public void NullGuard_NullInputWithNoInner_StaysQuiet() =>
            Assert.AreEqual("—", SetField(Empty<NullGuardConverter<string, string>>(), "_nullResult", "—").Convert(null));

        [Test]
        public void Cached_RepeatedInput_RunsTheInnerOnce()
        {
            var inner = new Counting();
            var converter = new CachedConverter<int, int>(inner);

            converter.Convert(7);
            converter.Convert(7);
            converter.Convert(7);

            Assert.AreEqual(1, inner.Calls);
        }

        [Test]
        public void Cached_ChangedInput_RunsTheInnerAgain()
        {
            var inner = new Counting();
            var converter = new CachedConverter<int, int>(inner);

            converter.Convert(7);
            converter.Convert(8);
            converter.Convert(7);

            Assert.AreEqual(3, inner.Calls);
        }

        [Test]
        public void Cached_ReturnsTheSameResultItWouldWithout() =>
            Assert.AreEqual(8, new CachedConverter<int, int>(new Add(1)).Convert(7));

        [Test]
        public void Cached_CachesTheFirstResultIncludingDefaults()
        {
            var converter = new CachedConverter<int, int>(new Add(0));

            Assert.AreEqual(0, converter.Convert(0));
            Assert.AreEqual(0, converter.Convert(0));
        }

        [Test]
        public void Cached_NoInner_ReturnsTheFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("inner converter is required"));

            var converter = Empty<CachedConverter<int, int>>();
            Assert.AreEqual(0, converter.Convert(7));
            converter.Convert(7);
        }

        // The cache is written only after a successful conversion: a throw must not pair the new
        // input with the previous input's output.
        [Test]
        public void Cached_ThrowingInner_DoesNotPoisonTheCache()
        {
            var inner = new ThrowsOnce();
            var converter = new CachedConverter<int, int>(inner);

            Assert.AreEqual(8, converter.Convert(7));
            Assert.Throws<InvalidOperationException>(() => converter.Convert(9));

            // The same input again: a poisoned cache would answer with 7's result instead of retrying.
            Assert.AreEqual(10, converter.Convert(9));
        }

        [Test]
        public void Cached_UndoesTheInnerConverter()
        {
            var converter = new CachedConverter<int, int>(new TwoWayAdd(1));

            Assert.AreEqual(8, converter.Convert(7));
            Assert.AreEqual(7, converter.ConvertBack(8));
        }

        [Test]
        public void Cached_ConvertBack_RepeatedInput_RunsTheInnerOnce()
        {
            var inner = new CountingTwoWay();
            var converter = new CachedConverter<int, int>(inner);

            Assert.AreEqual(6, converter.ConvertBack(7));
            Assert.AreEqual(6, converter.ConvertBack(7));

            Assert.AreEqual(1, inner.BackCalls);
        }

        // The inner converter need not be a bijection, so neither direction may answer from the
        // other's cache — the same number pushed both ways has two different right answers.
        [Test]
        public void Cached_TheTwoDirectionsDoNotShareACache()
        {
            var inner = new CountingTwoWay();
            var converter = new CachedConverter<int, int>(inner);

            Assert.AreEqual(8, converter.Convert(7));
            Assert.AreEqual(6, converter.ConvertBack(7));

            // Each direction still answers from its own cache, untouched by the other.
            Assert.AreEqual(8, converter.Convert(7));
            Assert.AreEqual(6, converter.ConvertBack(7));

            Assert.AreEqual(1, inner.Calls);
            Assert.AreEqual(1, inner.BackCalls);
        }

        [Test]
        public void Cached_ThrowingConvertBack_DoesNotPoisonTheCache()
        {
            var inner = new ThrowsBackOnce();
            var converter = new CachedConverter<int, int>(inner);

            Assert.AreEqual(6, converter.ConvertBack(7));
            Assert.Throws<InvalidOperationException>(() => converter.ConvertBack(9));

            // The same input again: a poisoned cache would answer with 7's result instead of retrying.
            Assert.AreEqual(8, converter.ConvertBack(9));
        }

        [Test]
        public void Cached_ConvertBack_WithAOneWayInner_ReturnsTheDefaultAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("Add converts one way only"));

            var converter = new CachedConverter<int, int>(new Add(1));
            Assert.AreEqual(0, converter.ConvertBack(8));
            converter.ConvertBack(8);
        }

        [Test]
        public void Cached_ConvertBack_NoInner_ReturnsTheDefaultAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("inner converter is required"));

            var converter = Empty<CachedConverter<int, int>>();
            Assert.AreEqual(0, converter.ConvertBack(8));
            converter.ConvertBack(8);
        }

        [Test]
        public void Passthrough_ReturnsTheInput() =>
            Assert.AreEqual(7, new PassthroughConverter<int>().Convert(7));

        // The Inspector shape: a wrapper deserialized with any subset of its slots filled in. The
        // constructor refuses a null predicate, so the half-configured states are built field by field.
        private static ConditionalConverter<int> Conditional(
            IConverter<int, bool>? predicate,
            IConverter<int, int>? then,
            IConverter<int, int>? @else)
        {
            var converter = Empty<ConditionalConverter<int>>();

            SetField(converter, "_predicate", predicate);
            SetField(converter, "_then", then);
            SetField(converter, "_else", @else);

            return converter;
        }

        // The wrappers keep their parameterless constructor non-public, so the empty shape is built
        // the way the type picker builds it: Activator.CreateInstance(type, nonPublic: true).
        private static T Empty<T>()
            where T : class =>
            (T)Activator.CreateInstance(typeof(T), nonPublic: true)!;

        private static T SetField<T>(T target, string name, object? value)
            where T : class
        {
            var field = target!.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");

            field!.SetValue(target, value);
            return target;
        }

        private sealed class Add : IConverter<int, int>
        {
            private readonly int _amount;

            public Add(int amount) => _amount = amount;

            public int Convert(int value) => value + _amount;
        }

        private sealed class ToText : IConverter<int, string>
        {
            public string Convert(int value) => value.ToString();
        }

        private sealed class TwoWayAdd : ITwoWayConverter<int, int>
        {
            private readonly int _amount;

            public TwoWayAdd(int amount) => _amount = amount;

            public int Convert(int value) => value + _amount;

            public int ConvertBack(int value) => value - _amount;
        }

        private sealed class TwoWayText : ITwoWayConverter<int, string>
        {
            public string Convert(int value) => value.ToString();

            public int ConvertBack(string value) => int.Parse(value);
        }

        private sealed class ToUpper : IConverter<string?, string?>
        {
            public string? Convert(string? value) => value?.ToUpperInvariant();
        }

        private sealed class ThrowsOnNull : IConverter<string?, string?>
        {
            public string? Convert(string? value) => value!.ToUpperInvariant();
        }

        private sealed class IsPositive : IConverter<int, bool>
        {
            public bool Convert(int value) => value > 0;
        }

        private sealed class Throws : IConverter<int, int>
        {
            public int Convert(int value) => throw new InvalidOperationException("boom");
        }

        private sealed class Counting : IConverter<int, int>
        {
            public int Calls { get; private set; }

            public int Convert(int value)
            {
                Calls++;
                return value;
            }
        }

        // Succeeds, throws on the next call, then succeeds again — the shape that catches a cache
        // written before the conversion has actually produced a result.
        private sealed class ThrowsOnce : IConverter<int, int>
        {
            private int _calls;

            public int Convert(int value) => ++_calls == 2
                ? throw new InvalidOperationException("boom")
                : value + 1;
        }

        private sealed class ThrowsBack : ITwoWayConverter<int, int>
        {
            public int Convert(int value) => value + 1;

            public int ConvertBack(int value) => throw new InvalidOperationException("boom");
        }

        // The reverse counterpart of ThrowsOnce: the second reverse conversion throws.
        private sealed class ThrowsBackOnce : ITwoWayConverter<int, int>
        {
            private int _backCalls;

            public int Convert(int value) => value + 1;

            public int ConvertBack(int value) => ++_backCalls == 2
                ? throw new InvalidOperationException("boom")
                : value - 1;
        }

        // Counts each direction on its own, so a cache answering from the other direction shows up.
        private sealed class CountingTwoWay : ITwoWayConverter<int, int>
        {
            public int Calls { get; private set; }

            public int BackCalls { get; private set; }

            public int Convert(int value)
            {
                Calls++;
                return value + 1;
            }

            public int ConvertBack(int value)
            {
                BackCalls++;
                return value - 1;
            }
        }
    }
}
