using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using UnityEngine.TestTools.Constraints;
using Is = UnityEngine.TestTools.Constraints.Is;

namespace Aspid.FastTools.Enums.Tests
{
    internal enum Season
    {
        Winter,
        Spring,
        Summer,
        Autumn,
    }

    [Flags]
    internal enum Sides
    {
        None = 0,
        Left = 1,
        Right = 2,
        Both = Left | Right,
    }

    // Exercises the 64-bit branch of the typed variant's numeric key conversion.
    [Flags]
    internal enum BigFlags : long
    {
        None = 0,
        High = 1L << 40,
        Top = 1L << 62,
        All = High | Top,
    }

    // Exercises sign extension in the typed variant's numeric key conversion.
    internal enum SignedValues : short
    {
        Negative = -5,
        Positive = 7,
    }

    // Exercises the ulong branch of the numeric key conversion (top bit set —
    // the unchecked ulong→long reinterpretation must stay consistent on both sides).
    internal enum UnsignedValues : ulong
    {
        Zero = 0,
        Top = 1UL << 63,
    }

    /// <summary>
    /// Coverage for <see cref="EnumValues{TValue}"/> and <see cref="EnumValues{TEnum,TValue}"/>:
    /// lookup semantics (regular + <c>[Flags]</c>), the typed variant's auto-stamped
    /// <c>_enumType</c>, the serialized-layout compatibility between the two variants, and the
    /// degrade paths (unconfigured/unresolvable/non-enum enum type, unparseable entry keys).
    /// All data is written through <see cref="SerializedObject"/> so the real
    /// serialize/deserialize path (including <see cref="ISerializationCallbackReceiver"/>) runs.
    /// </summary>
    [TestFixture]
    internal sealed class EnumValuesTests
    {
        private sealed class Host : ScriptableObject
        {
            [SerializeField] private EnumValues<int> _untyped = new();
            [SerializeField] private EnumValues<Season, int> _seasons = new();
            [SerializeField] private EnumValues<Sides, int> _sides = new();
            [SerializeField] private EnumValues<BigFlags, int> _bigFlags = new();
            [SerializeField] private EnumValues<SignedValues, int> _signed = new();
            [SerializeField] private EnumValues<UnsignedValues, int> _unsigned = new();

            public EnumValues<int> Untyped => _untyped;

            public EnumValues<Season, int> Seasons => _seasons;

            public EnumValues<Sides, int> Sides => _sides;

            public EnumValues<BigFlags, int> BigFlags => _bigFlags;

            public EnumValues<SignedValues, int> Signed => _signed;

            public EnumValues<UnsignedValues, int> Unsigned => _unsigned;
        }

        private Host _host;

        [SetUp]
        public void SetUp() =>
            _host = ScriptableObject.CreateInstance<Host>();

        [TearDown]
        public void TearDown() =>
            UnityEngine.Object.DestroyImmediate(_host);

        private void AddEntry(string field, string key, int value, string enumType = null)
        {
            var serializedObject = new SerializedObject(_host);

            if (enumType is not null)
                serializedObject.FindProperty($"{field}._enumType").stringValue = enumType;

            var values = serializedObject.FindProperty($"{field}._values");
            values.arraySize++;

            var element = values.GetArrayElementAtIndex(values.arraySize - 1);
            element.FindPropertyRelative("_key").stringValue = key;
            element.FindPropertyRelative("_value").intValue = value;

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void SetDefaultValue(string field, int value)
        {
            var serializedObject = new SerializedObject(_host);
            serializedObject.FindProperty($"{field}._defaultValue").intValue = value;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void SetEnumType(string field, string enumType)
        {
            var serializedObject = new SerializedObject(_host);
            serializedObject.FindProperty($"{field}._enumType").stringValue = enumType;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        [Test]
        public void Typed_GetValue_ReturnsMappedValue()
        {
            AddEntry("_seasons", nameof(Season.Summer), 42);
            Assert.AreEqual(42, _host.Seasons.GetValue(Season.Summer));
        }

        [Test]
        public void Typed_GetValue_ReturnsDefaultWhenNoEntryMatches()
        {
            SetDefaultValue("_seasons", -1);
            AddEntry("_seasons", nameof(Season.Summer), 42);

            Assert.AreEqual(-1, _host.Seasons.GetValue(Season.Winter));
        }

        [Test]
        public void Typed_GetValue_DoesNotDependOnEnumTypeMirror()
        {
            // The typed variant must resolve TEnum from the generic argument alone — the
            // serialized _enumType mirror is editor-only sugar for the drawers. Clear it
            // (undoing the stamp AddEntry's serialize pass wrote) and the lookup must still work.
            AddEntry("_seasons", nameof(Season.Autumn), 7);

            var serializedObject = new SerializedObject(_host);
            serializedObject.FindProperty("_seasons._enumType").stringValue = string.Empty;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            Assert.AreEqual(7, _host.Seasons.GetValue(Season.Autumn));
        }

        [Test]
        public void Typed_Flags_ExactMatchWinsOverContainment()
        {
            AddEntry("_sides", nameof(Sides.Left), 1);
            AddEntry("_sides", nameof(Sides.Both), 3);

            Assert.AreEqual(3, _host.Sides.GetValue(Sides.Both));
        }

        [Test]
        public void Typed_Flags_ContainmentMatchesWhenNoExactEntry()
        {
            AddEntry("_sides", nameof(Sides.Left), 1);
            Assert.AreEqual(1, _host.Sides.GetValue(Sides.Both));
        }

        [Test]
        public void Typed_Flags_ZeroOnlyMatchesZero()
        {
            SetDefaultValue("_sides", -1);
            AddEntry("_sides", nameof(Sides.None), 100);

            Assert.AreEqual(100, _host.Sides.GetValue(Sides.None));
            Assert.AreEqual(-1, _host.Sides.GetValue(Sides.Left));
        }

        [Test]
        public void Typed_Equals_UsesFlagsSemantics()
        {
            Assert.IsTrue(_host.Sides.Equals(Sides.Both, Sides.Left));
            Assert.IsFalse(_host.Sides.Equals(Sides.Left, Sides.Both));
            Assert.IsFalse(_host.Sides.Equals(Sides.Left, Sides.None));
        }

        [Test]
        public void Typed_Enumerator_YieldsTypedConfiguredEntries()
        {
            AddEntry("_seasons", nameof(Season.Winter), 1);
            AddEntry("_seasons", nameof(Season.Spring), 2);

            var entries = _host.Seasons.ToArray();

            Assert.AreEqual(2, entries.Length);
            Assert.AreEqual(Season.Winter, entries[0].Key);
            Assert.AreEqual(1, entries[0].Value);
            Assert.AreEqual(Season.Spring, entries[1].Key);
            Assert.AreEqual(2, entries[1].Value);
        }

        [Test]
        public void Typed_Foreach_YieldsEntriesAndDoesNotAllocate()
        {
            AddEntry("_seasons", nameof(Season.Winter), 1);
            AddEntry("_seasons", nameof(Season.Spring), 2);

            var sum = 0;

            // Warm-up: the first pass lazily resolves the keys (which allocates) — the
            // steady-state foreach below must bind to the struct enumerator and stay clean.
            foreach (var entry in _host.Seasons)
                sum += entry.Value;

            Assert.AreEqual(3, sum);

            Assert.That(() =>
            {
                foreach (var entry in _host.Seasons)
                    sum += entry.Value;
            }, Is.Not.AllocatingGCMemory());

            Assert.AreEqual(6, sum);
        }

        [Test]
        public void Untyped_Foreach_YieldsEntriesAndDoesNotAllocate()
        {
            AddEntry("_untyped", nameof(Season.Winter), 1, typeof(Season).AssemblyQualifiedName);
            AddEntry("_untyped", nameof(Season.Spring), 2);

            var sum = 0;

            foreach (var entry in _host.Untyped)
                sum += entry.Value;

            Assert.AreEqual(3, sum);

            Assert.That(() =>
            {
                foreach (var entry in _host.Untyped)
                    sum += entry.Value;
            }, Is.Not.AllocatingGCMemory());

            Assert.AreEqual(6, sum);
        }

        [Test]
        public void Typed_OnBeforeSerialize_StampsEnumType()
        {
            // Creating a SerializedObject forces a serialize pass, which runs OnBeforeSerialize.
            var serializedObject = new SerializedObject(_host);

            Assert.AreEqual(
                typeof(Season).AssemblyQualifiedName,
                serializedObject.FindProperty("_seasons._enumType").stringValue);
        }

        [Test]
        public void Typed_LayoutMatchesUntypedFieldPaths()
        {
            // Serialized-layout compatibility contract: both variants expose the same
            // _enumType / _defaultValue / _values(_key, _value) property paths, so switching
            // a field between them migrates existing data.
            var serializedObject = new SerializedObject(_host);

            foreach (var field in new[] { "_untyped", "_seasons" })
            {
                Assert.IsNotNull(serializedObject.FindProperty($"{field}._enumType"), field);
                Assert.IsNotNull(serializedObject.FindProperty($"{field}._defaultValue"), field);
                Assert.IsNotNull(serializedObject.FindProperty($"{field}._values"), field);
            }
        }

        [Test]
        public void Typed_Flags_LongUnderlyingType_HighBitsSurviveLookup()
        {
            SetDefaultValue("_bigFlags", -1);
            AddEntry("_bigFlags", nameof(BigFlags.High), 1);
            AddEntry("_bigFlags", nameof(BigFlags.All), 3);

            Assert.AreEqual(3, _host.BigFlags.GetValue(BigFlags.All));
            Assert.AreEqual(1, _host.BigFlags.GetValue(BigFlags.High));
            Assert.AreEqual(-1, _host.BigFlags.GetValue(BigFlags.Top));
        }

        [Test]
        public void Typed_NegativeUnderlyingValue_MatchesItsEntry()
        {
            SetDefaultValue("_signed", -1);
            AddEntry("_signed", nameof(SignedValues.Negative), 5);

            Assert.AreEqual(5, _host.Signed.GetValue(SignedValues.Negative));
            Assert.AreEqual(-1, _host.Signed.GetValue(SignedValues.Positive));
        }

        [Test]
        public void Typed_ULongUnderlyingType_TopBitSurvivesLookup()
        {
            SetDefaultValue("_unsigned", -1);
            AddEntry("_unsigned", nameof(UnsignedValues.Top), 5);

            Assert.AreEqual(5, _host.Unsigned.GetValue(UnsignedValues.Top));
            Assert.AreEqual(-1, _host.Unsigned.GetValue(UnsignedValues.Zero));
        }

        [Test]
        public void Typed_Flags_ContainmentPrefersFirstSerializedEntry()
        {
            // Documented tie-break: with no exact entry, the first entry in serialized
            // order whose bits are contained in the lookup value wins.
            AddEntry("_sides", nameof(Sides.Right), 20);
            AddEntry("_sides", nameof(Sides.Left), 10);

            Assert.AreEqual(20, _host.Sides.GetValue(Sides.Both));
        }

        [Test]
        public void Typed_Equals_RegularEnum_UsesExactEquality()
        {
            Assert.IsTrue(_host.Seasons.Equals(Season.Winter, Season.Winter));
            Assert.IsFalse(_host.Seasons.Equals(Season.Winter, Season.Spring));
        }

        [Test]
        public void Typed_UnparseableKey_LogsErrorAndEntryNeverMatches()
        {
            SetDefaultValue("_seasons", -1);
            AddEntry("_seasons", nameof(Season.Winter), 1);
            AddEntry("_seasons", "Bogus", 99);

            LogAssert.Expect(LogType.Error, new Regex("Couldn't parse key 'Bogus'"));

            Assert.AreEqual(1, _host.Seasons.GetValue(Season.Winter));
            Assert.AreEqual(-1, _host.Seasons.GetValue(Season.Summer));

            // The unresolved entry must be skipped by the enumerator too.
            var entries = _host.Seasons.ToArray();

            Assert.AreEqual(1, entries.Length);
            Assert.AreEqual(Season.Winter, entries[0].Key);
        }

        [Test]
        public void Untyped_GetValue_ReturnsMappedValue()
        {
            AddEntry("_untyped", nameof(Season.Summer), 42, typeof(Season).AssemblyQualifiedName);
            Assert.AreEqual(42, _host.Untyped.GetValue(Season.Summer));
        }

        [Test]
        public void Untyped_GetValue_ReturnsDefaultWhenNoEntryMatches()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Summer), 42, typeof(Season).AssemblyQualifiedName);

            Assert.AreEqual(-1, _host.Untyped.GetValue(Season.Winter));
        }

        [Test]
        public void Untyped_GetValue_DifferentEnumType_NeverMatchesNumericCollision()
        {
            SetDefaultValue("_untyped", -1);
            // Season.Winter and Sides.None share the numeric value 0 — the type guard must
            // keep a lookup with the wrong enum type from matching it.
            AddEntry("_untyped", nameof(Season.Winter), 42, typeof(Season).AssemblyQualifiedName);

            Assert.AreEqual(42, _host.Untyped.GetValue(Season.Winter));
            Assert.AreEqual(-1, _host.Untyped.GetValue(Sides.None));
        }

        [Test]
        public void Untyped_GetValue_NullLookup_ReturnsDefault()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Winter), 42, typeof(Season).AssemblyQualifiedName);

            Assert.AreEqual(-1, _host.Untyped.GetValue(null));
        }

        [Test]
        public void Untyped_NoEnumTypeConfigured_WarnsAndReturnsDefault()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Winter), 42);

            LogAssert.Expect(LogType.Warning, new Regex("No enum type configured"));
            Assert.AreEqual(-1, _host.Untyped.GetValue(Season.Winter));
        }

        [Test]
        public void Untyped_UnresolvableEnumType_LogsErrorAndReturnsDefault()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Winter), 42, "Not.A.Real.Type, Fake.Assembly");

            LogAssert.Expect(LogType.Error, new Regex("Couldn't resolve enum type"));
            Assert.AreEqual(-1, _host.Untyped.GetValue(Season.Winter));
        }

        [Test]
        public void Untyped_NonEnumType_LogsErrorAndReturnsDefault()
        {
            // A type that resolves but is not an enum (e.g. an enum refactored into a
            // class/struct with the same name) must degrade like an unresolvable one
            // instead of throwing from Enum.TryParse on every lookup.
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Winter), 42, typeof(string).AssemblyQualifiedName);

            LogAssert.Expect(LogType.Error, new Regex("is not an enum"));
            Assert.AreEqual(-1, _host.Untyped.GetValue(Season.Winter));
        }

        [Test]
        public void Untyped_EnumTypeClearedAfterResolution_DegradesAndYieldsNothing()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Winter), 1, typeof(Season).AssemblyQualifiedName);

            // Resolve the keys once, then clear the type — degrading must reset the
            // previously resolved keys instead of letting them keep matching lookups
            // and being yielded by the enumerator.
            Assert.AreEqual(1, _host.Untyped.GetValue(Season.Winter));
            SetEnumType("_untyped", string.Empty);

            LogAssert.Expect(LogType.Warning, new Regex("No enum type configured"));

            Assert.AreEqual(-1, _host.Untyped.GetValue(Season.Winter));
            Assert.AreEqual(0, _host.Untyped.ToArray().Length);
        }

        [Test]
        public void Untyped_UnparseableKey_LogsErrorAndEntryNeverMatches()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(Season.Winter), 1, typeof(Season).AssemblyQualifiedName);
            AddEntry("_untyped", "Bogus", 99);

            LogAssert.Expect(LogType.Error, new Regex("Couldn't parse key 'Bogus'"));

            Assert.AreEqual(1, _host.Untyped.GetValue(Season.Winter));
            Assert.AreEqual(-1, _host.Untyped.GetValue(Season.Summer));

            // The unresolved entry must be skipped by the enumerator too.
            var entries = _host.Untyped.ToArray();

            Assert.AreEqual(1, entries.Length);
            Assert.AreEqual(Season.Winter, entries[0].Key);
        }

        [Test]
        public void Untyped_Equals_NullArgument_ReturnsFalse()
        {
            SetEnumType("_untyped", typeof(Season).AssemblyQualifiedName);

            Assert.IsFalse(_host.Untyped.Equals(null, Season.Winter));
            Assert.IsFalse(_host.Untyped.Equals(Season.Winter, null));
            Assert.IsFalse(_host.Untyped.Equals(null, null));
        }

        [Test]
        public void Untyped_Equals_DifferentEnumTypes_ReturnsFalse()
        {
            SetEnumType("_untyped", typeof(Season).AssemblyQualifiedName);

            // Season.Winter and Sides.None share the numeric value 0 — never equal anyway.
            Assert.IsFalse(_host.Untyped.Equals(Season.Winter, Sides.None));
        }

        [Test]
        public void Untyped_Equals_RegularEnum_UsesExactEquality()
        {
            SetEnumType("_untyped", typeof(Season).AssemblyQualifiedName);

            Assert.IsTrue(_host.Untyped.Equals(Season.Winter, Season.Winter));
            Assert.IsFalse(_host.Untyped.Equals(Season.Winter, Season.Spring));
        }

        [Test]
        public void Untyped_Equals_FlagsEnum_UsesFlagsSemantics()
        {
            // The flags semantics come from the configured enum type, not the arguments.
            SetEnumType("_untyped", typeof(Sides).AssemblyQualifiedName);

            Assert.IsTrue(_host.Untyped.Equals(Sides.Both, Sides.Left));
            Assert.IsFalse(_host.Untyped.Equals(Sides.Left, Sides.Both));
            Assert.IsFalse(_host.Untyped.Equals(Sides.Left, Sides.None));
        }

        [Test]
        public void Untyped_Flags_LongUnderlyingType_HighBitsSurviveLookup()
        {
            // Exercises the boxed EnumInfo.ToInt64 Int64 branch (the typed variant
            // goes through the separate EnumInfo<TEnum> converter).
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(BigFlags.High), 1, typeof(BigFlags).AssemblyQualifiedName);
            AddEntry("_untyped", nameof(BigFlags.All), 3);

            Assert.AreEqual(3, _host.Untyped.GetValue(BigFlags.All));
            Assert.AreEqual(1, _host.Untyped.GetValue(BigFlags.High));
            Assert.AreEqual(-1, _host.Untyped.GetValue(BigFlags.Top));
        }

        [Test]
        public void Untyped_NegativeUnderlyingValue_MatchesItsEntry()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(SignedValues.Negative), 5, typeof(SignedValues).AssemblyQualifiedName);

            Assert.AreEqual(5, _host.Untyped.GetValue(SignedValues.Negative));
            Assert.AreEqual(-1, _host.Untyped.GetValue(SignedValues.Positive));
        }

        [Test]
        public void Untyped_ULongUnderlyingType_TopBitSurvivesLookup()
        {
            SetDefaultValue("_untyped", -1);
            AddEntry("_untyped", nameof(UnsignedValues.Top), 5, typeof(UnsignedValues).AssemblyQualifiedName);

            Assert.AreEqual(5, _host.Untyped.GetValue(UnsignedValues.Top));
            Assert.AreEqual(-1, _host.Untyped.GetValue(UnsignedValues.Zero));
        }
    }
}
