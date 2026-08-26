using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="GenericToStringConverter{TFrom}"/>, including its
    /// <see langword="object"/>-typed closed form.
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

        // A broken format string is the authoring mistake the fallback exists for, and only that.
        // Anything else out of an overridden Format is a bug in the override, and swallowing it would
        // present as a value rather than as a problem.
        [Test]
        public void Convert_FormatOverrideThrowingSomethingElse_Propagates() =>
            Assert.Throws<InvalidOperationException>(() => new ThrowingFormat().Convert(42));

        // The fallback path has a failure of its own: a value whose ToString throws would take the
        // binder down from inside the handler that exists to keep it up.
        [Test]
        public void Convert_BrokenFormatAndThrowingToString_ReturnsTheTypeName()
        {
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));
            LogAssert.Expect(LogType.Error, new Regex(@"ToString\(\) also threw"));

            // An unbalanced brace fails while the format is being parsed, before the value is ever
            // asked to print itself — otherwise the throw would leave Format as an InvalidOperationException.
            Assert.AreEqual("Unprintable", new GenericToStringConverter<Unprintable>("{").Convert(new Unprintable()));
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

        private sealed class Unprintable
        {
            public override string ToString() =>
                throw new InvalidOperationException("boom");
        }

        private sealed class CustomErrorFallback : GenericToStringConverter<int>
        {
            public CustomErrorFallback()
                : base("{0}/{1}") { }

            protected override string HandleFormatError(int value, Exception exception) => "n/a";
        }

        [Test]
        public void ObjectClosedForm_NoFormat_FallsBackToToString() =>
            Assert.AreEqual("42", new GenericToStringConverter<object>().Convert(42));

        [Test]
        public void ObjectClosedForm_Format_IsApplied() =>
            Assert.AreEqual("HP: 42", new GenericToStringConverter<object>("HP: {0}").Convert(42));

        [Test]
        public void ObjectClosedForm_Null_ReturnsNull() =>
            Assert.IsNull(new GenericToStringConverter<object>("HP: {0}").Convert(null));

        // The obvious spelling — the one a TimeSpan.ToString() user reaches for — silently
        // returns the pattern itself, because there is no placeholder to substitute into.
        [Test]
        public void GenericToString_BareTimeSpanPattern_ReturnsThePatternVerbatim() =>
            Assert.AreEqual("mm\\:ss", new GenericToStringConverter<TimeSpan>("mm\\:ss").Convert(TimeSpan.FromSeconds(305)));
    }
}
