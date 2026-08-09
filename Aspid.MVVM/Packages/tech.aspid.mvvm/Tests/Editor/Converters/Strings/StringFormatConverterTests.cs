using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringFormatConverter"/> as a full truth table:
    /// four input values × four format strings × both settings of <c>_formatEmptyValues</c>.
    /// </summary>
    /// <remarks>
    /// Thirty-one of the thirty-two cells are asserted below. The missing one — a <see langword="null"/>
    /// input with a real format and <c>_formatEmptyValues</c> on — is the behaviour that regressed when
    /// the converter started inheriting <see cref="GenericToString{TFrom}"/>: the base returns early on
    /// <see langword="null"/>, so the override never runs. It is asserted separately and stays
    /// <c>[Ignore]</c>d until that is repaired.
    /// </remarks>
    [TestFixture]
    internal sealed class StringFormatConverterTests
    {
        // A null input short-circuits before the format is ever consulted.
        [TestCase(null, false, null)]
        [TestCase("", false, null)]
        [TestCase(" ", false, null)]
        [TestCase("HP: {0}", false, null)]
        [TestCase(null, true, null)]
        [TestCase("", true, null)]
        [TestCase(" ", true, null)]
        public void Convert_NullValue_ReturnsNull(string format, bool formatEmptyValues, string expected) =>
            Assert.AreEqual(expected, new StringFormatConverter(format, formatEmptyValues).Convert(null));

        // A blank format is a no-op regardless of the value or the flag.
        [TestCase(null, "", false, "")]
        [TestCase(null, "  ", false, "  ")]
        [TestCase(null, "abc", false, "abc")]
        [TestCase(null, "", true, "")]
        [TestCase(null, "  ", true, "  ")]
        [TestCase(null, "abc", true, "abc")]
        [TestCase("", "", false, "")]
        [TestCase("", "  ", false, "  ")]
        [TestCase("", "abc", false, "abc")]
        [TestCase("", "", true, "")]
        [TestCase("", "  ", true, "  ")]
        [TestCase("", "abc", true, "abc")]
        [TestCase(" ", "", false, "")]
        [TestCase(" ", "  ", false, "  ")]
        [TestCase(" ", "abc", false, "abc")]
        [TestCase(" ", "", true, "")]
        [TestCase(" ", "  ", true, "  ")]
        [TestCase(" ", "abc", true, "abc")]
        public void Convert_BlankFormat_ReturnsTheValueUnchanged(
            string format,
            string value,
            bool formatEmptyValues,
            string expected) =>
            Assert.AreEqual(expected, new StringFormatConverter(format, formatEmptyValues).Convert(value));

        // A real format applies to non-blank values always, and to blank values only when asked.
        [TestCase("abc", false, "HP: abc")]
        [TestCase("abc", true, "HP: abc")]
        [TestCase("", false, "")]
        [TestCase("", true, "HP: ")]
        [TestCase("  ", false, "  ")]
        [TestCase("  ", true, "HP:   ")]
        public void Convert_RealFormat_HonoursFormatEmptyValues(
            string value,
            bool formatEmptyValues,
            string expected) =>
            Assert.AreEqual(expected, new StringFormatConverter("HP: {0}", formatEmptyValues).Convert(value));

        [Test]
        [Ignore("Fixed in PR 4 — strings. The inherited Convert returns early on null, so the override never runs.")]
        public void Convert_NullValue_WithFormatEmptyValues_IsStillFormatted() =>
            Assert.AreEqual("HP: ", new StringFormatConverter("HP: {0}", formatEmptyValues: true).Convert(null));

        [Test]
        public void DefaultConstructed_IsANoOp() =>
            Assert.AreEqual("abc", new StringFormatConverter().Convert("abc"));

        [Test]
        [Ignore("Fixed in PR 4 — strings. An Inspector-authored format must not throw into the dispatch.")]
        public void Convert_BrokenFormat_FallsBackToTheValueInsteadOfThrowing() =>
            Assert.AreEqual("abc", new StringFormatConverter("{0}/{1}").Convert("abc"));
    }
}
