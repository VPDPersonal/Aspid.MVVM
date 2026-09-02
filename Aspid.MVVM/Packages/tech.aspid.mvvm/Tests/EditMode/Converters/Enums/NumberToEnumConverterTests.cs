using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="NumberToEnumConverter{TEnum}"/> in both the value and the position mode.
    /// </summary>
    /// <remarks>
    /// The byte-backed cases are the ones a plain mask down to the underlying width would silently pass.
    /// </remarks>
    [TestFixture]
    public sealed class NumberToEnumConverterTests
    {
        // Values no member of their enum declares, so an assertion against one can only be satisfied
        // by the fallback and never coincidentally by a real member.
        private const Medal MissingMedal = (Medal)99;
        private const Depth MissingDepth = (Depth)9;

        // ---------------------------------------------------------------------------------------
        // Value mode
        // ---------------------------------------------------------------------------------------

        // The review bug. 456 & 0xFF == 200 and 712 & 0xFF == 200, so a converter that masks a
        // POSITIVE integer to the enum's width answers Legend — a rank the number never named.
        [TestCase(456)]
        [TestCase(712)]
        [TestCase(201)]
        [TestCase(255)]
        [TestCase(int.MaxValue)]
        public void Convert_ValueMode_ByteBackedEnum_OutOfRangeInteger_FallsBackInsteadOfWrapping(int value)
        {
            ExpectRefusal();

            Assert.AreEqual(ByteRank.Unranked, ByteRankByValue().Convert(value));
        }

        // Keeps the row above honest: the fallback is not what this converter always returns.
        [Test]
        public void Convert_ValueMode_ByteBackedEnum_ExactUnderlyingValue_ReturnsTheMember() =>
            Assert.AreEqual(ByteRank.Legend, ByteRankByValue().Convert(200));

        // The width mask is applied to a NEGATIVE integer on purpose — -1 into an unsigned enum has
        // to mean every bit of that width. The consequence is an asymmetry worth pinning: -56 lands
        // on Legend (256 - 56 == 200) while +456 does not.
        [Test]
        public void Convert_ValueMode_ByteBackedEnum_NegativeInteger_IsReadAtTheEnumsWidth() =>
            Assert.AreEqual(ByteRank.Legend, ByteRankByValue().Convert(-56));

        // The same rule on the width that makes the mask necessary: no int can name uint.MaxValue
        // positively, so a uint-backed member is only reachable through a negative one.
        [Test]
        public void Convert_ValueMode_UnsignedEnum_MinusOne_ReachesTheAllBitsMember() =>
            Assert.AreEqual(Bitfield.Full, new NumberToEnumConverter<Bitfield>(byIndexNotValue: false, fallback: Bitfield.Empty).Convert(-1));

        [TestCase(0, Medal.None)]
        [TestCase(10, Medal.Bronze)]
        [TestCase(20, Medal.Silver)]
        public void Convert_ValueMode_ReturnsTheMemberHoldingThatNumber(int value, Medal expected) =>
            Assert.AreEqual(expected, MedalByValue().Convert(value));

        // 1 and 2 are the positions of Bronze and Silver, and are exactly what a caller who reached
        // for the wrong mode would send.
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(11)]
        [TestCase(21)]
        [TestCase(-1)]
        public void Convert_ValueMode_NumberNamingNoMember_ReturnsTheFallback(int value)
        {
            ExpectRefusal();

            Assert.AreEqual(MissingMedal, MedalByValue().Convert(value));
        }

        [Test]
        public void Convert_DefaultConstructed_ReadsTheUnderlyingNumber() =>
            Assert.AreEqual(Medal.Bronze, new NumberToEnumConverter<Medal>().Convert(10));

        // The parameterless constructor leaves the fallback at default(TEnum); Convert(1) can only
        // reach None through it, since no member holds 1. The fallback being a real member is exactly
        // why the refusal is also reported: the answer alone cannot tell the two apart.
        [Test]
        public void Convert_DefaultConstructed_FallsBackToTheDefaultMember()
        {
            ExpectRefusal();

            Assert.AreEqual(Medal.None, new NumberToEnumConverter<Medal>().Convert(1));
        }

        [Test]
        public void Convert_ValueMode_FlagsEnum_AcceptsACombinationOfDeclaredFlags() =>
            Assert.AreEqual(Hazard.Fire | Hazard.Ice, HazardByValue().Convert(3));

        [Test]
        public void Convert_ValueMode_FlagsEnum_AcceptsEveryDeclaredFlagAtOnce() =>
            Assert.AreEqual(Hazard.Fire | Hazard.Ice | Hazard.Shock, HazardByValue().Convert(7));

        // 8 is a bit no member declares and 9 is that bit alongside a real one. [Flags] widens what
        // counts as legal to the declared bits, not to every integer.
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(16)]
        [TestCase(-1)]
        public void Convert_ValueMode_FlagsEnum_BitOutsideTheDeclaredFlags_ReturnsTheFallback(int value)
        {
            ExpectRefusal();

            Assert.AreEqual(
                Hazard.Shock,
                new NumberToEnumConverter<Hazard>(byIndexNotValue: false, fallback: Hazard.Shock).Convert(value));
        }

        // ---------------------------------------------------------------------------------------
        // Position mode
        // ---------------------------------------------------------------------------------------

        [TestCase(0, Medal.None)]
        [TestCase(1, Medal.Bronze)]
        [TestCase(2, Medal.Silver)]
        public void Convert_IndexMode_ReturnsTheMemberAtThatPosition(int index, Medal expected) =>
            Assert.AreEqual(expected, MedalByIndex().Convert(index));

        [TestCase(3)]
        [TestCase(20)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void Convert_IndexMode_PositionOutsideTheEnum_ReturnsTheFallback(int index)
        {
            ExpectRefusal();

            Assert.AreEqual(MissingMedal, MedalByIndex().Convert(index));
        }

        // Why the position mode exists: a dropdown's row 1 is Bronze, while the number 1 names no
        // medal at all. Reading one as the other selects the wrong row or refuses a valid one.
        [Test]
        public void Convert_IndexMode_AndValueMode_DisagreeOnASparseEnum()
        {
            ExpectRefusal();

            Assert.AreEqual(Medal.Bronze, MedalByIndex().Convert(1));
            Assert.AreEqual(MissingMedal, MedalByValue().Convert(1));
        }

        // Position 1 on a byte-backed enum is Legend even though 1 is nowhere near its value, and
        // 456 is out of range as a position too — the mode changes the meaning, not the strictness.
        [Test]
        public void Convert_IndexMode_ByteBackedEnum_CountsPositionsNotValues()
        {
            var converter = new NumberToEnumConverter<ByteRank>(byIndexNotValue: true, fallback: ByteRank.Unranked);

            ExpectRefusal();

            Assert.AreEqual(ByteRank.Legend, converter.Convert(1));
            Assert.AreEqual(ByteRank.Unranked, converter.Convert(456));
        }

        // A dropdown listing flag members numbers them 0,1,2,3 while their values run 0,1,2,4.
        [Test]
        public void NumberToEnum_LongBackedEnum_ReadsTheMemberItNames() =>
            Assert.AreEqual(
                Distance.Far,
                ((IConverter<long, Distance>)new NumberToEnumConverter<Distance>()).Convert(5_000_000_000L));

        [Test]
        public void NumberToEnum_WholeDouble_ReadsTheMemberItNames() =>
            Assert.AreEqual(
                Medal.Silver,
                ((IConverter<double, Medal>)new NumberToEnumConverter<Medal>()).Convert(20d));

        // A fraction sits between two members and names neither.
        [Test]
        public void NumberToEnum_FractionalDouble_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("NumberToEnumConverter.*a whole number naming a member"));

            Assert.AreEqual(
                Medal.None,
                ((IConverter<double, Medal>)new NumberToEnumConverter<Medal>()).Convert(20.5d));
        }

        [Test]
        public void Convert_IndexMode_FlagsEnum_ReadsPositionThreeAsShockNotAsACombination()
        {
            Assert.AreEqual(Hazard.Shock, new NumberToEnumConverter<Hazard>(byIndexNotValue: true, fallback: Hazard.None).Convert(3));
            Assert.AreEqual(Hazard.Fire | Hazard.Ice, HazardByValue().Convert(3));
        }

        // Enum.GetValues orders members by UNSIGNED underlying value, so Below — declared first —
        // comes back last. A position mode that assumed declaration order would be off by two here.
        [TestCase(0, Depth.Surface)]
        [TestCase(1, Depth.Above)]
        [TestCase(2, Depth.Below)]
        public void Convert_IndexMode_FollowsUnsignedMemberOrderNotDeclarationOrder(int index, Depth expected) =>
            Assert.AreEqual(expected, new NumberToEnumConverter<Depth>(byIndexNotValue: true, fallback: MissingDepth).Convert(index));

        // A refused integer is reported every time it arrives, so each refusal an assertion provokes
        // has to be declared or the fixture fails on the console entry rather than on the value.
        private static void ExpectRefusal() =>
            LogAssert.Expect(LogType.Error, new Regex("NumberToEnumConverter"));

        private static NumberToEnumConverter<ByteRank> ByteRankByValue() =>
            new(byIndexNotValue: false, fallback: ByteRank.Unranked);

        private static NumberToEnumConverter<Medal> MedalByValue() =>
            new(byIndexNotValue: false, fallback: MissingMedal);

        private static NumberToEnumConverter<Medal> MedalByIndex() =>
            new(byIndexNotValue: true, fallback: MissingMedal);

        private static NumberToEnumConverter<Hazard> HazardByValue() =>
            new(byIndexNotValue: false, fallback: Hazard.None);
    }
}
