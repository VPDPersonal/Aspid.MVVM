using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RectOffsetScaleConverter"/> — the side mask, the rounding, the reused
    /// result instance, the null padding, and the out-of-range saturation.
    /// </summary>
    [TestFixture]
    public sealed class RectOffsetScaleConverterTests
    {
        [Test]
        public void Convert_ScalesTheChosenSides()
        {
            var scaled = new RectOffsetScaleConverter(2f, RectSides.Vertical)
                .Convert(new RectOffset(3, 3, 3, 3));

            Assert.AreEqual(3, scaled.left);
            Assert.AreEqual(6, scaled.top);
            Assert.AreEqual(6, scaled.bottom);
        }

        // A null padding is the reset a binder pushes, and reading it must not allocate a throwaway
        // RectOffset to take four zeroes off. Zero scaled by anything is still zero.
        [Test]
        public void Convert_NullPadding_ReadsAsNoPadding()
        {
            var scaled = new RectOffsetScaleConverter(2f).Convert(null);

            Assert.AreEqual(0, scaled.left);
            Assert.AreEqual(0, scaled.right);
            Assert.AreEqual(0, scaled.top);
            Assert.AreEqual(0, scaled.bottom);
        }

        // 3 * 1.5 is 4.5, which Ceil takes to 5 and Floor to 4.
        [TestCase(RoundMode.Ceil, 5)]
        [TestCase(RoundMode.Floor, 4)]
        public void Convert_Rounding_DecidesWhereTheFractionGoes(RoundMode rounding, int expected) =>
            Assert.AreEqual(
                expected,
                new RectOffsetScaleConverter(1.5f, RectSides.All, rounding)
                    .Convert(new RectOffset(3, 3, 3, 3)).left);

        // A plain (int) cast of an out-of-range float is undefined in C#, so the scaled side is held
        // at the bounds of what a padding can carry instead of wrapping to a negative number.
        [Test]
        public void Convert_ScaleBeyondWhatAnIntHolds_SaturatesRatherThanWraps() =>
            Assert.AreEqual(
                int.MaxValue,
                new RectOffsetScaleConverter(1e12f).Convert(new RectOffset(3, 3, 3, 3)).left);

        // RectOffset is a class, so a new one per push would allocate once per notification.
        [Test]
        public void Convert_ReusesOneInstance()
        {
            var converter = new RectOffsetScaleConverter(2f);

            Assert.AreSame(
                converter.Convert(new RectOffset(1, 1, 1, 1)),
                converter.Convert(new RectOffset(2, 2, 2, 2)));
        }
    }
}
