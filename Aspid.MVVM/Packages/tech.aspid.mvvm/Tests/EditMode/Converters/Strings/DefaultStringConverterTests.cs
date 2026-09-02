using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DefaultStringConverter"/> — substituting a fallback for a blank value.
    /// </summary>
    [TestFixture]
    public sealed class DefaultStringConverterTests
    {
        [TestCase(null, "—")]
        [TestCase("", "—")]
        [TestCase("   ", "—")]
        [TestCase("abc", "abc")]
        public void Convert_Blank_SubstitutesForBlank(string value, string expected) =>
            Assert.AreEqual(expected, new DefaultStringConverter("—").Convert(value));
    }
}
