using System;
using NUnit.Framework;
using Aspid.FastTools.Editors;
using System.Collections.Generic;

namespace Aspid.FastTools.Types.Editors.Tests
{
    /// <summary>
    /// Guards <see cref="TypeExtensions.GetCollectionElementTypeOrSelf"/> — the collection unwrapping that
    /// <see cref="SerializableTypeUtility"/> and the drawers rely on to treat an array / <see cref="List{T}"/>
    /// field like its element.
    /// </summary>
    [TestFixture]
    internal sealed class TypeExtensionsTests
    {
        [TestCase(typeof(int[]), typeof(int))]
        [TestCase(typeof(List<string>), typeof(string))]
        [TestCase(typeof(SerializableType[]), typeof(SerializableType))]
        [TestCase(typeof(List<SerializableType<Exception>>), typeof(SerializableType<Exception>))]
        public void UnwrapsArrayAndListElements(Type fieldType, Type expected) =>
            Assert.AreEqual(expected, fieldType.GetCollectionElementTypeOrSelf());

        [TestCase(typeof(string))]
        [TestCase(typeof(SerializableType))]
        public void NonCollections_AreReturnedUnchanged(Type fieldType) =>
            Assert.AreEqual(fieldType, fieldType.GetCollectionElementTypeOrSelf());

        [Test]
        public void SingleArgumentGenericWrapper_IsNotMistakenForACollection() =>
            Assert.AreEqual(typeof(SerializableType<Exception>),
                typeof(SerializableType<Exception>).GetCollectionElementTypeOrSelf());
    }
}
