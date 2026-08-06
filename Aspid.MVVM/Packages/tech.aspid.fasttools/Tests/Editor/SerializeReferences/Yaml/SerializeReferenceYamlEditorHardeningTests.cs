using System.IO;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors.Tests
{
    /// <summary>
    /// Hardening coverage for the destructive write paths of <see cref="SerializeReferenceYamlEditor"/>: a file that is
    /// not a Unity-serialized YAML asset (no <c>%TAG !u!</c> directive) or whose target entry uses unexpected (tab /
    /// mixed) indentation must never be rewritten. The line-scanning editor relies on Unity's space-only indentation
    /// invariant; outside it the <c>IndentOf</c> measure and the <c>"- rid:"</c> <c>\s*</c> regex can disagree on where
    /// an entry ends, so the writers bail (no-op) rather than risk a mis-bounded, non-undoable edit.
    /// </summary>
    [TestFixture]
    internal sealed class SerializeReferenceYamlEditorHardeningTests
    {
        // The single object-document file id shared by the fixtures below.
        private const long FileId = 11400000L;
        private const long Rid = 1001L;

        // A RefIds-shaped document that carries a Unity object header (so the document is locatable) but LACKS the
        // "%YAML 1.1" / "%TAG !u! …" directive preamble Unity always writes. Without the sniff every writer below would
        // find rid 1001 and rewrite it — so a successful no-op here proves the %TAG guard, not a missing target.
        private const string MissingDirectivePreamble =
@"--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_Script: {fileID: 11500000, guid: b7874533c7294db1b8aa77e7d4102c9f, type: 3}
  m_Name: NotAUnityAsset
  _weapon:
    rid: 1001
  references:
    version: 2
    RefIds:
    - rid: 1001
      type: {class: Pistol, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}
      data:
        _damage: 10
";

        // A genuine Unity asset (full %YAML / %TAG preamble, so it passes the sniff) whose RefIds entry is TAB-indented.
        // Built with explicit \t/\n so the indentation is unambiguous in source. The "- rid:" \s* regex still matches the
        // tab-indented header, so the writers reach the bounding step — where the space-only-indent guard must stop them.
        private const string TabIndentedUnityAsset =
            "%YAML 1.1\n" +
            "%TAG !u! tag:unity3d.com,2011:\n" +
            "--- !u!114 &11400000\n" +
            "MonoBehaviour:\n" +
            "\tm_ObjectHideFlags: 0\n" +
            "\tm_Script: {fileID: 11500000, guid: b7874533c7294db1b8aa77e7d4102c9f, type: 3}\n" +
            "\tm_Name: TabIndentedAsset\n" +
            "\t_weapon:\n" +
            "\t\trid: 1001\n" +
            "\treferences:\n" +
            "\t\tversion: 2\n" +
            "\t\tRefIds:\n" +
            "\t\t- rid: 1001\n" +
            "\t\t  type: {class: Pistol, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}\n" +
            "\t\t  data:\n" +
            "\t\t\t_damage: 10\n";

        // A genuine, space-indented Unity asset whose rid 1001 entry block contains a blank line that holds nothing but a
        // stray TAB (built with explicit \t / \n so it is unambiguous in source). Such a line carries no indentation to
        // measure — FindEntryEnd spans it — so the space-only-indent guard must NOT mistake the tab for tab-indentation
        // and abort: a removal must still run. The negative control above proves a tab in a CONTENT line still bails.
        private const string BlankTabLineInsideEntryAsset =
            "%YAML 1.1\n" +
            "%TAG !u! tag:unity3d.com,2011:\n" +
            "--- !u!114 &11400000\n" +
            "MonoBehaviour:\n" +
            "  m_ObjectHideFlags: 0\n" +
            "  m_Script: {fileID: 11500000, guid: b7874533c7294db1b8aa77e7d4102c9f, type: 3}\n" +
            "  m_Name: BlankTabLineAsset\n" +
            "  _weapon:\n" +
            "    rid: 1001\n" +
            "  references:\n" +
            "    version: 2\n" +
            "    RefIds:\n" +
            "    - rid: 1001\n" +
            "      type: {class: Pistol, ns: Aspid.FastTools.Samples.SerializeReferences, asm: Aspid.FastTools.Samples.SerializeReferences}\n" +
            "      data:\n" +
            "        _damage: 10\n" +
            "\t\n" +
            "        _magazineSize: 7\n";

        private static readonly ManagedTypeName SomeType = new ManagedTypeName(
            "Aspid.FastTools.Samples.SerializeReferences",
            "Aspid.FastTools.Samples.SerializeReferences",
            "Shotgun");

        // ---- %TAG !u! sniff: a non-Unity YAML file is never rewritten -------------------------------------------------

        [Test]
        public void TryComputeRewrite_MissingDirectivePreamble_ReturnsFalse()
        {
            var path = YamlFixtures.WriteTemp(MissingDirectivePreamble);
            try
            {
                Assert.IsFalse(SerializeReferenceYamlEditor.TryComputeRewrite(path, FileId, Rid, SomeType, out var edit),
                    "A file without Unity's %TAG directive must not even be offered as a rewrite candidate.");
                Assert.IsFalse(edit.IsValid, "No edit should be produced for a non-Unity file.");
            }
            finally { YamlFixtures.Delete(path); }
        }

        [Test]
        public void TryRewriteType_MissingDirectivePreamble_ReturnsFalse_LeavesFileUnchanged()
        {
            var path = YamlFixtures.WriteTemp(MissingDirectivePreamble);

            try
            {
                var before = File.ReadAllText(path);
                Assert.IsFalse(SerializeReferenceYamlEditor.TryRewriteType(path, FileId, Rid, SomeType));
                Assert.AreEqual(before, File.ReadAllText(path), "A refused rewrite must leave the file byte-identical.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryRemoveEntry_MissingDirectivePreamble_ReturnsFalse_LeavesFileUnchanged()
        {
            var path = YamlFixtures.WriteTemp(MissingDirectivePreamble);
            try
            {
                var before = File.ReadAllText(path);
                Assert.IsFalse(SerializeReferenceYamlEditor.TryRemoveEntry(path, FileId, Rid));
                Assert.AreEqual(before, File.ReadAllText(path), "A refused remove must leave the file byte-identical.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryNullReference_MissingDirectivePreamble_ReturnsFalse_LeavesFileUnchanged()
        {
            var path = YamlFixtures.WriteTemp(MissingDirectivePreamble);
            try
            {
                var before = File.ReadAllText(path);
                Assert.IsFalse(SerializeReferenceYamlEditor.TryNullReference(path, FileId, Rid));
                Assert.AreEqual(before, File.ReadAllText(path), "A refused null must leave the file byte-identical.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        // ---- indent bail: a tab-indented entry block is never destructively rewritten ---------------------------------

        [Test]
        public void TryRemoveEntry_TabIndentedEntry_BailsBeforeWrite_LeavesFileUnchanged()
        {
            var path = YamlFixtures.WriteTemp(TabIndentedUnityAsset);
            try
            {
                var before = File.ReadAllText(path);
                Assert.IsFalse(SerializeReferenceYamlEditor.TryRemoveEntry(path, FileId, Rid),
                    "A tab-indented entry can be mis-bounded — the remover must bail rather than write.");
                Assert.AreEqual(before, File.ReadAllText(path), "A refused remove must leave the file byte-identical.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryNullReference_TabIndentedEntry_BailsBeforeWrite_LeavesFileUnchanged()
        {
            var path = YamlFixtures.WriteTemp(TabIndentedUnityAsset);
            try
            {
                var before = File.ReadAllText(path);
                Assert.IsFalse(SerializeReferenceYamlEditor.TryNullReference(path, FileId, Rid),
                    "A tab-indented entry block can be mis-bounded — the nuller must bail before writing.");
                Assert.AreEqual(before, File.ReadAllText(path), "A refused null must leave the file byte-identical.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }

        [Test]
        public void TryRemoveEntry_BlankLineWithStrayTabInsideEntry_StillRemoves_LeavesNoEntry()
        {
            var path = YamlFixtures.WriteTemp(BlankTabLineInsideEntryAsset);
            try
            {
                Assert.IsTrue(SerializeReferenceYamlEditor.TryRemoveEntry(path, FileId, Rid),
                    "A blank line that merely contains a stray tab carries no indentation — it must not abort the removal.");
                StringAssert.DoesNotContain("Pistol", File.ReadAllText(path),
                    "The rid 1001 entry must be gone once the blank-line guard no longer falsely mistrusts the block.");
            }
            finally
            {
                YamlFixtures.Delete(path);
            }
        }
    }
}
