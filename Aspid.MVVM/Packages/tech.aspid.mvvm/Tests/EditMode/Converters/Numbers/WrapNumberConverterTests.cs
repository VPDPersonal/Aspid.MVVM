using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="WrapNumberConverter"/> — folding a value into a range under each wrap
    /// mode, the degenerate range, the integer overload, and the misconfigured-mode guard.
    /// </summary>
    [TestFixture]
    public sealed class WrapNumberConverterTests
    {
        [TestCase(NumberWrapMode.Repeat, 1.25f, 0.25f)]
        [TestCase(NumberWrapMode.Repeat, -0.25f, 0.75f)]
        [TestCase(NumberWrapMode.PingPong, 1.25f, 0.75f)]
        public void Wrap_FoldsIntoTheRange(NumberWrapMode mode, float value, float expected) =>
            Assert.AreEqual(expected, new WrapNumberConverter(mode, 0f, 1f).Convert(value), delta: 1e-5f);

        [Test]
        public void Wrap_DegenerateRangeYieldsItsLowEnd() =>
            Assert.AreEqual(5f, new WrapNumberConverter(NumberWrapMode.Repeat, 5f, 5f).Convert(9f), delta: 1e-6f);

        // An int folds through the same double path: 12 over 0..10 comes back as 2, not clamped to 10.
        [Test]
        public void Wrap_Int_FoldsIntoTheRange()
        {
            var converter = (IConverter<int, int>)new WrapNumberConverter(NumberWrapMode.Repeat, 0f, 10f);

            Assert.AreEqual(2, converter.Convert(12));
        }

        // 9 is outside 0..1 under either declared mode, so an unchanged 9 is proof the fold was
        // skipped rather than performed with the wrong rule.
        [Test]
        public void Wrap_UndeclaredMode_ReportsAndReturnsTheValueUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("WrapNumberConverter.*not a declared NumberWrapMode"));

            Assert.AreEqual(9f, new WrapNumberConverter((NumberWrapMode)42, 0f, 1f).Convert(9f), delta: 1e-6f);
        }

        [Test]
        public void Wrap_Double_FoldsIntoTheRange() =>
            Assert.AreEqual(
                0.25d,
                ((IConverter<double, double>)new WrapNumberConverter(NumberWrapMode.Repeat, 0f, 1f)).Convert(1.25d),
                1e-12d);
    }
}
