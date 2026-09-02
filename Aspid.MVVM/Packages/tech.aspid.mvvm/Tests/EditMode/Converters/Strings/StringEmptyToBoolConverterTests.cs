using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringEmptyToBoolConverter"/> — the four <see cref="StringEmptiness"/>
    /// readings and the default.
    /// </summary>
    [TestFixture]
    public sealed class StringEmptyToBoolConverterTests
    {
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("   ", true)]
        [TestCase("\t", true)]
        [TestCase("abc", false)]
        public void Convert_CountsBlankAsEmptyWhenAsked(string value, bool expected) =>
            Assert.AreEqual(
                expected,
                new StringEmptyToBoolConverter(StringEmptiness.NullOrWhiteSpace).Convert(value));

        [TestCase(StringEmptiness.Null, null, true)]
        [TestCase(StringEmptiness.Null, "", false)]
        [TestCase(StringEmptiness.NullOrEmpty, "", true)]
        [TestCase(StringEmptiness.NullOrEmpty, "   ", false)]
        public void Convert_HonoursTheConfiguredEmptiness(
            StringEmptiness emptiness,
            string value,
            bool expected) =>
            Assert.AreEqual(expected, new StringEmptyToBoolConverter(emptiness).Convert(value));

        [Test]
        public void Convert_DefaultsToNullOrEmpty() =>
            Assert.IsTrue(new StringEmptyToBoolConverter().Convert(string.Empty));
    }
}
