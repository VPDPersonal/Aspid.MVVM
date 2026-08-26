using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="DecimalFormatConverter"/> — the standard .NET format strings and the
    /// fallback for one .NET refuses.
    /// </summary>
    [TestFixture]
    internal sealed class DecimalFormatConverterTests
    {
        [Test]
        public void Convert_DefaultFormat_WritesTwoDecimalsWithGrouping() =>
            Assert.AreEqual("1,234.50", new DecimalFormatConverter("N2").Convert(1234.5m));

        [Test]
        public void Convert_FFormat_OmitsGrouping() =>
            Assert.AreEqual("1234.50", new DecimalFormatConverter("F2").Convert(1234.5m));

        // A typed-in format is not picked from a list, so a typo is not a compile error.
        [Test]
        public void Convert_UnusableFormat_FallsBackToTheGeneralRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a numeric format"));

            Assert.AreEqual(
                (1234.5m).ToString(CultureInfo.CurrentCulture),
                new DecimalFormatConverter("Q").Convert(1234.5m));
        }
    }
}
