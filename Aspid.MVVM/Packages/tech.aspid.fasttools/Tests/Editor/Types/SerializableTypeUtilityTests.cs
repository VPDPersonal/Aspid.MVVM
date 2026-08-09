using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Guards the <see cref="ISerializableType"/> contract shared by both wrappers and the
    /// <see cref="SerializableTypeUtility"/> field detection built on it: any wrapper (plain, generic,
    /// or an array / <see cref="List{T}"/> element) must be recognized and expose the right
    /// <see cref="ISerializableType.BaseType"/> — <see cref="object"/> when unconstrained, <c>T</c> otherwise.
    /// </summary>
    [TestFixture]
    internal sealed class SerializableTypeUtilityTests
    {
        [Test]
        public void BaseType_IsObject_OnTheUnconstrainedWrapper()
        {
            ISerializableType wrapper = new SerializableType();
            Assert.AreEqual(typeof(object), wrapper.BaseType);
        }

        [Test]
        public void BaseType_IsTheGenericArgument_OnTheConstrainedWrapper()
        {
            ISerializableType wrapper = new SerializableType<Exception>();
            Assert.AreEqual(typeof(Exception), wrapper.BaseType);
        }

        [Test]
        public void Type_IsNull_OnACodeConstructedWrapper_ViaTheInterface()
        {
            Assert.IsNull(((ISerializableType)new SerializableType()).Type);
            Assert.IsNull(((ISerializableType)new SerializableType<Exception>()).Type);
        }

        [TestCase(typeof(SerializableType))]
        [TestCase(typeof(SerializableType<Exception>))]
        [TestCase(typeof(SerializableType[]))]
        [TestCase(typeof(SerializableType<Exception>[]))]
        [TestCase(typeof(List<SerializableType>))]
        [TestCase(typeof(List<SerializableType<Exception>>))]
        public void IsSerializableTypeField_Matches_WrapperShapes(Type fieldType) =>
            Assert.IsTrue(SerializableTypeUtility.IsSerializableTypeField(fieldType));

        [TestCase(typeof(string))]
        [TestCase(typeof(Type))]
        [TestCase(typeof(List<string>))]
        [TestCase(typeof(int[]))]
        public void IsSerializableTypeField_Rejects_NonWrapperTypes(Type fieldType) =>
            Assert.IsFalse(SerializableTypeUtility.IsSerializableTypeField(fieldType));

        [TestCase(typeof(SerializableType), typeof(object))]
        [TestCase(typeof(SerializableType<Exception>), typeof(Exception))]
        [TestCase(typeof(SerializableType<Exception>[]), typeof(Exception))]
        [TestCase(typeof(List<SerializableType<Exception>>), typeof(Exception))]
        public void TryGetBaseType_ResolvesTheWrapperConstraint(Type fieldType, Type expected)
        {
            Assert.IsTrue(SerializableTypeUtility.TryGetBaseType(fieldType, out var baseType));
            Assert.AreEqual(expected, baseType);
        }

        [Test]
        public void TryGetBaseType_Fails_OnANonWrapperType()
        {
            Assert.IsFalse(SerializableTypeUtility.TryGetBaseType(typeof(string), out _));
        }
    }
}
