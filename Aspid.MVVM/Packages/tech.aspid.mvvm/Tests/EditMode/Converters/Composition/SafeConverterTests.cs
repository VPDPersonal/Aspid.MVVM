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
    /// Coverage for <see cref="SafeConverter{TIn,TOut}"/> — substituting the fallback when the inner
    /// converter throws or is missing, in both directions.
    /// </summary>
    [TestFixture]
    public sealed class SafeConverterTests
    {
        [Test]
        public void Safe_NullInnerInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new SafeConverter<int, int>(null!));

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
            Assert.AreEqual(8, new SafeConverter<int, int>(new AddConverter(1), fallback: -1).Convert(7));

        [Test]
        public void Safe_UndoesTheInnerConverter()
        {
            var converter = new SafeConverter<int, int>(new TwoWayAddConverter(1), fallback: -1, convertBackFallback: -2);

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
                LogAssert.Expect(LogType.Error, new Regex("AddConverter converts one way only"));

            var converter = new SafeConverter<int, int>(new AddConverter(1), fallback: -1, convertBackFallback: -2);
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

        private sealed class Throws : IConverter<int, int>
        {
            public int Convert(int value) => throw new InvalidOperationException("boom");
        }

        private sealed class ThrowsBack : ITwoWayConverter<int, int>
        {
            public int Convert(int value) => value + 1;

            public int ConvertBack(int value) => throw new InvalidOperationException("boom");
        }
    }
}
