using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Coverage for the two data-preservation copiers behind type switches and Make-unique:
    /// <list type="bullet">
    /// <item><see cref="SerializeReferenceHelpers.CreateInstancePreservingData"/> carries nested
    /// <c>[SerializeReference]</c> children ACROSS a type switch by reference — JsonUtility alone drops them,
    /// which silently reset every nested reference before this coverage existed;</item>
    /// <item><see cref="SerializeReferenceHelpers.CloneManagedReferenceGraph"/> deep-copies for Make-unique /
    /// de-alias: children become independent, internal aliasing topology survives, cycles terminate.</item>
    /// </list>
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceCloneTests
    {
        private interface IPart { }

        [Serializable]
        private sealed class Gem : IPart
        {
            public int power;
        }

        [Serializable]
        private sealed class Weapon : IPart
        {
            public int damage;
            [SerializeReference] public IPart gem;
            [SerializeReference] public List<IPart> mods = new();
            [SerializeReference] public IPart[] attachments = Array.Empty<IPart>();
        }

        [Serializable]
        private sealed class OtherWeapon : IPart
        {
            public int damage;
            [SerializeReference] public IPart gem;
        }

        [Serializable]
        private sealed class Link : IPart
        {
            [SerializeReference] public IPart next;
        }

        [Test]
        public void CreateInstancePreservingData_TypeSwitch_CarriesNestedReferencesByIdentity()
        {
            var previous = new Weapon { damage = 7, gem = new Gem { power = 3 } };

            var switched = (OtherWeapon)SerializeReferenceHelpers.CreateInstancePreservingData(
                typeof(OtherWeapon), previous);

            Assert.AreEqual(7, switched.damage, "Plain shared fields must keep riding the JSON round-trip.");
            Assert.AreSame(previous.gem, switched.gem,
                "A nested [SerializeReference] shared by name must carry over as the same instance — the old " +
                "parent is discarded, so reuse is what preserves the child (JsonUtility alone drops it).");
        }

        [Test]
        public void CloneManagedReferenceGraph_MakesNestedChildrenIndependent()
        {
            var source = new Weapon
            {
                damage = 5,
                gem = new Gem { power = 9 },
                mods = new List<IPart> { new Gem { power = 1 } },
                attachments = new IPart[] { new Gem { power = 2 } },
            };

            var clone = (Weapon)SerializeReferenceHelpers.CloneManagedReferenceGraph(source);

            Assert.AreEqual(5, clone.damage);
            Assert.AreNotSame(source.gem, clone.gem, "Make-unique promises an independent nested instance.");
            Assert.AreEqual(9, ((Gem)clone.gem).power, "The independent copy must keep the child's data.");

            Assert.AreNotSame(source.mods, clone.mods, "A list of references must be rebuilt, never shared.");
            Assert.AreNotSame(source.mods[0], clone.mods[0]);
            Assert.AreEqual(1, ((Gem)clone.mods[0]).power);

            Assert.AreNotSame(source.attachments, clone.attachments, "An array of references must be rebuilt too.");
            Assert.AreNotSame(source.attachments[0], clone.attachments[0]);
            Assert.AreEqual(2, ((Gem)clone.attachments[0]).power);
        }

        [Test]
        public void CloneManagedReferenceGraph_PreservesInternalAliasing()
        {
            var shared = new Gem { power = 4 };
            var source = new Weapon { gem = shared, mods = new List<IPart> { shared } };

            var clone = (Weapon)SerializeReferenceHelpers.CloneManagedReferenceGraph(source);

            Assert.AreNotSame(shared, clone.gem, "The shared child itself must still be copied.");
            Assert.AreSame(clone.gem, clone.mods[0],
                "Two fields aliasing one nested instance must alias one copy — internal topology is data.");
        }

        [Test]
        public void CloneManagedReferenceGraph_TerminatesOnCycles()
        {
            var first = new Link();
            var second = new Link { next = first };
            first.next = second;

            var clone = (Link)SerializeReferenceHelpers.CloneManagedReferenceGraph(first);

            Assert.AreNotSame(first, clone);
            Assert.AreNotSame(second, clone.next);
            Assert.AreSame(clone, ((Link)clone.next).next,
                "A cyclic graph must clone into its own cycle instead of recursing forever.");
        }
    }
}
