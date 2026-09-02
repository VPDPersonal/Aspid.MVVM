using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="BoolInvertConverter"/> negating in both directions.
    /// </summary>
    [TestFixture]
    public sealed class BoolInvertConverterTests
    {
        [Test]
        public void Convert_Negates()
        {
            Assert.IsFalse(new BoolInvertConverter().Convert(true));
            Assert.IsTrue(new BoolInvertConverter().Convert(false));
        }

        [Test]
        public void Convert_IsItsOwnInverse() =>
            Assert.IsTrue(new BoolInvertConverter().ConvertBack(new BoolInvertConverter().Convert(true)));
    }
}
