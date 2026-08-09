using System;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="GenericToString{TFrom}"/> and its two sealed specialisations,
    /// <see cref="ObjectToStringConverter"/> and <see cref="TimeSpanToStringConverter"/>.
    /// </summary>
    /// <remarks>
    /// The format is a <b>composite</b> format string handed to <see cref="string.Format(string, object)"/>,
    /// not a plain format specifier — <c>"F2"</c> is a literal, and a <see cref="TimeSpan"/> pattern has
    /// to be wrapped as <c>{0:…}</c>. Both traps are pinned below because nothing in the API tells the
    /// caller. Assertions stay culture-independent so the suite does not depend on the editor locale.
    /// </remarks>
    [TestFixture]
    internal sealed class GenericToStringTests
    {
        [Test]
        public void Convert_Null_ReturnsNull() =>
            Assert.IsNull(new GenericToString<string>("{0}").Convert(null));

        [Test]
        public void Convert_NoFormat_FallsBackToToString() =>
            Assert.AreEqual("42", new GenericToString<int>().Convert(42));

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        public void Convert_BlankFormat_FallsBackToToString(string format) =>
            Assert.AreEqual("42", new GenericToString<int>(format).Convert(42));

        [Test]
        public void Convert_NullFormat_FallsBackToToString() =>
            Assert.AreEqual("42", new GenericToString<int>(null).Convert(42));

        [Test]
        public void Convert_Format_IsAppliedToTheTypedValue() =>
            Assert.AreEqual("HP: 42", new GenericToString<int>("HP: {0}").Convert(42));

        // A format specifier without a placeholder is a literal, not a specifier.
        [Test]
        public void Convert_FormatWithoutPlaceholder_ReturnsTheFormatVerbatim() =>
            Assert.AreEqual("F2", new GenericToString<float>("F2").Convert(3.5f));

        [Test]
        [Ignore("Fixed in PR 4 — strings. An Inspector-authored format must not throw into the dispatch.")]
        public void Convert_BrokenFormat_FallsBackToToStringInsteadOfThrowing() =>
            Assert.AreEqual("42", new GenericToString<int>("{0}/{1}").Convert(42));

        [Test]
        [Ignore("Fixed in PR 4 — strings.")]
        public void Convert_UnbalancedBrace_FallsBackToToStringInsteadOfThrowing() =>
            Assert.AreEqual("42", new GenericToString<int>("HP: {0} {").Convert(42));

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
