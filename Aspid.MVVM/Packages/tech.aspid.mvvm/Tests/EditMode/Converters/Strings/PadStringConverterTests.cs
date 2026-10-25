using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="PadStringConverter"/> — padding to a fixed width on either side.
    /// </summary>
    [TestFixture]
    public sealed class PadStringConverterTests
    {
        [Test]
        public void Convert_PadsToWidth()
        {
            Assert.AreEqual("     abc", new PadStringConverter(8).Convert("abc"));
            Assert.AreEqual("abc     ", new PadStringConverter(8, padLeft: false).Convert("abc"));
        }

        // PadLeft and PadRight throw on a negative width. [Min(0)] holds the Inspector at zero, so the
        // constructor is the only way one gets in, and it refuses it.
        [Test]
        public void Constructor_NegativeWidth_Throws() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new PadStringConverter(-4));
    }
}
