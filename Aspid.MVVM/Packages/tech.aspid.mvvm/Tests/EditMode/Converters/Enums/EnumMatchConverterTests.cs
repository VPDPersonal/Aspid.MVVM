using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumMatchConverter{TEnum}"/> — the four <see cref="EnumMatch"/> tests,
    /// the invert flag and the undeclared-match fallback.
    /// </summary>
    [TestFixture]
    public sealed class EnumMatchConverterTests
    {
        private const Hazard FireAndIce = Hazard.Fire | Hazard.Ice;

        [Test]
        public void Convert_DefaultConstructed_TestsEqualityAgainstTheEnumsDefaultValue()
        {
            var converter = new EnumMatchConverter<Hazard>();

            Assert.IsTrue(converter.Convert(Hazard.None));
            Assert.IsFalse(converter.Convert(Hazard.Fire));
        }

        [TestCase(Hazard.Fire, EnumMatch.Equal, true)]
        [TestCase(Hazard.Ice, EnumMatch.Equal, false)]
        [TestCase(Hazard.Ice, EnumMatch.NotEquals, true)]
        public void Convert_Equality_TestsTheValue(Hazard value, EnumMatch match, bool expected) =>
            Assert.AreEqual(expected, new EnumMatchConverter<Hazard>(Hazard.Fire, match).Convert(value));

        [TestCase(FireAndIce, EnumMatch.HasAllFlags, true)]
        [TestCase(Hazard.Fire, EnumMatch.HasAllFlags, false)]
        [TestCase(Hazard.Fire, EnumMatch.HasAnyFlag, true)]
        [TestCase(Hazard.Shock, EnumMatch.HasAnyFlag, false)]
        public void Convert_Flags_TestsTheFlagsAgainstTheTarget(Hazard value, EnumMatch match, bool expected) =>
            Assert.AreEqual(expected, new EnumMatchConverter<Hazard>(FireAndIce, match).Convert(value));

        [Test]
        public void Convert_IsInvert_FlipsTheResult() =>
            Assert.IsFalse(new EnumMatchConverter<Hazard>(Hazard.Fire, isInvert: true).Convert(Hazard.Fire));

        // Inverting an answer no test produced would turn the refusal into a true, so the fallback
        // is returned without inverting it, whichever way isInvert is set.
        [TestCase(false)]
        [TestCase(true)]
        public void Convert_UndeclaredMatch_ReturnsTheFallbackWithoutInverting(bool isInvert)
        {
            ExpectUndeclaredMatchError();

            Assert.IsFalse(new EnumMatchConverter<Hazard>(Hazard.Fire, (EnumMatch)99, isInvert).Convert(Hazard.Fire));
        }

        private static void ExpectUndeclaredMatchError() =>
            LogAssert.Expect(LogType.Error, new Regex("EnumMatchConverter.*not a declared EnumMatch"));
    }
}
