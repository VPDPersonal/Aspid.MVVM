using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the color interop converters — <see cref="HdrIntensityConverter"/>, the
    /// <see cref="Color"/>/<see cref="Vector4"/> pair and the
    /// <see cref="Color"/>/<see cref="Color32"/> pair.
    /// </summary>
    /// <remarks>
    /// These are the converters where an assumption about range costs the most: the exposure maths is
    /// unclamped on purpose and a well-meaning <c>Clamp01</c> would kill every bloom that depends on it;
    /// the <see cref="Vector4"/> pair must stay a plain channel copy, or the round trip the docs promise
    /// breaks; and the <see cref="Color32"/> pair rides Unity's implicit operators, whose clamping and
    /// rounding are easy to mis-describe from memory. Every expectation is the value the conversion
    /// actually produces.
    /// </remarks>
    [TestFixture]
    internal sealed class ColorInteropConverterTests
    {
        // Fractional stops are reachable from a bound slider, so the factor is a power curve rather
        // than a multiply: half a stop is 1.414, not 1.5.
        [TestCase(0f, 0.5f, 0.5f)]
        [TestCase(1f, 0.5f, 1f)]
        [TestCase(2f, 0.25f, 1f)]
        [TestCase(-1f, 0.5f, 0.25f)]
        [TestCase(-3f, 1f, 0.125f)]
        [TestCase(10f, 1f, 1024f)]
        [TestCase(0.5f, 1f, 1.4142135f)]
        public void HdrIntensity_AnyExposure_ScalesTheChannelByTwoToThatPower(
            float intensity,
            float channel,
            float expected) =>
            Assert.AreEqual(
                expected,
                new HdrIntensityConverter(intensity).Convert(new Color(channel, channel, channel, 1f)).r,
                1e-5f);

        [Test]
        public void HdrIntensity_DefaultConstructed_LeavesTheColorAlone() =>
            Assert.AreEqual(
                new Color(0.1f, 0.2f, 0.3f, 0.4f),
                new HdrIntensityConverter().Convert(new Color(0.1f, 0.2f, 0.3f, 0.4f)));

        // The whole point of the converter: the result has to leave 0..1 so a material can bloom with
        // it. A Clamp01 added "for safety" would make every positive exposure look like plain white.
        [Test]
        public void HdrIntensity_PositiveExposure_PushesChannelsPastOne()
        {
            var result = new HdrIntensityConverter(2f).Convert(Color.white);

            Assert.AreEqual(4f, result.r, 1e-5f);
            Assert.AreEqual(4f, result.g, 1e-5f);
            Assert.AreEqual(4f, result.b, 1e-5f);
        }

        // Exposure is brightness, not opacity. Multiplying alpha too would fade a glowing sprite out
        // as it charges up, which is the opposite of what the binding is for.
        [TestCase(3f)]
        [TestCase(-3f)]
        public void HdrIntensity_AnyExposure_LeavesAlphaUntouched(float intensity) =>
            Assert.AreEqual(
                0.4f,
                new HdrIntensityConverter(intensity).Convert(new Color(1f, 1f, 1f, 0.4f)).a,
                1e-6f);

        // Color's constructor does not clamp, so a negative channel survives the multiply with its
        // sign intact — the converter never reaches for Abs or Max.
        [Test]
        public void HdrIntensity_NegativeChannel_KeepsItsSign() =>
            Assert.AreEqual(
                -2f,
                new HdrIntensityConverter(2f).Convert(new Color(-0.5f, 0f, 0f, 1f)).r,
                1e-5f);

        // 2^200 is past float.MaxValue, so an exposure bound to a runaway value yields infinity rather
        // than saturating. Anything downstream that averages or lerps channels then produces NaN.
        [Test]
        public void HdrIntensity_AbsurdExposure_OverflowsToInfinity()
        {
            var result = new HdrIntensityConverter(200f).Convert(Color.white);

            Assert.IsTrue(float.IsPositiveInfinity(result.r));
            Assert.AreEqual(1f, result.a, 1e-6f);
        }

        // Distinct channels, so a converter that transposed two of them could not pass.
        [Test]
        public void ColorToVector4_AnyColor_CopiesRgbaIntoXyzw()
        {
            var result = new ColorVector4Converter().Convert(new Color(0.1f, 0.2f, 0.3f, 0.4f));

            Assert.AreEqual(0.1f, result.x, 1e-6f);
            Assert.AreEqual(0.2f, result.y, 1e-6f);
            Assert.AreEqual(0.3f, result.z, 1e-6f);
            Assert.AreEqual(0.4f, result.w, 1e-6f);
        }

        [Test]
        public void Vector4ToColor_AnyVector_CopiesXyzwIntoRgba()
        {
            var result = new ColorVector4Converter().Convert(new Vector4(0.1f, 0.2f, 0.3f, 0.4f));

            Assert.AreEqual(0.1f, result.r, 1e-6f);
            Assert.AreEqual(0.2f, result.g, 1e-6f);
            Assert.AreEqual(0.3f, result.b, 1e-6f);
            Assert.AreEqual(0.4f, result.a, 1e-6f);
        }

        // A vector read back off a material is not promised to sit in 0..1, and clamping it here would
        // quietly rewrite a shader parameter that was correct.
        [Test]
        public void Vector4ToColor_OutOfRangeComponents_AreNotClamped()
        {
            var result = new ColorVector4Converter().Convert(new Vector4(1.5f, -0.5f, 0f, 2f));

            Assert.AreEqual(1.5f, result.r, 1e-6f);
            Assert.AreEqual(-0.5f, result.g, 1e-6f);
            Assert.AreEqual(2f, result.a, 1e-6f);
        }

        // Asserted with the exact-equality overload rather than a delta: the docs promise this trip is
        // lossless, and only bit equality actually proves no gamma or sRGB step crept in.
        [Test]
        public void Vector4RoundTrip_AwkwardChannels_IsBitExact()
        {
            var color = new Color(0.1f, 0.7f, 0.13f, 0.37f);
            var result = new ColorVector4Converter().Convert(new ColorVector4Converter().Convert(color));

            Assert.AreEqual(color, result);
        }

        // A binder reaches the reverse direction only through the interface, so the merged class has
        // to expose both two-way pairs, not just the four public overloads.
        [Test]
        public void ColorAndVector4_ConvertBack_IsReachableThroughBothTwoWayInterfaces()
        {
            ITwoWayConverter<Color, Vector4> toVector = new ColorVector4Converter();
            ITwoWayConverter<Vector4, Color> toColor = new ColorVector4Converter();
            var color = new Color(0.1f, 0.7f, 0.13f, 0.37f);

            Assert.AreEqual(color, toVector.ConvertBack(toVector.Convert(color)));
            Assert.AreEqual(color, toColor.Convert(toColor.ConvertBack(color)));
        }

        [TestCase((byte)0, 0f)]
        [TestCase((byte)1, 0.003921569f)]
        [TestCase((byte)64, 0.2509804f)]
        [TestCase((byte)128, 0.5019608f)]
        [TestCase((byte)255, 1f)]
        public void Color32ToColor_AnyByte_DividesByTwoHundredFiftyFive(byte channel, float expected) =>
            Assert.AreEqual(
                expected,
                new ColorColor32Converter().Convert(new Color32(channel, channel, channel, 255)).r,
                1e-6f);

        // A widening that forgot alpha would turn a fully transparent stored color into opaque black.
        [Test]
        public void Color32ToColor_ZeroAlpha_StaysTransparent() =>
            Assert.AreEqual(0f, new ColorColor32Converter().Convert(new Color32(0, 0, 0, 0)).a, 1e-6f);

        // The discriminating cases are 0.5 and 0.25: Unity's implicit operator adds a half before the
        // byte cast, so they land on 128 and 64. A hand-rolled (byte)(c.r * 255f) would give 127 and 63
        // and drift every color a step darker on each save/load cycle.
        [TestCase(0f, 0)]
        [TestCase(0.2f, 51)]
        [TestCase(0.25f, 64)]
        [TestCase(0.5f, 128)]
        [TestCase(0.6f, 153)]
        [TestCase(0.75f, 191)]
        [TestCase(1f, 255)]
        public void ColorToColor32_AnyChannel_RoundsRatherThanTruncates(float channel, int expected) =>
            Assert.AreEqual(
                expected,
                new ColorColor32Converter().Convert(new Color(channel, channel, channel, 1f)).r);

        [TestCase(2f, 255)]
        [TestCase(5f, 255)]
        [TestCase(-1f, 0)]
        public void ColorToColor32_OutOfRangeChannel_IsClampedNotWrapped(float channel, int expected) =>
            Assert.AreEqual(
                expected,
                new ColorColor32Converter().Convert(new Color(channel, channel, channel, 1f)).r);

        [Test]
        public void ColorToColor32_FractionalAlpha_NarrowsLikeAnyOtherChannel() =>
            Assert.AreEqual(128, new ColorColor32Converter().Convert(new Color(1f, 1f, 1f, 0.5f)).a);

        // The lossy direction the remarks warn about, pinned to the value it actually returns: a
        // mid-gray comes back as 128/255, not as the 0.5 that went in. Anything comparing a color
        // before and after a save round trip with == on floats will therefore see a change.
        [Test]
        public void ColorRoundTrip_ThroughColor32_ReturnsTheQuantisedChannel()
        {
            var result = new ColorColor32Converter().Convert(
                new ColorColor32Converter().Convert(new Color(0.5f, 0.5f, 0.5f, 1f)));

            Assert.AreEqual(128f / 255f, result.r, 1e-6f);
            Assert.AreNotEqual(0.5f, result.r);
        }

        // The other direction is the one that must survive intact: vertex colors and save-file
        // colors make this trip on every load, and a channel drifting by one per pass would show up
        // as a mesh that slowly fades.
        [Test]
        public void Color32RoundTrip_ThroughColor_IsExactForEveryByte()
        {
            var converter = new ColorColor32Converter();

            for (var channel = 0; channel <= 255; channel++)
            {
                var source = new Color32((byte)channel, (byte)channel, (byte)channel, (byte)channel);
                var result = converter.Convert(converter.Convert(source));

                Assert.AreEqual(channel, (int)result.r, $"Byte {channel} did not survive the round trip.");
                Assert.AreEqual(channel, (int)result.a, $"Alpha {channel} did not survive the round trip.");
            }
        }

        // Why the remarks send an HDR color to a material rather than to anything byte-backed: one
        // stop and five stops are already indistinguishable once narrowed, while the unexposed gray
        // still carries its value. The glow is not dimmed by the trip, it is gone.
        [Test]
        public void HdrIntensity_NarrowedToColor32_CollapsesEveryGlowToWhite()
        {
            var narrow = new ColorColor32Converter();

            Assert.AreEqual(128, narrow.Convert(Color.gray).r);
            Assert.AreEqual(255, narrow.Convert(new HdrIntensityConverter(1f).Convert(Color.gray)).r);
            Assert.AreEqual(255, narrow.Convert(new HdrIntensityConverter(5f).Convert(Color.gray)).r);
        }
    }
}
