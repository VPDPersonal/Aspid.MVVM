using System.Linq;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Coverage for <see cref="SerializeReferenceGateScanner.IsPendingMigration"/> — the gate's pre-filter that keeps
    /// properly declared renames from ever warning or failing a build / CI run — and for <see cref="SerializeReferenceGateScanner.Scan"/>
    /// itself surfacing unset required fields (the data source for the Project References "Required violations" group).
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceGateScannerTests
    {
        private const string ProbeAssetPath = "Assets/__AspidGateScannerRequiredProbe__.asset";

        // Scan(RequiredOnly) is the exact call the Project References "Required violations" group makes; this proves
        // it surfaces both an unset managed reference and an unset [TypeSelector(Required = true)] string field on a
        // saved asset (RequiredTestObject, shared fixture — see SerializeReferenceTestFixtures.cs).
        [Test]
        public void Scan_RequiredOnly_SurfacesUnsetRequiredFieldsOnSavedAsset()
        {
            var probe = ScriptableObject.CreateInstance<RequiredTestObject>();
            try
            {
                AssetDatabase.CreateAsset(probe, ProbeAssetPath);
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(probe, out _, out long fileId);

                var violations = SerializeReferenceGateScanner.Scan(GateOptions.RequiredOnly);
                var forProbe = violations.Where(v => v.AssetPath == ProbeAssetPath && v.FileId == fileId).ToList();

                Assert.IsTrue(forProbe.All(v => v.Kind == GateViolationKind.RequiredUnset));
                Assert.IsTrue(forProbe.Any(v => v.FieldPath == nameof(RequiredTestObject.requiredRef)),
                    "Unset [SerializeReference, TypeSelector(Required = true)] field must be reported.");
                Assert.IsTrue(forProbe.Any(v => v.FieldPath == nameof(RequiredTestObject.requiredString)),
                    "Unset [TypeSelector(Required = true)] string field must be reported.");
            }
            finally
            {
                AssetDatabase.DeleteAsset(ProbeAssetPath);
            }
        }

        // ScanAssetRequiredFields is the scoped, single-asset entry point the Inspect Asset graph calls on every
        // Rescan; it must agree with Scan(RequiredOnly) for that one asset without sweeping the whole project.
        [Test]
        public void ScanAssetRequiredFields_SavedAsset_MatchesProjectScan()
        {
            var probe = ScriptableObject.CreateInstance<RequiredTestObject>();
            try
            {
                AssetDatabase.CreateAsset(probe, ProbeAssetPath);
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(probe, out _, out long fileId);

                var violations = SerializeReferenceGateScanner.ScanAssetRequiredFields(ProbeAssetPath);
                var forProbe = violations.Where(v => v.FileId == fileId).ToList();

                Assert.IsTrue(forProbe.All(v => v.Kind == GateViolationKind.RequiredUnset));
                Assert.IsTrue(forProbe.Any(v => v.FieldPath == nameof(RequiredTestObject.requiredRef)),
                    "Unset [SerializeReference, TypeSelector(Required = true)] field must be reported.");
                Assert.IsTrue(forProbe.Any(v => v.FieldPath == nameof(RequiredTestObject.requiredString)),
                    "Unset [TypeSelector(Required = true)] string field must be reported.");
            }
            finally
            {
                AssetDatabase.DeleteAsset(ProbeAssetPath);
            }
        }

        [Test]
        public void ScanAssetRequiredFields_NonCandidatePath_ReturnsEmpty()
        {
            Assert.AreEqual(0, SerializeReferenceGateScanner.ScanAssetRequiredFields("Assets/Fake.txt").Count);
        }

        // The Inspect Asset graph (SerializeReferenceGraphView) badges an empty [SerializeReference] slot as REQUIRED
        // by matching its graph field path — built independently by SerializeReferenceGraphScanner straight from
        // YAML — against this scan's GateViolation.FieldPath (a live SerializedProperty.propertyPath), after the same
        // "[i]" -> ".Array.data[i]" normalization the view applies. This proves the two paths actually agree for a
        // real saved asset, not just that each individually finds the field.
        [Test]
        public void ScanAssetRequiredFields_UnsetManagedReference_MatchesGraphScannerEmptyRootPath()
        {
            var probe = ScriptableObject.CreateInstance<RequiredTestObject>();
            try
            {
                AssetDatabase.CreateAsset(probe, ProbeAssetPath);
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(probe, out _, out long fileId);

                var violations = SerializeReferenceGateScanner.ScanAssetRequiredFields(ProbeAssetPath);
                var document = SerializeReferenceGraphScanner.Build(ProbeAssetPath).Single(doc => doc.FileId == fileId);
                var emptyRoot = document.Roots.Single(root => root.IsEmpty);

                var normalizedGraphPath = Regex.Replace(emptyRoot.Label, @"\[(\d+)\]", ".Array.data[$1]");

                Assert.IsTrue(
                    violations.Any(v => v.FileId == fileId && v.FieldPath == normalizedGraphPath),
                    $"Normalized graph path '{normalizedGraphPath}' must match a ScanAssetRequiredFields violation's FieldPath.");
            }
            finally
            {
                AssetDatabase.DeleteAsset(ProbeAssetPath);
            }
        }

        // A [MovedFrom]-claimed stale name is a pending migration, not a violation: Unity migrates the reference in
        // memory at load, so the gate must accept it — a properly declared rename can never warn or fail a build /
        // CI run. A scene path exercises the trust-the-claim branch (constraints are unrecoverable for scenes).
        // (RenamedRanged is the shared [MovedFrom(..., "OldRenamedRanged")] fixture.)
        [Test]
        public void IsPendingMigration_MovedFromClaimedName_IsNotAViolation()
        {
            var stored = new ManagedTypeName(typeof(RenamedRanged).Assembly.GetName().Name, typeof(RenamedRanged).Namespace, "OldRenamedRanged");
            var entry = new MissingReferenceEntry(fileId: 1, rid: 100, stored);

            Assert.IsTrue(SerializeReferenceGateScanner.IsPendingMigration("Assets/Fake.unity", entry));
        }

        [Test]
        public void IsPendingMigration_UnknownName_StaysAViolation()
        {
            var stored = new ManagedTypeName(
                typeof(RenamedRanged).Assembly.GetName().Name, typeof(RenamedRanged).Namespace, "GhostNeverExisted");
            var entry = new MissingReferenceEntry(fileId: 1, rid: 100, stored);

            Assert.IsFalse(SerializeReferenceGateScanner.IsPendingMigration("Assets/Fake.unity", entry));
        }
    }
}
