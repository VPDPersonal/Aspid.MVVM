using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
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

        [Test]
        public void BoolToValue_ReadsTheAuthoredBranchBack()
        {
            var converter = new BoolToValueConverter<Color>(Color.green, Color.red);

            Assert.IsTrue(converter.ConvertBack(Color.green));
            Assert.IsFalse(converter.ConvertBack(Color.red));
        }

        [Test]
        public void BoolToValue_UnmatchedValue_ReturnsFallbackAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("one of the two authored values"));

            var converter = new BoolToValueConverter<Color>(Color.green, Color.red, convertBackFallback: true);

            Assert.IsTrue(converter.ConvertBack(Color.blue));
            Assert.IsTrue(converter.ConvertBack(Color.blue));
        }

        [Test]
        public void BoolToValue_BranchesAuthoredAlike_ReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("both branches hold"));

            var converter = new BoolToValueConverter<Color>(Color.green, Color.green, convertBackFallback: true);

            Assert.IsTrue(converter.ConvertBack(Color.green));
            Assert.IsTrue(converter.ConvertBack(Color.green));
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
        public void StringEmpty_CountsBlankAsEmptyWhenAsked(string value, bool expected) =>
            Assert.AreEqual(
                expected,
                new StringEmptyToBoolConverter(StringEmptiness.NullOrWhiteSpace).Convert(value));

        [TestCase(StringEmptiness.Null, null, true)]
        [TestCase(StringEmptiness.Null, "", false)]
        [TestCase(StringEmptiness.NullOrEmpty, "", true)]
        [TestCase(StringEmptiness.NullOrEmpty, "   ", false)]
        public void StringEmpty_HonoursTheConfiguredEmptiness(
            StringEmptiness emptiness,
            string value,
            bool expected) =>
            Assert.AreEqual(expected, new StringEmptyToBoolConverter(emptiness).Convert(value));

        [Test]
        public void StringEmpty_DefaultsToNullOrEmpty() =>
            Assert.IsTrue(new StringEmptyToBoolConverter().Convert(string.Empty));

        [Test]
        public void EnumToValue_MapsAndFallsBack()
        {
            var converter = new EnumToValueConverter<Weather, Color>(
                new EnumToValueConverter<Weather, Color>.Entry[]
                {
                    new(Weather.Clear, Color.yellow),
                    new(Weather.Rain, Color.blue),
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

        [TestCase(Weather.Rain, EnumMatch.Equal, true)]
        [TestCase(Weather.Snow, EnumMatch.Equal, false)]
        [TestCase(Weather.Snow, EnumMatch.NotEquals, true)]
        public void EnumMatch_TestsTheValue(Weather value, EnumMatch match, bool expected) =>
            Assert.AreEqual(expected, new EnumMatchConverter<Weather>(Weather.Rain, match).Convert(value));

        [TestCase(Damage.Both, EnumMatch.HasAllFlags, true)]
        [TestCase(Damage.Fire, EnumMatch.HasAllFlags, false)]
        [TestCase(Damage.Fire, EnumMatch.HasAnyFlag, true)]
        [TestCase(Damage.None, EnumMatch.HasAnyFlag, false)]
        public void EnumMatch_TestsFlags(Damage value, EnumMatch match, bool expected) =>
            Assert.AreEqual(expected, new EnumMatchConverter<Damage>(Damage.Both, match).Convert(value));

        [Test]
        public void EnumToNumber_RoundTrips()
        {
            var converter = new EnumToNumberConverter<Weather>();

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
        public void EnumToString_UndeclaredValue_ReturnsTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("EnumToStringConverter.*a declared member"));

            Assert.AreEqual("?", new EnumToStringConverter<Weather>(EnumNameSource.Name, "?").Convert((Weather)99));
        }

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
        public void IndexToValue_EmptyArray_ReportsAndReturnsTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("IndexToValueConverter.*no values are authored"));

            Assert.AreEqual("?", new IndexToValueConverter<string>(null, IndexMode.Clamp, "?").Convert(0));
        }

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
        // would stay on screen. Only Unity's overloaded == catches it — the null-operand form of
        // EqualityToBool makes that check.
        [Test]
        public void EqualityToBool_NullOperand_CountsADestroyedObjectAsNull()
        {
            var gameObject = new GameObject(nameof(EqualityToBool_NullOperand_CountsADestroyedObjectAsNull));
            var converter = new EqualityToBoolConverter<object>(null);

            Assert.IsFalse(converter.Convert(gameObject));

            UnityEngine.Object.DestroyImmediate(gameObject);

            Assert.IsTrue(converter.Convert(gameObject));
        }

        [Test]
        public void EqualityToBool_NullOperand_PlainReferenceIsNotNull()
        {
            Assert.IsFalse(new EqualityToBoolConverter<object>(null).Convert("abc"));
            Assert.IsTrue(new EqualityToBoolConverter<object>(null).Convert(null));
        }

        [Test]
        public void EqualityToBool_NullOperand_InvertFlipsTheResult() =>
            Assert.IsFalse(new EqualityToBoolConverter<object>(null, isInvert: true).Convert(null));

        private static IndexToValueConverter<string> Index(IndexMode mode) =>
            new(new[] { "a", "b", "c" }, mode, "?");
    }
}
