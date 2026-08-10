using System;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the colour converters added in this wave — <see cref="ColorChannelConverter"/>
    /// across every <see cref="ChannelOp"/>, every <see cref="ColorChannels"/> mask and the clamp
    /// flag, the <see cref="ColorLerpConverter"/> curve together with its unclamped extrapolation,
    /// and <see cref="ThresholdColorConverter"/> blending over stops authored out of order.
    /// </summary>
    /// <remarks>
    /// The mistake this fixture guards against is always "a channel that should not have moved,
    /// moved": a mask compared with <c>==</c> instead of <c>HasFlag</c>, a clamp applied to channels
    /// the mask never wrote, a curve consulted for an amount it cannot answer for, and a threshold
    /// search that assumes the Inspector left the stops sorted.
    /// <para>
    /// Several assertions pin behaviour the XML docs do not describe, or describe loosely — the
    /// clamp reaching only written channels, a curve keeping the endpoints away from the two stops,
    /// and the fallback never taking part in a blend. Those cases carry a comment saying which claim
    /// they are measured against.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ColorConverterAdditionsTests
    {
        private const float Delta = 1e-5f;
        private const float CurveDelta = 1e-4f;

        #region ColorChannelConverter

        // A mask written as `_channels == flag` instead of HasFlag passes R, G, B and A but fails
        // every composite mask; a mask written as `(_channels & flag) != 0` with None==0 would be
        // right, so None is here to catch the inverted guard rather than the composite one.
        [TestCase(ColorChannels.None, 0.4f, 0.4f, 0.4f, 0.4f)]
        [TestCase(ColorChannels.R, 0.5f, 0.4f, 0.4f, 0.4f)]
        [TestCase(ColorChannels.G, 0.4f, 0.5f, 0.4f, 0.4f)]
        [TestCase(ColorChannels.B, 0.4f, 0.4f, 0.5f, 0.4f)]
        [TestCase(ColorChannels.A, 0.4f, 0.4f, 0.4f, 0.5f)]
        [TestCase(ColorChannels.R | ColorChannels.A, 0.5f, 0.4f, 0.4f, 0.5f)]
        [TestCase(ColorChannels.Rgb, 0.5f, 0.5f, 0.5f, 0.4f)]
        [TestCase(ColorChannels.All, 0.5f, 0.5f, 0.5f, 0.5f)]
        public void Convert_Set_WritesOnlyTheMaskedChannels(
            ColorChannels channels,
            float red,
            float green,
            float blue,
            float alpha)
        {
            var converter = new ColorChannelConverter(ChannelOp.Set, Uniform(0.5f), channels);

            AssertColor(new Color(red, green, blue, alpha), converter.Convert(Uniform(0.4f)));
        }

        // Red operates on red, green on green. A transposed operand would still pass every uniform
        // case above, so the operand here has a different value in each channel.
        [Test]
        public void Convert_Set_PairsEachOperandChannelWithItsOwn()
        {
            var operand = new Color(0.1f, 0.2f, 0.3f, 0.4f);
            var converter = new ColorChannelConverter(ChannelOp.Set, operand, ColorChannels.All);

            AssertColor(operand, converter.Convert(new Color(0f, 0f, 0f, 0f)));
        }

        // 0.8 + 0.5 leaves the unit range, which is the only place the clamp flag is observable for
        // these three operations: Set and Multiply stay inside it for this operand.
        [TestCase(ChannelOp.Set, true, 0.5f)]
        [TestCase(ChannelOp.Set, false, 0.5f)]
        [TestCase(ChannelOp.Multiply, true, 0.4f)]
        [TestCase(ChannelOp.Multiply, false, 0.4f)]
        [TestCase(ChannelOp.Add, true, 1f)]
        [TestCase(ChannelOp.Add, false, 1.3f)]
        public void Convert_EachOperation_AgainstTheClampFlag(ChannelOp operation, bool clamp, float expected)
        {
            var converter = new ColorChannelConverter(operation, Uniform(0.5f), ColorChannels.All, clamp);

            AssertColor(Uniform(expected), converter.Convert(Uniform(0.8f)));
        }

        // The clamp is Clamp01, not an upper bound: clearing it lets a channel go negative, which a
        // subtractive damage flash relies on and a Mathf.Min-shaped clamp would hide.
        [TestCase(true, 0f)]
        [TestCase(false, -0.3f)]
        public void Convert_Add_NegativeOperand_ClampFlagDecidesTheFloor(bool clamp, float expected)
        {
            var converter = new ColorChannelConverter(ChannelOp.Add, Uniform(-0.5f), ColorChannels.All, clamp);

            AssertColor(Uniform(expected), converter.Convert(Uniform(0.2f)));
        }

        // Set clamps the operand itself, so an HDR colour cannot be authored into the operand field
        // while the flag is on.
        [TestCase(true, 1f)]
        [TestCase(false, 4f)]
        public void Convert_Set_OperandAboveOne_ClampFlagDecidesTheCeiling(bool clamp, float expected)
        {
            var converter = new ColorChannelConverter(ChannelOp.Set, Uniform(4f), ColorChannels.All, clamp);

            AssertColor(Uniform(expected), converter.Convert(Uniform(0.2f)));
        }

        [Test]
        public void Convert_Unclamped_MultipliesHdrPastOne()
        {
            var converter = new ColorChannelConverter(
                ChannelOp.Multiply,
                new Color(3f, 3f, 3f, 1f),
                ColorChannels.Rgb,
                clamp: false);

            AssertColor(new Color(6f, 6f, 6f, 1f), converter.Convert(new Color(2f, 2f, 2f, 1f)));
        }

        // The clamp reaches written channels only — an unmasked channel is returned as it arrived,
        // HDR value and all. The tooltip says "Hold every written channel inside 0..1", so this is
        // the documented behaviour, but the surprise is worth pinning: one converter can emit a
        // colour that is clamped in RGB and above one in alpha at the same time.
        [Test]
        public void Convert_Clamped_LeavesUnmaskedHdrChannelsAlone()
        {
            var converter = new ColorChannelConverter(
                ChannelOp.Multiply,
                Color.white,
                ColorChannels.Rgb,
                clamp: true);

            AssertColor(new Color(1f, 1f, 1f, 4f), converter.Convert(new Color(2f, 2f, 2f, 4f)));
        }

        [Test]
        public void Convert_DefaultConstructed_IsAnIdentityForAnLdrColour()
        {
            var value = new Color(0.2f, 0.4f, 0.6f, 0.8f);

            AssertColor(value, new ColorChannelConverter().Convert(value));
        }

        // The source comment claims the defaults exist so "a freshly picked converter passes the
        // bound colour through". It does for LDR only: the default clamp is on and the default mask
        // is Rgb, so a freshly picked converter silently crushes an HDR colour to white while
        // leaving its alpha untouched.
        [Test]
        public void Convert_DefaultConstructed_IsNotAnIdentityForHdr() =>
            AssertColor(
                new Color(1f, 0.5f, 0f, 3f),
                new ColorChannelConverter().Convert(new Color(2f, 0.5f, 0f, 3f)));

        [Test]
        public void Convert_UndeclaredOperation_Throws()
        {
            var converter = new ColorChannelConverter((ChannelOp)42, Color.white, ColorChannels.All);

            Assert.Throws<ArgumentOutOfRangeException>(() => converter.Convert(Color.white));
        }

        // The operation is validated per channel and only after the mask test, so an unreachable
        // operation is never reported. A converter left in this state looks like it works.
        [Test]
        public void Convert_UndeclaredOperation_WithNoChannelMasked_DoesNotThrow()
        {
            var converter = new ColorChannelConverter((ChannelOp)42, Color.white, ColorChannels.None);

            AssertColor(Color.cyan, converter.Convert(Color.cyan));
        }

        #endregion

        #region ColorLerpConverter

        // A straight line to 0.25 makes the curve's contribution readable: an ignored curve would
        // answer 0.5 here, the curve's own value at 0.5 is 0.125.
        [Test]
        public void Convert_WithCurve_ShapesTheAmount()
        {
            var converter = new ColorLerpConverter(Color.black, Color.white, AnimationCurve.Linear(0f, 0f, 1f, 0.25f));

            Assert.AreEqual(0.125f, converter.Convert(0.5f).r, CurveDelta);
        }

        // The curve replaces the amount outright rather than modulating it, so an authored curve
        // that does not pass through (0,0) and (1,1) means Convert(0) is not _from and Convert(1) is
        // not _to. Nothing in the class normalises the curve.
        [TestCase(0f)]
        [TestCase(0.5f)]
        [TestCase(1f)]
        public void Convert_WithConstantCurve_NeverReachesEitherStop(float amount)
        {
            var converter = new ColorLerpConverter(Color.black, Color.white, AnimationCurve.Constant(0f, 1f, 0.25f));

            Assert.AreEqual(0.25f, converter.Convert(amount).r, CurveDelta);
        }

        // A one-key curve evaluates to that key's value everywhere, which would pin every amount to
        // 0.75. The length guard is `> 1`, so the curve is skipped instead.
        [Test]
        public void Convert_WithCurveOfOneKey_IgnoresTheCurve()
        {
            var converter = new ColorLerpConverter(Color.black, Color.white, new AnimationCurve(new Keyframe(0f, 0.75f)));

            Assert.AreEqual(0.5f, converter.Convert(0.5f).r, Delta);
        }

        // A curve field that was never authored deserializes with no keys rather than as null, and
        // evaluating that answers zero — which would pin every amount to _from.
        [Test]
        public void Convert_WithEmptyCurve_IgnoresTheCurve()
        {
            var converter = new ColorLerpConverter(Color.black, Color.white, new AnimationCurve());

            Assert.AreEqual(0.5f, converter.Convert(0.5f).r, Delta);
        }

        [TestCase(3f, 0.6f)]
        [TestCase(-1f, 0.2f)]
        [TestCase(0.5f, 0.4f)]
        public void Convert_Clamped_HoldsAtBothStops(float amount, float expectedRed)
        {
            // The empty curve keeps the shaping out of the way, so the clamp is what is measured.
            var converter = new ColorLerpConverter(Uniform(0.2f), Uniform(0.6f), new AnimationCurve());

            Assert.AreEqual(expectedRed, converter.Convert(amount).r, Delta);
        }

        // The review fix. With the clamp cleared the amount reaches Color.LerpUnclamped as it
        // arrived, so 3 carries a third of the way past _to and past one, and -1 goes below zero.
        // A clamped implementation answers 0.6 and 0.2 for the first two rows.
        [TestCase(3f, 1.4f)]
        [TestCase(-1f, -0.2f)]
        [TestCase(0.5f, 0.4f)]
        public void Convert_Unclamped_CarriesPastBothStops(float amount, float expectedRed) =>
            Assert.AreEqual(expectedRed, Unclamped(Uniform(0.2f), Uniform(0.6f)).Convert(amount).r, Delta);

        // Extrapolation is not RGB-only: alpha rides the same unclamped lerp, so a fade-out pair
        // driven past one produces a negative alpha rather than a transparent colour.
        [Test]
        public void Convert_Unclamped_ExtrapolatesAlphaToo()
        {
            var converter = Unclamped(new Color(0f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 0f));

            Assert.AreEqual(-1f, converter.Convert(2f).a, Delta);
        }

        // The other half of the fix: unclamped skips the curve entirely, because a curve answers
        // with its end key past either end of its own range and would clamp the amount back. Under
        // the pre-fix routing both rows would read 0.3 — the constant curve's 0.25 of the span.
        [TestCase(0f, 0.2f)]
        [TestCase(1f, 0.6f)]
        public void Convert_Unclamped_IgnoresTheCurve(float amount, float expectedRed)
        {
            var converter = Unclamped(Uniform(0.2f), Uniform(0.6f), AnimationCurve.Constant(0f, 1f, 0.25f));

            Assert.AreEqual(expectedRed, converter.Convert(amount).r, Delta);
        }

        #endregion

        #region ThresholdColorConverter

        // The stops are authored in whatever order the Inspector left them; the array below is
        // deliberately scrambled. A search that trusts the order returns the last qualifying stop
        // rather than the highest one, which is cyan at 0.5 for a value of 0.8.
        [TestCase(0.8f, 0f, 1f, 0f)]
        [TestCase(0.75f, 0f, 1f, 0f)]
        [TestCase(0.6f, 0f, 1f, 1f)]
        [TestCase(0.3f, 0f, 0f, 1f)]
        [TestCase(0.25f, 0f, 0f, 1f)]
        [TestCase(0.1f, 1f, 0f, 0f)]
        public void Convert_UnsortedStops_PicksTheHighestQualifying(
            float value,
            float red,
            float green,
            float blue)
        {
            var converter = new ThresholdColorConverter(
                new[]
                {
                    Stop(0.75f, Color.green),
                    Stop(0.25f, Color.blue),
                    Stop(0.5f, Color.cyan),
                },
                fallback: Color.red);

            AssertColor(new Color(red, green, blue, 1f), converter.Convert(value));
        }

        // The running lower bound starts at 0 and is guarded by a separate `hasLower` flag. Drop the
        // flag and a stop below zero never qualifies, so this returns the fallback instead.
        [Test]
        public void Convert_NegativeThreshold_StillQualifies()
        {
            var converter = new ThresholdColorConverter(new[] { Stop(-1f, Color.green) }, fallback: Color.red);

            AssertColor(Color.green, converter.Convert(-0.5f));
        }

        [Test]
        public void Convert_NotInterpolating_HoldsTheStopColourUntilTheNextThreshold()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(0f, Color.red), Stop(1f, Color.green) },
                fallback: Color.white);

            AssertColor(Color.red, converter.Convert(0.5f));
        }

        [Test]
        public void Convert_Interpolating_UnsortedStops_BlendsTowardsTheNextUp()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(1f, Color.green), Stop(0f, Color.red) },
                fallback: Color.white,
                interpolate: true);

            AssertColor(new Color(0.75f, 0.25f, 0f, 1f), converter.Convert(0.25f));
        }

        // Both neighbours are found in one pass rather than by sorting. Picking the outermost pair
        // instead of the immediate one gives 0.75 grey here.
        [Test]
        public void Convert_Interpolating_UsesOnlyTheImmediateNeighbours()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(1f, Color.white), Stop(0f, Color.black), Stop(0.5f, Color.red) },
                fallback: Color.green,
                interpolate: true);

            AssertColor(new Color(1f, 0.5f, 0.5f, 1f), converter.Convert(0.75f));
        }

        // The fallback never takes part in a blend: below every threshold there is no lower stop, so
        // the converter returns it flat. The docs say blending gives "the colour between that stop
        // and the next one up" and never mention the fallback, so a reader could reasonably expect a
        // ramp out of it. There is none — the first stop still arrives as a step.
        [Test]
        public void Convert_Interpolating_BelowEveryStop_ReturnsTheFallbackUnblended()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(0.5f, Color.green) },
                fallback: Color.red,
                interpolate: true);

            AssertColor(Color.red, converter.Convert(0.1f));
        }

        // Above the top stop there is no upper neighbour, so blending stops rather than
        // extrapolating past it.
        [TestCase(1f)]
        [TestCase(1.5f)]
        public void Convert_Interpolating_AboveTheTopStop_HoldsThatStop(float value)
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(0f, Color.red), Stop(1f, Color.green) },
                fallback: Color.white,
                interpolate: true);

            AssertColor(Color.green, converter.Convert(value));
        }

        // t is zero at the lower stop, so blending does not shift the colour of a value sitting
        // exactly on a threshold — the step boundary stays where it was authored.
        [Test]
        public void Convert_Interpolating_AtAStop_IsExactlyThatStop()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(0f, Color.red), Stop(1f, Color.green) },
                fallback: Color.white,
                interpolate: true);

            AssertColor(Color.red, converter.Convert(0f));
        }

        // The span is upper - lower, not a normalised 0..1 distance, so a negative lower bound has
        // to survive the division.
        [Test]
        public void Convert_Interpolating_AcrossANegativeSpan()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(-2f, Color.red), Stop(2f, Color.green) },
                fallback: Color.white,
                interpolate: true);

            AssertColor(new Color(0.5f, 0.5f, 0f, 1f), converter.Convert(0f));
        }

        // Equal thresholds are resolved by `threshold <= lower` -> skip, so the first stop authored
        // at a threshold wins and the later duplicate is dead. Two stops swapped in the Inspector
        // therefore change the output even though they describe the same threshold.
        [Test]
        public void Convert_DuplicateThresholds_TheFirstAuthoredWins()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(0.5f, Color.green), Stop(0.5f, Color.blue) },
                fallback: Color.red);

            AssertColor(Color.green, converter.Convert(0.5f));
        }

        // Same tie-break on the upper side, where it decides which colour is blended towards:
        // green wins, so blue never reaches the result and the blue channel stays at zero.
        [Test]
        public void Convert_Interpolating_DuplicateUpperThresholds_TheFirstAuthoredWins()
        {
            var converter = new ThresholdColorConverter(
                new[] { Stop(0f, Color.black), Stop(1f, Color.green), Stop(1f, Color.blue) },
                fallback: Color.white,
                interpolate: true);

            AssertColor(new Color(0f, 0.5f, 0f, 1f), converter.Convert(0.5f));
        }

        [Test]
        public void Convert_NullStops_ReturnsTheFallbackEvenWhenInterpolating() =>
            AssertColor(Color.red, new ThresholdColorConverter(null, Color.red, interpolate: true).Convert(5f));

        [Test]
        public void Convert_EmptyStops_ReturnsTheFallbackEvenWhenInterpolating() =>
            AssertColor(
                Color.red,
                new ThresholdColorConverter(Array.Empty<ColorStop>(), Color.red, interpolate: true).Convert(5f));

        #endregion

        private static Color Uniform(float channel) => new Color(channel, channel, channel, channel);

        private static ColorStop Stop(float threshold, Color color) =>
            new ColorStop { Threshold = threshold, Color = color };

        private static ColorLerpConverter Unclamped(Color from, Color to) =>
            With(new ColorLerpConverter(from, to), "_clamp", false);

        private static ColorLerpConverter Unclamped(Color from, Color to, AnimationCurve curve) =>
            With(new ColorLerpConverter(from, to, curve), "_clamp", false);

        // Color.Equals compares the four floats exactly, so every assertion goes through a per
        // channel delta instead — a blended colour is arithmetic, not a copied constant.
        private static void AssertColor(Color expected, Color actual)
        {
            Assert.AreEqual(expected.r, actual.r, Delta, $"red: expected {expected}, was {actual}");
            Assert.AreEqual(expected.g, actual.g, Delta, $"green: expected {expected}, was {actual}");
            Assert.AreEqual(expected.b, actual.b, Delta, $"blue: expected {expected}, was {actual}");
            Assert.AreEqual(expected.a, actual.a, Delta, $"alpha: expected {expected}, was {actual}");
        }

        // The clamp is Inspector state with no constructor overload, so the tests set it the way the
        // Inspector does. A renamed field throws here rather than leaving the default in place,
        // which would let the unclamped assertions pass for the wrong reason.
        private static T With<T>(T converter, string field, object value)
            where T : class
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var info = converter.GetType().GetField(field, flags);
            if (info is null) throw new InvalidOperationException($"{converter.GetType().Name} has no {field} field.");

            info.SetValue(converter, value);
            return converter;
        }
    }
}
