using System;
using NUnit.Framework;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Guards <see cref="TypeSelectorConstraintResolver"/>: the member-first resolution of a
    /// <see cref="TypeSelectorAttribute"/> string argument (a field/property named by the string supplies the
    /// constraint dynamically) with an assembly-qualified-name fallback, and the warnings surfaced when an
    /// argument resolves to nothing — the runtime mirror of analyzer rules AFT0006–AFT0008.
    /// </summary>
    [TestFixture]
    internal sealed class TypeSelectorConstraintResolverTests
    {
        private sealed class FakeSerializableType : ISerializableType
        {
            public Type BaseType => typeof(object);
            public Type Type { get; set; }
        }

        // The host's fields are read only through reflection (by the resolver under test), so the compiler cannot
        // see them being used — silence the resulting "assigned but never used" / "never used" warnings.
#pragma warning disable CS0169, CS0414, CS0649
        private class BaseHost
        {
            protected Type _inheritedType = typeof(short);
        }

        private sealed class Host : BaseHost
        {
            private Type _weaponType = typeof(int);
            private Type[] _weaponTypes = { typeof(int), typeof(long) };
            private string _typeName = typeof(int).AssemblyQualifiedName;
            private string[] _typeNames = { typeof(int).AssemblyQualifiedName, typeof(long).AssemblyQualifiedName };
            private ISerializableType _wrapper = new FakeSerializableType { Type = typeof(int) };
            private ISerializableType[] _wrappers =
            {
                new FakeSerializableType { Type = typeof(int) },
                new FakeSerializableType { Type = typeof(long) }
            };
            private ISerializableType _unsetWrapper = new FakeSerializableType { Type = null };
            private Type _nullType = null;
            private int _count = 3;
            private static Type _staticType = typeof(int);

            private Type WeaponProperty => typeof(int);
        }
#pragma warning restore CS0169, CS0414, CS0649

        private static TypeSelectorConstraintResolver.Result Resolve(string name, object target) =>
            TypeSelectorConstraintResolver.Resolve(target, new[] { name });

        [Test]
        public void TypeFieldMember_ReturnsItsValue()
        {
            var result = Resolve("_weaponType", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int) }, result.Types);
        }

        [Test]
        public void TypePropertyMember_ReturnsItsValue()
        {
            var result = Resolve("WeaponProperty", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int) }, result.Types);
        }

        [Test]
        public void TypeArrayMember_ReturnsEveryElement()
        {
            var result = Resolve("_weaponTypes", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int), typeof(long) }, result.Types);
        }

        [Test]
        public void StringMember_ResolvesTheAssemblyQualifiedName()
        {
            var result = Resolve("_typeName", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int) }, result.Types);
        }

        [Test]
        public void StringArrayMember_ResolvesEveryName()
        {
            var result = Resolve("_typeNames", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int), typeof(long) }, result.Types);
        }

        [Test]
        public void SerializableTypeMember_ReturnsTheWrappedType()
        {
            var result = Resolve("_wrapper", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int) }, result.Types);
        }

        [Test]
        public void SerializableTypeArrayMember_ReturnsEveryWrappedType()
        {
            var result = Resolve("_wrappers", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int), typeof(long) }, result.Types);
        }

        [Test]
        public void InheritedMember_IsResolvedThroughTheBaseType()
        {
            var result = Resolve("_inheritedType", new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(short) }, result.Types);
        }

        [Test]
        public void AssemblyQualifiedName_NotAMember_ResolvesAsAType()
        {
            var result = Resolve(typeof(int).AssemblyQualifiedName, new Host());
            CollectionAssert.AreEquivalent(new[] { typeof(int) }, result.Types);
        }

        [Test]
        public void UnknownIdentifier_AddsAWarningAndNoType()
        {
            var result = Resolve("_missing", new Host());

            CollectionAssert.IsEmpty(result.Types);
            Assert.AreEqual(1, result.Warnings.Count);
        }

        [Test]
        public void UnsuitableMember_AddsAWarningAndNoType()
        {
            var result = Resolve("_count", new Host());

            CollectionAssert.IsEmpty(result.Types);
            Assert.AreEqual(1, result.Warnings.Count);
        }

        [Test]
        public void StaticMember_IsInvisibleToTheInstanceLookup_AndWarns()
        {
            var result = Resolve("_staticType", new Host());

            CollectionAssert.IsEmpty(result.Types);
            Assert.AreEqual(1, result.Warnings.Count);
        }

        [Test]
        public void SuitableMemberWithNullValue_AddsNoWarning()
        {
            var result = Resolve("_nullType", new Host());

            CollectionAssert.IsEmpty(result.Types);
            Assert.AreEqual(0, result.Warnings.Count);
        }

        [Test]
        public void SuitableWrapperWithNullType_AddsNoWarning()
        {
            var result = Resolve("_unsetWrapper", new Host());

            CollectionAssert.IsEmpty(result.Types);
            Assert.AreEqual(0, result.Warnings.Count);
        }

        [Test]
        public void BlankName_IsSkippedWithoutWarning()
        {
            var result = TypeSelectorConstraintResolver.Resolve(new Host(), new[] { "", "   " });

            CollectionAssert.IsEmpty(result.Types);
            Assert.AreEqual(0, result.Warnings.Count);
        }

        [Test]
        public void MultipleArguments_AppendEveryResolvedConstraint()
        {
            var result = TypeSelectorConstraintResolver.Resolve(
                new Host(), new[] { "_weaponType", "_inheritedType" });

            CollectionAssert.AreEquivalent(new[] { typeof(int), typeof(short) }, result.Types);
            Assert.AreEqual(0, result.Warnings.Count);
        }

        // The live-constraint contract the drawers depend on: Resolve holds no state between calls, so
        // each call reads the member's CURRENT value — that is what lets the pickers re-resolve while
        // the inspector is open.
        [Test]
        public void MemberValueChange_IsReflectedByTheNextResolve()
        {
            var host = new MutableHost { Constraint = typeof(int) };
            var before = Resolve(nameof(MutableHost.Constraint), host);

            host.Constraint = typeof(long);
            var after = Resolve(nameof(MutableHost.Constraint), host);

            CollectionAssert.AreEquivalent(new[] { typeof(int) }, before.Types);
            CollectionAssert.AreEquivalent(new[] { typeof(long) }, after.Types);
        }

        private sealed class MutableHost
        {
            public Type Constraint { get; set; }
        }
    }
}
