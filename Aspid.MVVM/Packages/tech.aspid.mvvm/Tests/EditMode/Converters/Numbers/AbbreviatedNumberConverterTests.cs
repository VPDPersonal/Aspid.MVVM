using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="AbbreviatedNumberConverter"/> — the tier suffixes, the trailing-zero
    /// trim, the threshold, the sign, and the empty suffix list.
    /// </summary>
    [TestFixture]
    public sealed class AbbreviatedNumberConverterTests
    {
        [TestCase(999d, "999")]
        [TestCase(1500d, "1.5K")]
        [TestCase(1234567d, "1.23M")]
        [TestCase(2_500_000_000d, "2.5B")]
        [TestCase(1_000_000_000_000d, "1T")]
        public void Convert_PicksTheTierAndTrimsZeros(double value, string expected) =>
            Assert.AreEqual(expected, new AbbreviatedNumberConverter(2).Convert(value));

        [Test]
        public void Convert_NegativeValue_KeepsTheSignInFront() =>
            Assert.AreEqual("-1.5K", new AbbreviatedNumberConverter(2).Convert(-1500d));

        [Test]
        public void Convert_NegativeValue_AtTheMillionsTier_KeepsTheSign() =>
            Assert.AreEqual("-1.23M", new AbbreviatedNumberConverter(2).Convert(-1234567d));

        // The decimals decide the tier as much as the magnitude does: 999 999 is below a million, but
        // written with two decimals it reads as one, and "1000.00K" is not a number anyone writes.
        [TestCase(999_999d, 2, "1M")]
        [TestCase(999_999d, 3, "999.999K")]
        public void Convert_RoundingUpToTheNextThousand_MovesToTheTierAbove(
            double value,
            int decimals,
            string expected) =>
            Assert.AreEqual(expected, new AbbreviatedNumberConverter(decimals).Convert(value));

        // The largest tier is the last suffix, so a magnitude past it stays there rather than
        // reaching for a suffix the array does not have.
        [Test]
        public void Convert_BeyondTheLargestSuffix_StaysOnIt() =>
            Assert.AreEqual("1000T", new AbbreviatedNumberConverter(0).Convert(1_000_000_000_000_000d));

        // _trimTrailingZeros has no constructor parameter, so turning it off is only reachable
        // through the Inspector — set here the same way.
        [Test]
        public void Convert_WithoutTrimmingZeros_KeepsThem()
        {
            var converter = new AbbreviatedNumberConverter(2);
            SetField(converter, "_trimTrailingZeros", false);

            Assert.AreEqual("1.50K", converter.Convert(1500d));
        }

        [Test]
        public void Convert_CustomSuffixes_AreUsedInsteadOfTheDefaults() =>
            Assert.AreEqual("1.5k", new AbbreviatedNumberConverter(2, new[] { "", "k", "m" }).Convert(1500d));

        // An empty list has nothing to abbreviate with, so the misconfiguration is reported and the
        // number is written in full.
        [Test]
        public void Convert_EmptySuffixes_ReportsAndWritesTheNumberInFull()
        {
            LogAssert.Expect(LogType.Error, new Regex("AbbreviatedNumberConverter.*no suffixes"));

            Assert.AreEqual(
                "1234567",
                new AbbreviatedNumberConverter(2, System.Array.Empty<string>()).Convert(1234567d));
        }

        // The int, long and float overloads are explicit, so they are reached through the interface
        // rather than the class.
        [Test]
        public void Convert_IntInput_PicksTheSameTierAsTheDouble() =>
            Assert.AreEqual(
                "1.5K",
                ((IConverter<int, string>)new AbbreviatedNumberConverter(2)).Convert(1500));

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field!.SetValue(target, value);

            // Unity reads the object again after an Inspector edit, which is where a converter
            // holding a cache built from its settings drops it.
            if (target is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }
    }
}
