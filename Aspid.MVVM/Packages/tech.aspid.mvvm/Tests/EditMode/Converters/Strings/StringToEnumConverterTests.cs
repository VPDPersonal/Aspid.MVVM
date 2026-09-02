using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToEnumConverter{TEnum}"/> — reading a member name, a flag
    /// combination, and the refusals that keep a bare or undeclared number from slipping through.
    /// </summary>
    [TestFixture]
    public sealed class StringToEnumConverterTests
    {
        [TestCase("Rain", Weather.Rain)]
        [TestCase("rain", Weather.Rain)]
        [TestCase("", Weather.Clear)]
        public void Convert_ReadsTheMember(string value, Weather expected) =>
            Assert.AreEqual(expected, new StringToEnumConverter<Weather>(Weather.Clear).Convert(value));

        [Test]
        public void Convert_UnknownNameFallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter"));
            Assert.AreEqual(Weather.Clear, new StringToEnumConverter<Weather>(Weather.Clear).Convert("nonsense"));
        }

        // Enum.TryParse accepts a bare number and hands back an undeclared member for it, which is
        // rarely what a name-shaped input means.
        [Test]
        public void Convert_RejectsANumberThatNamesNoMember()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter"));
            Assert.AreEqual(Weather.Clear, new StringToEnumConverter<Weather>(Weather.Clear).Convert("99"));
        }

        [Test]
        public void Convert_RoundTrips()
        {
            var converter = new StringToEnumConverter<Weather>(Weather.Clear);

            Assert.AreEqual(Weather.Snow, converter.Convert(converter.ConvertBack(Weather.Snow)));
        }

        // A combination of flags is a legal value that is not a member of its own, so the check that
        // keeps a bare number out cannot be Enum.IsDefined: it would throw the combination away and
        // report a perfectly good input as a failure.
        [TestCase("Red, Blue", Palette.Red | Palette.Blue)]
        [TestCase("red, blue", Palette.Red | Palette.Blue)]
        [TestCase("Red", Palette.Red)]
        [TestCase("None", Palette.None)]
        public void Convert_ReadsACombinationOfFlags(string value, Palette expected) =>
            Assert.AreEqual(expected, new StringToEnumConverter<Palette>(Palette.None).Convert(value));

        [Test]
        public void Convert_FlagsRoundTrip()
        {
            const Palette value = Palette.Red | Palette.Blue;
            var converter = new StringToEnumConverter<Palette>(Palette.None);

            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)));
        }

        // Bits no member of the enum declares are still refused: 8 is outside the mask the members
        // build, so it names nothing even though the enum is read as bits.
        [Test]
        public void Convert_FlagsRejectBitsNoMemberDeclares()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter"));

            Assert.AreEqual(Palette.None, new StringToEnumConverter<Palette>(Palette.None).Convert("8"));
        }
    }
}
