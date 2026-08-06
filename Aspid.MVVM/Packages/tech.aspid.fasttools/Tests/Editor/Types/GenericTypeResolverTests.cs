using NUnit.Framework;

namespace Aspid.FastTools.Types.Editors.Tests
{
    // Test-only generic hierarchy: a structure-constrained box and an unconstrained variant. 
    internal interface IResolverThing { }

    [System.Serializable]
    internal struct ResolverStruct : IResolverThing { }

    [System.Serializable]
    internal sealed class ResolverClass : IResolverThing { }

    internal sealed class ResolverNoDefaultCtor : IResolverThing
    {
        public ResolverNoDefaultCtor(int _) { }
    }

    internal sealed class StructBox<T>
        where T : struct, IResolverThing { }

    internal sealed class ClassBox<T>
        where T : class { }

    internal sealed class CtorBox<T>
        where T : new() { }

    internal sealed class OpenBox<T> { }

    /// <summary>
    /// Coverage for <see cref="GenericTypeResolver"/> — pure reflection logic that gates which closed generic types the
    /// picker may instantiate. A regression here lets the picker construct a managed reference Unity silently nulls.
    /// </summary>
    [TestFixture]
    internal sealed class GenericTypeResolverTests
    {
        [Test]
        public void SatisfiesSpecialConstraints_StructConstraint_AcceptsValueType_RejectsClass()
        {
            var parameter = typeof(StructBox<>).GetGenericArguments()[0];

            Assert.IsTrue(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverStruct)),
                "A value type must satisfy a 'struct' constraint.");
            Assert.IsFalse(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverClass)),
                "A reference type must not satisfy a 'struct' constraint.");
        }

        [Test]
        public void SatisfiesSpecialConstraints_ClassConstraint_AcceptsClass_RejectsValueType()
        {
            var parameter = typeof(ClassBox<>).GetGenericArguments()[0];

            Assert.IsTrue(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverClass)),
                "A reference type must satisfy a 'class' constraint.");
            Assert.IsFalse(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverStruct)),
                "A value type must not satisfy a 'class' constraint.");
        }

        [Test]
        public void SatisfiesSpecialConstraints_NewConstraint_RequiresAParameterlessConstructor()
        {
            var parameter = typeof(CtorBox<>).GetGenericArguments()[0];

            Assert.IsTrue(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverClass)),
                "A class with a public parameterless constructor must satisfy 'new()'.");
            Assert.IsTrue(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverStruct)),
                "A value type always satisfies 'new()'.");
            Assert.IsFalse(GenericTypeResolver.SatisfiesSpecialConstraints(parameter, typeof(ResolverNoDefaultCtor)),
                "A class without a parameterless constructor must not satisfy 'new()'.");
        }

        [Test]
        public void GetConstraintBaseTypes_ReturnsExplicitConstraint()
        {
            var parameter = typeof(StructBox<>).GetGenericArguments()[0];
            CollectionAssert.Contains(GenericTypeResolver.GetConstraintBaseTypes(parameter), typeof(IResolverThing));
        }

        [Test]
        public void GetConstraintBaseTypes_Unconstrained_FallsBackToObject()
        {
            var parameter = typeof(OpenBox<>).GetGenericArguments()[0];
            CollectionAssert.AreEqual(new[] { typeof(object) }, GenericTypeResolver.GetConstraintBaseTypes(parameter));
        }

        [Test]
        public void TryConstruct_ValidArgument_ClosesType()
        {
            Assert.IsTrue(GenericTypeResolver.TryConstruct(
                typeof(StructBox<>), new[] { typeof(ResolverStruct) }, fieldTypes: null, out var closed, out var error));
            Assert.AreEqual(typeof(StructBox<ResolverStruct>), closed);
            Assert.IsNull(error);
        }

        [Test]
        public void TryConstruct_ConstraintViolated_FailsWithError()
        {
            Assert.IsFalse(GenericTypeResolver.TryConstruct(
                typeof(StructBox<>), new[] { typeof(ResolverClass) }, fieldTypes: null, out var closed, out var error));
            Assert.IsNull(closed);
            Assert.IsNotNull(error, "A violated struct constraint must report an error.");
        }

        [Test]
        public void TryConstruct_NotAssignableToField_Fails()
        {
            // OpenBox<int> does not implement IResolverThing, so it is rejected against that field type.
            Assert.IsFalse(GenericTypeResolver.TryConstruct(
                typeof(OpenBox<>), new[] { typeof(int) }, new[] { typeof(IResolverThing) }, out var closed, out var error));
            Assert.IsNull(closed);
            Assert.IsNotNull(error);
        }

        [Test]
        public void TryInferFromFieldType_ClosedGenericField_InfersArguments()
        {
            Assert.IsTrue(GenericTypeResolver.TryInferFromFieldType(typeof(OpenBox<int>), typeof(OpenBox<>), out var closed));
            Assert.AreEqual(typeof(OpenBox<int>), closed);
        }

        [Test]
        public void TryInferFromFieldType_NonGenericField_Fails()
        {
            Assert.IsFalse(GenericTypeResolver.TryInferFromFieldType(typeof(IResolverThing), typeof(OpenBox<>), out var closed));
            Assert.IsNull(closed);
        }

        [Test]
        public void TryInferFromFieldType_UnrelatedDefinition_Fails()
        {
            Assert.IsFalse(GenericTypeResolver.TryInferFromFieldType(typeof(OpenBox<int>), typeof(StructBox<>), out var closed));
            Assert.IsNull(closed);
        }

        [Test]
        public void IsAssignableToFieldTypes_ChecksEveryMeaningfulEntry()
        {
            Assert.IsTrue(GenericTypeResolver.IsAssignableToFieldTypes(typeof(ResolverClass), new[] { typeof(IResolverThing) }));
            Assert.IsFalse(GenericTypeResolver.IsAssignableToFieldTypes(typeof(OpenBox<int>), new[] { typeof(IResolverThing) }));
        }

        [Test]
        public void IsAssignableToFieldTypes_NullsAndObject_ImposeNoRestriction()
        {
            Assert.IsTrue(GenericTypeResolver.IsAssignableToFieldTypes(typeof(ResolverClass), fieldTypes: null));
            Assert.IsTrue(GenericTypeResolver.IsAssignableToFieldTypes(typeof(ResolverClass), new[] { null, typeof(object) }));
            Assert.IsFalse(GenericTypeResolver.IsAssignableToFieldTypes(null, fieldTypes: null),
                "No closed type can never pass the guard, whatever the field types.");
        }
    }
}
