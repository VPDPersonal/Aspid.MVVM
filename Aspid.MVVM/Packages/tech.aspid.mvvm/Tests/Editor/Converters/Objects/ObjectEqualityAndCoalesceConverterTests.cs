using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the corners of the object converters that the catalogue-wide fixtures leave
    /// alone: the reference-equality option on <see cref="EqualityToBoolConverter{T}"/>, what
    /// <see cref="NullCoalesceConverter{T}"/> makes of a destroyed <see cref="UnityEngine.Object"/>,
    /// and the undeclared-<see cref="IndexMode"/> branch of <see cref="IndexToValueConverter{T}"/>.
    /// </summary>
    /// <remarks>
    /// The mistakes that only surface once the project is running: a reference-equality option reduced
    /// to value equality the day a value object grows an <c>Equals</c>; a boxed struct making a
    /// reference comparison answer false for a value against itself; and a destroyed asset reaching a
    /// binder because the emptiness check was written as <c>??</c> or <c>is null</c>. Every equality
    /// expectation below was confirmed by executing it in an Editor.
    /// </remarks>
    [TestFixture]
    internal sealed class ObjectEqualityAndCoalesceConverterTests
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

        // The Unity caveat the class docs commit to: under value equality a null side goes through
        // Unity's overloaded ==, so a destroyed object matches an empty operand and the converter
        // doubles as an is-null test. Reference equality stays raw, so the same destroyed object
        // does not match there.
        [Test]
        public void EqualityToBool_NullOperand_MatchesADestroyedObjectUnderValueEquality()
        {
            var target = NewGameObject(nameof(EqualityToBool_NullOperand_MatchesADestroyedObjectUnderValueEquality));
            var byValue = new EqualityToBoolConverter<GameObject>(null);
            var byReference = new EqualityToBoolConverter<GameObject>(null, referenceEquality: true);

            Assert.IsTrue(byValue.Convert(null));
            Assert.IsFalse(byValue.Convert(target));

            UnityEngine.Object.DestroyImmediate(target);

            Assert.IsTrue(byValue.Convert(target));
            Assert.IsFalse(byReference.Convert(target));
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

        // A fallback destroyed after the converter was built meets the same emptiness check the bound
        // value gets, and is reported — then returned exactly as authored rather than turned back into
        // null: the converter guarantees "not the bound value", not "not destroyed".
        [Test]
        public void NullCoalesce_DestroyedFallback_IsReportedAndStillReturned()
        {
            var fallback = NewGameObject(nameof(NullCoalesce_DestroyedFallback_IsReportedAndStillReturned));
            var converter = new NullCoalesceConverter<GameObject>(fallback);

            UnityEngine.Object.DestroyImmediate(fallback);
            LogAssert.Expect(LogType.Error, new Regex("fallback is missing or destroyed"));

            Assert.AreSame(fallback, converter.Convert(null));
        }

        // The constructor runs that same check rather than a plain ??, which would read a destroyed
        // object as a perfectly good fallback and postpone the complaint to the first conversion.
        [Test]
        public void NullCoalesce_DestroyedFallbackInTheConstructor_Throws()
        {
            var fallback = NewGameObject(nameof(NullCoalesce_DestroyedFallbackInTheConstructor_Throws));
            UnityEngine.Object.DestroyImmediate(fallback);

            Assert.Throws<ArgumentNullException>(() => new NullCoalesceConverter<GameObject>(fallback));
        }

        // An unassigned fallback reduces the converter to a no-op that forwards the very null it exists
        // to replace. The constructor rejects that shape, so it only ever arrives from the Inspector —
        // built here the way the type picker builds it. Reported on every conversion, not once: the
        // second call is what pins it.
        [Test]
        public void NullCoalesce_MissingFallback_IsReportedEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("fallback is missing or destroyed"));

            var converter = (NullCoalesceConverter<object>)Activator.CreateInstance(
                typeof(NullCoalesceConverter<object>), nonPublic: true);

            Assert.IsNull(converter.Convert(null));
            converter.Convert(null);
        }

        // A serialized asset can hold an IndexMode that the enum no longer declares, after a rename or
        // a removal. The switch has no fall-through, so that asset reports and answers with the
        // authored fallback rather than quietly clamping.
        [Test]
        public void IndexToValue_UndeclaredMode_ReportsAndUsesTheFallbackOnAnOutOfRangeIndex()
        {
            var converter = new IndexToValueConverter<string>(new[] { "a", "b" }, (IndexMode)99, "?");

            LogAssert.Expect(LogType.Error, new Regex("IndexToValueConverter.*not a declared"));

            Assert.AreEqual("?", converter.Convert(5));
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
