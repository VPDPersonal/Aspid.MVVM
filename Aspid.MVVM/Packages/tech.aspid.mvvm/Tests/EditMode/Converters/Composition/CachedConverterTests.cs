#nullable enable
using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using static Aspid.MVVM.Tests.ConverterReflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="CachedConverter{TIn,TOut}"/> — the per-direction caches, cache
    /// invalidation on a changed input, and the throw-does-not-poison guarantee.
    /// </summary>
    [TestFixture]
    public sealed class CachedConverterTests
    {
        [Test]
        public void Cached_NullInnerInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new CachedConverter<int, int>(null!));

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
            Assert.AreEqual(8, new CachedConverter<int, int>(new AddConverter(1)).Convert(7));

        [Test]
        public void Cached_CachesTheFirstResultIncludingDefaults()
        {
            var converter = new CachedConverter<int, int>(new AddConverter(0));

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
            var converter = new CachedConverter<int, int>(new TwoWayAddConverter(1));

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
                LogAssert.Expect(LogType.Error, new Regex("AddConverter converts one way only"));

            var converter = new CachedConverter<int, int>(new AddConverter(1));
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
