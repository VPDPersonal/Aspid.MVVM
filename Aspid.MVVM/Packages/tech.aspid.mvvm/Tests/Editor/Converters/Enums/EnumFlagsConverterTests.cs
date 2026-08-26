using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumFlagsToStringConverter{TEnum}"/> and
    /// <see cref="EnumMaskConverter{TEnum}"/> — flag naming, composite members, the non-[Flags]
    /// passthrough, the four mask operations and the size-one cache both converters keep.
    /// </summary>
    /// <remarks>
    /// The mistake guarded against is reading a plain enum as a set of bits: masking it either blanks it
    /// or produces a number no member declares. Every non-[Flags] assertion is paired with the same
    /// operation on a [Flags] enum, so the passthrough cannot pass by being a no-op everywhere.
    /// <para>
    /// The second is naming a composite member instead of its parts — a bit reachable only through a
    /// composite ends up with no name at all. The third is documentation: three assertions pin behavior
    /// the XML docs read against, and are named to say so.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class EnumFlagsConverterTests
    {
        private const Hazard FireAndIce = Hazard.Fire | Hazard.Ice;
        private const Hazard FireAndShock = Hazard.Fire | Hazard.Shock;
        private const Hazard IceAndShock = Hazard.Ice | Hazard.Shock;
        private const Hazard EveryHazard = Hazard.Fire | Hazard.Ice | Hazard.Shock;

        // A bit no member declares, and the same bit beside a real flag.
        private const Hazard UnnamedBit = (Hazard)8;
        private const Hazard FireAndUnnamedBit = (Hazard)9;

        private const Element TwoElements = Element.Fire | Element.Ice;
        private const Permission ReadWriteAndExecute = Permission.ReadWrite | Permission.Execute;

        // Bit 1 of Permission is reachable only through ReadWrite, and bit 1 beside Execute.
        private const Permission HalfOfReadWrite = (Permission)1;
        private const Permission HalfOfReadWriteAndExecute = (Permission)5;

        private const Status BurningAndPoisoned = Status.Burning | Status.Poisoned;
        private const Channel LeftAndMuted = Channel.Left | Channel.Muted;
        private const Medal MissingMedal = (Medal)99;

        // -------------------------------------------------------------------------------------------
        // EnumFlagsToStringConverter — a [Flags] enum
        // -------------------------------------------------------------------------------------------

        [TestCase(Hazard.Fire, "Fire")]
        [TestCase(FireAndIce, "Fire, Ice")]
        [TestCase(IceAndShock, "Ice, Shock")]
        [TestCase(EveryHazard, "Fire, Ice, Shock")]
        public void Convert_Flags_NamesEveryFlagInAscendingValueOrder(Hazard value, string expected) =>
            Assert.AreEqual(expected, new EnumFlagsToStringConverter<Hazard>().Convert(value));

        [Test]
        public void Convert_Flags_UsesTheAuthoredSeparator() =>
            Assert.AreEqual("Fire + Ice + Shock", new EnumFlagsToStringConverter<Hazard>(" + ").Convert(EveryHazard));

        // The zero member is skipped by value, not by name: matched like any other it would be named
        // by every value, since every value has all zero of its bits.
        [Test]
        public void Convert_Flags_ZeroValue_ReturnsTheNoneTextRatherThanTheZeroMembersName() =>
            Assert.AreEqual("Nothing", Hazards("Nothing").Convert(Hazard.None));

        [Test]
        public void Convert_DefaultConstructed_JoinsWithCommasAndHasAnEmptyNoneText()
        {
            Assert.AreEqual("Fire, Ice", new EnumFlagsToStringConverter<Hazard>().Convert(FireAndIce));
            Assert.AreEqual(string.Empty, new EnumFlagsToStringConverter<Hazard>().Convert(Hazard.None));
        }

        // A bit no member declares has no name to give. Writing the leftover number would put
        // something in the middle of a sentence that reads as a bug rather than as data.
        [Test]
        public void Convert_Flags_UndeclaredBitAlone_ReturnsTheNoneText() =>
            Assert.AreEqual("Nothing", Hazards("Nothing").Convert(UnnamedBit));

        // The undeclared half is dropped in silence — the text under-reports rather than refusing.
        [Test]
        public void Convert_Flags_UndeclaredBitBesideADeclaredOne_NamesOnlyTheDeclaredOne() =>
            Assert.AreEqual("Fire", Hazards("Nothing").Convert(FireAndUnnamedBit));

        // -------------------------------------------------------------------------------------------
        // EnumFlagsToStringConverter — composite members
        // -------------------------------------------------------------------------------------------

        // All is declared first and still loses to its parts: Enum.GetValues sorts by value, and 7
        // cannot sort ahead of 1, 2 and 4. Moving a composite up the enum therefore changes nothing.
        [Test]
        public void Convert_Flags_CompositeWhoseBitsAreDeclared_NamesThePartsRegardlessOfDeclarationOrder()
        {
            Assert.AreEqual("Fire, Ice, Poison", new EnumFlagsToStringConverter<Element>().Convert(Element.All));
            Assert.AreEqual("Fire, Ice", new EnumFlagsToStringConverter<Element>().Convert(TwoElements));
        }

        // Nothing consumes bits 1 and 2 before ReadWrite is reached, so the composite is the name.
        [TestCase(Permission.ReadWrite, "ReadWrite")]
        [TestCase(ReadWriteAndExecute, "ReadWrite, Execute")]
        public void Convert_Flags_CompositeWhoseBitsAreNotDeclared_NamesTheComposite(Permission value, string expected) =>
            Assert.AreEqual(expected, Permissions().Convert(value));

        // The flip side of the rule above: a member is named only when EVERY bit it covers is still
        // unclaimed, so half of a composite matches nothing and the text falls to the empty case.
        [Test]
        public void Convert_Flags_BitReachableOnlyThroughAComposite_HasNoNameOfItsOwn()
        {
            Assert.AreEqual("Nothing", Permissions().Convert(HalfOfReadWrite));
            Assert.AreEqual("Execute", Permissions().Convert(HalfOfReadWriteAndExecute));
        }

        // Ping consumes bit 1 first, leaving PingAndPong unmatchable and bit 2 unnamed — so the whole
        // value reads as "Ping", which is half of what it carries.
        [Test]
        public void Convert_Flags_CompositeOverOneDeclaredAndOneUndeclaredBit_NamesTheDeclaredPartOnly() =>
            Assert.AreEqual("Ping", new EnumFlagsToStringConverter<Signal>(", ", EnumNameSource.Name, "Nothing").Convert(Signal.PingAndPong));

        // -------------------------------------------------------------------------------------------
        // EnumFlagsToStringConverter — the name source
        // -------------------------------------------------------------------------------------------

        // Each flag is named on its own, so a member's attribute reads here exactly as it does through
        // EnumToStringConverter — including the fall back to the member name when it is absent.
        [Test]
        public void Convert_Flags_NameSource_IsAskedForEachFlagSeparately()
        {
            Assert.AreEqual("Burning, Poisoned", new EnumFlagsToStringConverter<Status>().Convert(BurningAndPoisoned));
            Assert.AreEqual("On fire, Poisoned", new EnumFlagsToStringConverter<Status>(", ", EnumNameSource.InspectorName).Convert(BurningAndPoisoned));
            Assert.AreEqual("Burning, Losing health slowly", new EnumFlagsToStringConverter<Status>(", ", EnumNameSource.Description).Convert(BurningAndPoisoned));
        }

        // Raw is the one source that writes an undeclared value as its number. Reached one flag at a
        // time it never sees an undeclared value, so the number cannot leak into the text: the same
        // input through EnumToStringConverter reads "9".
        [Test]
        public void Convert_Flags_Raw_NamesEachFlagAndNeverWritesTheNumber()
        {
            Assert.AreEqual("Fire", new EnumFlagsToStringConverter<Hazard>(", ", EnumNameSource.Raw, "Nothing").Convert(FireAndUnnamedBit));
            Assert.AreEqual("9", new EnumToStringConverter<Hazard>(EnumNameSource.Raw).Convert(FireAndUnnamedBit));
        }

        // The source is only consulted once a flag has to be named, so a value carrying none stays
        // quiet, while a named flag reports the undeclared source and falls back to the member name.
        [Test]
        public void Convert_UndeclaredNameSource_ReportsOnlyWhenAFlagHasToBeNamed()
        {
            var converter = new EnumFlagsToStringConverter<Hazard>(", ", (EnumNameSource)99, "Nothing");

            LogAssert.Expect(LogType.Error, new Regex("not a declared EnumNameSource"));

            Assert.AreEqual("Fire", converter.Convert(Hazard.Fire));
            Assert.AreEqual("Nothing", converter.Convert(Hazard.None));
        }

        // -------------------------------------------------------------------------------------------
        // EnumFlagsToStringConverter — an enum that is not [Flags]
        // -------------------------------------------------------------------------------------------

        // The separator is authored on the same object either way, and a value split into bits here
        // would name whichever members happen to sit inside the number.
        [TestCase(Medal.None, "None")]
        [TestCase(Medal.Bronze, "Bronze")]
        [TestCase(Medal.Silver, "Silver")]
        public void Convert_NonFlags_NamesTheWholeValueAndLeavesTheSeparatorUnused(Medal value, string expected) =>
            Assert.AreEqual(expected, Medals().Convert(value));

        // The pair that shows the two paths really are different code: zero is the absence of every
        // flag on one enum and a member with a name on the other.
        [Test]
        public void Convert_ZeroValue_IsTheNoneTextOnAFlagsEnumAndAMemberNameOtherwise()
        {
            Assert.AreEqual("Nothing", Hazards("Nothing").Convert(Hazard.None));
            Assert.AreEqual("None", Medals().Convert(Medal.None));
        }

        // The whole value is handed to the inner name converter, which has no name for it and says so.
        [Test]
        public void Convert_NonFlags_UndeclaredValue_ReturnsTheNoneText()
        {
            LogAssert.Expect(LogType.Error, new Regex("EnumToStringConverter.*a declared member"));

            Assert.AreEqual("n/a", Medals().Convert(MissingMedal));
        }

        // Documented as the text shown "when the value names no flags". Under Raw the inner converter
        // answers with the number, which is not empty, so the empty text is never reached — the View
        // gets "99" where every other source gives "n/a".
        [Test]
        public void Convert_NonFlags_Raw_UndeclaredValue_WritesTheNumberInsteadOfTheNoneText() =>
            Assert.AreEqual("99", new EnumFlagsToStringConverter<Medal>("###", EnumNameSource.Raw, "n/a").Convert(MissingMedal));

        [Test]
        public void Convert_NonFlags_InspectorName_ReadsTheAttribute() =>
            Assert.AreEqual("In progress", new EnumFlagsToStringConverter<Quest>("###", EnumNameSource.InspectorName).Convert(Quest.Active));

        // -------------------------------------------------------------------------------------------
        // EnumFlagsToStringConverter — the cache
        // -------------------------------------------------------------------------------------------

        // A binder pushes on every notification rather than on every change, so a summary rebuilt per
        // push would allocate a string per push. Same value in, same instance out.
        [Test]
        public void Convert_SameValueTwice_ReturnsTheSameStringInstance()
        {
            var converter = new EnumFlagsToStringConverter<Hazard>();

            Assert.AreSame(converter.Convert(FireAndIce), converter.Convert(FireAndIce));
        }

        // Only the last value is remembered. A cache keyed on anything less than the value would hand
        // the third push the text built for the first.
        [Test]
        public void Convert_AlternatingBetweenTwoValues_AnswersForTheValueItWasGiven()
        {
            var converter = new EnumFlagsToStringConverter<Hazard>();

            Assert.AreEqual("Fire", converter.Convert(Hazard.Fire));
            Assert.AreEqual("Fire, Ice", converter.Convert(FireAndIce));
            Assert.AreEqual("Fire", converter.Convert(Hazard.Fire));
        }

        // -------------------------------------------------------------------------------------------
        // EnumMaskConverter — a [Flags] enum
        // -------------------------------------------------------------------------------------------

        [TestCase(EnumMaskOperation.And, Hazard.Fire)]
        [TestCase(EnumMaskOperation.Or, EveryHazard)]
        [TestCase(EnumMaskOperation.Xor, IceAndShock)]
        [TestCase(EnumMaskOperation.Clear, Hazard.Ice)]
        public void MaskConvert_Flags_AppliesTheOperation(EnumMaskOperation operation, Hazard expected) =>
            Assert.AreEqual(expected, new EnumMaskConverter<Hazard>(FireAndShock, operation).Convert(FireAndIce));

        // An empty mask under the default operation is what an unauthored converter carries, and And
        // is that default — so a converter dropped on a binder and left alone blanks the value it is
        // given. This is the behavior the non-[Flags] passthrough below exists to avoid.
        [Test]
        public void MaskConvert_EmptyMask_AndsWithAnEmptyMaskAndBlanksTheValue() =>
            Assert.AreEqual(Hazard.None, new EnumMaskConverter<Hazard>(default).Convert(FireAndIce));

        // A combination is a legal value the member list does not hold, so the result is not checked
        // against it — including when the value carries a bit no member declares.
        [Test]
        public void MaskConvert_Flags_ResultNeedNotBeADeclaredMember()
        {
            Assert.AreEqual(FireAndIce, new EnumMaskConverter<Hazard>(Hazard.Ice, EnumMaskOperation.Or).Convert(Hazard.Fire));
            Assert.AreEqual(FireAndUnnamedBit, new EnumMaskConverter<Hazard>(Hazard.Fire, EnumMaskOperation.Or).Convert(UnnamedBit));
        }

        // An operation outside the enum is what a renamed or reordered member deserializes into. It
        // is reported rather than combining bits under a rule nobody authored, and the value the
        // View already shows is left alone.
        [Test]
        public void MaskConvert_Flags_UndeclaredOperation_ReportsItAndPassesTheValueThrough()
        {
            ExpectUndeclaredOperationError();

            Assert.AreEqual(
                FireAndIce,
                new EnumMaskConverter<Hazard>(Hazard.Fire, (EnumMaskOperation)99).Convert(FireAndIce));
        }

        // The passthrough is never cached, so a converter left broken says so on every push instead
        // of answering the second one out of the cache.
        [Test]
        public void MaskConvert_Flags_UndeclaredOperation_ReportsItOnEveryPush()
        {
            var converter = new EnumMaskConverter<Hazard>(Hazard.Fire, (EnumMaskOperation)99);

            ExpectUndeclaredOperationError();
            ExpectUndeclaredOperationError();

            converter.Convert(FireAndIce);
            converter.Convert(FireAndIce);
        }

        private static void ExpectUndeclaredOperationError() =>
            LogAssert.Expect(LogType.Error, new Regex("EnumMaskConverter.*not a declared"));

        // -------------------------------------------------------------------------------------------
        // EnumMaskConverter — an enum that is not [Flags]
        // -------------------------------------------------------------------------------------------

        [TestCase(EnumMaskOperation.And)]
        [TestCase(EnumMaskOperation.Or)]
        [TestCase(EnumMaskOperation.Xor)]
        [TestCase(EnumMaskOperation.Clear)]
        public void MaskConvert_NonFlags_PassesTheValueThroughUnchanged(EnumMaskOperation operation)
        {
            ExpectNonFlagsError();

            Assert.AreEqual(Medal.Silver, new EnumMaskConverter<Medal>(Medal.Bronze, operation).Convert(Medal.Silver));
        }

        // Both masks reduce Silver's 20 to 0 when read as bits, which would hand the View Medal.None —
        // a medal the player does not have, indistinguishable from the real thing.
        [TestCase(EnumMaskOperation.And, Medal.None)]     // 20 & 0 == 0
        [TestCase(EnumMaskOperation.Clear, Medal.Silver)] // 20 & ~20 == 0
        public void MaskConvert_NonFlags_MaskThatWouldBlankTheValue_LeavesItAlone(EnumMaskOperation operation, Medal mask)
        {
            ExpectNonFlagsError();

            Assert.AreEqual(Medal.Silver, new EnumMaskConverter<Medal>(mask, operation).Convert(Medal.Silver));
        }

        // 20 against 10 gives 30 under both operations, and no medal holds 30 — a value every switch
        // in the game would fall through.
        [TestCase(EnumMaskOperation.Or)]  // 20 | 10 == 30
        [TestCase(EnumMaskOperation.Xor)] // 20 ^ 10 == 30
        public void MaskConvert_NonFlags_MaskThatWouldProduceAnUndeclaredNumber_LeavesItAlone(EnumMaskOperation operation)
        {
            ExpectNonFlagsError();

            Assert.AreEqual(Medal.Silver, new EnumMaskConverter<Medal>(Medal.Bronze, operation).Convert(Medal.Silver));
        }

        [Test]
        public void MaskConvert_NonFlags_UndeclaredValue_PassesThroughUnchanged()
        {
            ExpectNonFlagsError();

            Assert.AreEqual(MissingMedal, new EnumMaskConverter<Medal>(Medal.Bronze).Convert(MissingMedal));
        }

        // The non-[Flags] passthrough happens before the switch, so the same broken operation that is
        // reported on Hazard is never read here: only the one error comes out.
        [Test]
        public void MaskConvert_NonFlags_UndeclaredOperation_ReportsOnlyTheNonFlagsError()
        {
            ExpectNonFlagsError();

            Assert.AreEqual(Medal.Silver, new EnumMaskConverter<Medal>(Medal.Bronze, (EnumMaskOperation)99).Convert(Medal.Silver));
        }

        // The passthrough is a misconfiguration, not a feature: every push says so rather than
        // leaving a converter that does nothing looking as if it works.
        [Test]
        public void MaskConvert_NonFlags_ReportsTheMisconfigurationOnEveryPush()
        {
            var converter = new EnumMaskConverter<Medal>(Medal.Bronze);

            ExpectNonFlagsError();
            ExpectNonFlagsError();

            converter.Convert(Medal.Silver);
            converter.Convert(Medal.Silver);
        }

        private static void ExpectNonFlagsError() =>
            LogAssert.Expect(LogType.Error, new Regex(@"EnumMaskConverter.*is not marked \[Flags\]"));

        // -------------------------------------------------------------------------------------------
        // EnumMaskConverter — a signed enum holding the sign bit
        // -------------------------------------------------------------------------------------------

        // Muted reads as 0xFFFF_FFFF_FFFF_FF80, so every route below leaves bits set above the sbyte
        // the enum is backed by. Building the result truncates to that width, the same way an
        // assignment in code would; without the truncation none of these are a member at all.
        [TestCase(EnumMaskOperation.And, Channel.Muted)]
        [TestCase(EnumMaskOperation.Clear, Channel.Left)]
        [TestCase(EnumMaskOperation.Xor, Channel.Left)]
        public void MaskConvert_SignedEnumHoldingTheSignBit_TruncatesToTheEnumsWidth(EnumMaskOperation operation, Channel mask) =>
            Assert.AreEqual(Channel.Muted, new EnumMaskConverter<Channel>(mask, operation).Convert(LeftAndMuted));

        [Test]
        public void MaskConvert_SignedEnumHoldingTheSignBit_Or_KeepsBothMembers() =>
            Assert.AreEqual(LeftAndMuted, new EnumMaskConverter<Channel>(Channel.Muted, EnumMaskOperation.Or).Convert(Channel.Left));

        [Test]
        public void Convert_SignedEnumHoldingTheSignBit_NamesBothMembers() =>
            Assert.AreEqual("Left, Muted", new EnumFlagsToStringConverter<Channel>().Convert(LeftAndMuted));

        // -------------------------------------------------------------------------------------------
        // EnumMaskConverter — the cache and the chain
        // -------------------------------------------------------------------------------------------

        // The result is cached because an enum has no non-boxing route back from its bits. Only the
        // last input is remembered, so the third call has to be answered from scratch.
        [Test]
        public void MaskConvert_AlternatingBetweenTwoValues_AnswersForTheValueItWasGiven()
        {
            var converter = new EnumMaskConverter<Hazard>(FireAndIce);

            Assert.AreEqual(Hazard.Fire, converter.Convert(FireAndShock));
            Assert.AreEqual(Hazard.Ice, converter.Convert(IceAndShock));
            Assert.AreEqual(Hazard.Fire, converter.Convert(FireAndShock));
        }

        // What the mask is for: one panel names the elemental part of a value another panel names in
        // full, off the same property, with the choice authored beside the panel it belongs to.
        [Test]
        public void MaskConvert_ChainedIntoTheStringConverter_NarrowsWhatTheTextNames()
        {
            var mask = new EnumMaskConverter<Hazard>(FireAndIce);
            var text = new EnumFlagsToStringConverter<Hazard>();

            Assert.AreEqual("Fire, Ice", text.Convert(mask.Convert(EveryHazard)));
            Assert.AreEqual("Fire, Ice, Shock", text.Convert(EveryHazard));
        }

        private static EnumFlagsToStringConverter<Hazard> Hazards(string noneText) =>
            new(", ", EnumNameSource.Name, noneText);

        private static EnumFlagsToStringConverter<Permission> Permissions() =>
            new(", ", EnumNameSource.Name, "Nothing");

        private static EnumFlagsToStringConverter<Medal> Medals() =>
            new("###", EnumNameSource.Name, "n/a");
    }
}
