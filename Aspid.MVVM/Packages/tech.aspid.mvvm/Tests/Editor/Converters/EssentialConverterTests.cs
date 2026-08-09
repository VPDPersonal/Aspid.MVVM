using System;
using UnityEngine;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    internal enum Weather
    {
        Clear,

        [InspectorName("Light rain")]
        Rain,

        Snow,
    }

    [Flags]
    internal enum Damage
    {
        None = 0,
        Fire = 1,
        Ice = 2,
        Both = Fire | Ice,
    }

    /// <summary>
    /// Coverage for the first catalogue wave — the converters whose absence forced a View concern
    /// into the ViewModel: negation, two-value selection, enum mapping, index lookup.
    /// </summary>
    [TestFixture]
    internal sealed class EssentialConverterTests
    {
        [Test]
        public void BoolInvert_Negates()
        {
            Assert.IsFalse(new BoolInvertConverter().Convert(true));
            Assert.IsTrue(new BoolInvertConverter().Convert(false));
        }

        [Test]
        public void BoolInvert_IsItsOwnInverse() =>
            Assert.IsTrue(new BoolInvertConverter().ConvertBack(new BoolInvertConverter().Convert(true)));

        [Test]
        public void BoolToValue_PicksTheAuthoredBranch()
        {
            var converter = new BoolToValueConverter<Color>(Color.green, Color.red);

            Assert.AreEqual(Color.green, converter.Convert(true));
            Assert.AreEqual(Color.red, converter.Convert(false));
        }

        [TestCase(LogicOperation.And, true, true, true)]
        [TestCase(LogicOperation.And, true, false, false)]
        [TestCase(LogicOperation.Or, false, true, true)]
        [TestCase(LogicOperation.Or, false, false, false)]
        [TestCase(LogicOperation.Xor, true, true, false)]
        [TestCase(LogicOperation.Xor, true, false, true)]
        [TestCase(LogicOperation.Nand, true, true, false)]
        [TestCase(LogicOperation.Nor, false, false, true)]
        [TestCase(LogicOperation.Xnor, true, true, true)]
        public void BoolLogic_CombinesWithTheOperand(
            LogicOperation operation,
            bool value,
            bool operand,
            bool expected) =>
            Assert.AreEqual(expected, new BoolLogicConverter(operation, operand).Convert(value));

        [TestCase(StringMatch.Equals, "abc", true)]
        [TestCase(StringMatch.Equals, "abcd", false)]
        [TestCase(StringMatch.Contains, "xabcx", true)]
        [TestCase(StringMatch.StartsWith, "abcx", true)]
        [TestCase(StringMatch.StartsWith, "xabc", false)]
        [TestCase(StringMatch.EndsWith, "xabc", true)]
        public void StringMatch_TestsAgainstTheAuthoredText(StringMatch match, string value, bool expected) =>
            Assert.AreEqual(expected, new StringMatchToBoolConverter(match, "abc").Convert(value));

        [Test]
        public void StringMatch_IgnoresCaseByDefault() =>
            Assert.IsTrue(new StringMatchToBoolConverter(StringMatch.Equals, "abc").Convert("ABC"));

        [Test]
        public void StringMatch_HonoursCaseWhenAsked() =>
            Assert.IsFalse(
                new StringMatchToBoolConverter(StringMatch.Equals, "abc", ignoreCase: false).Convert("ABC"));

        [Test]
        public void StringMatch_NullMatchesNothing() =>
            Assert.IsFalse(new StringMatchToBoolConverter(StringMatch.Equals, "abc").Convert(null));

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("   ", true)]
        [TestCase("\t", true)]
        [TestCase("abc", false)]
        public void StringWhiteSpace_CountsBlankAsEmpty(string value, bool expected) =>
            Assert.AreEqual(expected, new StringWhiteSpaceToBoolConverter().Convert(value));

        [Test]
        public void EnumToValue_MapsAndFallsBack()
        {
            var converter = new EnumToValueConverter<Weather, Color>(
                new[]
                {
                    new EnumEntry<Weather, Color> { Key = Weather.Clear, Value = Color.yellow },
                    new EnumEntry<Weather, Color> { Key = Weather.Rain, Value = Color.blue },
                },
                fallback: Color.gray);

            Assert.AreEqual(Color.yellow, converter.Convert(Weather.Clear));
            Assert.AreEqual(Color.blue, converter.Convert(Weather.Rain));
            Assert.AreEqual(Color.gray, converter.Convert(Weather.Snow));
        }

        [Test]
        public void EnumToValue_EmptyMap_ReturnsTheFallback() =>
            Assert.AreEqual(
                Color.gray,
                new EnumToValueConverter<Weather, Color>(null, Color.gray).Convert(Weather.Clear));

        [TestCase(Weather.Rain, EnumMatch.Equals, true)]
        [TestCase(Weather.Snow, EnumMatch.Equals, false)]
        [TestCase(Weather.Snow, EnumMatch.NotEquals, true)]
        public void EnumToBool_TestsTheValue(Weather value, EnumMatch match, bool expected) =>
            Assert.AreEqual(expected, new EnumToBoolConverter<Weather>(Weather.Rain, match).Convert(value));

        [TestCase(Damage.Both, EnumMatch.HasAllFlags, true)]
        [TestCase(Damage.Fire, EnumMatch.HasAllFlags, false)]
        [TestCase(Damage.Fire, EnumMatch.HasAnyFlag, true)]
        [TestCase(Damage.None, EnumMatch.HasAnyFlag, false)]
        public void EnumToBool_TestsFlags(Damage value, EnumMatch match, bool expected) =>
            Assert.AreEqual(expected, new EnumToBoolConverter<Damage>(Damage.Both, match).Convert(value));

        [Test]
        public void EnumToInt_RoundTrips()
        {
            var converter = new EnumToIntConverter<Weather>();

            Assert.AreEqual(2, converter.Convert(Weather.Snow));
            Assert.AreEqual(Weather.Snow, converter.ConvertBack(2));
        }

        [Test]
        public void EnumToString_UsesTheMemberName() =>
            Assert.AreEqual("Rain", new EnumToStringConverter<Weather>().Convert(Weather.Rain));

        [Test]
        public void EnumToString_UsesTheInspectorNameWhenAsked() =>
            Assert.AreEqual(
                "Light rain",
                new EnumToStringConverter<Weather>(EnumNameSource.InspectorName).Convert(Weather.Rain));

        [Test]
        public void EnumToString_FallsBackToTheMemberNameWithoutAnAttribute() =>
            Assert.AreEqual(
                "Snow",
                new EnumToStringConverter<Weather>(EnumNameSource.InspectorName).Convert(Weather.Snow));

        [Test]
        public void EnumToString_UndeclaredValue_ReturnsTheFallback() =>
            Assert.AreEqual("?", new EnumToStringConverter<Weather>(EnumNameSource.Name, "?").Convert((Weather)99));

        [TestCase(0, "a")]
        [TestCase(2, "c")]
        [TestCase(-1, "a")]
        [TestCase(5, "c")]
        public void IndexToValue_ClampsByDefault(int index, string expected) =>
            Assert.AreEqual(expected, Index(IndexMode.Clamp).Convert(index));

        [TestCase(3, "a")]
        [TestCase(4, "b")]
        [TestCase(-1, "c")]
        public void IndexToValue_WrapsWhenAsked(int index, string expected) =>
            Assert.AreEqual(expected, Index(IndexMode.Wrap).Convert(index));

        [TestCase(3)]
        [TestCase(-1)]
        public void IndexToValue_FallsBackWhenAsked(int index) =>
            Assert.AreEqual("?", Index(IndexMode.Fallback).Convert(index));

        [Test]
        public void IndexToValue_EmptyArray_ReturnsTheFallback() =>
            Assert.AreEqual("?", new IndexToValueConverter<string>(null, IndexMode.Clamp, "?").Convert(0));

        [Test]
        public void NullCoalesce_SubstitutesTheFallback()
        {
            Assert.AreEqual("x", new NullCoalesceConverter<string>("x").Convert(null));
            Assert.AreEqual("abc", new NullCoalesceConverter<string>("x").Convert("abc"));
        }

        [Test]
        public void EqualityToBool_ComparesWithTheOperand()
        {
            Assert.IsTrue(new EqualityToBoolConverter<string>("abc").Convert("abc"));
            Assert.IsFalse(new EqualityToBoolConverter<string>("abc").Convert("xyz"));
            Assert.IsTrue(new EqualityToBoolConverter<string>("abc", isInvert: true).Convert("xyz"));
        }

        // `is null` reports false for a destroyed object, so a crosshair bound to a dead target
        // would stay on screen. Only Unity's overloaded == catches it.
        [Test]
        public void UnityObjectNullToBool_CountsADestroyedObjectAsMissing()
        {
            var gameObject = new GameObject(nameof(UnityObjectNullToBool_CountsADestroyedObjectAsMissing));
            var converter = new UnityObjectNullToBoolConverter();

            Assert.IsFalse(converter.Convert(gameObject));

            UnityEngine.Object.DestroyImmediate(gameObject);

            Assert.IsTrue(converter.Convert(gameObject));
        }

        [Test]
        public void UnityObjectNullToBool_UnassignedIsMissing() =>
            Assert.IsTrue(new UnityObjectNullToBoolConverter().Convert(null));

        private static IndexToValueConverter<string> Index(IndexMode mode) =>
            new(new[] { "a", "b", "c" }, mode, "?");
    }
}
