using System;
using UnityEngine;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the members added to the enum converters after the first wave:
    /// <see cref="IntToEnumConverter{TEnum}"/> in both modes, the position mode on
    /// <see cref="EnumToIntConverter{TEnum}"/>, and the <see cref="EnumNameSource.Description"/> and
    /// <see cref="EnumNameSource.Raw"/> text sources.
    /// </summary>
    /// <remarks>
    /// The integer these converters read comes from outside the ViewModel — a save file, a server
    /// field, a dropdown row — so it is whatever someone else put there. The mistake guarded against
    /// is turning such a number into an enum by casting it, or by masking it down to the underlying
    /// width: both hand the View a value no <c>switch</c> in the game has a case for, and the symptom
    /// surfaces far from the number that caused it. The byte-backed cases below are the ones a mask
    /// would silently pass.
    /// <para>
    /// Two assertions pin behaviour the documentation reads against. <c>EnumToIntConverter</c>
    /// documents its <see cref="OverflowException"/> as something "only a long- or ulong-backed enum
    /// can manage"; a <see cref="uint"/>-backed member above <see cref="int.MaxValue"/> throws it
    /// too, and the test asserts the throw. And <c>ConvertBack</c> in the value mode still casts, so
    /// the number <see cref="IntToEnumConverter{TEnum}"/> refuses is one a TwoWay binder on the other
    /// converter accepts — an asymmetry between the two, not a bug in either.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class EnumConverterAdditionsTests
    {
        // Values no member of their enum declares, so an assertion against one can only be satisfied
        // by the fallback and never coincidentally by a real member.
        private const Medal MissingMedal = (Medal)99;
        private const Depth MissingDepth = (Depth)9;
        private const Quest MissingQuest = (Quest)42;

        // ---------------------------------------------------------------------------------------
        // IntToEnumConverter — value mode
        // ---------------------------------------------------------------------------------------

        // The review bug. 456 & 0xFF == 200 and 712 & 0xFF == 200, so a converter that masks a
        // POSITIVE integer to the enum's width answers Legend — a rank the number never named.
        [TestCase(456)]
        [TestCase(712)]
        [TestCase(201)]
        [TestCase(255)]
        [TestCase(int.MaxValue)]
        public void Convert_ValueMode_ByteBackedEnum_OutOfRangeInteger_FallsBackInsteadOfWrapping(int value) =>
            Assert.AreEqual(ByteRank.Unranked, ByteRankByValue().Convert(value));

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
            Assert.AreEqual(Bitfield.Full, new IntToEnumConverter<Bitfield>(byIndexNotValue: false, fallback: Bitfield.Empty).Convert(-1));

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
        public void Convert_ValueMode_NumberNamingNoMember_ReturnsTheFallback(int value) =>
            Assert.AreEqual(MissingMedal, MedalByValue().Convert(value));

        [Test]
        public void Convert_DefaultConstructed_ReadsTheUnderlyingNumber() =>
            Assert.AreEqual(Medal.Bronze, new IntToEnumConverter<Medal>().Convert(10));

        // The parameterless constructor leaves the fallback at default(TEnum); Convert(1) can only
        // reach None through it, since no member holds 1.
        [Test]
        public void Convert_DefaultConstructed_FallsBackToTheDefaultMember() =>
            Assert.AreEqual(Medal.None, new IntToEnumConverter<Medal>().Convert(1));

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
        public void Convert_ValueMode_FlagsEnum_BitOutsideTheDeclaredFlags_ReturnsTheFallback(int value) =>
            Assert.AreEqual(Hazard.Shock, new IntToEnumConverter<Hazard>(byIndexNotValue: false, fallback: Hazard.Shock).Convert(value));

        // ---------------------------------------------------------------------------------------
        // IntToEnumConverter — index mode
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
        public void Convert_IndexMode_PositionOutsideTheEnum_ReturnsTheFallback(int index) =>
            Assert.AreEqual(MissingMedal, MedalByIndex().Convert(index));

        // Why the position mode exists: a dropdown's row 1 is Bronze, while the number 1 names no
        // medal at all. Reading one as the other selects the wrong row or refuses a valid one.
        [Test]
        public void Convert_IndexMode_AndValueMode_DisagreeOnASparseEnum()
        {
            Assert.AreEqual(Medal.Bronze, MedalByIndex().Convert(1));
            Assert.AreEqual(MissingMedal, MedalByValue().Convert(1));
        }

        // Position 1 on a byte-backed enum is Legend even though 1 is nowhere near its value, and
        // 456 is out of range as a position too — the mode changes the meaning, not the strictness.
        [Test]
        public void Convert_IndexMode_ByteBackedEnum_CountsPositionsNotValues()
        {
            var converter = new IntToEnumConverter<ByteRank>(byIndexNotValue: true, fallback: ByteRank.Unranked);

            Assert.AreEqual(ByteRank.Legend, converter.Convert(1));
            Assert.AreEqual(ByteRank.Unranked, converter.Convert(456));
        }

        // A dropdown listing flag members numbers them 0,1,2,3 while their values run 0,1,2,4.
        [Test]
        public void Convert_IndexMode_FlagsEnum_ReadsPositionThreeAsShockNotAsACombination()
        {
            Assert.AreEqual(Hazard.Shock, new IntToEnumConverter<Hazard>(byIndexNotValue: true, fallback: Hazard.None).Convert(3));
            Assert.AreEqual(Hazard.Fire | Hazard.Ice, HazardByValue().Convert(3));
        }

        // Enum.GetValues orders members by UNSIGNED underlying value, so Below — declared first —
        // comes back last. A position mode that assumed declaration order would be off by two here.
        [TestCase(0, Depth.Surface)]
        [TestCase(1, Depth.Above)]
        [TestCase(2, Depth.Below)]
        public void Convert_IndexMode_FollowsUnsignedMemberOrderNotDeclarationOrder(int index, Depth expected) =>
            Assert.AreEqual(expected, new IntToEnumConverter<Depth>(byIndexNotValue: true, fallback: MissingDepth).Convert(index));

        // ---------------------------------------------------------------------------------------
        // EnumToIntConverter — the position mode on both directions
        // ---------------------------------------------------------------------------------------

        [Test]
        public void EnumToInt_IndexMode_ReportsThePosition() =>
            Assert.AreEqual(2, new EnumToIntConverter<Medal>(byIndexNotValue: true).Convert(Medal.Silver));

        [Test]
        public void EnumToInt_ValueMode_ReportsTheUnderlyingNumber() =>
            Assert.AreEqual(20, new EnumToIntConverter<Medal>(byIndexNotValue: false).Convert(Medal.Silver));

        // A dropdown reads -1 as no selection, which is the honest answer for a value that has no
        // row. Returning 0 instead would silently highlight the first one.
        [Test]
        public void EnumToInt_IndexMode_UndeclaredValue_ReportsMinusOne() =>
            Assert.AreEqual(-1, new EnumToIntConverter<Medal>(byIndexNotValue: true).Convert(MissingMedal));

        [Test]
        public void EnumToInt_ValueMode_UndeclaredValue_PassesTheNumberThrough() =>
            Assert.AreEqual(99, new EnumToIntConverter<Medal>(byIndexNotValue: false).Convert(MissingMedal));

        [TestCase(Medal.None)]
        [TestCase(Medal.Bronze)]
        [TestCase(Medal.Silver)]
        public void EnumToInt_IndexMode_RoundTripsEveryMember(Medal value)
        {
            var converter = new EnumToIntConverter<Medal>(byIndexNotValue: true, fallback: MissingMedal);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)));
        }

        [TestCase(3)]
        [TestCase(-1)]
        public void EnumToInt_IndexMode_ConvertBack_PositionOutsideTheEnum_ReturnsTheFallback(int value) =>
            Assert.AreEqual(MissingMedal, new EnumToIntConverter<Medal>(byIndexNotValue: true, fallback: MissingMedal).ConvertBack(value));

        // The fallback is authored on the same object in both modes, and the tooltip promises the
        // value mode ignores it. It does: an undeclared number survives the trip untouched, which is
        // what lets a flag combination round trip.
        [Test]
        public void EnumToInt_ValueMode_ConvertBack_IgnoresTheFallback() =>
            Assert.AreEqual(MissingMedal, new EnumToIntConverter<Medal>(byIndexNotValue: false, fallback: Medal.Silver).ConvertBack(99));

        // Documented on EnumToIntConverter as "only a long- or ulong-backed enum can manage" — not
        // so. A uint-backed member above int.MaxValue overflows the int conversion just as well.
        [Test]
        public void EnumToInt_ValueMode_UnsignedMemberAboveIntMaxValue_Overflows() =>
            Assert.Throws<OverflowException>(() => new EnumToIntConverter<Bitfield>().Convert(Bitfield.Full));

        // The position mode never touches the underlying value, so the member the value mode cannot
        // express still has a usable dropdown row.
        [Test]
        public void EnumToInt_IndexMode_UnsignedMemberAboveIntMaxValue_ReportsThePosition() =>
            Assert.AreEqual(1, new EnumToIntConverter<Bitfield>(byIndexNotValue: true).Convert(Bitfield.Full));

        // The pair that explains why IntToEnumConverter exists at all. ConvertBack casts through
        // Enum.ToObject, which truncates 456 to the low byte and lands on a real member; the one-way
        // converter refuses the same number. A TwoWay binder therefore still admits it — that is a
        // property of ConvertBack, not an oversight of the test.
        [Test]
        public void EnumToInt_ValueMode_ConvertBack_WrapsWhereIntToEnumRefuses()
        {
            Assert.AreEqual(ByteRank.Legend, new EnumToIntConverter<ByteRank>().ConvertBack(456));
            Assert.AreEqual(ByteRank.Unranked, ByteRankByValue().Convert(456));
        }

        // ---------------------------------------------------------------------------------------
        // EnumNameSource.Description
        // ---------------------------------------------------------------------------------------

        [TestCase(Quest.Idle, "Not started yet")]
        [TestCase(Quest.Active, "The quest is running")]
        [TestCase(Quest.Done, "Done")]
        [TestCase(Quest.Failed, "Failed")]
        [TestCase(Quest.Abandoned, "Abandoned")]
        public void Convert_Description_ReadsTheDescriptionAttributeAndFallsBackToTheName(Quest value, string expected) =>
            Assert.AreEqual(expected, new EnumToStringConverter<Quest>(EnumNameSource.Description, "?").Convert(value));

        // Each source caches its labels in its own static array. One array serving both would return
        // whichever source was asked for first, for the other one too.
        [Test]
        public void Convert_Description_AndInspectorName_DoNotShareACache()
        {
            Assert.AreEqual("In progress", new EnumToStringConverter<Quest>(EnumNameSource.InspectorName).Convert(Quest.Active));
            Assert.AreEqual("The quest is running", new EnumToStringConverter<Quest>(EnumNameSource.Description).Convert(Quest.Active));
        }

        // Done carries an InspectorName and no Description, so the two sources must part ways there.
        [Test]
        public void Convert_Description_MemberWithOnlyAnInspectorName_ReturnsTheMemberName()
        {
            Assert.AreEqual("Wrapped up", new EnumToStringConverter<Quest>(EnumNameSource.InspectorName).Convert(Quest.Done));
            Assert.AreEqual("Done", new EnumToStringConverter<Quest>(EnumNameSource.Description).Convert(Quest.Done));
        }

        [Test]
        public void Convert_Description_UndeclaredValue_ReturnsTheFallback() =>
            Assert.AreEqual("?", new EnumToStringConverter<Quest>(EnumNameSource.Description, "?").Convert(MissingQuest));

        // ---------------------------------------------------------------------------------------
        // EnumNameSource.Raw
        // ---------------------------------------------------------------------------------------

        [TestCase(Medal.Bronze, "Bronze")]
        [TestCase(Medal.None, "None")]
        public void Convert_Raw_DeclaredMember_WritesTheMemberName(Medal value, string expected) =>
            Assert.AreEqual(expected, new EnumToStringConverter<Medal>(EnumNameSource.Raw, "?").Convert(value));

        // Raw is the only source that answers for a value the metadata does not hold, so it never
        // reaches the fallback — the "?" below would be the result under every other source.
        [Test]
        public void Convert_Raw_UndeclaredValue_WritesTheNumberRatherThanTheFallback() =>
            Assert.AreEqual("99", new EnumToStringConverter<Medal>(EnumNameSource.Raw, "?").Convert(MissingMedal));

        [Test]
        public void Convert_Raw_FlagCombination_WritesTheFlagsInAscendingValueOrder() =>
            Assert.AreEqual("Fire, Ice", new EnumToStringConverter<Hazard>(EnumNameSource.Raw).Convert(Hazard.Fire | Hazard.Ice));

        // A bit no member declares cannot be named, so ToString gives up on names entirely — even
        // for 9, where one of the two bits is a real flag.
        [Test]
        public void Convert_Raw_BitOutsideTheDeclaredFlags_WritesTheNumber() =>
            Assert.AreEqual("9", new EnumToStringConverter<Hazard>(EnumNameSource.Raw, "?").Convert((Hazard)9));

        // The contrast that makes Raw worth having: a combination is not a declared member, so every
        // metadata source falls back on it.
        [Test]
        public void Convert_Name_FlagCombination_ReturnsTheFallback() =>
            Assert.AreEqual("?", new EnumToStringConverter<Hazard>(EnumNameSource.Name, "?").Convert(Hazard.Fire | Hazard.Ice));

        private static IntToEnumConverter<ByteRank> ByteRankByValue() =>
            new(byIndexNotValue: false, fallback: ByteRank.Unranked);

        private static IntToEnumConverter<Medal> MedalByValue() =>
            new(byIndexNotValue: false, fallback: MissingMedal);

        private static IntToEnumConverter<Medal> MedalByIndex() =>
            new(byIndexNotValue: true, fallback: MissingMedal);

        private static IntToEnumConverter<Hazard> HazardByValue() =>
            new(byIndexNotValue: false, fallback: Hazard.None);
    }
}
