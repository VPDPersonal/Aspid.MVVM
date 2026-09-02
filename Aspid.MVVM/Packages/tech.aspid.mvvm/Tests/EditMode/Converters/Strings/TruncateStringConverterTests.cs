using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="TruncateStringConverter"/> — the three <see cref="TruncateSide"/>
    /// options, the word-boundary flag and the surrogate-pair guard.
    /// </summary>
    [TestFixture]
    public sealed class TruncateStringConverterTests
    {
        [Test]
        public void Convert_CutsTheEnd() =>
            Assert.AreEqual("abcdefghi…", new TruncateStringConverter(10).Convert("abcdefghijklmnop"));

        [Test]
        public void Convert_LeavesShortStringsAlone() =>
            Assert.AreEqual("abc", new TruncateStringConverter(10).Convert("abc"));

        [Test]
        public void Convert_CutsTheStartWhenAsked() =>
            Assert.AreEqual("…hijklmnop", new TruncateStringConverter(10, TruncateSide.Start).Convert("abcdefghijklmnop"));

        [Test]
        public void Convert_CutsTheMiddleWhenAsked() =>
            Assert.AreEqual("abcde…mnop", new TruncateStringConverter(10, TruncateSide.Middle).Convert("abcdefghijklmnop"));

        [Test]
        public void Convert_StopsAtAWordBoundaryWhenAsked() =>
            Assert.AreEqual(
                "hello…",
                new TruncateStringConverter(10, atWordBoundary: true).Convert("hello beautiful world"));

        // A limit shorter than the marker leaves nothing sensible to keep.
        [Test]
        public void Convert_LimitShorterThanTheEllipsis() =>
            Assert.AreEqual("…", new TruncateStringConverter(1).Convert("abcdef"));

        // The word boundary is honoured by the End side alone — the other two have no head to walk
        // back through — so a Start cut lands mid-word however the flag is set.
        [Test]
        public void Convert_WordBoundaryAppliesToTheEndSideOnly() =>
            Assert.AreEqual(
                "…iful world",
                new TruncateStringConverter(11, TruncateSide.Start, atWordBoundary: true).Convert("hello beautiful world"));

        // A surrogate pair is one character stored as two: a cut between the halves leaves a lone half
        // that renders as a replacement box, so the cut moves off it and the character is dropped whole.
        [Test]
        public void Convert_DoesNotSplitASurrogatePair()
        {
            const string value = "abcd😀ef";

            Assert.AreEqual("abcd…", new TruncateStringConverter(6).Convert(value));
            Assert.AreEqual("…ef", new TruncateStringConverter(4, TruncateSide.Start).Convert(value));
        }

        // A limit no string could ever be shortened to is a misconfiguration, not a way to switch the
        // converter off, so it is reported on every push.
        [TestCase(0)]
        [TestCase(-5)]
        public void Convert_ANonPositiveLimit_IsReported(int maxLength)
        {
            LogAssert.Expect(LogType.Error, new Regex("TruncateStringConverter.*not positive"));

            Assert.AreEqual("abcdef", new TruncateStringConverter(maxLength).Convert("abcdef"));
        }

        // The side is consulted only once the string is over the limit and the marker fits, so an
        // undeclared side is reached only past both guards. Unshortened is the only answer that cannot
        // be mistaken for one of the three declared cuts.
        [Test]
        public void Convert_UndeclaredSide_ReportsAndReturnsTheStringUnshortened()
        {
            LogAssert.Expect(LogType.Error, new Regex("TruncateStringConverter.*not a declared TruncateSide"));

            Assert.AreEqual(
                "hello beautiful world",
                new TruncateStringConverter(10, (TruncateSide)42).Convert("hello beautiful world"));
        }
    }
}
