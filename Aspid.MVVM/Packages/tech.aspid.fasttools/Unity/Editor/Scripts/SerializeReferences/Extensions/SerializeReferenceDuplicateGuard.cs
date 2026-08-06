using System;
using UnityEditor;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Auto-de-aliases freshly duplicated <c>[SerializeReference]</c> list elements. When the user duplicates an array
    /// element (context-menu <i>Duplicate</i>, Ctrl+D) or adds one with the list <c>+</c> button, Unity copies the
    /// source element's managed-reference <c>rid</c>, so two elements end up backed by a single instance and editing one
    /// silently edits the other. This guard watches the live <see cref="SerializedObject"/>: per (target, array path) it
    /// keeps a snapshot of <c>index → rid</c> and, when a <i>new</i> same-array alias appears between observations, it
    /// replaces the later (appended / higher-index) element with an independent clone via the same
    /// <see cref="SerializeReferenceHelpers.CreateInstancePreservingData"/> machinery the Make-unique flow uses,
    /// registered as a single Undo step.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Detection is purely live-state (<see cref="SerializedProperty.managedReferenceId"/> / value), so it works for
    /// scene objects, Prefab Mode, and saved assets alike — no YAML is read. The fix is silent by product decision: no
    /// notice, no dialog. Undo reverts to the aliased state; after an Undo/Redo the snapshots are resynced rather than
    /// re-evaluated, so a restored alias is never re-fixed.
    /// </para>
    /// <para>
    /// Intentional cross-<i>field</i> sharing is out of scope — that is handled by the existing shared-reference notice.
    /// This guard only acts on same-array aliasing that <i>appears</i> while the inspector is alive; pre-existing aliases
    /// present on the first observation of an array (inspector just opened, domain reload) are recorded, never fixed.
    /// A fix is considered only when the array <i>grew</i> since the last observation and the duplicated rid's
    /// occurrence count rose with it — the signature of an actual duplicate-element operation. Same-size observations
    /// (a reorder) and shrunk ones (a removal) only resync the snapshot, so shuffling or deleting around a pre-existing
    /// alias never de-aliases it.
    /// </para>
    /// </remarks>
    internal static class SerializeReferenceDuplicateGuard
    {
        // A managed reference with no value reports RefIdNull (-2); a missing-type one reports RefIdUnknown (-1). Only
        // ids >= 0 are real instances that can alias — the rest are excluded from the index → rid map.
        private const long FirstValidReferenceId = 0;

        // Unity's array-element path marker (e.g. "_slots.Array.data[3]"). The text before it is the parent array path.
        private const string ArrayElementMarker = ".Array.data[";

        // Caps the live cache. On overflow the whole cache is dropped: a re-snapshot never auto-fixes, so at worst a
        // not-yet-observed fix is lost — the conservative direction.
        private const int MaxTrackedArrays = 512;

        // Per (target instance id, parent array path) snapshot of the last observed index → rid layout. Static, so it is
        // cleared automatically on domain reload; dead-target entries are pruned lazily on access and on Undo resync.
        private static readonly Dictionary<ArrayKey, Snapshot> _snapshots = new();

        // Arrays whose de-alias fix is queued for the next editor tick. Guards against re-detecting (the layout still
        // shows the alias until the deferred fix runs) and re-scheduling on every intervening repaint.
        private static readonly HashSet<ArrayKey> _pending = new();

        private static bool _undoHooked;

        /// <summary>
        /// Observes <paramref name="elementProperty"/> (an element of a <c>[SerializeReference]</c> array) and, when it
        /// detects that the element is part of a freshly created same-array duplicate, <i>schedules</i> a fix that
        /// replaces the later element with an independent clone (single Undo step) and returns <see langword="true"/>.
        /// The mutation runs on the next editor tick (<see cref="EditorApplication.delayCall"/>), never inside the
        /// caller's draw/binding pass, so the inspector's live property iteration is not disturbed mid-frame; the field's
        /// own property tracking re-renders once the fix lands. Returns <see langword="false"/> for the no-op fast paths
        /// (not an array element, multi-object edit, first observation, pre-existing alias, or a fix already pending).
        /// Cheap on the unchanged path: an array-size + rolling-hash compare gates the full map rebuild, so it is safe to
        /// call from IMGUI's per-frame repaint.
        /// </summary>
        public static bool Observe(SerializedProperty elementProperty)
        {
            if (!SerializeReferenceSettings.AutoDeAliasEnabled) return false;
            if (elementProperty is null) return false;
            if (elementProperty.propertyType != SerializedPropertyType.ManagedReference) return false;

            // Multi-object editing is owned by the per-target apply path; the live SerializedObject here walks only the
            // first target, so the guard cannot reason about the others — skip it entirely (conservative).
            if (elementProperty.serializedObject.isEditingMultipleObjects) return false;

            if (!TryGetArrayPath(elementProperty.propertyPath, out var arrayPath)) return false;

            EnsureUndoHook();

            var serializedObject = elementProperty.serializedObject;
            var target = serializedObject.targetObject;
            if (target == null) return false;

            var key = new ArrayKey(target.GetInstanceID(), arrayPath);

            if (_pending.Contains(key)) return false;

            var arrayProperty = serializedObject.FindProperty(arrayPath);
            if (arrayProperty is null || !arrayProperty.isArray) return false;

            // Fast no-change gate: a size + order-sensitive rolling hash of the element rids skips the index → rid
            // rebuild and alias diffing on the IMGUI per-repaint path, allocating no per-observation collection.
            var size = arrayProperty.arraySize;
            var signature = ComputeSignature(arrayProperty, size);

            if (_snapshots.TryGetValue(key, out var snapshot) &&
                snapshot.Size == size && snapshot.Signature == signature)
                return false;

            var current = BuildMap(arrayProperty, size);

            // First time we see this array (fresh inspector, domain reload): record the layout but never auto-fix —
            // pre-existing same-array aliases keep the existing shared notice instead.
            if (snapshot is null)
            {
                Store(key, size, signature, current);
                return false;
            }

            // Only a growth of EXACTLY ONE element (Ctrl+D / Duplicate / list +) can be a fresh duplicate. Same-size /
            // shrunk layouts are reorders/removals; a multi-element growth is a bulk restore (Paste Component Values,
            // Revert to Prefab, presets) that can legitimately bring back an INTENTIONAL alias — those only resync.
            if (size == snapshot.Size + 1 &&
                TryFindFreshDuplicate(snapshot.Map, current, out var duplicateIndex))
            {
                // Keep the baseline (do not advance it to the aliased layout): once the fix lands the element reads as
                // unique against it, while a further duplicate made during the pending window is still caught as fresh.
                ScheduleFix(key, target, arrayPath, duplicateIndex);
                return true;
            }

            Store(key, size, signature, current);
            return false;
        }

        // The mutation must not run inside the drawer's draw/binding pass — writing the SerializedObject mid-iteration
        // can invalidate the inspector's active property walk — so it is deferred to the next editor tick.
        private static void ScheduleFix(ArrayKey key, Object target, string arrayPath, int duplicateIndex)
        {
            _pending.Add(key);
            EditorApplication.delayCall += () =>
            {
                // The fix re-verifies the alias on a fresh read, so a stale schedule is a safe no-op.
                _pending.Remove(key);
                MakeElementUnique(target, arrayPath, duplicateIndex);
            };
        }

        // A fresh instance gets a new managedReferenceId on assignment, breaking the alias; single Undo step.
        private static void MakeElementUnique(Object target, string arrayPath, int duplicateIndex)
        {
            if (target == null) return;

            using var serializedObject = new SerializedObject(target);
            var arrayProperty = serializedObject.FindProperty(arrayPath);
            if (arrayProperty is null || !arrayProperty.isArray) return;
            if (duplicateIndex < 0 || duplicateIndex >= arrayProperty.arraySize) return;

            var element = arrayProperty.GetArrayElementAtIndex(duplicateIndex);
            if (element.propertyType != SerializedPropertyType.ManagedReference) return;

            var current = element.managedReferenceValue;
            if (current is null) return;

            // Re-verify the alias still holds on this fresh read — the layout may have changed between the scheduling
            // tick and now (another Undo, a manual edit) — so a stale schedule never clobbers an already-unique element.
            if (!SharesReferenceWithEarlierElement(arrayProperty, duplicateIndex, element.managedReferenceId)) return;

            // Deep copy: the silent de-alias must hand the duplicate its own nested [SerializeReference] children
            // too — a shallow clone would keep the copy's subtree aliased to the original element's.
            element.managedReferenceValue = SerializeReferenceHelpers.CloneManagedReferenceGraph(current);
            serializedObject.ApplyModifiedProperties();

            // Same-frame IMGUI repaints must not read the pre-split alias memo (it is keyed by frame, not content).
            SerializeReferenceHelpers.InvalidateSharedReferenceCache();
        }

        private static bool SharesReferenceWithEarlierElement(SerializedProperty arrayProperty, int index, long rid)
        {
            if (rid < FirstValidReferenceId) return false;

            for (var i = 0; i < index; i++)
            {
                var other = arrayProperty.GetArrayElementAtIndex(i);
                if (other.propertyType == SerializedPropertyType.ManagedReference && other.managedReferenceId == rid)
                    return true;
            }

            return false;
        }

        // A fresh duplicate is the LATER element of a pair whose (index, rid) binding is new since the snapshot AND
        // whose rid occurs more times than before — the count gate keeps a reorder of a pre-existing alias (new
        // binding, unchanged count) from reading as fresh. The lowest such index is the appended/duplicated copy.
        private static bool TryFindFreshDuplicate(
            IReadOnlyDictionary<int, long> previous,
            IReadOnlyDictionary<int, long> current,
            out int duplicateIndex)
        {
            duplicateIndex = -1;

            var lowestIndexByRid = new Dictionary<long, int>();
            foreach (var pair in current)
                if (!lowestIndexByRid.TryGetValue(pair.Value, out var existing) || pair.Key < existing)
                    lowestIndexByRid[pair.Value] = pair.Key;

            var previousCount = CountByRid(previous);
            var currentCount = CountByRid(current);

            var best = int.MaxValue;
            foreach (var pair in current)
            {
                var index = pair.Key;
                var rid = pair.Value;

                // Only the later element of an alias pair is a candidate (an earlier owner of the rid keeps its instance).
                if (lowestIndexByRid[rid] >= index) continue;

                // Skip aliases that already existed: a binding present unchanged in the previous snapshot is intentional
                // (or pre-existing) sharing, not a fresh duplicate. A new index, or a changed rid at this index, is fresh.
                if (previous.TryGetValue(index, out var previousRid) && previousRid == rid) continue;

                // Require the rid to have multiplied since the snapshot. A reorder that merely moved a pre-existing
                // alias into a new binding leaves the rid's count unchanged and so is not a fresh duplicate.
                previousCount.TryGetValue(rid, out var before);
                if (currentCount[rid] <= before) continue;

                if (index < best) best = index;
            }

            if (best == int.MaxValue) return false;
            duplicateIndex = best;
            return true;
        }

        private static Dictionary<long, int> CountByRid(IReadOnlyDictionary<int, long> map)
        {
            var counts = new Dictionary<long, int>(map.Count);
            foreach (var pair in map)
                counts[pair.Value] = counts.TryGetValue(pair.Value, out var existing) ? existing + 1 : 1;

            return counts;
        }

        private static Dictionary<int, long> BuildMap(SerializedProperty arrayProperty, int size)
        {
            var map = new Dictionary<int, long>(size);
            for (var i = 0; i < size; i++)
            {
                var element = arrayProperty.GetArrayElementAtIndex(i);
                if (element.propertyType != SerializedPropertyType.ManagedReference) continue;

                var rid = element.managedReferenceId;
                if (rid >= FirstValidReferenceId) map[i] = rid;
            }

            return map;
        }

        // Cheap order-sensitive rolling hash of the element rids. Siblings are walked with a single SerializedProperty
        // (Next(enterChildren: false)), so the no-change gate allocates one property per call rather than one per element.
        private static int ComputeSignature(SerializedProperty arrayProperty, int size)
        {
            unchecked
            {
                var hash = 17;
                if (size == 0) return hash;

                var element = arrayProperty.GetArrayElementAtIndex(0);
                for (var i = 0; i < size; i++)
                {
                    var rid = element.propertyType == SerializedPropertyType.ManagedReference
                        ? element.managedReferenceId
                        : long.MinValue;
                    hash = hash * 31 + rid.GetHashCode();

                    if (i + 1 < size && !element.Next(enterChildren: false)) break;
                }

                return hash;
            }
        }

        private static void Store(ArrayKey key, int size, int signature, Dictionary<int, long> map)
        {
            // On overflow drop the whole cache, with Pending alongside so the two never desync: a re-snapshot never
            // auto-fixes and a queued fix re-verifies before applying, so at worst a fix is cancelled, never mis-applied.
            if (!_snapshots.ContainsKey(key) && _snapshots.Count >= MaxTrackedArrays)
            {
                _snapshots.Clear();
                _pending.Clear();
            }

            _snapshots[key] = new Snapshot(size, signature, map);
        }

        // The parent array path of an element path. Nested arrays resolve to the innermost array (last marker) — the
        // array this element directly belongs to.
        private static bool TryGetArrayPath(string elementPath, out string arrayPath)
        {
            arrayPath = null;
            if (string.IsNullOrEmpty(elementPath)) return false;

            var marker = elementPath.LastIndexOf(ArrayElementMarker, StringComparison.Ordinal);
            if (marker < 0) return false;

            // Only the array entry itself ("...Array.data[N]") carries the element's own managed reference — a
            // sub-field path ("...Array.data[N]._weapon") must not match.
            var close = elementPath.IndexOf(']', marker + ArrayElementMarker.Length);
            if (close < 0 || close != elementPath.Length - 1) return false;

            arrayPath = elementPath[..marker];
            return arrayPath.Length > 0;
        }

        private static void EnsureUndoHook()
        {
            if (_undoHooked) return;
            _undoHooked = true;
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }

        private static void OnUndoRedoPerformed()
        {
            // An Undo can revert to a state a fix was scheduled against, or restore an intentional alias; dropping both
            // makes the next observation re-record (never auto-fix), so a reverted alias is recorded, not re-fixed.
            _snapshots.Clear();
            _pending.Clear();
        }

        private readonly struct ArrayKey : IEquatable<ArrayKey>
        {
            private readonly int _targetInstanceId;
            private readonly string _arrayPath;

            public ArrayKey(int targetInstanceId, string arrayPath)
            {
                _targetInstanceId = targetInstanceId;
                _arrayPath = arrayPath;
            }

            public bool Equals(ArrayKey other) =>
                _targetInstanceId == other._targetInstanceId && _arrayPath == other._arrayPath;

            public override bool Equals(object obj) => obj is ArrayKey other && Equals(other);

            public override int GetHashCode()
            {
                unchecked
                {
                    return (_targetInstanceId * 397) ^ (_arrayPath?.GetHashCode() ?? 0);
                }
            }
        }

        private sealed class Snapshot
        {
            public Snapshot(int size, int signature, Dictionary<int, long> map)
            {
                Size = size;
                Signature = signature;
                Map = map;
            }

            public int Size { get; }
            public int Signature { get; }
            public Dictionary<int, long> Map { get; }
        }
    }
}
