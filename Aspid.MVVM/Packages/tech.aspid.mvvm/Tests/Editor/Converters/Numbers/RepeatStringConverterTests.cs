using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="RepeatStringConverter"/> — the empty-unit remainder, the negative-count
    /// clamp, and the ceiling reported when no maximum is set.
    /// </summary>
    [TestFixture]
    internal sealed class RepeatStringConverterTests
    {
        [Test]
        public void Convert_WritesOneUnitPerCount() =>
            Assert.AreEqual("★★★", new RepeatStringConverter("★").Convert(3));

        [Test]
        public void Convert_FillsTheRemainderToTheMaximum() =>
            Assert.AreEqual("★★★☆☆", new RepeatStringConverter("★", 5, "☆").Convert(3));

        [Test]
        public void Convert_ClampsAboveTheMaximum() =>
            Assert.AreEqual("★★★★★", new RepeatStringConverter("★", 5, "☆").Convert(9));

        [Test]
        public void Convert_NegativeCount_WritesNothing() =>
            Assert.AreEqual(string.Empty, new RepeatStringConverter("★").Convert(-3));

        [Test]
        public void Convert_EmptyRemainderUnit_LeavesTheRemainderUnwritten() =>
            Assert.AreEqual("★★★", new RepeatStringConverter("★", 5, string.Empty).Convert(3));

        // With no maximum the count is whatever the ViewModel sends, and a runaway one is capped and
        // the cap is reported.
        [Test]
        public void Convert_NoMaximum_CapsTheCountAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("RepeatStringConverter.*ceiling"));

            Assert.AreEqual(1000, new RepeatStringConverter("★", max: 0).Convert(5000).Length);
        }

        [Test]
        public void Convert_NoMaximum_BelowTheCeiling_WritesTheExactCount() =>
            Assert.AreEqual(7, new RepeatStringConverter("★", max: 0).Convert(7).Length);

        // A null unit has nothing to repeat, and the failure belongs where it was passed rather than
        // inside a conversion far away.
        [Test]
        public void Constructor_NullUnit_Throws() =>
            Assert.Throws<System.ArgumentNullException>(() => new RepeatStringConverter(null!));
    }
}
