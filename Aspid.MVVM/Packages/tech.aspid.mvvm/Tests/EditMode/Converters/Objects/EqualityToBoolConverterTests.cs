using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EqualityToBoolConverter{T}"/>'s reference-equality option, including
    /// the value-type and Unity-object corners it interacts with.
    /// </summary>
    [TestFixture]
    public sealed class EqualityToBoolConverterTests : SceneFixture
    {
        [Test]
        public void Convert_ComparesWithTheOperand()
        {
            Assert.IsTrue(new EqualityToBoolConverter<string>("abc").Convert("abc"));
            Assert.IsFalse(new EqualityToBoolConverter<string>("abc").Convert("xyz"));
            Assert.IsTrue(new EqualityToBoolConverter<string>("abc", isInvert: true).Convert("xyz"));
        }

        [Test]
        public void NullOperand_PlainReferenceIsNotNull()
        {
            Assert.IsFalse(new EqualityToBoolConverter<object>(null).Convert("abc"));
            Assert.IsTrue(new EqualityToBoolConverter<object>(null).Convert(null));
        }

        [Test]
        public void NullOperand_InvertFlipsTheResult() =>
            Assert.IsFalse(new EqualityToBoolConverter<object>(null, isInvert: true).Convert(null));

        // The whole reason the option exists: two loadouts with the same numbers are Equals-equal,
        // while the View is asking whether this is the very instance the player picked.
        [TestCase(false, true)]
        [TestCase(true, false)]
        public void EqualButDistinctInstance_MatchesOnlyByValue(bool referenceEquality, bool expected)
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
        public void SameInstance_MatchesUnderEitherComparison(bool referenceEquality)
        {
            var picked = new Loadout(damage: 7, weight: 3);

            var converter = new EqualityToBoolConverter<Loadout>(picked, referenceEquality: referenceEquality);

            Assert.IsTrue(converter.Convert(picked));
        }

        // Invert applies to whichever comparison was chosen, not instead of choosing one; an
        // implementation that inverted before picking the mode would pass one of these and fail the
        // other.
        [Test]
        public void ReferenceEqualityInverted_ReportsOnlyTheOtherInstance()
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
        public void ValueType_IgnoresReferenceEquality(int operand, int value, bool expected)
        {
            var converter = new EqualityToBoolConverter<int>(operand, referenceEquality: true);

            Assert.AreEqual(expected, converter.Convert(value));
        }

        // An operand authored in the inspector arrives as an interned literal; a caption assembled at
        // runtime does not. Identical text, different instances — the trap that makes the option a
        // poor default for strings.
        [Test]
        public void ReferenceEquality_RejectsAnEqualStringBuiltAtRuntime()
        {
            var runtimeBuilt = new string(new[] { 'f', 'i', 'r', 'e' });

            Assert.IsTrue(new EqualityToBoolConverter<string>("fire").Convert(runtimeBuilt));
            Assert.IsFalse(new EqualityToBoolConverter<string>("fire", referenceEquality: true).Convert(runtimeBuilt));
        }

        // ReferenceEquals(null, null) is true, so an unassigned operand still answers "yes, empty"
        // under reference equality rather than degenerating to false.
        [Test]
        public void ReferenceEquality_TreatsTwoUnassignedOperandsAsMatching() =>
            Assert.IsTrue(new EqualityToBoolConverter<Loadout>(null, referenceEquality: true).Convert(null));

        // The Unity caveat the class docs commit to: under value equality a null side goes through
        // Unity's overloaded ==, so a destroyed object matches an empty operand and the converter
        // doubles as an is-null test. Reference equality stays raw, so the same destroyed object
        // does not match there.
        [Test]
        public void NullOperand_MatchesADestroyedObjectUnderValueEquality()
        {
            var target = Spawn(nameof(NullOperand_MatchesADestroyedObjectUnderValueEquality));
            var byValue = new EqualityToBoolConverter<GameObject>(null);
            var byReference = new EqualityToBoolConverter<GameObject>(null, referenceEquality: true);

            Assert.IsTrue(byValue.Convert(null));
            Assert.IsFalse(byValue.Convert(target));

            Destroy(target);

            Assert.IsTrue(byValue.Convert(target));
            Assert.IsFalse(byReference.Convert(target));
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
