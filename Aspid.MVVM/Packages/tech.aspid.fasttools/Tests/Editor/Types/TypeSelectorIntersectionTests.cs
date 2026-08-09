using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Locks the base-type intersection the SerializableType + <c>[TypeSelector]</c> picker relies on. The drawer
    /// passes <c>[genericArgument, ...attributeTypes]</c> to <see cref="HierarchyBuilder"/>, which lists only types
    /// assignable to <b>all</b> base entries — so appending the generic argument <c>T</c> narrows the attribute's
    /// set to <c>T</c>'s subtypes rather than widening it.
    /// </summary>
    [TestFixture]
    internal sealed class TypeSelectorIntersectionTests
    {
        private interface IWeapon { }
        private interface IMelee : IWeapon { }
        private sealed class Sword : IMelee { }
        private sealed class Bow : IWeapon { }

        private static List<string> ConcreteLeafNames(params Type[] baseTypes)
        {
            var root = HierarchyBuilder.Build(baseTypes, TypeAllow.None, includeNoneOption: false);
            var names = new List<string>();
            Collect(root, names);
            return names;
        }

        private static void Collect(TreeNode node, List<string> names)
        {
            // FlattenSingleChildChain merges a lone leaf into its namespace chain ("Ns.Sub.Sword"),
            // so keep only the trailing segment of the display name.
            if (node.IsType) names.Add(node.DisplayName[(node.DisplayName.LastIndexOf('.') + 1)..]);
            foreach (var child in node.Children) Collect(child, names);
        }

        [Test]
        public void SingleBase_ListsEveryConcreteSubtype()
        {
            var names = ConcreteLeafNames(typeof(IWeapon));

            Assert.Contains(nameof(Sword), names, "Sword is an IWeapon and must appear.");
            Assert.Contains(nameof(Bow), names, "Bow is an IWeapon and must appear.");
        }

        [Test]
        public void TwoBases_ListOnlyTypesAssignableToBoth()
        {
            // IWeapon ∩ IMelee → only the IMelee branch (Sword). Bow is an IWeapon but not an IMelee, so the
            // intersection must exclude it — this is exactly what SerializableType<IWeapon> + [TypeSelector(typeof(IMelee))] does.
            var names = ConcreteLeafNames(typeof(IWeapon), typeof(IMelee));

            Assert.Contains(nameof(Sword), names, "Sword is assignable to both IWeapon and IMelee.");
            Assert.IsFalse(names.Contains(nameof(Bow)), "Bow is not an IMelee, so the intersection must exclude it.");
        }
    }
}
