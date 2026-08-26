using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="NumberFormatConverter"/> — the four numeric overloads and the
    /// unusable-format fallback shared by all of them.
    /// </summary>
    [TestFixture]
    internal sealed class NumberFormatConverterTests
    {
        [Test]
        public void Convert_Float_UsesTheFormat() =>
            Assert.AreEqual("1,234", new NumberFormatConverter("N0").Convert(1234f));

        [Test]
        public void Convert_Double_UsesTheFormat() =>
            Assert.AreEqual("1,234", new NumberFormatConverter("N0").Convert(1234d));

        [Test]
        public void Convert_Int_UsesTheFormat() =>
            Assert.AreEqual("1,234", new NumberFormatConverter("N0").Convert(1234));

        [Test]
        public void Convert_Long_UsesTheFormat() =>
            Assert.AreEqual("1,234", new NumberFormatConverter("N0").Convert(1234L));

        [Test]
        public void Convert_UnusableFormat_FallsBackToTheGeneralRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a numeric format"));

            Assert.AreEqual((1234).ToString(CultureInfo.CurrentCulture), new NumberFormatConverter("Q").Convert(1234));
        }
    }
}
