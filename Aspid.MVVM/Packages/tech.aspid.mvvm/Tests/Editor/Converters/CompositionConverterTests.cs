using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

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
        public void Compose_MissingLink_ReturnsDefaultAndReportsEveryTime()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("both links are required"));

            var converter = new ComposeConverter<int, int, string>(new Add(1), null);
            Assert.IsNull(converter.Convert(7));
            converter.Convert(8);
            converter.Convert(9);
        }

        [Test]
        public void Conditional_TrueBranch_IsApplied() =>
            Assert.AreEqual(11, Conditional(new IsPositive(), new Add(1), new Add(100)).Convert(10));

        [Test]
        public void Conditional_FalseBranch_IsApplied() =>
            Assert.AreEqual(90, Conditional(new IsPositive(), new Add(1), new Add(100)).Convert(-10));

        [Test]
        public void Conditional_NoPredicate_PassesTheValueThrough() =>
            Assert.AreEqual(10, Conditional(null, new Add(1), new Add(100)).Convert(10));

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
        public void Safe_ReportsOncePerInstance()
        {
            LogAssert.Expect(LogType.Error, new Regex("threw"));

            var converter = new SafeConverter<int, int>(new Throws(), fallback: -1);
            converter.Convert(1);
            converter.Convert(2);
            converter.Convert(3);
        }

        [Test]
        public void Safe_StaysQuietWhenAsked()
        {
            Assert.AreEqual(-1, new SafeConverter<int, int>(new Throws(), -1, logErrors: false).Convert(7));
        }

        [Test]
        public void Safe_NoInner_ReturnsTheFallback() =>
            Assert.AreEqual(-1, new SafeConverter<int, int>(null, fallback: -1).Convert(7));

        [Test]
        public void Safe_PassesThroughWhenTheInnerSucceeds() =>
            Assert.AreEqual(8, new SafeConverter<int, int>(new Add(1), fallback: -1).Convert(7));

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
        public void Passthrough_ReturnsTheInput() =>
            Assert.AreEqual(7, new PassthroughConverter<int>().Convert(7));

        private static ConditionalConverter<int> Conditional(
            IConverter<int, bool>? predicate,
            IConverter<int, int>? then,
            IConverter<int, int>? @else) =>
            new(predicate, then, @else);

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
    }
}
