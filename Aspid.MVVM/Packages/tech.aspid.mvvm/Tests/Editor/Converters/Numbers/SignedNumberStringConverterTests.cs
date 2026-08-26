using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="SignedNumberStringConverter"/> — the explicit plus sign, the hidden
    /// zero, and the unusable-format fallback.
    /// </summary>
    [TestFixture]
    internal sealed class SignedNumberStringConverterTests
    {
        [Test]
        public void Convert_Positive_ShowsAnExplicitPlus() =>
            Assert.AreEqual("+15", new SignedNumberStringConverter("0.##").Convert(15f));

        [Test]
        public void Convert_Negative_ShowsTheMinus() =>
            Assert.AreEqual("-3", new SignedNumberStringConverter("0.##").Convert(-3f));

        [Test]
        public void Convert_Zero_ShowsAPlusByDefault() =>
            Assert.AreEqual("+0", new SignedNumberStringConverter("0.##").Convert(0f));

        [Test]
        public void Convert_HideZero_ReturnsEmptyForZero() =>
            Assert.AreEqual(string.Empty, new SignedNumberStringConverter("0.##", hideZero: true).Convert(0f));

        // The int, long and double overloads are explicit, so they are reached through the interface
        // rather than the class.
        [Test]
        public void Convert_IntInput_ShowsAnExplicitPlus() =>
            Assert.AreEqual(
                "+15",
                ((IConverter<int, string>)new SignedNumberStringConverter("0.##")).Convert(15));

        [Test]
        public void Convert_UnusableFormat_FallsBackToTheGeneralRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a numeric format"));

            Assert.AreEqual("+15", new SignedNumberStringConverter("Q").Convert(15f));
        }
    }
}
