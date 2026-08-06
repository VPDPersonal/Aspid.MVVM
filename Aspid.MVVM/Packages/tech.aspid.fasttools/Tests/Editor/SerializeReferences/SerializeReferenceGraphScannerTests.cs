using System.Linq;
using NUnit.Framework;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Structural coverage for <see cref="SerializeReferenceGraphScanner"/>: the field-pointer roots, the nested
    /// parent → child edges (and the relative field path each carries), and the unassigned (null-sentinel) slots the
    /// scanner now surfaces as empty roots / edges. Drives the public <c>Build</c> against temp YAML files, so it
    /// exercises the parser in isolation — no asset import, no SerializedObject. Assertions stay off the per-node
    /// <c>Resolves</c> flag (which depends on whether the Samples assembly's types load in the test domain) and only
    /// check graph structure, which is purely a function of the YAML.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceGraphScannerTests
    {
        private string _missingPath;
        private string _emptyPath;
        private string _allUnassignedPath;

        // Every test only reads the fixtures, so the temp files are written once per fixture, not per test.
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _missingPath = YamlFixtures.WriteTemp(YamlFixtures.MissingTypePrefab);
            _emptyPath = YamlFixtures.WriteTemp(YamlFixtures.EmptyFieldsPrefab);
            _allUnassignedPath = YamlFixtures.WriteTemp(YamlFixtures.AllUnassignedAsset);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            YamlFixtures.Delete(_missingPath);
            YamlFixtures.Delete(_emptyPath);
            YamlFixtures.Delete(_allUnassignedPath);
        }

        // The MonoBehaviour document is the one carrying the RefIds graph; the GameObject document has none and is
        // dropped by the scanner, so Build returns exactly the MonoBehaviour graph.
        private static ReferenceGraphDocument SingleDocument(string path)
        {
            var documents = SerializeReferenceGraphScanner.Build(path);
            Assert.AreEqual(1, documents.Count, "Only the MonoBehaviour document carries managed references.");
            return documents[0];
        }

        // --- Issue 1: nested references carry their full field path -------------------------------------------------

        [Test]
        public void Build_NestedReference_EdgeCarriesRelativeFieldPath()
        {
            var document = SingleDocument(_missingPath);

            // Railgun (_primaryWeapon) owns BurnEffect through its _chargeEffect field. The edge records the path
            // relative to Railgun's data block — "_chargeEffect" — which the view joins onto "_primaryWeapon" to show
            // the full "_primaryWeapon._chargeEffect" path on the nested card.
            var children = document.ChildrenOf(YamlFixtures.RailgunRid);
            Assert.AreEqual(1, children.Count);
            Assert.AreEqual(YamlFixtures.BurnEffectRid, children[0].Rid);
            Assert.AreEqual("_chargeEffect", children[0].Label);
            Assert.IsFalse(children[0].IsEmpty);
        }

        [Test]
        public void Build_Roots_CarryIndexedFieldPaths()
        {
            var document = SingleDocument(_missingPath);

            Assert.AreEqual("_primaryWeapon", LabelOfRoot(document, YamlFixtures.RailgunRid));
            Assert.AreEqual("_sidearms[0]", LabelOfRoot(document, YamlFixtures.GhostPistolRid));
            Assert.AreEqual("_sidearms[1]", LabelOfRoot(document, YamlFixtures.ShotgunRid));
            Assert.AreEqual("_onHitEffect", LabelOfRoot(document, YamlFixtures.FreezeEffectRid));
        }

        [Test]
        public void Build_AllAssigned_HasNoEmptySlots()
        {
            var document = SingleDocument(_missingPath);

            Assert.IsFalse(document.Roots.Any(root => root.IsEmpty), "Every field in the fixture is assigned.");
            Assert.IsFalse(document.Edges.Values.SelectMany(edges => edges).Any(edge => edge.IsEmpty));
        }

        // --- Issue 2: unassigned (null-sentinel) slots surface ------------------------------------------------------

        [Test]
        public void Build_ClearedTopLevelField_SurfacesAsEmptyRoot()
        {
            var document = SingleDocument(_emptyPath);

            var emptyRoots = document.Roots.Where(root => root.IsEmpty).Select(root => root.Label).ToList();

            // The cleared single field and the null list element both surface, each at its own field path.
            CollectionAssert.AreEquivalent(new[] { "_onHitEffect", "_sidearms[1]" }, emptyRoots);
            Assert.IsTrue(document.Roots.All(root => !root.IsEmpty || root.Rid < 0),
                "An empty root must carry a null sentinel rid.");
        }

        [Test]
        public void Build_ClearedNestedField_SurfacesAsEmptyEdge()
        {
            var document = SingleDocument(_emptyPath);

            // Railgun's _chargeEffect was cleared: the edge is kept (so the empty slot renders nested) but points at the
            // null sentinel, so the view draws an "<None>" leaf and never recurses.
            var children = document.ChildrenOf(YamlFixtures.EmptyRailgunRid);
            Assert.AreEqual(1, children.Count);
            Assert.IsTrue(children[0].IsEmpty);
            Assert.AreEqual("_chargeEffect", children[0].Label);
        }

        [Test]
        public void Build_NullSentinels_ExcludedFromSharedAndOrphans()
        {
            var document = SingleDocument(_emptyPath);

            // Two empty roots and one empty edge all point at the same -2 sentinel — it must never be flagged shared,
            // and the assigned references are all reachable, so there are no orphans either.
            Assert.IsEmpty(document.Shared, "The shared -2 null sentinel must not count as an aliased reference.");
            Assert.IsEmpty(document.Orphans, "Every assigned reference is reachable from a root.");
        }

        // --- An asset whose every managed-ref field is unassigned must still surface (regression) ------------------

        [Test]
        public void Build_AllFieldsUnassigned_KeepsDocumentWithEmptyRoot()
        {
            // The whole RefIds block is just Unity's null sentinel, so the document carries zero real nodes — yet its
            // single unassigned field is a slot worth showing, so the scanner must keep the document (with one empty
            // root) rather than dropping it as "no managed references". Regression: a Nodes.Count == 0 early-return
            // used to drop these assets entirely so the Inspect Asset view showed its empty state.
            var documents = SerializeReferenceGraphScanner.Build(_allUnassignedPath);
            Assert.AreEqual(1, documents.Count, "An all-unassigned asset still has a slot to surface.");

            var document = documents[0];
            Assert.IsEmpty(document.Nodes, "The RefIds block holds only the null sentinel — no real nodes.");

            var emptyRoots = document.Roots.Where(root => root.IsEmpty).Select(root => root.Label).ToList();
            CollectionAssert.AreEquivalent(new[] { "_weapon" }, emptyRoots);

            Assert.IsEmpty(document.Shared, "The shared -2 sentinel must not count as an aliased reference.");
            Assert.IsEmpty(document.Orphans, "There are no real nodes, so nothing can be orphaned.");
        }

        private static string LabelOfRoot(ReferenceGraphDocument document, long rid)
        {
            foreach (var root in document.Roots)
                if (root.Rid == rid) return root.Label;

            return null;
        }
    }
}
