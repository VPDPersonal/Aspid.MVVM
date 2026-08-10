using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="GenericToStringConverter{TFrom}"/> and its two sealed specialisations,
    /// <see cref="ObjectToStringConverter"/> and <see cref="TimeSpanToStringConverter"/>.
    /// </summary>
    /// <remarks>
    /// The format is a <b>composite</b> format string handed to <see cref="string.Format(string, object)"/>,
    /// not a plain format specifier — <c>"F2"</c> is a literal, and a <see cref="TimeSpan"/> pattern has
    /// to be wrapped as <c>{0:…}</c>. Both traps are pinned below because nothing in the API tells the
    /// caller. Assertions stay culture-independent so the suite does not depend on the editor locale.
    /// </remarks>
    [TestFixture]
    internal sealed class GenericToStringConverterTests
    {
        [Test]
        public void Convert_Null_ReturnsNull() =>
            Assert.IsNull(new GenericToStringConverter<string>("{0}").Convert(null));

        [Test]
        public void Convert_NoFormat_FallsBackToToString() =>
            Assert.AreEqual("42", new GenericToStringConverter<int>().Convert(42));

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void Convert_BlankFormat_FallsBackToToString(string format) =>
            Assert.AreEqual("42", new GenericToStringConverter<int>(format).Convert(42));

        [Test]
        public void Convert_NullFormat_FallsBackToToString() =>
            Assert.AreEqual("42", new GenericToStringConverter<int>(null).Convert(42));

        [Test]
        public void Convert_Format_IsAppliedToTheTypedValue() =>
            Assert.AreEqual("HP: 42", new GenericToStringConverter<int>("HP: {0}").Convert(42));

        // A format specifier without a placeholder is a literal, not a specifier.
        [Test]
        public void Convert_FormatWithoutPlaceholder_ReturnsTheFormatVerbatim() =>
            Assert.AreEqual("F2", new GenericToStringConverter<float>("F2").Convert(3.5f));

        // An Inspector-authored format is unvalidated input; throwing from here would tear the
        // multicast dispatch and take unrelated binders on the same object down with it.
        [Test]
        public void Convert_BrokenFormat_FallsBackToToStringInsteadOfThrowing()
        {
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));

            Assert.AreEqual("42", new GenericToStringConverter<int>("{0}/{1}").Convert(42));
        }

        [Test]
        public void Convert_UnbalancedBrace_FallsBackToToStringInsteadOfThrowing()
        {
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));

            Assert.AreEqual("42", new GenericToStringConverter<int>("HP: {0} {").Convert(42));
        }

        [Test]
        public void Convert_BrokenFormat_LogsEveryFailure()
        {
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));

            var converter = new GenericToStringConverter<int>("{0}/{1}");
            converter.Convert(1);
            converter.Convert(2);
            converter.Convert(3);
        }

        // Converters feed UI, so any exception out of Format — not just FormatException — must
        // degrade instead of tearing the binder dispatch.
        [Test]
        public void Convert_ThrowingFormatOverride_FallsBackToToString()
        {
            LogAssert.Expect(LogType.Error, new Regex("is invalid or threw"));

            Assert.AreEqual("42", new ThrowingFormat().Convert(42));
        }

        [Test]
        public void Convert_ErrorHookOverride_SuppliesTheFallback() =>
            Assert.AreEqual("n/a", new CustomErrorFallback().Convert(42));

        private sealed class ThrowingFormat : GenericToStringConverter<int>
        {
            public ThrowingFormat()
                : base("{0}") { }

            protected override string Format(int value) =>
                throw new InvalidOperationException("boom");
        }

        private sealed class CustomErrorFallback : GenericToStringConverter<int>
        {
            public CustomErrorFallback()
                : base("{0}/{1}") { }

            protected override string HandleFormatError(int value, Exception exception) => "n/a";
        }

        [Test]
        public void ObjectToStringConverter_NoFormat_FallsBackToToString() =>
            Assert.AreEqual("42", new ObjectToStringConverter().Convert(42));

        [Test]
        public void ObjectToStringConverter_Format_IsApplied() =>
            Assert.AreEqual("HP: 42", new ObjectToStringConverter("HP: {0}").Convert(42));

        [Test]
        public void ObjectToStringConverter_Null_ReturnsNull() =>
            Assert.IsNull(new ObjectToStringConverter("HP: {0}").Convert(null));

        [Test]
        public void TimeSpanToStringConverter_CompositeFormat_IsApplied() =>
            Assert.AreEqual("05:05", new TimeSpanToStringConverter("{0:mm\\:ss}").Convert(TimeSpan.FromSeconds(305)));

        // The obvious spelling — the one a TimeSpan.ToString() user reaches for — silently
        // returns the pattern itself, because there is no placeholder to substitute into.
        [Test]
        public void TimeSpanToStringConverter_BareTimeSpanPattern_ReturnsThePatternVerbatim() =>
            Assert.AreEqual("mm\\:ss", new TimeSpanToStringConverter("mm\\:ss").Convert(TimeSpan.FromSeconds(305)));
    }
}
