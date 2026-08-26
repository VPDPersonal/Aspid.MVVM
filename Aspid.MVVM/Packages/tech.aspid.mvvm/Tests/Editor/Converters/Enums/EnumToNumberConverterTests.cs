using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumToNumberConverter{TEnum}"/> in both directions and both modes.
    /// </summary>
    /// <remarks>
    /// The two modes disagree on a sparse enum, and <c>ConvertBack</c> in the value mode still casts
    /// through <c>Enum.ToObject</c> where the value-mode <c>Convert</c> refuses — the two directions
    /// are asymmetric on purpose.
    /// </remarks>
    [TestFixture]
    internal sealed class EnumToNumberConverterTests
    {
        private const Medal MissingMedal = (Medal)99;

        [Test]
        public void EnumToNumber_IndexMode_ReportsThePosition() =>
            Assert.AreEqual(2, new EnumToNumberConverter<Medal>(byIndexNotValue: true).Convert(Medal.Silver));

        [Test]
        public void EnumToNumber_ValueMode_ReportsTheUnderlyingNumber() =>
            Assert.AreEqual(20, new EnumToNumberConverter<Medal>(byIndexNotValue: false).Convert(Medal.Silver));

        [Test]
        public void EnumToNumber_ValueMode_LongBackedEnum_ReadsTheWholeNumber() =>
            Assert.AreEqual(
                5_000_000_000L,
                ((IConverter<Distance, long>)new EnumToNumberConverter<Distance>()).Convert(Distance.Far));

        // The int overload cannot hold it, and a silent wrap would name a member nobody declared.
        [Test]
        public void EnumToNumber_ValueMode_LongBackedEnum_ReportsWhatTheIntOverloadCannotHold()
        {
            LogAssert.Expect(LogType.Error, new Regex("EnumToNumberConverter.*does not fit in an int"));

            Assert.AreEqual(int.MaxValue, new EnumToNumberConverter<Distance>().Convert(Distance.Far));
        }

        [Test]
        public void EnumToNumber_ValueMode_ConvertBackFromLong_ReadsTheMemberItNames() =>
            Assert.AreEqual(
                Distance.Far,
                ((ITwoWayConverter<Distance, long>)new EnumToNumberConverter<Distance>()).ConvertBack(5_000_000_000L));

        [Test]
        public void EnumToNumber_ValueMode_ReadsTheUnderlyingNumberAsADouble() =>
            Assert.AreEqual(
                20d,
                ((IConverter<Medal, double>)new EnumToNumberConverter<Medal>()).Convert(Medal.Silver));

        [Test]
        public void EnumToNumber_ValueMode_ConvertBack_ReadsTheUnderlyingNumber() =>
            Assert.AreEqual(Medal.Silver, new EnumToNumberConverter<Medal>(byIndexNotValue: false).ConvertBack(20));

        // A dropdown reads -1 as no selection, which is the honest answer for a value that has no
        // row. Returning 0 instead would silently highlight the first one.
        [Test]
        public void EnumToNumber_IndexMode_UndeclaredValue_ReportsMinusOne()
        {
            LogAssert.Expect(LogType.Error, new Regex("EnumToNumberConverter.*a declared member"));

            Assert.AreEqual(-1, new EnumToNumberConverter<Medal>(byIndexNotValue: true).Convert(MissingMedal));
        }

        [Test]
        public void EnumToNumber_ValueMode_UndeclaredValue_PassesTheNumberThrough() =>
            Assert.AreEqual(99, new EnumToNumberConverter<Medal>(byIndexNotValue: false).Convert(MissingMedal));

        [TestCase(Medal.None)]
        [TestCase(Medal.Bronze)]
        [TestCase(Medal.Silver)]
        public void EnumToNumber_IndexMode_RoundTripsEveryMember(Medal value)
        {
            var converter = new EnumToNumberConverter<Medal>(byIndexNotValue: true, fallback: MissingMedal);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)));
        }

        [TestCase(3)]
        [TestCase(-1)]
        public void EnumToNumber_IndexMode_ConvertBack_PositionOutsideTheEnum_ReturnsTheFallback(int value)
        {
            LogAssert.Expect(LogType.Error, new Regex("EnumToNumberConverter.*a position inside"));

            Assert.AreEqual(MissingMedal, new EnumToNumberConverter<Medal>(byIndexNotValue: true, fallback: MissingMedal).ConvertBack(value));
        }

        // The fallback is authored on the same object in both modes, and the tooltip promises the
        // value mode ignores it. It does: an undeclared number survives the trip untouched, which is
        // what lets a flag combination round trip.
        [Test]
        public void EnumToNumber_ValueMode_ConvertBack_IgnoresTheFallback() =>
            Assert.AreEqual(MissingMedal, new EnumToNumberConverter<Medal>(byIndexNotValue: false, fallback: Medal.Silver).ConvertBack(99));

        // Documented on EnumToNumberConverter as "only a long- or ulong-backed enum can manage" — not
        // so. A uint-backed member above int.MaxValue does not fit an int either, and it degrades the
        // same way the long-backed enum above does rather than throwing an overflow into the binder.
        [Test]
        public void EnumToNumber_ValueMode_UnsignedMemberAboveIntMaxValue_ReportsAndSaturates()
        {
            LogAssert.Expect(LogType.Error, new Regex("EnumToNumberConverter.*4294967295 does not fit in an int"));

            Assert.AreEqual(int.MaxValue, new EnumToNumberConverter<Bitfield>().Convert(Bitfield.Full));
        }

        // The position mode never touches the underlying value, so the member the value mode cannot
        // express still has a usable dropdown row.
        [Test]
        public void EnumToNumber_IndexMode_UnsignedMemberAboveIntMaxValue_ReportsThePosition() =>
            Assert.AreEqual(1, new EnumToNumberConverter<Bitfield>(byIndexNotValue: true).Convert(Bitfield.Full));

        // The pair that explains why NumberToEnumConverter exists at all. ConvertBack casts through
        // Enum.ToObject, which truncates 456 to the low byte and lands on a real member; the one-way
        // converter refuses the same number. A TwoWay binder therefore still admits it — that is a
        // property of ConvertBack, not an oversight of the test.
        [Test]
        public void EnumToNumber_ValueMode_ConvertBack_WrapsWhereIntToEnumRefuses()
        {
            LogAssert.Expect(LogType.Error, new Regex("NumberToEnumConverter"));

            Assert.AreEqual(ByteRank.Legend, new EnumToNumberConverter<ByteRank>().ConvertBack(456));
            Assert.AreEqual(ByteRank.Unranked, new NumberToEnumConverter<ByteRank>(byIndexNotValue: false, fallback: ByteRank.Unranked).Convert(456));
        }
    }
}
