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
    /// Coverage for <see cref="NullGuardConverter{TIn,TOut}"/> — the configured null result and the
    /// missing-inner reports.
    /// </summary>
    [TestFixture]
    public sealed class NullGuardConverterTests
    {
        [Test]
        public void NullGuard_NullInnerInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new NullGuardConverter<string, string>(null!));

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

        private sealed class ToUpper : IConverter<string?, string?>
        {
            public string? Convert(string? value) => value?.ToUpperInvariant();
        }

        private sealed class ThrowsOnNull : IConverter<string?, string?>
        {
            public string? Convert(string? value) => value!.ToUpperInvariant();
        }
    }
}
