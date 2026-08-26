using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumToStringConverter{TEnum}"/> across its four
    /// <see cref="EnumNameSource"/> options.
    /// </summary>
    /// <remarks>
    /// <see cref="EnumNameSource.Raw"/> is the one source that answers for a value the metadata does
    /// not hold — a flag combination or an undeclared number — so it never reaches the fallback the
    /// other three sources share.
    /// </remarks>
    [TestFixture]
    internal sealed class EnumToStringConverterTests
    {
        private const Medal MissingMedal = (Medal)99;
        private const Quest MissingQuest = (Quest)42;

        // ---------------------------------------------------------------------------------------
        // EnumNameSource.Name
        // ---------------------------------------------------------------------------------------

        [TestCase(Medal.None, "None")]
        [TestCase(Medal.Bronze, "Bronze")]
        public void Convert_Name_DeclaredMember_WritesTheMemberName(Medal value, string expected) =>
            Assert.AreEqual(expected, new EnumToStringConverter<Medal>().Convert(value));

        [Test]
        public void Convert_Name_UndeclaredValue_ReturnsTheFallback()
        {
            ExpectUndeclaredMemberError();

            Assert.AreEqual("?", new EnumToStringConverter<Medal>(EnumNameSource.Name, "?").Convert(MissingMedal));
        }

        // The contrast that makes Raw worth having: a combination is not a declared member, so every
        // metadata source falls back on it.
        [Test]
        public void Convert_Name_FlagCombination_ReturnsTheFallback()
        {
            ExpectUndeclaredMemberError();

            Assert.AreEqual("?", new EnumToStringConverter<Hazard>(EnumNameSource.Name, "?").Convert(Hazard.Fire | Hazard.Ice));
        }

        // ---------------------------------------------------------------------------------------
        // EnumNameSource.InspectorName
        // ---------------------------------------------------------------------------------------

        [Test]
        public void Convert_InspectorName_ReadsTheAttribute() =>
            Assert.AreEqual("In progress", new EnumToStringConverter<Quest>(EnumNameSource.InspectorName).Convert(Quest.Active));

        [Test]
        public void Convert_InspectorName_MemberWithoutTheAttribute_ReturnsTheMemberName() =>
            Assert.AreEqual("Failed", new EnumToStringConverter<Quest>(EnumNameSource.InspectorName).Convert(Quest.Failed));

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
        public void Convert_Description_UndeclaredValue_ReturnsTheFallback()
        {
            ExpectUndeclaredMemberError();

            Assert.AreEqual("?", new EnumToStringConverter<Quest>(EnumNameSource.Description, "?").Convert(MissingQuest));
        }

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

        private static void ExpectUndeclaredMemberError() =>
            LogAssert.Expect(LogType.Error, new Regex("EnumToStringConverter.*a declared member"));
    }
}
