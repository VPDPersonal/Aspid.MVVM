using System;
using UnityEngine;
using UnityEditor;
using Aspid.FastTools.Editors;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Every single-entry repair the Asset References graph offers, without any of its UI: assigning, re-pointing and
    /// clearing one managed reference, and dropping one orphaned payload.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Two edit routes, picked by what the entry is rather than by the caller. A healthy or empty slot goes through
    /// <see cref="SerializedProperty.managedReferenceValue"/> (<see cref="ApplyLive"/>), so Unity creates, rewrites or
    /// removes the <c>RefIds</c> entry exactly as the Inspector would. A <em>missing</em> reference cannot be
    /// reassigned through that API at all, so it is edited by rewriting the YAML in place
    /// (<see cref="ApplyFix"/> / <see cref="ClearReference"/> / <see cref="TryClearOrphan"/>) — which is also why those
    /// three confirm first, cannot be undone through Unity's undo stack, and refuse to run against an asset whose open
    /// in-memory copy would clobber the write (see <see cref="SerializeReferenceOpenCopyGuard"/>).
    /// </para>
    /// <para>
    /// Each entry point reports whether anything actually changed; re-rendering the graph afterwards is the caller's
    /// concern.
    /// </para>
    /// </remarks>
    internal static class SerializeReferenceGraphEditor
    {
        /// <summary>
        /// Re-points a missing reference at <paramref name="assemblyQualifiedName"/> by rewriting the stored type name
        /// in the YAML, keeping the orphaned payload. An empty name clears the reference instead (see
        /// <see cref="ClearReference"/>). Returns whether the file changed.
        /// </summary>
        public static bool ApplyFix(string assetPath, long fileId, long rid, string assemblyQualifiedName)
        {
            // <None> emits an empty name: clear the reference (dropping the broken payload) rather than letting it
            // fall through to the null-type guard below as a silent no-op.
            if (string.IsNullOrEmpty(assemblyQualifiedName)) return ClearReference(assetPath, fileId, rid);

            if (SerializeReferenceOpenCopyGuard.BlockedByOpenCopy(assetPath)) return false;

            var type = Type.GetType(assemblyQualifiedName, throwOnError: false);
            if (type is null) return false;

            // Rewrite only the captured file id's document: a rid is unique within a document but can collide across
            // documents, so looping the asset's documents could rewrite a healthy reference that shares the rid.
            if (!SerializeReferenceYamlEditor.TryRewriteType(assetPath, fileId, rid, ManagedTypeName.FromType(type)))
                return false;

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            SerializeReferenceRepairSuggestions.ClearCache();
            return true;
        }

        /// <summary>
        /// Resets a missing reference to <c>&lt;None&gt;</c> in the YAML: nulls every pointer to Unity's null sentinel
        /// (-2) and drops the <c>RefIds</c> entry — exactly what Unity writes for a cleared field. Confirmed and not
        /// undoable; the broken payload is discarded. Returns whether the file changed.
        /// </summary>
        public static bool ClearReference(string assetPath, long fileId, long rid)
        {
            if (SerializeReferenceOpenCopyGuard.BlockedByOpenCopy(assetPath)) return false;

            // Name how many fields the clear will null so an aliased reference doesn't silently take down siblings.
            // A non-positive count means the pointers couldn't be located — use the unnumbered wording, not "0 fields".
            var fieldCount = SerializeReferenceYamlEditor.CountPointersTo(assetPath, fileId, rid);
            var pointerLine = fieldCount switch
            {
                1 => "This nulls the 1 field pointing at it",
                > 1 => $"This reference is aliased across {fieldCount} fields — clearing it nulls every one of them",
                _ => "This nulls every field pointing at it",
            };

            if (!EditorUtility.DisplayDialog(
                    "Clear Reference",
                    $"Reset this managed reference (rid {rid}) to <None> in\n{assetPath}?\n\n" +
                    $"{pointerLine} and discards its stored data. It edits the asset file directly and cannot be undone.",
                    "Clear", "Cancel"))
                return false;

            if (!SerializeReferenceYamlEditor.TryNullReference(assetPath, fileId, rid)) return false;

            // The forced import lets the index invalidator patch this one asset surgically — a full ClearCache here
            // would dump the whole warm index and put Project References back on its modal first-scan.
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            SerializeReferenceRepairSuggestions.ClearCache();
            return true;
        }

        /// <summary>
        /// Drops a dangling <c>RefIds</c> entry no field points at, after confirming. Returns whether the file changed.
        /// </summary>
        /// <param name="staleRescan">
        /// The fresh scan proving the rid is no longer an orphan, when the on-screen graph turned out to be stale;
        /// <see langword="null"/> otherwise. Re-render from it rather than reading the unchanged file a second time.
        /// </param>
        public static bool TryClearOrphan(string assetPath, long fileId, long rid, out List<ReferenceGraphDocument> staleRescan)
        {
            staleRescan = null;

            if (SerializeReferenceOpenCopyGuard.BlockedByOpenCopy(assetPath)) return false;

            if (!EditorUtility.DisplayDialog(
                    "Drop Orphaned Entry",
                    $"Remove the orphaned managed-reference entry (rid {rid}) from\n{assetPath}?\n\n" +
                    "This edits the asset file directly and cannot be undone.",
                    "Remove", "Cancel"))
                return false;

            // Guard against a stale graph: confirm the rid is still an orphan against a fresh scan before deleting.
            var fresh = SerializeReferenceGraphScanner.Build(assetPath);
            foreach (var document in fresh)
            {
                if (document.FileId != fileId || !document.Orphans.Contains(rid)) continue;

                if (!SerializeReferenceYamlEditor.TryRemoveEntry(assetPath, fileId, rid)) return false;

                // Surgical index patch via the import invalidator, not a full ClearCache (see ClearReference).
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                SerializeReferenceRepairSuggestions.ClearCache();
                return true;
            }

            staleRescan = fresh;
            return false;
        }

        /// <summary>
        /// Edits a healthy or empty slot through <see cref="SerializedProperty.managedReferenceValue"/>, so Unity
        /// creates / rewrites / removes the <c>RefIds</c> entry exactly as the Inspector would. An empty name clears
        /// the slot to <c>&lt;None&gt;</c>. Returns whether the asset changed.
        /// </summary>
        /// <remarks>
        /// The asset is saved to disk so the disk-read graph reflects the edit on rescan; a path the API cannot reach
        /// is reported through a dialog and skipped.
        /// </remarks>
        public static bool ApplyLive(string assetPath, long fileId, string graphPath, string assemblyQualifiedName)
        {
            var type = string.IsNullOrEmpty(assemblyQualifiedName)
                ? null
                : Type.GetType(assemblyQualifiedName, throwOnError: false);

            // A non-empty name that fails to load is an unresolved pick, not a clear — leave the slot untouched rather
            // than silently nulling it.
            if (!string.IsNullOrEmpty(assemblyQualifiedName) && type is null) return false;

            if (!TryResolveLiveProperty(assetPath, fileId, graphPath, out var serializedObject, out var property))
            {
                EditorUtility.DisplayDialog(
                    "Edit Reference",
                    "This slot cannot be edited here — its field is not reachable through the serialization API " +
                    "(it may be an orphan, live in a scene, or sit under a missing parent). Edit it in the Inspector " +
                    "or repair its parent first.",
                    "OK");
                return false;
            }

            using (serializedObject)
            {
                var previous = property.managedReferenceValue;
                // type == null clears to <None>; a concrete type carries over the previous value's matching fields.
                property.SetManagedReferenceAndApply(SerializeReferenceHelpers.CreateInstancePreservingData(type, previous));
                property.isExpanded = type is not null;

                var target = serializedObject.targetObject;
                EditorUtility.SetDirty(target);
                PersistEdit(assetPath, target);
            }

            // PersistEdit's save triggers the import that lets the index invalidator patch this asset surgically —
            // no full ClearCache (see ClearReference).
            SerializeReferenceRepairSuggestions.ClearCache();
            SerializeReferenceYamlProbeCache.ClearCache();
            return true;
        }

        /// <summary>
        /// Writes an assembly-qualified type name into the backing string of a required <c>[TypeSelector]</c> field —
        /// the one required shape the managed-reference routes above cannot reach, since a string /
        /// <c>SerializableType</c> field is never threaded into <c>RefIds</c>. Returns whether the asset changed.
        /// </summary>
        public static bool ApplyRequiredString(GateViolation violation, string assemblyQualifiedName)
        {
            // A non-empty name that fails to load is an unresolved pick, not a clear — leave the field untouched.
            // <None> (empty) writes an empty name: for a required field that just keeps the violation visible.
            if (!string.IsNullOrEmpty(assemblyQualifiedName) &&
                Type.GetType(assemblyQualifiedName, throwOnError: false) is null)
                return false;

            if (!TryResolveRequiredStringProperty(violation, out var serializedObject, out var property))
            {
                EditorUtility.DisplayDialog(
                    "Assign Required Type",
                    "This field cannot be edited here — it is not reachable through the serialization API. " +
                    "Edit it in the Inspector instead.",
                    "OK");
                return false;
            }

            using (serializedObject)
            {
                property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);

                var target = serializedObject.targetObject;
                EditorUtility.SetDirty(target);
                PersistEdit(violation.AssetPath, target);
            }

            SerializeReferenceYamlProbeCache.ClearCache();
            return true;
        }

        /// <summary>
        /// Resolves the live document at <paramref name="fileId"/> and the managed-reference property at
        /// <paramref name="graphPath"/>. The caller disposes the returned <see cref="SerializedObject"/>.
        /// </summary>
        /// <returns>
        /// <see langword="false"/> for a path the API cannot reach — an empty path, a scene asset, or a field under a
        /// missing / null parent.
        /// </returns>
        public static bool TryResolveLiveProperty(string assetPath, long fileId, string graphPath,
            out SerializedObject serializedObject, out SerializedProperty property)
        {
            serializedObject = null;
            property = null;

            if (string.IsNullOrEmpty(graphPath)) return false;
            // Scenes are not loadable through LoadAllAssetsAtPath (see SerializeReferenceHelpers.IsScene).
            if (SerializeReferenceHelpers.IsScene(assetPath)) return false;

            return TryResolveProperty(assetPath, fileId, ToSerializedPropertyPath(graphPath),
                SerializedPropertyType.ManagedReference, out serializedObject, out property);
        }

        /// <summary>
        /// Resolves the live document at the violation's file id and the string property at its field path. The caller
        /// disposes the returned <see cref="SerializedObject"/>.
        /// </summary>
        /// <remarks>
        /// The violation's field path is already a <see cref="SerializedProperty"/> path (the gate scanner records
        /// <c>iterator.propertyPath</c> verbatim), so unlike <see cref="TryResolveLiveProperty"/> no graph-path
        /// conversion applies. Returns <see langword="false"/> for a scene asset (not object-loadable).
        /// </remarks>
        public static bool TryResolveRequiredStringProperty(GateViolation violation,
            out SerializedObject serializedObject, out SerializedProperty property)
        {
            serializedObject = null;
            property = null;

            if (SerializeReferenceHelpers.IsScene(violation.AssetPath)) return false;

            return TryResolveProperty(violation.AssetPath, violation.FileId, violation.FieldPath,
                SerializedPropertyType.String, out serializedObject, out property);
        }

        /// <summary>
        /// Converts a graph field path's <c>"name[i]"</c> list indices into Unity's <c>"name.Array.data[i]"</c> form —
        /// the inverse of the <c>.Array.data</c> stripping <see cref="SerializeReferenceYamlEditor"/> does when it
        /// normalises a property path.
        /// </summary>
        public static string ToSerializedPropertyPath(string graphPath) =>
            Regex.Replace(graphPath, @"\[(\d+)\]", ".Array.data[$1]");

        // Shared lookup behind both resolvers: find the sub-asset carrying fileId, then the property at propertyPath,
        // and accept it only when it is of the expected kind.
        private static bool TryResolveProperty(string assetPath, long fileId, string propertyPath,
            SerializedPropertyType expected, out SerializedObject serializedObject, out SerializedProperty property)
        {
            serializedObject = null;
            property = null;

            foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(assetPath))
            {
                if (obj == null) continue;
                if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out _, out var id) || id != fileId) continue;

                var serialized = new SerializedObject(obj);
                var found = serialized.FindProperty(propertyPath);
                if (found is not null && found.propertyType == expected)
                {
                    serializedObject = serialized;
                    property = found;
                    return true;
                }

                // The document matched but the path did not resolve to the expected kind — no other document shares
                // this file id, so bail rather than scan on.
                serialized.Dispose();
                return false;
            }

            return false;
        }

        // A prefab component edit does not reliably flush through the generic asset-dirty path (the prefab pipeline
        // owns its serialization), so prefabs save via SavePrefabAsset on the in-memory root; anything else via
        // SaveAssetIfDirty.
        private static void PersistEdit(string assetPath, Object target)
        {
            var prefabRoot = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefabRoot != null) PrefabUtility.SavePrefabAsset(prefabRoot);
            else AssetDatabase.SaveAssetIfDirty(target);
        }
    }
}
