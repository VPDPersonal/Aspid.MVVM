using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="OffsetThenScaleConverter"/> — the order in which the offset and the
    /// scale are applied, and its inverse.
    /// </summary>
    /// <remarks>
    /// Every order-of-operations row is chosen so that <c>(x + a) * b</c> and <c>x + (a * b)</c> disagree.
    /// </remarks>
    [TestFixture]
    public sealed class OffsetThenScaleConverterTests
    {
        // The reason the class is worth having as one node. Each row disagrees with x + (a * b):
        // 30 vs 21, 9 vs 1, 2.5 vs 3.5, 0 vs 18, -21 vs 9.
        [TestCase(1f, 2f, 10f, 30f)]
        [TestCase(4f, -1f, 3f, 9f)]
        [TestCase(2f, 3f, 0.5f, 2.5f)]
        [TestCase(-3f, 3f, 7f, 0f)]
        [TestCase(10f, 0.5f, -2f, -21f)]
        public void OffsetThenScale_Convert_AddsTheOffsetBeforeScaling(float value, float offset, float scale, float expected) =>
            Assert.AreEqual(expected, new OffsetThenScaleConverter(offset, scale).Convert(value), delta: 1e-5f);

        // The scale field initializes to 1, not to default(float). A converter that started at scale 0
        // would flatten every value to nothing the instant it was picked in the Inspector, and the
        // author would blame the binding rather than the freshly added node.
        [TestCase(0f)]
        [TestCase(7.5f)]
        [TestCase(-7.5f)]
        public void OffsetThenScale_DefaultConstructed_IsIdentity(float value) =>
            Assert.AreEqual(value, new OffsetThenScaleConverter().Convert(value), delta: 1e-6f);

        [Test]
        public void OffsetThenScale_ScaleOmitted_DefaultsToOne() =>
            Assert.AreEqual(9f, new OffsetThenScaleConverter(5f).Convert(4f), delta: 1e-6f);

        [TestCase(7f, 0.1f, 3f)]
        [TestCase(1f, 2f, 10f)]
        [TestCase(-4f, -2.5f, 0.25f)]
        [TestCase(100f, 0f, -2f)]
        public void OffsetThenScale_ConvertBack_UndoesConvert(float value, float offset, float scale)
        {
            var converter = new OffsetThenScaleConverter(offset, scale);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)), delta: 1e-4f);
        }

        // The inverse has its own order to get wrong: value / b - a, never (value - a) / b. The wrong
        // form round-trips perfectly whenever the offset is zero, so a round-trip test alone would let
        // it through — hence this fixed pair, where the two forms give 1 and 2.8.
        [Test]
        public void OffsetThenScale_ConvertBack_DividesBeforeSubtractingTheOffset() =>
            Assert.AreEqual(1f, new OffsetThenScaleConverter(2f, 10f).ConvertBack(30f), delta: 1e-6f);

        // Scale zero annihilates the input, offset included — the result is 0 and not the offset.
        [TestCase(5f)]
        [TestCase(-5f)]
        [TestCase(0f)]
        public void OffsetThenScale_ZeroScale_FlattensEveryValue(float value) =>
            Assert.AreEqual(0f, new OffsetThenScaleConverter(3f, 0f).Convert(value), delta: 1e-6f);

        // The zero-scale branch is what stops ConvertBack producing Infinity or NaN and sending it to a
        // Transform. The price is that the ITwoWayConverter contract is broken here: the forward pass
        // discarded the input, so the round trip lands on 0 rather than recovering 42. Asserted so the
        // guard is not "fixed" into a division, and so the drift is a documented result rather than a
        // surprise in the field. Two errors are expected, one per ConvertBack: the report is per push,
        // not once per converter, so a misconfigured scene stays loud.
        [Test]
        public void OffsetThenScale_ZeroScale_ConvertBackReturnsTheInputUnchanged()
        {
            var converter = new OffsetThenScaleConverter(3f, 0f);

            LogAssert.Expect(LogType.Error, new Regex("the scale is zero"));
            Assert.AreEqual(42f, converter.ConvertBack(42f), delta: 1e-6f);

            LogAssert.Expect(LogType.Error, new Regex("the scale is zero"));
            Assert.AreEqual(0f, converter.ConvertBack(converter.Convert(42f)), delta: 1e-6f);
        }

        // Scale-then-offset is reachable by dividing the offset by the scale, an identity that holds
        // only while the order of operations stands: 3 * 2 + 5 == (3 + 5 / 2) * 2 == 11.
        [Test]
        public void OffsetThenScale_OffsetDividedByScale_ExpressesScaleThenOffset() =>
            Assert.AreEqual(3f * 2f + 5f, new OffsetThenScaleConverter(5f / 2f, 2f).Convert(3f), delta: 1e-5f);

        [Test]
        public void OffsetThenScale_IsUsableThroughTheTwoWayInterface()
        {
            var converter = (ITwoWayConverter<float, float>)new OffsetThenScaleConverter(2f, 10f);

            Assert.AreEqual(30f, converter.Convert(1f), delta: 1e-6f);
            Assert.AreEqual(1f, converter.ConvertBack(30f), delta: 1e-6f);
        }

        // The int overloads share the one double pipeline, so a whole-number round trip has to close
        // exactly rather than nearly.
        [Test]
        public void OffsetThenScale_Int_RoundTrips()
        {
            var converter = (ITwoWayConverter<int, int>)new OffsetThenScaleConverter(2f, 10f);

            Assert.AreEqual(30, converter.Convert(1));
            Assert.AreEqual(1, converter.ConvertBack(30));
        }

        // Reversing a tiny scale is a division that leaves int behind. Saturating holds the result at
        // the bound rather than at whatever an unchecked cast produces on the platform.
        [Test]
        public void OffsetThenScale_Int_ReverseOutOfRange_SaturatesAtTheBound() =>
            Assert.AreEqual(
                int.MaxValue,
                ((ITwoWayConverter<int, int>)new OffsetThenScaleConverter(0f, 1e-10f)).ConvertBack(1));
    }
}
