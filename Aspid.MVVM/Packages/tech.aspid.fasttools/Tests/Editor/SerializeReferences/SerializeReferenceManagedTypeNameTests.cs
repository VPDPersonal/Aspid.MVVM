using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    // Top-level (namespace-scoped) types so the nested-identity test sees exactly "NestingOuter/NestingInner" rather than
    // the test-fixture class prepended to the chain.
    internal sealed class NestingOuter
    {
        internal sealed class NestingInner { }
    }

    // Top-level generic types so the open/closed identity test keys on "Modifier`1"/"Pair`2" rather than a nested name.
    internal sealed class GenericModifier<T> { }

    internal sealed class GenericPair<T, TU> { }

    /// <summary>
    /// Coverage for <see cref="ManagedTypeName"/> — the YAML type-identity builder used by every repair write. Pins the
    /// closed-generic <c>Name`N[[arg, asm]]</c> shape and the single-quote escaping Unity requires for class identities
    /// containing reserved characters, the write-side half of the generic round-trip (risk register #1).
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceManagedTypeNameTests
    {
        [Test]
        public void FromType_ClosedGeneric_BuildsBacktickName_AndQuotesYaml()
        {
            var name = ManagedTypeName.FromType(typeof(List<float>));

            StringAssert.Contains("List`1[[", name.Class, "A closed generic must use Unity's Name`N[[arg]] class shape.");
            StringAssert.Contains("System.Single", name.Class);
            StringAssert.Contains("{class: '", name.ToYamlType(),
                "A class identity with reserved chars ([ ] , { }) must be single-quoted in the inline mapping.");
        }

        [Test]
        public void ToYamlType_NonGeneric_IsNotQuoted()
        {
            var name = new ManagedTypeName("Asm", "Ns", "Pistol");
            Assert.AreEqual("{class: Pistol, ns: Ns, asm: Asm}", name.ToYamlType());
        }

        [Test]
        public void FromType_Null_IsEmpty() =>
            Assert.IsTrue(ManagedTypeName.FromType(null).IsEmpty);

        [Test]
        public void FromType_NestedType_JoinsDeclaringChainWithSlash()
        {
            // Unity stores nested managed-reference types as "Outer/Inner"; reflection's Type.Name is only the leaf, so
            // FromType must rebuild the declaring-type prefix or a repair to a nested type writes an unresolvable class.
            var name = ManagedTypeName.FromType(typeof(NestingOuter.NestingInner));
            Assert.AreEqual("NestingOuter/NestingInner", name.Class);
        }

        [Test]
        public void OpenTypeKey_OpenAndClosedGeneric_CollapseToSameKey()
        {
            // The delete guard reads a generic script's OPEN definition (Modifier`1[[T]]), while YAML stores each CLOSED
            // instantiation (Modifier`1[[System.Single, …]]) — they must reduce to the same open-generic key, or deleting
            // a generic [SerializeReference] type's script never warns (the closed StoredTypeKey would never match).
            var open = SerializeReferenceHelpers.OpenTypeKey(ManagedTypeName.FromType(typeof(GenericModifier<>)));
            var closedFloat = SerializeReferenceHelpers.OpenTypeKey(ManagedTypeName.FromType(typeof(GenericModifier<float>)));
            var closedInt = SerializeReferenceHelpers.OpenTypeKey(ManagedTypeName.FromType(typeof(GenericModifier<int>)));

            Assert.AreEqual(open, closedFloat, "The open definition and a closed instantiation must share one open-generic key.");
            Assert.AreEqual(open, closedInt, "Every closed instantiation of the same definition must share one open-generic key.");
            StringAssert.Contains("GenericModifier`1", open, "The open key keeps the backtick arity and drops the [[…]] argument expansion.");
            StringAssert.DoesNotContain("[", open, "The open key must drop the bracketed closed-argument expansion.");
        }

        [Test]
        public void OpenTypeKey_DifferentArity_DoNotCollapse()
        {
            // The backtick arity is retained, so a one-arg and a two-arg definition never share a key.
            var arityOne = SerializeReferenceHelpers.OpenTypeKey(ManagedTypeName.FromType(typeof(GenericModifier<>)));
            var arityTwo = SerializeReferenceHelpers.OpenTypeKey(ManagedTypeName.FromType(typeof(GenericPair<,>)));

            Assert.AreNotEqual(arityOne, arityTwo);
        }
    }
}
