using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DirectionAngleConverter"/>'s reverse pass — the unit-vector default and the
    /// configured magnitude.
    /// </summary>
    [TestFixture]
    public sealed class DirectionAngleConverterTests
    {
        [TestCase(0f, 1f, 0f)]
        [TestCase(90f, 0f, 1f)]
        [TestCase(180f, -1f, 0f)]
        public void Convert_TurnsTheAngleIntoAUnitDirection(float degrees, float x, float y)
        {
            var direction = new DirectionAngleConverter().ConvertBack(degrees);

            Assert.AreEqual(x, direction.x, 1e-4f);
            Assert.AreEqual(y, direction.y, 1e-4f);
        }

        [Test]
        public void Convert_Magnitude_ScalesTheDirection()
        {
            var direction = new DirectionAngleConverter(offset: 0f, magnitude: 5f).ConvertBack(0f);

            Assert.AreEqual(5f, direction.x, 1e-4f);
        }
    }
}
