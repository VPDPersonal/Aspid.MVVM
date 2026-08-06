using UnityEditor;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Preserves a <c>[SerializeReference]</c> list's <b>missing-type</b> element across a list resize. Unity keeps a
    /// missing (renamed / moved / deleted) managed reference only in the asset's YAML — it is dropped from the live
    /// object (<see cref="SerializationUtility.GetManagedReferencesWithMissingTypes"/> returns nothing for a saved
    /// prefab / GameObject, and the per-property API reads it back as the null id <c>-2</c>). When the user adds an
    /// element to the list, Unity re-serialises the array from that live (null) state on the next <b>save</b>, collapsing
    /// the named missing entry into the anonymous <c>-2</c> sentinel — the type identity and its orphaned payload are
    /// destroyed on disk, and the field silently becomes <c>&lt;None&gt;</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The loss lands at <b>save</b> time, not on <c>ApplyModifiedProperties</c> (which only dirties the in-memory
    /// object), so this guards the save boundary rather than the inspector: <see cref="OnWillSaveAssets"/> fires with the
    /// on-disk YAML still pristine, so it snapshots every top-level missing list element there (rid, type and payload),
    /// then schedules a post-save pass. Unity writes the (possibly degraded) file; the deferred pass re-reads it and,
    /// for any snapshotted element the save collapsed to a null/sentinel pointer, re-materialises the reference via
    /// <see cref="SerializeReferenceYamlEditor.TryRestoreArrayElementReference"/> and reimports the asset. An element the
    /// save left intact (a plain save that did not resize the list) fails the restore's own "slot is empty" check and is
    /// left untouched, so a normal save is a no-op and the pass never loops.
    /// </para>
    /// <para>
    /// Scope: saved assets only (prefabs / ScriptableObjects), which is where the loss reproduces and where the on-disk
    /// YAML is the source of truth. Objects open in Prefab Mode and loose scene objects are a follow-up (their live copy
    /// is not the file being saved here). Only <i>top-level</i> array elements are protected — the shape a list <c>+</c>
    /// destroys; a single missing field or a nested pointer is not resized and so is never at risk.
    /// </para>
    /// </remarks>
    internal sealed class SerializeReferenceMissingListGuard : AssetModificationProcessor
    {
        // Snapshots captured from the pre-save (still-pristine) YAML, keyed by the asset path Unity is about to write.
        // Consumed once by the post-save pass and then dropped, so a later save re-snapshots from the then-current file.
        private static readonly Dictionary<string, List<Snapshot>> PendingByPath = new();

        // Fires with the file still holding its pre-save state; only reads the pristine YAML to snapshot at-risk
        // elements and queue the post-save repair — the returned set is never altered.
        private static string[] OnWillSaveAssets(string[] paths)
        {
            foreach (var path in paths)
            {
                if (string.IsNullOrEmpty(path)) continue;
                if (!SerializeReferenceYaml.IsCandidateAssetPath(path)) continue;

                var snapshots = SnapshotMissingArrayElements(path);
                if (snapshots.Count == 0) continue;

                PendingByPath[path] = snapshots;

                // Anchor the repair to the path (not a captured SerializedObject): it runs after Unity has written the
                // file, re-reads from disk and is a no-op unless the save actually collapsed the element.
                var captured = path;
                EditorApplication.delayCall += () => RestoreAfterSave(captured);
            }

            return paths;
        }

        // Captures every top-level missing array element — the only shape a list resize destroys — with the exact
        // RefIds entry text needed to re-materialise it after the save.
        private static List<Snapshot> SnapshotMissingArrayElements(string assetPath)
        {
            var result = new List<Snapshot>();

            var missing = SerializeReferenceYamlEditor.FindMissingReferences(assetPath, SerializeReferenceHelpers.StoredTypeResolves);
            if (missing.Count == 0) return result;

            foreach (var entry in missing)
            {
                if (!SerializeReferenceYamlEditor.TryFindTopLevelArrayElementForRid(assetPath, entry.FileId, entry.Rid, out var field, out var index))
                    continue; // a single field or nested pointer — not resized, so not at risk

                var elementPath = $"{field}.Array.data[{index}]";
                if (SerializeReferenceYamlEditor.TryReadArrayElementEntryBlock(assetPath, entry.FileId, elementPath, out _, out var entryLines))
                    result.Add(new Snapshot(entry.FileId, elementPath, entryLines));
            }

            return result;
        }

        // Re-reads the now-written file and restores any snapshotted element the save collapsed; an intact element is
        // a no-op (the restore declines a non-empty slot). Reimports once so the live object picks up the recovery.
        private static void RestoreAfterSave(string assetPath)
        {
            if (!PendingByPath.TryGetValue(assetPath, out var snapshots)) return;
            PendingByPath.Remove(assetPath);

            var restored = 0;
            foreach (var snapshot in snapshots)
            {
                if (SerializeReferenceYamlEditor.TryRestoreArrayElementReference(assetPath, snapshot.FileId, snapshot.ElementPath, snapshot.EntryLines))
                    restored++;
            }

            if (restored == 0) return;

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            UnityEngine.Debug.Log($"[Aspid FastTools] Preserved {restored} missing reference(s) that a list resize would have dropped in '{assetPath}'.");
        }

        private readonly struct Snapshot
        {
            public readonly long FileId;
            public readonly string ElementPath;
            public readonly List<string> EntryLines;

            public Snapshot(long fileId, string elementPath, List<string> entryLines)
            {
                FileId = fileId;
                ElementPath = elementPath;
                EntryLines = entryLines;
            }
        }
    }
}
