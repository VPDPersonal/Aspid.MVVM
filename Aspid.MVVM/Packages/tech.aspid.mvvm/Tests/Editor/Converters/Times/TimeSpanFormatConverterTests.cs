using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="TimeSpanFormatConverter"/> — the real <see cref="TimeSpan"/> format
    /// string and the fallback for a broken one.
    /// </summary>
    [TestFixture]
    internal sealed class TimeSpanFormatConverterTests
    {
        private static readonly TimeSpan _duration = new(0, 1, 2, 3);

        [Test]
        public void Convert_UsesTheAuthoredFormat() =>
            Assert.AreEqual(@"01:02:03", new TimeSpanFormatConverter(@"hh\:mm\:ss").Convert(_duration));

        [Test]
        public void Convert_ABrokenFormat_FallsBackToTheDefaultRendering()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a TimeSpan format"));

            Assert.AreEqual(_duration.ToString(), new TimeSpanFormatConverter("'unterminated").Convert(_duration));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Convert_AnEmptyFormat_UsesTheDefaultRendering(string format) =>
            Assert.AreEqual(_duration.ToString(), new TimeSpanFormatConverter(format).Convert(_duration));
    }
}
