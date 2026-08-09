using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.Scripting.APIUpdating;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    // A namespace move recorded by [MovedFrom]: the stored identity keeps the class name, only the namespace differs.
    [Serializable]
    [MovedFrom(true, sourceNamespace: "Aspid.FastTools.SerializeReferences.Editors.Tests.Legacy")]
    internal sealed class MovedNamespacePistol
    {
        [SerializeField] private int _damage;
    }

    // Two types claiming the same recorded old class name: neither claim is authoritative.
    [Serializable]
    [MovedFrom(false, null, null, "AmbiguousOldSword")]
    internal sealed class AmbiguousNewSwordA { }

    [Serializable]
    [MovedFrom(false, null, null, "AmbiguousOldSword")]
    internal sealed class AmbiguousNewSwordB { }

    /// <summary>
    /// Coverage for <see cref="SerializeReferenceMovedFromResolver"/> — the authoritative-rename resolver behind the
    /// migration classification (breakage report entries and the Project References bulk <b>Migrate all</b>):
    /// a recorded namespace move and a recorded class rename each resolve to their single declaring type, while an
    /// ambiguous claim, an unknown identity or a mismatched assembly resolve to nothing.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceMovedFromResolverTests
    {
        private static string Assembly => typeof(MovedNamespacePistol).Assembly.GetName().Name;

        private static string Namespace => typeof(MovedNamespacePistol).Namespace;

        [Test]
        public void TryResolve_RecordedNamespaceMove_FindsTheSingleTarget()
        {
            var stored = new ManagedTypeName(Assembly, Namespace + ".Legacy", nameof(MovedNamespacePistol));

            Assert.IsTrue(SerializeReferenceMovedFromResolver.TryResolve(stored, out var target),
                "A stored identity matching a recorded [MovedFrom] namespace move must resolve.");
            Assert.AreEqual(typeof(MovedNamespacePistol), target);
        }

        [Test]
        public void TryResolve_RecordedClassRename_FindsTheSingleTarget()
        {
            // Reuses the RenamedRanged fixture declared for the ranking tests: [MovedFrom(..., "OldRenamedRanged")].
            var stored = new ManagedTypeName(Assembly, Namespace, "OldRenamedRanged");

            Assert.IsTrue(SerializeReferenceMovedFromResolver.TryResolve(stored, out var target),
                "A stored identity matching a recorded [MovedFrom] class rename must resolve.");
            Assert.AreEqual(typeof(RenamedRanged), target);
        }

        [Test]
        public void TryResolve_AmbiguousClaims_RefusesToPick()
        {
            var stored = new ManagedTypeName(Assembly, Namespace, "AmbiguousOldSword");

            Assert.IsFalse(SerializeReferenceMovedFromResolver.TryResolve(stored, out var target),
                "Two types claiming the same old identity make the rename non-authoritative.");
            Assert.IsNull(target);
        }

        [Test]
        public void TryResolve_UnknownIdentity_ReturnsFalse()
        {
            var stored = new ManagedTypeName(Assembly, Namespace, "NoSuchOldTypeAnywhere");

            Assert.IsFalse(SerializeReferenceMovedFromResolver.TryResolve(stored, out _));
        }

        [Test]
        public void TryResolve_ClosedGenericIdentity_NeverMigrates()
        {
            // TypeCache yields definitions and the eligibility filter excludes generic parameters, so the only
            // possible claimant for a stored closed-generic identity is an arity-stripped name collision — a guess
            // the resolver must leave to the scored Smart Fix path.
            var stored = new ManagedTypeName(Assembly, Namespace, "OldRenamedRanged`1[[System.Single, mscorlib]]");

            Assert.IsFalse(SerializeReferenceMovedFromResolver.TryResolve(stored, out _));
        }

        [Test]
        public void TryResolve_AssemblyMismatch_ReturnsFalse()
        {
            // The fixture's [MovedFrom] records no assembly move, so the old assembly is the declaring one — a stored
            // identity from a different assembly must not match.
            var stored = new ManagedTypeName("Some.Other.Assembly", Namespace, "OldRenamedRanged");

            Assert.IsFalse(SerializeReferenceMovedFromResolver.TryResolve(stored, out _));
        }
    }
}
