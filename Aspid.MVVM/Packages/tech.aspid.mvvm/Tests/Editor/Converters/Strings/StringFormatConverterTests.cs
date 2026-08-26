using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringFormatConverter"/> as a full truth table:
    /// four input values × four format strings × both settings of <c>_formatEmptyValues</c>.
    /// </summary>
    /// <remarks>
    /// All thirty-two cells are asserted. The interesting ones are the <see langword="null"/> inputs
    /// with <c>_formatEmptyValues</c> on: the base class short-circuits on <see langword="null"/>
    /// before <c>Format</c> is reached, so the override substitutes an empty string ahead of it — and
    /// a null and an empty input then answer the same thing whatever the format is.
    /// </remarks>
    [TestFixture]
    internal sealed class StringFormatConverterTests
    {
        // Left alone, a null input short-circuits before the format is ever consulted.
        [TestCase(null, false, null)]
        [TestCase("", false, null)]
        [TestCase(" ", false, null)]
        [TestCase("HP: {0}", false, null)]
        public void Convert_NullValue_ReturnsNull(string format, bool formatEmptyValues, string expected) =>
            Assert.AreEqual(expected, new StringFormatConverter(format, formatEmptyValues).Convert(null));

        // With blank values being formatted, null and an empty string are the same absent value, so a
        // format that has nothing to say about it hands back the empty string rather than the null.
        // Anything else would make the two inputs disagree for no reason a caller could see.
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Convert_NullValue_WithFormatEmptyValues_ReadsAsEmpty(string format)
        {
            var converter = new StringFormatConverter(format, formatEmptyValues: true);

            Assert.AreEqual(string.Empty, converter.Convert(null));
            Assert.AreEqual(converter.Convert(string.Empty), converter.Convert(null));
        }

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
        public void Convert_NullValue_WithFormatEmptyValues_IsStillFormatted() =>
            Assert.AreEqual("HP: ", new StringFormatConverter("HP: {0}", formatEmptyValues: true).Convert(null));

        [Test]
        public void DefaultConstructed_IsANoOp() =>
            Assert.AreEqual("abc", new StringFormatConverter().Convert("abc"));

        [Test]
        public void Convert_BrokenFormat_FallsBackToTheValueInsteadOfThrowing()
        {
            LogAssert.Expect(LogType.Error, new Regex("is invalid"));

            Assert.AreEqual("abc", new StringFormatConverter("{0}/{1}").Convert("abc"));
        }
    }
}
