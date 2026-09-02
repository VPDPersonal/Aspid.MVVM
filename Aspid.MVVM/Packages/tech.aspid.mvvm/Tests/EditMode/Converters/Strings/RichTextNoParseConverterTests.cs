using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RichTextNoParseConverter"/> — wrapping markup so it renders literally.
    /// </summary>
    [TestFixture]
    public sealed class RichTextNoParseConverterTests
    {
        // A player name like <size=400%> resizes the label it lands in, on every screen showing that
        // player. noparse makes TMP render the characters instead of obeying them.
        [Test]
        public void Convert_NeutralisesMarkup() =>
            Assert.AreEqual(
                "<noparse><size=400%>troll</noparse>",
                new RichTextNoParseConverter().Convert("<size=400%>troll"));

        [TestCase("")]
        [TestCase("   ")]
        public void Convert_LeavesBlankAlone(string value) =>
            Assert.AreSame(value, new RichTextNoParseConverter().Convert(value));
    }
}
