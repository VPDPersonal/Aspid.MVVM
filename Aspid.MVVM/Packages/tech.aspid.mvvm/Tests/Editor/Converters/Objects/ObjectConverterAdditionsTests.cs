using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the corners of the object converters that the catalogue-wide fixtures leave
    /// alone: the reference-equality option on <see cref="EqualityToBoolConverter{T}"/>, what
    /// <see cref="NullCoalesceConverter{T}"/> makes of a destroyed <see cref="UnityEngine.Object"/>,
    /// and the undeclared-<see cref="IndexMode"/> branch of <see cref="IndexToValueConverter{T}"/>.
    /// </summary>
    /// <remarks>
    /// These are the mistakes that only surface once the project is running. A reference-equality
    /// option silently reduced to value equality (or the reverse) the day a value object grows an
    /// <c>Equals</c>; a boxed struct making a reference comparison answer false for a value against
    /// itself; and a destroyed asset reaching a binder because the emptiness check was written as
    /// <c>??</c> or <c>is null</c> — both of which see a managed reference that outlived its native
    /// object. Every equality expectation below was confirmed by executing it in an Editor rather
    /// than read off the XML docs.
    /// </remarks>
    [TestFixture]
    internal sealed class ObjectConverterAdditionsTests
    {
        private readonly List<GameObject> _created = new();

        [TearDown]
        public void DestroyCreatedObjects()
        {
            // Unity's implicit bool is false for the ones a test already destroyed on purpose.
            foreach (var gameObject in _created)
            {
                if (gameObject) UnityEngine.Object.DestroyImmediate(gameObject);
            }

            _created.Clear();
        }

        // The whole reason the option exists: two loadouts with the same numbers are Equals-equal,
        // while the View is asking whether this is the very instance the player picked.
        [TestCase(false, true)]
        [TestCase(true, false)]
        public void EqualityToBool_EqualButDistinctInstance_MatchesOnlyByValue(bool referenceEquality, bool expected)
        {
            var picked = new Loadout(damage: 7, weight: 3);
            var identical = new Loadout(damage: 7, weight: 3);

            var converter = new EqualityToBoolConverter<Loadout>(picked, referenceEquality: referenceEquality);

            Assert.AreEqual(expected, converter.Convert(identical));
        }

        // The half of the contract that keeps the option honest — switching it on must not stop the
        // selected instance from matching itself.
        [TestCase(false)]
        [TestCase(true)]
        public void EqualityToBool_SameInstance_MatchesUnderEitherComparison(bool referenceEquality)
        {
            var picked = new Loadout(damage: 7, weight: 3);

            var converter = new EqualityToBoolConverter<Loadout>(picked, referenceEquality: referenceEquality);

            Assert.IsTrue(converter.Convert(picked));
        }

        // Invert applies to whichever comparison was chosen, not instead of choosing one; an
        // implementation that inverted before picking the mode would pass one of these and fail the
        // other.
        [Test]
        public void EqualityToBool_ReferenceEqualityInverted_ReportsOnlyTheOtherInstance()
        {
            var picked = new Loadout(damage: 7, weight: 3);
            var identical = new Loadout(damage: 7, weight: 3);

            var converter = new EqualityToBoolConverter<Loadout>(picked, isInvert: true, referenceEquality: true);

            Assert.IsTrue(converter.Convert(identical));
            Assert.IsFalse(converter.Convert(picked));
        }

        // Boxing hands ReferenceEquals two fresh objects, so an unguarded reference comparison would
        // answer false for every pair of value types — 5 against 5 included. The converter drops the
        // option for a value type instead, which is the only reading that is ever useful.
        [TestCase(5, 5, true)]
        [TestCase(5, 6, false)]
        public void EqualityToBool_ValueType_IgnoresReferenceEquality(int operand, int value, bool expected)
        {
            var converter = new EqualityToBoolConverter<int>(operand, referenceEquality: true);

            Assert.AreEqual(expected, converter.Convert(value));
        }

        // An operand authored in the inspector arrives as an interned literal; a caption assembled at
        // runtime does not. Identical text, different instances — the trap that makes the option a
        // poor default for strings.
        [Test]
        public void EqualityToBool_ReferenceEquality_RejectsAnEqualStringBuiltAtRuntime()
        {
            var runtimeBuilt = new string(new[] { 'f', 'i', 'r', 'e' });

            Assert.IsTrue(new EqualityToBoolConverter<string>("fire").Convert(runtimeBuilt));
            Assert.IsFalse(new EqualityToBoolConverter<string>("fire", referenceEquality: true).Convert(runtimeBuilt));
        }

        // ReferenceEquals(null, null) is true, so an unassigned operand still answers "yes, empty"
        // under reference equality rather than degenerating to false.
        [Test]
        public void EqualityToBool_ReferenceEquality_TreatsTwoUnassignedOperandsAsMatching() =>
            Assert.IsTrue(new EqualityToBoolConverter<Loadout>(null, referenceEquality: true).Convert(null));

        // The asymmetry the class docs warn about, confirmed by execution: EqualityComparer settles a
        // null operand with a plain reference check, so a destroyed object — which Unity's own == calls
        // null — does not match an empty operand the way a genuinely unassigned one does. Anyone
        // wiring "is it missing?" through this converter gets the wrong answer for a destroyed asset.
        [Test]
        public void EqualityToBool_NullOperand_DoesNotMatchADestroyedObject()
        {
            var target = NewGameObject(nameof(EqualityToBool_NullOperand_DoesNotMatchADestroyedObject));
            var converter = new EqualityToBoolConverter<GameObject>(null);

            Assert.IsTrue(converter.Convert(null));

            UnityEngine.Object.DestroyImmediate(target);

            Assert.IsFalse(converter.Convert(target));
        }

        [Test]
        public void NullCoalesce_DestroyedUnityObject_ReturnsTheFallback()
        {
            var icon = NewGameObject(nameof(NullCoalesce_DestroyedUnityObject_ReturnsTheFallback));
            var fallback = NewGameObject("Fallback");
            var converter = new NullCoalesceConverter<GameObject>(fallback);

            // While it is alive it has to pass straight through, or the fallback would be permanent.
            Assert.AreSame(icon, converter.Convert(icon));

            UnityEngine.Object.DestroyImmediate(icon);

            // The managed reference is still alive at this point, so `??` and `is null` would both
            // hand the destroyed object to the binder. Only Unity's overloaded == catches it.
            Assert.AreSame(fallback, converter.Convert(icon));
        }

        // The Unity check is a runtime type test on the value, not a constraint on T, so a converter
        // declared over object still catches a destroyed asset flowing through an object-typed binding.
        [Test]
        public void NullCoalesce_DestroyedUnityObjectUnderObjectOfT_ReturnsTheFallback()
        {
            var icon = NewGameObject(nameof(NullCoalesce_DestroyedUnityObjectUnderObjectOfT_ReturnsTheFallback));
            var converter = new NullCoalesceConverter<object>("placeholder");

            Assert.AreSame(icon, converter.Convert(icon));

            UnityEngine.Object.DestroyImmediate(icon);

            Assert.AreEqual("placeholder", converter.Convert(icon));
        }

        // The fallback is returned exactly as authored — it is never put through the same check — so a
        // destroyed fallback reaches the binder instead of being turned back into null. Worth pinning:
        // the converter guarantees "not the bound value", not "not destroyed".
        [Test]
        public void NullCoalesce_DestroyedFallback_IsStillReturned()
        {
            var fallback = NewGameObject(nameof(NullCoalesce_DestroyedFallback_IsStillReturned));
            var converter = new NullCoalesceConverter<GameObject>(fallback);

            UnityEngine.Object.DestroyImmediate(fallback);

            Assert.AreSame(fallback, converter.Convert(null));
        }

        // A serialized asset can hold an IndexMode that the enum no longer declares, after a rename or
        // a removal. The switch has no fall-through, so that asset throws rather than quietly clamping.
        [Test]
        public void IndexToValue_UndeclaredMode_ThrowsOnAnOutOfRangeIndex()
        {
            var converter = new IndexToValueConverter<string>(new[] { "a", "b" }, (IndexMode)99, "?");

            Assert.Throws<ArgumentOutOfRangeException>(() => converter.Convert(5));
        }

        // ...and only there. An index inside the array returns before the switch is reached, so a
        // broken mode stays invisible until the first out-of-range value arrives — which is why the
        // throw above is a runtime surprise rather than an import-time one.
        [Test]
        public void IndexToValue_UndeclaredMode_IsNotReachedForAnIndexInsideTheArray()
        {
            var converter = new IndexToValueConverter<string>(new[] { "a", "b" }, (IndexMode)99, "?");

            Assert.AreEqual("b", converter.Convert(1));
        }

        // Hidden and unsaved so that a failing assertion cannot leave a stray object in the editor scene.
        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name) { hideFlags = HideFlags.HideAndDontSave };
            _created.Add(gameObject);

            return gameObject;
        }

        // A value object of exactly the kind the reference-equality option exists for: same numbers
        // means Equals-equal, and says nothing about being the same instance.
        private sealed class Loadout
        {
            private readonly int _damage;
            private readonly int _weight;

            public Loadout(int damage, int weight)
            {
                _damage = damage;
                _weight = weight;
            }

            public override bool Equals(object obj) =>
                obj is Loadout other && other._damage == _damage && other._weight == _weight;

            public override int GetHashCode() =>
                (_damage * 397) ^ _weight;
        }
    }
}
