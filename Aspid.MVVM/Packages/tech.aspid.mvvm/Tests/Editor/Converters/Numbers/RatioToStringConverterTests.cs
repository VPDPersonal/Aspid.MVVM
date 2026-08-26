using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="RatioToStringConverter"/> — the default format, the maximum, and the
    /// rounding.
    /// </summary>
    [TestFixture]
    internal sealed class RatioToStringConverterTests
    {
        [Test]
        public void Convert_DefaultFormat_WritesValueSlashMax() =>
            Assert.AreEqual("35 / 100", new RatioToStringConverter(100f).Convert(35f));

        [Test]
        public void Convert_CustomFormat_IsUsed() =>
            Assert.AreEqual("35 of 100", new RatioToStringConverter(100f, "{0} of {1}").Convert(35f));

        // A half lands on the nearest even number under the default rounding.
        [TestCase(0.5f, "0 / 100")]
        [TestCase(1.5f, "2 / 100")]
        public void Convert_Rounds_ToTheNearestEvenValue(float value, string expected) =>
            Assert.AreEqual(expected, new RatioToStringConverter(100f).Convert(value));

        // The format is typed in, so a stray brace or a third placeholder reaches string.Format from
        // the Inspector — the converter has to report it rather than take the binder down.
        [TestCase("{0} / {2}")]
        [TestCase("{0")]
        public void Convert_InvalidFormat_ReportsAndWritesTheDefaultLayout(string format)
        {
            LogAssert.Expect(LogType.Error, new Regex("RatioToStringConverter.*not a composite format"));

            Assert.AreEqual("35 / 100", new RatioToStringConverter(100f, format).Convert(35f));
        }
    }
}
