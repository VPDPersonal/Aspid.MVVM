using System.IO;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Edge-case read coverage the happy-path tests miss: the <c>-2</c> null-sentinel contract of
    /// <see cref="SerializeReferenceYamlEditor.TryReadReferenceId"/>, the single-document anchor fallback and
    /// multi-document write confinement of the internal <c>FindDocumentRange</c>, the single-quoted generic
    /// class round-trip (the risk-register generic case) through the reader, and the top-level-indent segment
    /// matching that keeps a same-named nested key from shadowing the real top-level field.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceYamlEditorReadEdgeTests
    {
        [Test]
        public void TryReadReferenceId_NullSentinelSlots_ReturnMinusTwo()
        {
            var path = YamlFixtures.WriteTemp(YamlFixtures.EmptyFieldsPrefab);
            try
            {
                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.MonoBehaviourFileId, "_onHitEffect", out var top));
                Assert.AreEqual(-2, top, "A cleared top-level [SerializeReference] field reads as the null id (-2).");

                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.MonoBehaviourFileId, "_sidearms.Array.data[1]", out var listElement));
                Assert.AreEqual(-2, listElement, "A null list element reads as the null id (-2).");

                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.MonoBehaviourFileId, "_primaryWeapon._chargeEffect", out var nested));
                Assert.AreEqual(-2, nested, "A cleared nested field reads as the null id (-2).");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryReadReferenceId_SingleDocAsset_FallsBackWhenFileIdDoesNotMatch()
        {
            var path = YamlFixtures.WriteTemp(YamlFixtures.AllUnassignedAsset);
            try
            {
                // A fileId matching no anchor still resolves in a single-object asset (the ScriptableObject case).
                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(path, fileId: 0, "_weapon", out var rid));
                Assert.AreEqual(-2, rid);
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryRewriteType_MultiDocAsset_WrongFileId_NoOp()
        {
            var path = YamlFixtures.WriteTemp(YamlFixtures.MissingTypePrefab);
            try
            {
                var before = File.ReadAllText(path);
                var newType = new ManagedTypeName("Asm", "Ns", "Pistol");

                // A fileId matching neither document in a multi-object asset must not rewrite anything.
                Assert.IsFalse(SerializeReferenceYamlEditor.TryRewriteType(
                    path, fileId: 123456789, YamlFixtures.GhostPistolRid, newType));
                Assert.AreEqual(before, File.ReadAllText(path),
                    "A rewrite against an absent document must leave the file byte-identical.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryReadReferenceId_ListOfStructElement_ResolvesNestedRid()
        {
            var path = YamlFixtures.WriteTemp(YamlFixtures.ListOfStructAsset);
            try
            {
                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.ListOfStructFileId, "_slots.Array.data[0]._weapon", out var first));
                Assert.AreEqual(3001, first, "First List<struct> element's nested managed reference must resolve.");

                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.ListOfStructFileId, "_slots.Array.data[1]._weapon", out var second));
                Assert.AreEqual(3002, second, "Second List<struct> element's nested managed reference must resolve.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryReadReferenceId_TopLevelFieldShadowedByEarlierNestedKey_ResolvesTopLevelRid()
        {
            var path = YamlFixtures.WriteTemp(YamlFixtures.ShadowedFieldPrefab);
            try
            {
                // The nested _config._weapon pointer appears before the top-level _weapon in the document — the first
                // path segment must be matched at the top-level field indent, not at any indent.
                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.ShadowedFileId, "_weapon", out var topLevel));
                Assert.AreEqual(YamlFixtures.ShadowedTopLevelRid, topLevel,
                    "A same-named key nested in an earlier container must not shadow the real top-level field.");

                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                    path, YamlFixtures.ShadowedFileId, "_config._weapon", out var nested));
                Assert.AreEqual(YamlFixtures.ShadowedNestedRid, nested,
                    "The nested occurrence must still resolve through its own path.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryReadStoredType_QuotedGenericType_ParsesClassIntact()
        {
            var path = YamlFixtures.WriteTemp(YamlFixtures.QuotedGenericAsset);
            try
            {
                Assert.IsTrue(SerializeReferenceYamlEditor.TryReadStoredType(
                    path, YamlFixtures.QuotedGenericFileId, "_modifier", out var rid, out var type));
                Assert.AreEqual(YamlFixtures.QuotedGenericRid, rid);
                Assert.AreEqual(YamlFixtures.QuotedGenericClass, type.Class,
                    "The single-quoted generic class identity must round-trip un-quoted.");
                Assert.AreEqual("Aspid.FastTools.Samples.SerializeReferences", type.Namespace);
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }
    }
}
