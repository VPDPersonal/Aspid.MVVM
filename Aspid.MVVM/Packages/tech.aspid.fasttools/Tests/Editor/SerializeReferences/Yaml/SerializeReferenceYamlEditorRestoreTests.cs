using System.IO;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Coverage for <see cref="SerializeReferenceYamlEditor.TryReadArrayElementEntryBlock"/> and
    /// <see cref="SerializeReferenceYamlEditor.TryRestoreArrayElementReference"/> — the snapshot/restore primitive that
    /// undoes Unity's list-resize data loss (a named missing <c>rid</c> collapsed to the anonymous <c>-2</c> sentinel).
    /// The snapshot is captured from a pristine fixture; the restore is applied to a <see cref="DegradedListPrefab"/>
    /// standing in for the post-resize file, and the round trip must re-point the element and re-materialise its entry
    /// without disturbing the surviving siblings.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceYamlEditorRestoreTests
    {
        private const string ElementPath = "_sidearms.Array.data[0]";

        // The MissingTypePrefab list after Unity's default "+" collapsed the named missing GhostPistol (rid 1002) into
        // the anonymous null sentinel (-2) and appended a fresh <None> element: _sidearms is [-2, 1003, -2] and the
        // GhostPistol RefIds entry is gone, replaced by the shared "- rid: -2 / type: {empty}" sentinel. Same document
        // anchor (MonoBehaviourFileId) and layout as the pristine fixture.
        private const string DegradedListPrefab =
@"%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!1 &6500000000000000001
GameObject:
  serializedVersion: 6
  m_Component:
  - component: {fileID: 6500000000000000003}
  m_Name: LoadoutMissingType
--- !u!114 &6500000000000000003
MonoBehaviour:
  m_GameObject: {fileID: 6500000000000000001}
  m_Enabled: 1
  m_Script: {fileID: 11500000, guid: 884d53b5154744d3af6948b1eef02505, type: 3}
  m_Name:
  _primaryWeapon:
    rid: 1001
  _sidearms:
  - rid: -2
  - rid: 1003
  - rid: -2
  _onHitEffect:
    rid: 1004
  references:
    version: 2
    RefIds:
    - rid: -2
      type: {class: , ns: , asm: }
    - rid: 1001
      type: {class: Railgun, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}
      data:
        _chargeTime: 2
        _chargeEffect:
          rid: 1005
    - rid: 1003
      type: {class: Shotgun, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}
      data:
        _pellets: 8
        _spreadAngle: 25
    - rid: 1004
      type: {class: FreezeEffect, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}
      data:
        _duration: 2.5
        _slowPercent: 40
    - rid: 1005
      type: {class: BurnEffect, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}
      data:
        _duration: 3
        _damagePerSecond: 5
";

        private string _pristinePath;
        private string _degradedPath;

        [SetUp]
        public void SetUp()
        {
            _pristinePath = YamlFixtures.WriteTemp(YamlFixtures.MissingTypePrefab);
            _degradedPath = YamlFixtures.WriteTemp(DegradedListPrefab);
        }

        [TearDown]
        public void TearDown()
        {
            YamlFixtures.Delete(_pristinePath);
            YamlFixtures.Delete(_degradedPath);
        }

        // The preamble the round-trip tests share: snapshot GhostPistol's entry from the pristine file and restore it
        // over the degraded file's sentinel element, asserting both steps succeed.
        private void RestoreGhostPistolIntoDegraded()
        {
            Assert.IsTrue(SerializeReferenceYamlEditor.TryReadArrayElementEntryBlock(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, ElementPath, out _, out var entryLines));
            Assert.IsTrue(SerializeReferenceYamlEditor.TryRestoreArrayElementReference(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, ElementPath, entryLines),
                "Restoring over a sentinel element must succeed.");
        }

        // ---- TryFindTopLevelArrayElementForRid: rid -> (field, index) attribution the delete-guard relies on ---------

        [Test]
        public void TryFindTopLevelArrayElementForRid_ListElements_ReportFieldAndIndex()
        {
            Assert.IsTrue(SerializeReferenceYamlEditor.TryFindTopLevelArrayElementForRid(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, out var field, out var index));
            Assert.AreEqual("_sidearms", field);
            Assert.AreEqual(0, index);

            Assert.IsTrue(SerializeReferenceYamlEditor.TryFindTopLevelArrayElementForRid(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, YamlFixtures.ShotgunRid, out field, out index));
            Assert.AreEqual("_sidearms", field);
            Assert.AreEqual(1, index, "Element indexing must count within the field, not across the document.");
        }

        [Test]
        public void TryFindTopLevelArrayElementForRid_SingleFieldPointer_ReturnsFalse()
        {
            // _primaryWeapon holds Railgun as a plain (non-array) field — a list resize cannot destroy it, so the
            // rid must not be attributed to any array element.
            Assert.IsFalse(SerializeReferenceYamlEditor.TryFindTopLevelArrayElementForRid(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, YamlFixtures.RailgunRid, out _, out _));
        }

        [Test]
        public void TryFindTopLevelArrayElementForRid_NestedPointer_ReturnsFalse()
        {
            // BurnEffect is pointed at from INSIDE Railgun's RefIds data (_chargeEffect), not from a top-level list —
            // the references block must be excluded from the walk.
            Assert.IsFalse(SerializeReferenceYamlEditor.TryFindTopLevelArrayElementForRid(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, YamlFixtures.BurnEffectRid, out _, out _));
        }

        [Test]
        public void TryFindTopLevelArrayElementForRid_UnknownRid_ReturnsFalse()
        {
            Assert.IsFalse(SerializeReferenceYamlEditor.TryFindTopLevelArrayElementForRid(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, 999_999, out _, out _));
        }

        [Test]
        public void TryReadArrayElementEntryBlock_CapturesTypeAndData()
        {
            var read = SerializeReferenceYamlEditor.TryReadArrayElementEntryBlock(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, ElementPath, out var rid, out var entryLines);

            Assert.IsTrue(read, "The missing element's entry must be captured from the pristine file.");
            Assert.AreEqual(YamlFixtures.GhostPistolRid, rid, "The captured rid must be the stored GhostPistol id.");

            var joined = string.Join("\n", entryLines);
            StringAssert.Contains("class: GhostPistol", joined, "The captured entry must carry the missing type identity.");
            StringAssert.Contains("_damage: 15", joined, "The captured entry must carry the orphaned payload.");
            StringAssert.Contains("_magazineSize: 12", joined, "The captured entry must carry every payload field.");
        }

        [Test]
        public void TryReadArrayElementEntryBlock_NullElement_ReturnsFalse()
        {
            // Element 0 of the degraded file is the null sentinel (-2) — there is no entry to snapshot.
            var read = SerializeReferenceYamlEditor.TryReadArrayElementEntryBlock(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, ElementPath, out _, out var entryLines);

            Assert.IsFalse(read, "A null/sentinel element carries no entry to capture.");
            Assert.IsNull(entryLines);
        }

        [Test]
        public void TryRestoreArrayElementReference_RepointsElement_AndReMaterialisesEntry()
        {
            RestoreGhostPistolIntoDegraded();

            // The reader resolves the element back to a real, GhostPistol-typed reference (no longer <None>).
            Assert.IsTrue(SerializeReferenceYamlEditor.TryReadStoredType(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, ElementPath, out var rid, out var type));
            Assert.Greater(rid, 0, "The restored element must point at a fresh positive rid.");
            Assert.AreEqual("GhostPistol", type.Class, "The restored reference must carry the original missing type.");

            var after = File.ReadAllText(_degradedPath);
            StringAssert.Contains("_damage: 15", after, "The restored entry must bring back the orphaned payload.");
        }

        [Test]
        public void TryRestoreArrayElementReference_UsesFreshRid_NotCollidingWithSurvivors()
        {
            RestoreGhostPistolIntoDegraded();

            SerializeReferenceYamlEditor.TryReadStoredType(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, ElementPath, out var rid, out _);

            // The document's max surviving id is 1005 (BurnEffect); the fresh id must be past it.
            Assert.AreEqual(1006, rid, "The fresh rid must be one past the document's maximum surviving id.");
        }

        [Test]
        public void TryRestoreArrayElementReference_LeavesSurvivingSiblingsIntact()
        {
            RestoreGhostPistolIntoDegraded();

            // Element 1 (Shotgun) and element 2 (the fresh <None>) must be untouched.
            Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, "_sidearms.Array.data[1]", out var shotgunRid));
            Assert.AreEqual(YamlFixtures.ShotgunRid, shotgunRid, "The Shotgun sibling must keep its rid.");

            Assert.IsTrue(SerializeReferenceYamlEditor.TryReadReferenceId(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, "_sidearms.Array.data[2]", out var noneRid));
            Assert.AreEqual(-2, noneRid, "The appended <None> element must stay the null sentinel.");
        }

        [Test]
        public void TryRestoreArrayElementReference_SkipsWhenSlotAlreadyReassigned()
        {
            // The user re-assigned element 0 to a real reference (rid 1003) after the loss — restore must not clobber it.
            var reassigned = File.ReadAllLines(_degradedPath);
            for (var i = 0; i < reassigned.Length; i++)
            {
                if (reassigned[i].Trim() == "- rid: -2" && reassigned[i].StartsWith("  - "))
                {
                    reassigned[i] = "  - rid: 1003"; // first field-level element pointer
                    break;
                }
            }

            File.WriteAllLines(_degradedPath, reassigned);

            var entryLines = new[]
            {
                "    - rid: 1002",
                "      type: {class: GhostPistol, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}",
                "      data:",
                "        _damage: 15",
            };

            var restored = SerializeReferenceYamlEditor.TryRestoreArrayElementReference(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, ElementPath, entryLines);

            Assert.IsFalse(restored, "A slot the user has re-assigned to a real reference must not be overwritten.");
        }

        [Test]
        public void TryRestoreArrayElementReference_NestedPath_ReturnsFalse()
        {
            Assert.IsTrue(SerializeReferenceYamlEditor.TryReadArrayElementEntryBlock(
                _pristinePath, YamlFixtures.MonoBehaviourFileId, ElementPath, out _, out var entryLines));

            // A nested (non-top-level) array path is out of scope for the surgical re-point — it must decline, not
            // mis-target.
            var restored = SerializeReferenceYamlEditor.TryRestoreArrayElementReference(
                _degradedPath, YamlFixtures.MonoBehaviourFileId, "_config._loadout._sidearms.Array.data[0]", entryLines);

            Assert.IsFalse(restored);
        }
    }
}
