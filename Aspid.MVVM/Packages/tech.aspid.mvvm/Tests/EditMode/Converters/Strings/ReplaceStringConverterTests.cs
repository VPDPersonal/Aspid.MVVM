using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ReplaceStringConverter"/> — case-insensitive matching and an empty
    /// search pattern.
    /// </summary>
    [TestFixture]
    public sealed class ReplaceStringConverterTests
    {
        [Test]
        public void Convert_SwapsEveryOccurrence() =>
            Assert.AreEqual("a-b-c", new ReplaceStringConverter("_", "-").Convert("a_b_c"));

        [Test]
        public void Convert_CanIgnoreCase() =>
            Assert.AreEqual("xbx", new ReplaceStringConverter("a", "x", ignoreCase: true).Convert("AbA"));

        [Test]
        public void Convert_EmptySearchPassesThrough() =>
            Assert.AreEqual("abc", new ReplaceStringConverter("", "x").Convert("abc"));
    }
}
