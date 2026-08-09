using System.IO;
using System.Linq;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Round-trip and confinement coverage for <see cref="SerializeReferenceYamlEditor.TryRewriteType"/> — the highest
    /// risk write path. Asserts the rewrite re-points the correct rid and touches exactly one line, so the regression
    /// guard locks in that no other YAML is disturbed.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceYamlEditorRewriteTests
    {
        private string _path;

        [SetUp]
        public void SetUp() =>
            _path = YamlFixtures.WriteTemp(YamlFixtures.MissingTypePrefab);

        [TearDown]
        public void TearDown() =>
            YamlFixtures.Delete(_path);

        [Test]
        public void TryRewriteType_RepointsRid_AndChangesExactlyOneLine()
        {
            var before = File.ReadAllLines(_path);
            var newType = new ManagedTypeName(
                "Aspid.FastTools.Samples.SerializeReferences",
                "Aspid.FastTools.Samples.SerializeReferences",
                "Pistol");

            var rewritten = SerializeReferenceYamlEditor.TryRewriteType(
                _path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, newType);
            Assert.IsTrue(rewritten, "Rewrite of a present rid must succeed.");

            var after = File.ReadAllLines(_path);
            Assert.AreEqual(before.Length, after.Length, "A type rewrite must not add or remove lines.");

            var changed = Enumerable.Range(0, before.Length).Where(i => before[i] != after[i]).ToArray();
            Assert.AreEqual(1, changed.Length, "Exactly one line (the type mapping) should change.");

            var line = after[changed[0]];
            StringAssert.Contains("class: Pistol", line);
            StringAssert.Contains("type:", line);
            StringAssert.Contains("GhostPistol", before[changed[0]]);
        }

        [Test]
        public void TryRewriteType_RoundTrips_ReadBackReportsNewType()
        {
            var newType = new ManagedTypeName(
                "Aspid.FastTools.Samples.SerializeReferences",
                "Aspid.FastTools.Samples.SerializeReferences",
                "Pistol");

            Assert.IsTrue(SerializeReferenceYamlEditor.TryRewriteType(
                _path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, newType));

            // Reading back also exercises the probe cache being invalidated by the writer (TryRewriteType clears it).
            Assert.IsTrue(SerializeReferenceYamlEditor.TryReadStoredType(
                _path, YamlFixtures.MonoBehaviourFileId, "_sidearms.Array.data[0]", out var rid, out var type));
            Assert.AreEqual(YamlFixtures.GhostPistolRid, rid);
            Assert.AreEqual("Pistol", type.Class);
        }

        [Test]
        public void TryRewriteType_UnknownRid_ReturnsFalse_AndLeavesFileUnchanged()
        {
            var before = File.ReadAllText(_path);
            var newType = new ManagedTypeName("Asm", "Ns", "Pistol");

            Assert.IsFalse(SerializeReferenceYamlEditor.TryRewriteType(
                _path, YamlFixtures.MonoBehaviourFileId, 999999, newType));
            Assert.AreEqual(before, File.ReadAllText(_path), "A no-op rewrite must leave the file byte-identical.");
        }

        [Test]
        public void TryRemoveEntry_RemovesOnlyTheTargetEntry()
        {
            var beforeLines = File.ReadAllLines(_path).Length;

            Assert.IsTrue(SerializeReferenceYamlEditor.TryRemoveEntry(
                _path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.ShotgunRid));

            var after = File.ReadAllText(_path);

            // The Shotgun entry (type line + its data) is gone...
            StringAssert.DoesNotContain("Shotgun", after);
            StringAssert.DoesNotContain("_pellets", after);
            StringAssert.DoesNotContain("_spreadAngle", after);

            // ...every other RefIds entry is intact...
            StringAssert.Contains("Railgun", after);
            StringAssert.Contains("GhostPistol", after);
            StringAssert.Contains("FreezeEffect", after);
            StringAssert.Contains("BurnEffect", after);

            // ...and exactly the entry's five lines were removed.
            Assert.AreEqual(beforeLines - 5, File.ReadAllLines(_path).Length);
        }

        [Test]
        public void TryRemoveEntry_UnknownRid_ReturnsFalse_AndLeavesFileUnchanged()
        {
            var before = File.ReadAllText(_path);
            Assert.IsFalse(SerializeReferenceYamlEditor.TryRemoveEntry(_path, YamlFixtures.MonoBehaviourFileId, 424242));
            Assert.AreEqual(before, File.ReadAllText(_path), "A no-op remove must leave the file byte-identical.");
        }

        [Test]
        public void TryComputeRewrite_PreviewEqualsApplied()
        {
            var newType = new ManagedTypeName(
                "Aspid.FastTools.Samples.SerializeReferences",
                "Aspid.FastTools.Samples.SerializeReferences",
                "Pistol");

            Assert.IsTrue(SerializeReferenceYamlEditor.TryComputeRewrite(
                _path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, newType, out var edit));
            StringAssert.Contains("GhostPistol", edit.OldLine);
            StringAssert.Contains("class: Pistol", edit.NewLine);

            Assert.IsTrue(SerializeReferenceYamlEditor.TryRewriteType(
                _path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, newType));

            // The preview promised edit.NewLine at edit.LineNumber — the applied file must match exactly.
            var after = File.ReadAllLines(_path);
            Assert.AreEqual(edit.NewLine, after[edit.LineNumber], "Applied line must equal the previewed NewLine.");
        }

        [Test]
        public void TryRewriteType_PreservesLfLineEndings()
        {
            // Unity writes its YAML with LF on every platform; a one-line rewrite must not introduce CR.
            var lf = YamlFixtures.MissingTypePrefab.Replace("\r\n", "\n");
            var path = YamlFixtures.WriteTemp(lf);
            try
            {
                var newType = new ManagedTypeName(
                    "Aspid.FastTools.Samples.SerializeReferences",
                    "Aspid.FastTools.Samples.SerializeReferences",
                    "Pistol");

                Assert.IsTrue(SerializeReferenceYamlEditor.TryRewriteType(
                    path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, newType));

                StringAssert.DoesNotContain("\r", File.ReadAllText(path),
                    "An LF asset must stay LF after a rewrite (no CRLF churn).");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryRewriteType_PreservesCrlfLineEndings()
        {
            // A CRLF asset must keep CRLF — the writer must not collapse it to Environment.NewLine (LF on the dev box).
            var crlf = YamlFixtures.MissingTypePrefab.Replace("\r\n", "\n").Replace("\n", "\r\n");
            var path = YamlFixtures.WriteTemp(crlf);
            try
            {
                var newType = new ManagedTypeName(
                    "Aspid.FastTools.Samples.SerializeReferences",
                    "Aspid.FastTools.Samples.SerializeReferences",
                    "Pistol");

                Assert.IsTrue(SerializeReferenceYamlEditor.TryRewriteType(
                    path, YamlFixtures.MonoBehaviourFileId, YamlFixtures.GhostPistolRid, newType));

                var raw = File.ReadAllText(path);
                Assert.IsTrue(raw.Contains("\r\n"), "A CRLF asset must keep CRLF after a rewrite.");
                Assert.IsFalse(raw.Replace("\r\n", "").Contains("\n"),
                    "Every newline in a CRLF file must remain CRLF (no lone LF introduced).");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }
    }
}
