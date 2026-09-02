using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RichTextStyleConverter"/> — wrapping a string in the configured style
    /// tags.
    /// </summary>
    [TestFixture]
    public sealed class RichTextStyleConverterTests
    {
        [Test]
        public void Convert_WrapsInTheRequestedTags() =>
            Assert.AreEqual("<i><b>hp</b></i>", new RichTextStyleConverter(bold: true, italic: true).Convert("hp"));

        [Test]
        public void Convert_WithNothingSetLeavesTheTextAlone() =>
            Assert.AreEqual("hp", new RichTextStyleConverter().Convert("hp"));

        // There is nothing to style in a blank string, so no tag is put around it — a label bound to
        // an unfilled field stays empty instead of holding a pair of empty tags.
        [TestCase("")]
        [TestCase("   ")]
        public void Convert_LeavesBlankUntagged(string value) =>
            Assert.AreSame(value, new RichTextStyleConverter(bold: true).Convert(value));
    }
}
