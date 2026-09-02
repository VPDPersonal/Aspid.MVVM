using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="SubstringConverter"/> — clamping the slice to what the string holds.
    /// </summary>
    [TestFixture]
    public sealed class SubstringConverterTests
    {
        [Test]
        public void Convert_TakesTheSlice() =>
            Assert.AreEqual("bcd", new SubstringConverter(1, 3).Convert("abcdef"));

        [Test]
        public void Convert_ClampsToWhatIsThere() =>
            Assert.AreEqual("ef", new SubstringConverter(4, 10).Convert("abcdef"));

        [Test]
        public void Convert_StartPastTheEndYieldsEmpty() =>
            Assert.AreEqual(string.Empty, new SubstringConverter(10, 3).Convert("abc"));

        // Nothing to slice: the guard hands the string back whole rather than cutting a space out of it.
        [Test]
        public void Convert_BlankIsReturnedUnchanged()
        {
            const string value = "   ";

            Assert.AreSame(value, new SubstringConverter(0, 1).Convert(value));
        }
    }
}
