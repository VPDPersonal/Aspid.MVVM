using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="AngleWrapConverter"/> — the two <see cref="AngleRange"/> readings, the
    /// offset, and the undeclared-range fallback.
    /// </summary>
    [TestFixture]
    internal sealed class AngleWrapConverterTests
    {
        [TestCase(0f, 0f)]
        [TestCase(360f, 0f)]
        [TestCase(-10f, 350f)]
        [TestCase(370f, 10f)]
        public void Convert_Zero360_FoldsIntoTheRange(float value, float expected) =>
            Assert.AreEqual(expected, new AngleWrapConverter(AngleRange.Zero360).Convert(value), 1e-3f);

        [TestCase(0f, 0f)]
        [TestCase(190f, -170f)]
        [TestCase(-190f, 170f)]
        [TestCase(180f, 180f)]
        public void Convert_Signed180_FoldsIntoTheRange(float value, float expected) =>
            Assert.AreEqual(expected, new AngleWrapConverter(AngleRange.Signed180).Convert(value), 1e-3f);

        [Test]
        public void Convert_OffsetIsAddedBeforeWrapping() =>
            Assert.AreEqual(10f, new AngleWrapConverter(AngleRange.Zero360, offset: 30f).Convert(340f), 1e-3f);

        [Test]
        public void Convert_UndeclaredRange_ReportsAndReturnsTheBoundAngleWithoutTheOffset()
        {
            LogAssert.Expect(LogType.Error, new Regex("AngleWrapConverter.*not a declared AngleRange"));

            Assert.AreEqual(45f, new AngleWrapConverter((AngleRange)99, offset: 30f).Convert(45f), 1e-3f);
        }
        // Every Unity wrapper in the catalogue takes a double the same way: through its own float path.
        [Test]
        public void AngleWrap_Double_WrapsAsTheFloatWidthDoes() =>
            Assert.AreEqual(
                new AngleWrapConverter().Convert(370f),
                ((IConverter<double, double>)new AngleWrapConverter()).Convert(370d),
                1e-4d);

    }
}
