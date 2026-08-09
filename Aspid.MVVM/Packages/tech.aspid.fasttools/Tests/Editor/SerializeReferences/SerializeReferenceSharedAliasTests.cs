using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    // A plain field plus a list, so alias paths cover both the "field" and the "field.Array.data[i]" shapes.
    internal sealed class SharedAliasTestObject : ScriptableObject
    {
        [SerializeReference] public ITestWeapon primary;
        [SerializeReference] public List<ITestWeapon> sidearms = new();
    }

    /// <summary>
    /// Covers the shared-reference notice's alias listing: <see cref="SerializeReferenceHelpers.GetSharedReferenceAliasPaths"/>
    /// (the OTHER members of a property's shared group, in document order),
    /// <see cref="SerializeReferenceHelpers.GetPropertyDisplayPath"/> (the inspector-style display form those paths are
    /// shown in) and <see cref="SerializeReferenceHelpers.BuildSharedReferenceDetail"/> (the tooltip both drawers share).
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceSharedAliasTests
    {
        // Builds the shared pair every aliasing test starts from: primary and sidearms[0] linked onto one rid, the
        // shared-reference cache invalidated. The caller owns the returned object (destroy in finally).
        private static SharedAliasTestObject CreateSharedPair(out SerializedObject serialized)
        {
            var obj = ScriptableObject.CreateInstance<SharedAliasTestObject>();
            serialized = new SerializedObject(obj);
            serialized.FindProperty("primary").managedReferenceValue = new TestSword { damage = 7 };
            serialized.FindProperty("sidearms").arraySize = 1;
            serialized.ApplyModifiedProperties();

            // Alias the list element onto the primary's instance (one shared rid across both paths).
            Assert.IsTrue(SerializeReferenceLinker.LinkTo(serialized.FindProperty("sidearms.Array.data[0]"), "primary"));
            serialized.Update();
            SerializeReferenceHelpers.InvalidateSharedReferenceCache();
            return obj;
        }

        [Test]
        public void GetPropertyDisplayPath_NicifiesFieldsAndListElements()
        {
            Assert.AreEqual("Primary", SerializeReferenceHelpers.GetPropertyDisplayPath("primary"));
            Assert.AreEqual("Sidearms › Element 1", SerializeReferenceHelpers.GetPropertyDisplayPath("sidearms.Array.data[1]"));
            Assert.AreEqual("Sidearms › Element 0 › On Hit Effect",
                SerializeReferenceHelpers.GetPropertyDisplayPath("sidearms.Array.data[0].onHitEffect"));
        }

        [Test]
        public void GetSharedReferenceAliasPaths_ListsTheOtherMembers_AndOnlyThem()
        {
            var obj = CreateSharedPair(out var serialized);
            try
            {
                var fromPrimary = SerializeReferenceHelpers.GetSharedReferenceAliasPaths(serialized.FindProperty("primary"));
                Assert.AreEqual(new[] { "sidearms.Array.data[0]" }, fromPrimary,
                    "The primary field's alias list must hold exactly the list element, not itself.");

                var fromElement = SerializeReferenceHelpers.GetSharedReferenceAliasPaths(
                    serialized.FindProperty("sidearms.Array.data[0]"));
                Assert.AreEqual(new[] { "primary" }, fromElement,
                    "The list element's alias list must hold exactly the primary field, not itself.");
            }
            finally
            {
                Object.DestroyImmediate(obj);
            }
        }

        [Test]
        public void GetSharedReferenceAliasPaths_EmptyWhenNotShared()
        {
            var obj = ScriptableObject.CreateInstance<SharedAliasTestObject>();
            try
            {
                var serialized = new SerializedObject(obj);
                serialized.FindProperty("primary").managedReferenceValue = new TestSword();
                serialized.ApplyModifiedProperties();
                serialized.Update();
                SerializeReferenceHelpers.InvalidateSharedReferenceCache();

                Assert.IsEmpty(SerializeReferenceHelpers.GetSharedReferenceAliasPaths(serialized.FindProperty("primary")),
                    "A reference used by a single field is not part of any shared group.");
            }
            finally
            {
                Object.DestroyImmediate(obj);
            }
        }

        [Test]
        public void BuildSharedReferenceDetail_ListsAliasesByDisplayPath()
        {
            var obj = CreateSharedPair(out var serialized);
            try
            {
                var detail = SerializeReferenceHelpers.BuildSharedReferenceDetail(serialized.FindProperty("primary"));

                // Structural assertions only — the surrounding tooltip copy is free to change without breaking this
                // test; what must hold is WHICH paths the detail lists and in what form.
                StringAssert.Contains("Sidearms › Element 0", detail,
                    "The alias must be listed by its inspector-style display path, not the raw property path.");
                StringAssert.DoesNotContain("Primary", detail,
                    "The queried field itself is not an alias — only the other members of the pair are listed.");
            }
            finally
            {
                Object.DestroyImmediate(obj);
            }
        }
    }
}
