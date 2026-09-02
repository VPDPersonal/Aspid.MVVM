using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ConcatStringConverter"/> — wrapping a value and stripping the
    /// decoration back off on <c>ConvertBack</c>.
    /// </summary>
    [TestFixture]
    public sealed class ConcatStringConverterTests
    {
        [Test]
        public void Convert_WrapsTheValue() =>
            Assert.AreEqual("[abc]", new ConcatStringConverter("[", "]").Convert("abc"));

        [TestCase("")]
        [TestCase("   ")]
        public void Convert_LeavesBlankUndecorated(string value) =>
            Assert.AreSame(value, new ConcatStringConverter("[", "]").Convert(value));

        [Test]
        public void Convert_DecoratesBlankWhenAsked() =>
            Assert.AreEqual("[]", new ConcatStringConverter("[", "]", skipWhenEmpty: false).Convert(string.Empty));

        [Test]
        public void Convert_RoundTrips()
        {
            var converter = new ConcatStringConverter("[", "]");

            Assert.AreEqual("abc", converter.ConvertBack(converter.Convert("abc")));
        }

        // Text the user typed without the decoration comes back as they typed it, so a two-way input
        // field does not have to carry the brackets to be read.
        [TestCase("abc", "abc")]
        [TestCase("[abc", "abc")]
        [TestCase("abc]", "abc")]
        public void ConvertBack_UndecoratedTextIsLeftAlone(string value, string expected) =>
            Assert.AreEqual(expected, new ConcatStringConverter("[", "]").ConvertBack(value));

        // The prefix is claimed first, so the two cannot both take the same characters of a string
        // shorter than they are together.
        [Test]
        public void ConvertBack_PrefixAndSuffixDoNotOverlap() =>
            Assert.AreEqual(string.Empty, new ConcatStringConverter("ab", "ab").ConvertBack("ab"));
    }
}
