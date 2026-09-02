using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="MaskStringConverter"/> — the kept head and tail, and the
    /// surrogate-pair guard.
    /// </summary>
    [TestFixture]
    public sealed class MaskStringConverterTests
    {
        [Test]
        public void Convert_HidesTheMiddle() =>
            Assert.AreEqual("ab••••gh", new MaskStringConverter(2, 2).Convert("abcdefgh"));

        // A string too short to keep both ends is masked completely, so a short value never leaks by
        // being left alone.
        [Test]
        public void Convert_ShortStringIsMaskedCompletely() =>
            Assert.AreEqual("•••", new MaskStringConverter(2, 2).Convert("abc"));

        // A surrogate pair is one character stored as two: a count landing between the halves hides
        // the whole character rather than showing a lone half, which renders as a replacement box.
        [Test]
        public void Convert_DoesNotSplitASurrogatePair()
        {
            const string value = "ab😀cd";

            Assert.AreEqual("ab••cd", new MaskStringConverter(3, 2).Convert(value));
            Assert.AreEqual("a•••cd", new MaskStringConverter(1, 3).Convert(value));
        }

        // A blank value has nothing to hide, so it comes back as it arrived rather than as a row of
        // bullets the width of the spaces.
        [Test]
        public void Convert_BlankIsLeftUnmasked()
        {
            const string value = "   ";

            Assert.AreSame(value, new MaskStringConverter(2, 2).Convert(value));
        }
    }
}
