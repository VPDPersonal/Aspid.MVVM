using System;
using System.Text;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Completes a "Create new script" flow across the domain reload that the new <c>.cs</c> triggers. The pending
    /// (target, propertyPath, expected type) is parked in <see cref="SessionState"/> before the reload and resolved on
    /// a later load once the script has compiled — assigning a fresh instance of the new type to the field.
    /// </summary>
    /// <remarks>
    /// The assignment can outlive several reloads: a stub may fail to compile, or the new assembly may register only on
    /// a later reload than the one whose <see cref="EditorApplication.delayCall"/> fires first. Unresolved entries are
    /// therefore <b>re-persisted</b> and retried on every subsequent load instead of being dropped. Only the
    /// <i>type-not-resolved</i> reason — the one a reload can actually fix — spends the cross-reload budget and is
    /// abandoned (with a warning) after <see cref="MaxResolveAttempts"/> loads; an entry whose <i>target is merely not
    /// loaded</i> (its scene/asset is closed) waits indefinitely without spending the budget, since no reload count can
    /// fix that. Provably-dead entries (malformed id, path no longer a managed reference) are dropped silently. Across
    /// the same load a small bounded number of in-session re-arms catches an assembly that lands a tick late without a
    /// fresh reload.
    /// </remarks>
    internal static class SerializeReferencePendingAssignment
    {
        public const string Key = "Aspid.FastTools.SerializeReference.PendingAssignment";
        private const char EntrySeparator = '\n';
        private const char FieldSeparator = '|';

        /// <summary>
        /// Cross-reload backstop: a still-unresolved entry is dropped (with a warning) after this many loads.
        /// </summary>
        public const int MaxResolveAttempts = 32;

        /// <summary>
        /// Per-load belt-and-suspenders: how many extra <c>delayCall</c> passes to arm for a late same-load load.
        /// </summary>
        public const int MaxInSessionRetries = 3;

        // Re-arms left for the current load; reset by Hook on every domain reload (static state does not survive a reload).
        private static int _inSessionRetriesLeft;

        /// <summary>
        /// Parks an assignment to complete after a later domain reload (when the new type compiles).
        /// </summary>
        public static void Enqueue(UnityEngine.Object target, string propertyPath, string fullTypeName)
        {
            if (target == null || string.IsNullOrEmpty(propertyPath) || string.IsNullOrEmpty(fullTypeName)) return;

            var globalId = GlobalObjectId.GetGlobalObjectIdSlow(target).ToString();
            var entry = new Entry(globalId, propertyPath, fullTypeName, attempts: 0);

            var queue = Decode(SessionState.GetString(Key, string.Empty));
            Merge(queue, entry);
            SessionState.SetString(Key, Encode(queue));
        }

        [InitializeOnLoadMethod]
        private static void Hook()
        {
            _inSessionRetriesLeft = MaxInSessionRetries;
            EditorApplication.delayCall += ResolveAfterLoad;
        }

        // First pass after a load: a still-pending entry counts one resolve attempt against its cross-reload budget.
        private static void ResolveAfterLoad() => Resolve(countAttempt: true);

        // In-session re-arm: retries without spending the cross-reload budget (no new information has reloaded yet).
        private static void ResolveRetry() => Resolve(countAttempt: false);

        private static void Resolve(bool countAttempt)
        {
            // Re-arm another same-load pass (bounded) only while something is still pending, in case an assembly lands a
            // tick after this one without a fresh reload. The cross-reload budget is spent by ResolvePass, not here.
            if (ResolvePass(countAttempt) && _inSessionRetriesLeft > 0)
            {
                _inSessionRetriesLeft--;
                EditorApplication.delayCall += ResolveRetry;
            }
        }

        /// <summary>
        /// Runs one resolve pass over the persisted queue: applies what it can, re-persists what is still pending, erases
        /// the queue once nothing remains. Returns <c>true</c> while at least one entry is still pending.
        /// </summary>
        public static bool ResolvePass(bool countAttempt)
        {
            var raw = SessionState.GetString(Key, string.Empty);
            if (string.IsNullOrEmpty(raw)) return false;

            var pending = Decode(raw);
            var survivors = new List<Entry>(pending.Count);

            foreach (var entry in pending)
            {
                ApplyOutcome outcome;
                try
                {
                    outcome = TryApply(entry);
                }
                catch (Exception)
                {
                    // A resolved-but-incompatible type throws on assign; treat it as an unresolved attempt so the
                    // give-up cap bounds it and it never strands the entries queued after it.
                    outcome = ApplyOutcome.PendingUnresolved;
                }

                switch (outcome)
                {
                    case ApplyOutcome.Applied:
                    case ApplyOutcome.Dead:
                        break; // resolved or provably dead — drop from the queue.

                    case ApplyOutcome.PendingUnloaded:
                        // The owning scene/asset isn't open; a reload cannot fix that, so wait without spending the budget.
                        survivors.Add(entry);
                        break;

                    case ApplyOutcome.PendingUnresolved:
                        // The type has not compiled/loaded yet (or could not be applied). This is what the budget bounds.
                        var next = countAttempt ? entry.WithIncrementedAttempt() : entry;
                        if (next.Attempts >= MaxResolveAttempts) WarnDropped(next);
                        else survivors.Add(next);
                        break;
                }
            }

            if (survivors.Count == 0)
            {
                SessionState.EraseString(Key);
                return false;
            }

            SessionState.SetString(Key, Encode(survivors));
            return true;
        }

        private enum ApplyOutcome
        {
            Applied,
            Dead,
            PendingUnloaded,
            PendingUnresolved,
        }

        private static ApplyOutcome TryApply(Entry entry)
        {
            if (!GlobalObjectId.TryParse(entry.GlobalId, out var globalId)) return ApplyOutcome.Dead;

            var target = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(globalId);
            if (target == null) return ApplyOutcome.PendingUnloaded; // the scene/asset holding the field isn't open yet.

            var type = ResolveType(entry.FullTypeName);
            if (type is null) return ApplyOutcome.PendingUnresolved; // the new assembly has not compiled/loaded yet.

            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty(entry.PropertyPath);
            if (property is null || property.propertyType != SerializedPropertyType.ManagedReference) return ApplyOutcome.Dead;

            property.SetManagedReferenceAndApply(SerializeReferenceHelpers.CreateInstance(type));
            return ApplyOutcome.Applied;
        }

        private static void WarnDropped(Entry entry) =>
            Debug.LogWarning(
                $"[Aspid.FastTools] Dropping the pending \"Create new script\" assignment of '{entry.FullTypeName}' to " +
                $"'{entry.PropertyPath}' after {MaxResolveAttempts} domain reloads — its type never resolved (a compile " +
                "error or unsupported generated stub) or could not be applied to the field. Re-pick the type once it compiles.");

        private static Type ResolveType(string fullName)
        {
            var direct = Type.GetType(fullName, throwOnError: false);
            if (direct is not null) return direct;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(fullName, throwOnError: false);
                if (type is not null) return type;
            }

            return null;
        }

        // --------------------------------------------------------------------------------------------------------------
        // Wire model: SessionState stores newline-separated entries, each a pipe-separated
        // (globalId | propertyPath | fullTypeName | attempts) record — none of those fields can contain a pipe or
        // newline. Legacy three-field records (written before retry tracking) decode as attempts = 0.
        // --------------------------------------------------------------------------------------------------------------

        public readonly struct Entry : IEquatable<Entry>
        {
            public readonly string GlobalId;
            public readonly string PropertyPath;
            public readonly string FullTypeName;
            public readonly int Attempts;

            public Entry(string globalId, string propertyPath, string fullTypeName, int attempts)
            {
                GlobalId = globalId;
                PropertyPath = propertyPath;
                FullTypeName = fullTypeName;
                Attempts = attempts;
            }

            public Entry WithIncrementedAttempt() => new(GlobalId, PropertyPath, FullTypeName, Attempts + 1);

            /// <summary>
            /// True when both entries target the same field on the same object (ignores type and attempts).
            /// </summary>
            public bool SameTarget(Entry other) => GlobalId == other.GlobalId && PropertyPath == other.PropertyPath;

            public string Encode() =>
                $"{GlobalId}{FieldSeparator}{PropertyPath}{FieldSeparator}{FullTypeName}{FieldSeparator}{Attempts}";

            public static bool TryDecode(string line, out Entry entry)
            {
                entry = default;
                if (string.IsNullOrEmpty(line)) return false;

                var parts = line.Split(FieldSeparator);
                if (parts.Length < 3) return false;
                if (string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1]) || string.IsNullOrEmpty(parts[2])) return false;

                var attempts = parts.Length > 3 && int.TryParse(parts[3], out var parsed) && parsed > 0 ? parsed : 0;
                entry = new Entry(parts[0], parts[1], parts[2], attempts);
                return true;
            }

            public bool Equals(Entry other) =>
                GlobalId == other.GlobalId && PropertyPath == other.PropertyPath &&
                FullTypeName == other.FullTypeName && Attempts == other.Attempts;

            public override bool Equals(object obj) => obj is Entry other && Equals(other);

            public override int GetHashCode() => HashCode.Combine(GlobalId, PropertyPath, FullTypeName, Attempts);

            public override string ToString() => Encode();
        }

        public static List<Entry> Decode(string raw)
        {
            var entries = new List<Entry>();
            if (string.IsNullOrEmpty(raw)) return entries;

            foreach (var line in raw.Split(EntrySeparator))
                if (Entry.TryDecode(line, out var entry))
                    entries.Add(entry);

            return entries;
        }

        public static string Encode(IReadOnlyList<Entry> entries)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < entries.Count; i++)
            {
                if (i > 0) builder.Append(EntrySeparator);
                builder.Append(entries[i].Encode());
            }

            return builder.ToString();
        }

        /// <summary>
        /// Appends <paramref name="entry"/> to <paramref name="queue"/>, replacing any earlier entry that targets the
        /// same field on the same object — re-picking a field's "new script" supersedes the previous pending pick rather
        /// than queuing a second, stale assignment to the same path.
        /// </summary>
        public static void Merge(List<Entry> queue, Entry entry)
        {
            queue.RemoveAll(existing => existing.SameTarget(entry));
            queue.Add(entry);
        }
    }
}
