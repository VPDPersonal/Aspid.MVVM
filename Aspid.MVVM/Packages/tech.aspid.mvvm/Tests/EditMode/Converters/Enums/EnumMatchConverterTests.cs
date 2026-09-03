using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EnumMatchConverter{TEnum}"/> — the four <see cref="EnumMatchMode"/> tests,
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

        [TestCase(Hazard.Fire, EnumMatchMode.Equal, true)]
        [TestCase(Hazard.Ice, EnumMatchMode.Equal, false)]
        [TestCase(Hazard.Ice, EnumMatchMode.NotEqual, true)]
        public void Convert_Equality_TestsTheValue(Hazard value, EnumMatchMode match, bool expected) =>
            Assert.AreEqual(expected, new EnumMatchConverter<Hazard>(Hazard.Fire, match).Convert(value));

        [TestCase(FireAndIce, EnumMatchMode.HasAllFlags, true)]
        [TestCase(Hazard.Fire, EnumMatchMode.HasAllFlags, false)]
        [TestCase(Hazard.Fire, EnumMatchMode.HasAnyFlag, true)]
        [TestCase(Hazard.Shock, EnumMatchMode.HasAnyFlag, false)]
        public void Convert_Flags_TestsTheFlagsAgainstTheTarget(Hazard value, EnumMatchMode match, bool expected) =>
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

            Assert.IsFalse(new EnumMatchConverter<Hazard>(Hazard.Fire, (EnumMatchMode)99, isInvert).Convert(Hazard.Fire));
        }

        private static void ExpectUndeclaredMatchError() =>
            LogAssert.Expect(LogType.Error, new Regex("EnumMatchConverter.*not a declared EnumMatchMode"));
    }
}
