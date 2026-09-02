using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringMatchToBoolConverter"/> — the four <see cref="StringMatch"/>
    /// modes, case sensitivity, invert, and the blank-text degrade.
    /// </summary>
    [TestFixture]
    public sealed class StringMatchToBoolConverterTests
    {
        [TestCase(StringMatch.Equals, "abc", true)]
        [TestCase(StringMatch.Equals, "abcd", false)]
        [TestCase(StringMatch.Contains, "xabcx", true)]
        [TestCase(StringMatch.StartsWith, "abcx", true)]
        [TestCase(StringMatch.StartsWith, "xabc", false)]
        [TestCase(StringMatch.EndsWith, "xabc", true)]
        public void Convert_TestsAgainstTheAuthoredText(StringMatch match, string value, bool expected) =>
            Assert.AreEqual(expected, new StringMatchToBoolConverter(match, "abc").Convert(value));

        [Test]
        public void Convert_IgnoresCaseByDefault() =>
            Assert.IsTrue(new StringMatchToBoolConverter(StringMatch.Equals, "abc").Convert("ABC"));

        [Test]
        public void Convert_HonoursCaseWhenAsked() =>
            Assert.IsFalse(
                new StringMatchToBoolConverter(StringMatch.Equals, "abc", ignoreCase: false).Convert("ABC"));

        [Test]
        public void Convert_NullMatchesNothing() =>
            Assert.IsFalse(new StringMatchToBoolConverter(StringMatch.Equals, "abc").Convert(null));

        // Contains, StartsWith and EndsWith all answer true for an empty needle, so an unfilled field
        // would present as a converter that is always on rather than as one nobody finished authoring.
        [Test]
        public void Convert_BlankText_ReportsEveryPushAndAnswersFalse()
        {
            var converter = new StringMatchToBoolConverter(StringMatch.Contains, string.Empty);

            LogAssert.Expect(LogType.Error, new Regex("StringMatchToBoolConverter.*blank"));
            LogAssert.Expect(LogType.Error, new Regex("StringMatchToBoolConverter.*blank"));

            Assert.IsFalse(converter.Convert("abc"));
            Assert.IsFalse(converter.Convert(null));
        }

        // The answer is the documented fallback rather than the result of a comparison, so inversion
        // has nothing to invert.
        [Test]
        public void Convert_BlankText_IsNotInverted()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringMatchToBoolConverter.*blank"));

            Assert.IsFalse(
                new StringMatchToBoolConverter(StringMatch.Equals, string.Empty, isInvert: true).Convert("abc"));
        }
    }
}
