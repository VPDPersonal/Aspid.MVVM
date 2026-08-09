using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Coverage for the <see cref="TypeSelectorDisplayAttribute.Name"/> and
    /// <see cref="TypeSelectorDisplayAttribute.Group"/> overrides:
    /// <list type="bullet">
    /// <item>a custom name replaces the picker label and the dropdown caption while search keeps matching
    /// the real type name;</item>
    /// <item>an explicit group replaces the namespace placement, its path segments are normalized and
    /// shared between types;</item>
    /// <item>label collisions disambiguate with the assembly suffix as plain same-named types always did, and
    /// two types given the same custom name within one assembly fall back to the real type name.</item>
    /// </list>
    /// </summary>
    [TestFixture]
    internal sealed class TypeSelectorDisplayTests
    {
        private interface IDisplayProbe { }

        [TypeSelectorDisplay(Name = "Blade")]
        private sealed class NamedSword : IDisplayProbe { }

        [TypeSelectorDisplay(Group = "Combat/Melee")]
        private sealed class GroupedAxe : IDisplayProbe { }

        [TypeSelectorDisplay(Name = "Sniper", Group = " Combat / Ranged //")]
        private sealed class NamedGroupedRifle : IDisplayProbe { }

        [TypeSelectorDisplay(Group = " / ")]
        private sealed class BlankGroupClub : IDisplayProbe { }

        private sealed class PlainMace : IDisplayProbe { }

        [TypeSelectorDisplay(Name = "Twin")]
        private sealed class TwinFirst : IDisplayProbe { }

        [TypeSelectorDisplay(Name = "Twin")]
        private sealed class TwinSecond : IDisplayProbe { }

        private static TreeNode BuildHierarchy() =>
            HierarchyBuilder.Build(new[] { typeof(IDisplayProbe) }, TypeAllow.None, includeNoneOption: false);

        [Test]
        public void CustomName_ReplacesLabel_WhileSearchStillMatchesTheRealName()
        {
            var leaf = FindLeaf(BuildHierarchy(), typeof(NamedSword));

            Assert.AreEqual("Blade", leaf.DisplayName, "The picker label must be the Name override.");
            Assert.AreEqual("NamedSword", leaf.SearchName, "The real type name must survive for search.");
            Assert.IsTrue(leaf.MatchesFilter("blade"), "Search must match the custom name.");
            Assert.IsTrue(leaf.MatchesFilter("namedsword"), "Search must still match the real type name.");
        }

        [Test]
        public void Group_PlacesTheTypeUnderItsExplicitPath_InsteadOfTheNamespace()
        {
            var root = BuildHierarchy();
            var melee = FindChild(FindChild(root, "Combat"), "Melee");
            var leaf = melee.Children.Single(node => node.AssemblyQualifiedName == typeof(GroupedAxe).AssemblyQualifiedName);

            Assert.AreEqual("GroupedAxe", leaf.DisplayName, "Without a Name override the label stays the real name.");
            Assert.AreEqual("Combat/Melee/GroupedAxe", leaf.Caption, "The caption must spell the group path.");

            var occurrences = Leaves(root).Count(node =>
                node.AssemblyQualifiedName == typeof(GroupedAxe).AssemblyQualifiedName);
            Assert.AreEqual(1, occurrences, "A grouped type must not also appear under its namespace.");
        }

        [Test]
        public void Group_SegmentsAreNormalized_AndSharedBetweenTypes()
        {
            var root = BuildHierarchy();

            var combats = root.Children.Where(node => node.DisplayName == "Combat").ToList();
            Assert.AreEqual(1, combats.Count, "Both group paths must meet under a single 'Combat' node.");

            var sniper = FindChild(combats[0], "Ranged").Children
                .Single(node => node.AssemblyQualifiedName == typeof(NamedGroupedRifle).AssemblyQualifiedName);
            Assert.AreEqual("Sniper", sniper.DisplayName,
                "Whitespace and empty segments must be trimmed out of the declared path, keeping the Name override.");

            Assert.AreEqual(2, combats[0].TypeCount, "'Combat' must count the leaves of both of its branches.");
        }

        [Test]
        public void BlankGroup_FallsBackToTheNamespacePlacement()
        {
            var leaf = FindLeaf(BuildHierarchy(), typeof(BlankGroupClub));

            StringAssert.EndsWith(".BlankGroupClub", leaf.Caption,
                "A Group that normalizes to nothing must leave the type under its namespace.");
        }

        [Test]
        public void SameCustomName_InOneAssembly_FallsBackToTheRealTypeName()
        {
            var root = BuildHierarchy();

            var twins = Leaves(root).Where(node =>
                node.AssemblyQualifiedName == typeof(TwinFirst).AssemblyQualifiedName ||
                node.AssemblyQualifiedName == typeof(TwinSecond).AssemblyQualifiedName).ToList();

            Assert.AreEqual(2, twins.Count, "Both same-named types must keep their own row.");
            CollectionAssert.AreEquivalent(
                new[] { "Twin (TwinFirst)", "Twin (TwinSecond)" },
                twins.Select(node => node.DisplayName).ToList(),
                "The assembly suffix cannot split a custom-name collision within one assembly — the real type " +
                "name is the identity that still can.");
        }

        [Test]
        public void DropdownTitle_UsesTheCustomName_WhileTheTooltipKeepsTheRealIdentity()
        {
            Assert.AreEqual("Blade", TypeSelectorHelpers.GetTypeSelectorTitle(typeof(NamedSword)));
            Assert.AreEqual("PlainMace", TypeSelectorHelpers.GetTypeSelectorTitle(typeof(PlainMace)));

            StringAssert.Contains("NamedSword", TypeSelectorHelpers.GetTypeSelectorTooltip(typeof(NamedSword)),
                "The hover tooltip must keep revealing the real Namespace.Class identity under a custom label.");
        }

        private static IEnumerable<TreeNode> Leaves(TreeNode node)
        {
            if (node.IsType) yield return node;

            foreach (var child in node.Children)
            {
                foreach (var leaf in Leaves(child))
                    yield return leaf;
            }
        }

        private static TreeNode FindLeaf(TreeNode root, Type type) =>
            Leaves(root).Single(node => node.AssemblyQualifiedName == type.AssemblyQualifiedName);

        private static TreeNode FindChild(TreeNode parent, string displayName)
        {
            var child = parent.Children.SingleOrDefault(node => node.DisplayName == displayName);
            Assert.IsNotNull(child, $"Expected a single '{displayName}' node under '{parent.DisplayName}'.");
            return child;
        }
    }
}
