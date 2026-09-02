using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ClampNumberConverter"/> — the clamp modes, fractional-bound rounding
    /// on the integer overloads, saturation beyond the integer range, and the misconfigured-mode guard.
    /// </summary>
    [TestFixture]
    public sealed class ClampNumberConverterTests
    {
        [TestCase(ClampMode.Both, -1f, 0f)]
        [TestCase(ClampMode.Both, 2f, 1f)]
        [TestCase(ClampMode.Both, 0.5f, 0.5f)]
        [TestCase(ClampMode.Min, -1f, 0f)]
        [TestCase(ClampMode.Min, 2f, 2f)]
        [TestCase(ClampMode.Max, -1f, -1f)]
        [TestCase(ClampMode.Max, 2f, 1f)]
        public void Clamp_HoldsTheBound(ClampMode mode, float value, float expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(0f, 1f, mode).Convert(value), delta: 1e-6f);

        // The bounds are authored as doubles, so a minimum of 0.5 has to round UP into the range.
        [TestCase(0.5d, 10.5d, 0, 1)]
        [TestCase(0.5d, 10.5d, 1, 1)]
        [TestCase(0.5d, 10.5d, 10, 10)]
        [TestCase(0.5d, 10.5d, 11, 10)]
        // Mirror image: the maximum has to round DOWN.
        [TestCase(-10.5d, -0.5d, 0, -1)]
        [TestCase(-10.5d, -0.5d, -11, -10)]
        public void Clamp_Int_FractionalBound_RoundsIntoTheRange(double min, double max, int value, int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max).Convert(value));

        [TestCase(0.5d, 10.5d, 0L, 1L)]
        [TestCase(0.5d, 10.5d, 1L, 1L)]
        [TestCase(0.5d, 10.5d, 10L, 10L)]
        [TestCase(0.5d, 10.5d, 11L, 10L)]
        [TestCase(-10.5d, -0.5d, 0L, -1L)]
        [TestCase(-10.5d, -0.5d, -11L, -10L)]
        public void Clamp_Long_FractionalBound_RoundsIntoTheRange(double min, double max, long value, long expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max).Convert(value));

        // The rounding belongs to the integer overloads only; the double overload keeps the bound as
        // authored.
        [Test]
        public void Clamp_Double_FractionalBound_KeepsTheFraction() =>
            Assert.AreEqual(0.5d, new ClampNumberConverter(0.5d, 10.5d).Convert(0d), delta: 1e-12);

        // A double bound can name a number no int can hold. Saturating keeps the result at the end of
        // the range instead of taking an undefined cast.
        [TestCase(1e18d, 1e19d, int.MaxValue)]
        [TestCase(-1e19d, -1e18d, int.MinValue)]
        public void Clamp_Int_BoundBeyondIntRange_SaturatesInsteadOfWrapping(double min, double max, int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max).Convert(0));

        [Test]
        public void Clamp_Long_BoundBeyondLongRange_Saturates() =>
            Assert.AreEqual(long.MaxValue, new ClampNumberConverter(1e30d, 1e31d).Convert(0L));

        // An in-range value is returned as it came in, never round-tripped through a double.
        // 2^53 + 1 is the first long a double cannot name exactly.
        [Test]
        public void Clamp_Long_InsideTheRange_StaysExactAboveTwoToTheFiftyThree() =>
            Assert.AreEqual(9007199254740993L, new ClampNumberConverter(0d, 1e30d).Convert(9007199254740993L));

        // Both bounds are consulted for Both, so an inverted pair is reported and clamped to the
        // swapped range rather than to whichever bound happens to be tested first.
        [Test]
        public void Clamp_Int_Both_InvertedBounds_ReportsAndClampsToTheSwappedRange()
        {
            LogAssert.Expect(LogType.Error, new Regex("ClampNumberConverter.*minimum 10 is above the maximum 0"));

            Assert.AreEqual(5, new ClampNumberConverter(10d, 0d).Convert(5));
        }

        // A single-bound mode never reads the other bound, so a minimum above the untouched default
        // maximum is authoring rather than a contradiction and is not reported.
        [TestCase(ClampMode.Min, 5d, 1d, 0, 5)]
        [TestCase(ClampMode.Max, 5d, 1d, 9, 1)]
        public void Clamp_Int_SingleBoundMode_KeepsTheAuthoredBound(
            ClampMode mode,
            double min,
            double max,
            int value,
            int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max, mode).Convert(value));

        [Test]
        public void Clamp_UndeclaredMode_ReportsAndLetsTheValueThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("ClampNumberConverter.*not a declared ClampMode"));

            Assert.AreEqual(5f, new ClampNumberConverter(0d, 1d, (ClampMode)42).Convert(5f), delta: 1e-6f);
        }
    }
}
