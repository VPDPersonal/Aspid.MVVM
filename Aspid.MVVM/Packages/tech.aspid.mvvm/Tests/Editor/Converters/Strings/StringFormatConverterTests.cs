using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringFormatConverter"/> as a full truth table:
    /// four input values × four format strings × both settings of <c>_formatEmptyValues</c>.
    /// </summary>
    /// <remarks>
    /// All thirty-two cells are asserted. The interesting one is a <see langword="null"/> input with a
    /// real format and <c>_formatEmptyValues</c> on: the base class short-circuits on
    /// <see langword="null"/> before <c>Format</c> is reached, so covering it takes an explicit
    /// <c>Convert</c> override and it gets a test of its own.
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
