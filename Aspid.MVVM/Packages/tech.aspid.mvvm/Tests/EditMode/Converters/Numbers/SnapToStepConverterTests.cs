using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="SnapToStepConverter"/> — snapping to the nearest step, the zero-step
    /// guard, and the integer overload.
    /// </summary>
    [TestFixture]
    public sealed class SnapToStepConverterTests
    {
        [TestCase(0.4f, 0.5f)]
        [TestCase(0.6f, 0.5f)]
        [TestCase(0.8f, 1f)]
        public void Snap_LandsOnTheNearestStep(float value, float expected) =>
            Assert.AreEqual(expected, new SnapToStepConverter(0.5f).Convert(value), delta: 1e-6f);

        // A step of zero snaps nothing, so it is reported on every push rather than passing for a
        // deliberate setting.
        [Test]
        public void Snap_ZeroStepPassesThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("SnapToStepConverter.*the step is zero"));

            Assert.AreEqual(0.37f, new SnapToStepConverter(0f).Convert(0.37f), delta: 1e-6f);
        }

        // The integer overload snaps in double and truncates the result; an out-of-range value
        // saturates rather than taking the undefined (int) cast.
        [TestCase(7d, 5)]
        [TestCase(1e20d, int.MaxValue)]
        [TestCase(-1e20d, int.MinValue)]
        public void Snap_ToInt_SnapsThenSaturates(double value, int expected) =>
            Assert.AreEqual(expected, ((IConverter<double, int>)new SnapToStepConverter(5f)).Convert(value));

        [Test]
        public void Snap_Double_LandsOnTheNearestStep() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new SnapToStepConverter(0.5f)).Convert(0.4d),
                1e-12d);
    }
}
