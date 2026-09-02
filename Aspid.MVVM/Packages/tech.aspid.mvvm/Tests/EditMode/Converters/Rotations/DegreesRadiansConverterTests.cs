using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for the inverted direction of <see cref="DegreesRadiansConverter"/> — the flag
    /// swaps the two directions and the round trip still holds.
    /// </summary>
    [TestFixture]
    public sealed class DegreesRadiansConverterTests
    {
        // The trap the flag exists to fall into: an inverted converter that kept the forward body
        // would answer 0.0175 here instead of 57.3. The numbers are large enough that the two
        // directions cannot be confused.
        [TestCase(0f, 0f)]
        [TestCase(1f, 57.29578f)]
        [TestCase(3.1415927f, 180f)]
        [TestCase(0.7853982f, 45f)]
        [TestCase(-1.5707964f, -90f)]
        public void Convert_Inverted_TurnsRadiansIntoDegrees(float value, float expected) =>
            Assert.AreEqual(expected, new DegreesRadiansConverter(isInvert: true).Convert(value), 1e-3f);

        [TestCase(0f, 0f)]
        [TestCase(180f, 3.1415927f)]
        [TestCase(90f, 1.5707964f)]
        [TestCase(-45f, -0.7853982f)]
        public void ConvertBack_Inverted_TurnsDegreesIntoRadians(float value, float expected) =>
            Assert.AreEqual(expected, new DegreesRadiansConverter(isInvert: true).ConvertBack(value), 1e-6f);

        // Same statement from the other side, and the one that would catch a flag wired up as a
        // no-op: the two directions must cross, not agree.
        [TestCase(1f)]
        [TestCase(90f)]
        [TestCase(-2.5f)]
        public void Convert_Inverted_IsTheForwardDirectionTheOtherWayRound(float value)
        {
            var inverted = new DegreesRadiansConverter(isInvert: true);
            var forward = new DegreesRadiansConverter();

            Assert.AreEqual(forward.ConvertBack(value), inverted.Convert(value), 1e-6f);
            Assert.AreEqual(forward.Convert(value), inverted.ConvertBack(value), 1e-6f);
        }

        [TestCase(0f)]
        [TestCase(1f)]
        [TestCase(-6.2831855f)]
        public void RoundTrips(float value)
        {
            var converter = new DegreesRadiansConverter(isInvert: true);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)), 1e-5f);
        }
    }
}
