using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="IntToRectOffsetConverter"/> — writing every side by default and
    /// writing only the chosen ones.
    /// </summary>
    [TestFixture]
    public sealed class IntToRectOffsetConverterTests
    {
        [Test]
        public void Convert_WritesEveryUnspecifiedSideByDefault()
        {
            var padding = new IntToRectOffsetConverter().Convert(5);

            Assert.AreEqual(5, padding.left);
            Assert.AreEqual(5, padding.right);
            Assert.AreEqual(5, padding.top);
            Assert.AreEqual(5, padding.bottom);
        }

        [Test]
        public void Convert_Horizontal_WritesOnlyLeftAndRight()
        {
            var padding = new IntToRectOffsetConverter(RectSides.Horizontal).Convert(5);

            Assert.AreEqual(5, padding.left);
            Assert.AreEqual(5, padding.right);
            Assert.AreEqual(0, padding.top);
            Assert.AreEqual(0, padding.bottom);
        }

        // RectOffset is a class, so the same instance is reused across pushes rather than
        // allocated fresh every time.
        [Test]
        public void Convert_ReturnsTheSameInstanceAcrossCalls()
        {
            var converter = new IntToRectOffsetConverter();

            Assert.AreSame(converter.Convert(1), converter.Convert(2));
        }
    }
}
