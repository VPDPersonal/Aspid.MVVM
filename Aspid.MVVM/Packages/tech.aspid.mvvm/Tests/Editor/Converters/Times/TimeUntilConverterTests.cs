using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="TimeUntilConverter"/> — the zero-clamp default, the unclamped negative
    /// reading, and the UTC/local clock choice.
    /// </summary>
    /// <remarks>
    /// The converter reads the clock through <see cref="DateTime.Now"/> with no seam to inject one, so
    /// the assertions are written to hold whatever the clock says.
    /// </remarks>
    [TestFixture]
    internal sealed class TimeUntilConverterTests
    {
        [Test]
        public void Convert_AMomentAlreadyPast_IsReportedAsZero() =>
            Assert.AreEqual(TimeSpan.Zero, new TimeUntilConverter().Convert(new DateTime(2000, 1, 1)));

        // Unclamped, a passed moment reads negative — what a "you are late by" label wants.
        [Test]
        public void Convert_AMomentAlreadyPast_Unclamped_IsNegative() =>
            Assert.AreEqual(
                -TimeSpan.FromMinutes(30).TotalSeconds,
                new TimeUntilConverter(useUtcNow: false, clampToZero: false)
                    .Convert(DateTime.Now.AddMinutes(-30))
                    .TotalSeconds,
                delta: 1d);

        // The clamp must not touch a moment still ahead, which is the whole working range.
        [Test]
        public void Convert_AFutureMoment_IsTheDistanceToIt() =>
            Assert.AreEqual(
                TimeSpan.FromMinutes(30).TotalSeconds,
                new TimeUntilConverter().Convert(DateTime.Now.AddMinutes(30)).TotalSeconds,
                delta: 1d);

        // Measuring a moment against the wrong clock is out by exactly the zone offset, which is the
        // failure the tooltip warns about and the only observable difference the flag makes.
        [Test]
        public void Convert_UtcAndLocal_DifferByTheZoneOffset()
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            if (offset == TimeSpan.Zero) Assert.Ignore("This machine runs at UTC, so the two clocks cannot differ.");

            var target = new DateTime(2200, 1, 1);
            var utc = new TimeUntilConverter(useUtcNow: true, clampToZero: false).Convert(target);
            var local = new TimeUntilConverter(useUtcNow: false, clampToZero: false).Convert(target);

            Assert.AreEqual(offset.TotalSeconds, (utc - local).TotalSeconds, delta: 1d);
        }
    }
}
