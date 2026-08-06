using System;
using NUnit.Framework;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Coverage for <see cref="SerializeReferenceRepairSuggestions.Rank"/> — the ranking engine behind the
    /// missing-type <b>Smart Fix</b> suggestion. Pins the three outcomes the bundled sample assets rely on: a
    /// same-named type in another namespace surfaces with the full field-shape bonus (MovedWeaponPreset.asset), a
    /// declared <c>[MovedFrom]</c> match takes the authoritative top score, and a Ghost-style identity with no
    /// plausible successor surfaces nothing (BrokenWeaponPreset.asset intentionally shows no suggestion).
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceRepairSuggestionTests
    {
        private static string Assembly => typeof(RelocatedRanged).Assembly.GetName().Name;

        private static string Namespace => typeof(RelocatedRanged).Namespace;

        [Test]
        public void Rank_SameNameInOtherNamespace_SurfacesWithFieldShapeBonus()
        {
            var stored = new ManagedTypeName(Assembly, Namespace + ".Legacy", nameof(RelocatedRanged));

            var ranked = SerializeReferenceRepairSuggestions.Rank(
                stored, new[] { "_damage", "_magazineSize" }, typeof(IRepairRankTarget));

            Assert.IsNotEmpty(ranked, "A same-named type in another namespace must surface as a Smart Fix candidate.");
            Assert.AreEqual(typeof(RelocatedRanged), ranked[0].Type);
            Assert.AreEqual("same type name", ranked[0].Reason);
            Assert.AreEqual(1f, ranked[0].Score, 1e-4f,
                "Full field-shape overlap must add the whole bonus on top of the same-name base score.");
        }

        [Test]
        public void Rank_DeclaredMovedFrom_TakesTopScore()
        {
            var stored = new ManagedTypeName(Assembly, Namespace, "OldRenamedRanged");

            var ranked = SerializeReferenceRepairSuggestions.Rank(
                stored, Array.Empty<string>(), typeof(IRepairRankTarget));

            Assert.IsNotEmpty(ranked, "A candidate whose [MovedFrom] records the stored identity must surface.");
            Assert.AreEqual(typeof(RenamedRanged), ranked[0].Type);
            Assert.AreEqual("declared [MovedFrom]", ranked[0].Reason);
            Assert.GreaterOrEqual(ranked[0].Score, 1f, "A declared rename is authoritative — the top base score.");
        }

        [Test]
        public void Rank_NoPlausibleSuccessor_SurfacesNothing()
        {
            // Levenshtein distance from every candidate is far above the near-miss bound, and no [MovedFrom] records
            // the identity — matching field names alone must never conjure a suggestion (bonus without base score).
            var stored = new ManagedTypeName(Assembly, Namespace, "GhostRanged");

            var ranked = SerializeReferenceRepairSuggestions.Rank(
                stored, new[] { "_damage", "_magazineSize" }, typeof(IRepairRankTarget));

            Assert.IsEmpty(ranked);
        }
    }
}
