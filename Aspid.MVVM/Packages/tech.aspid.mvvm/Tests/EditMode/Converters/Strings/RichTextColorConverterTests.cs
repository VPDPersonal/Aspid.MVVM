using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RichTextColorConverter"/> — wrapping a string in a color tag, with and
    /// without the alpha channel.
    /// </summary>
    [TestFixture]
    public sealed class RichTextColorConverterTests
    {
        [Test]
        public void Convert_TagsTheText() =>
            Assert.AreEqual("<color=#FF0000>hp</color>", new RichTextColorConverter(Color.red).Convert("hp"));

        [Test]
        public void Convert_IncludesAlphaWhenAsked() =>
            Assert.AreEqual(
                "<color=#FF0000FF>hp</color>",
                new RichTextColorConverter(Color.red, includeAlpha: true).Convert("hp"));

        [TestCase("")]
        [TestCase("   ")]
        public void Convert_LeavesBlankUntagged(string value) =>
            Assert.AreSame(value, new RichTextColorConverter(Color.red).Convert(value));
    }
}
