using System;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Resolves a stored (no longer loadable) managed-reference type identity to the type that declares it as its
    /// old name via <c>[MovedFrom]</c> — the authoritative rename signal. Unity itself migrates such references in
    /// memory when the asset loads, but the YAML on disk keeps the old name until the asset is re-saved, so a
    /// YAML-level scan keeps seeing the stale identity. This resolver is what lets those entries read as a pending
    /// <b>migration</b> instead of a breakage, and powers the bulk <b>Migrate all</b> that bakes the rename into the
    /// files (after which the attribute can be deleted from code).
    /// </summary>
    internal static class SerializeReferenceMovedFromResolver
    {
        // Stored-type key → the single authoritative target (null = no claimant, or an ambiguous pair). [MovedFrom]
        // declarations only change with a recompile, and a recompile resets this dictionary with the domain — so no
        // invalidation is needed. Negative results are cached too: the breakage paths probe every unresolved entry.
        private static readonly Dictionary<string, Type> Cache = new(StringComparer.Ordinal);

        /// <summary>
        /// True when exactly one loaded managed-reference-eligible type declares a <c>[MovedFrom]</c> whose recorded
        /// old identity matches <paramref name="stored"/>. Two types claiming the same old identity make the rename
        /// non-authoritative, so the resolver refuses to pick between them and returns <see langword="false"/>.
        /// </summary>
        public static bool TryResolve(ManagedTypeName stored, out Type target)
        {
            target = null;
            if (string.IsNullOrEmpty(stored.Class)) return false;

            // A stored closed-generic identity ("Modifier`1[[…]]") can never migrate authoritatively — the only
            // possible claimant is an arity-stripped name collision, a guess rather than a rename. Those stay
            // with the scored Smart Fix path.
            if (stored.Class.IndexOf('`') >= 0) return false;

            var key = SerializeReferenceHelpers.StoredTypeKey(stored);
            if (Cache.TryGetValue(key, out target)) return target is not null;

            target = ResolveUncached(stored);
            Cache[key] = target;
            return target is not null;
        }

        // Scans only the types that actually carry Unity's [MovedFrom] (TypeCache is index-backed, so this is cheap)
        // — only Unity's own attribute is authoritative, since it is the one Unity's serialization honours at load.
        private static Type ResolveUncached(ManagedTypeName stored)
        {
            var storedClass = NormalizeClassName(stored.Class);
            if (storedClass.Length == 0) return null;

            Type found = null;

            foreach (var candidate in TypeCache.GetTypesWithAttribute<MovedFromAttribute>())
            {
                if (!SerializeReferenceHelpers.IsAssignableManagedReference(candidate)) continue;
                if (!MatchesOldIdentity(candidate, stored, storedClass)) continue;

                if (found is not null && found != candidate) return null;
                found = candidate;
            }

            return found;
        }

        /// <summary>
        /// True when <paramref name="candidate"/> carries a <c>[MovedFrom]</c> whose recorded old identity matches the
        /// stored type's class (and, when declared, namespace / assembly). The attribute's backing data is not public
        /// API, so every member is read reflectively and any failure is treated as "no match" rather than throwing.
        /// <paramref name="storedClass"/> is the pre-normalized stored class name (see <see cref="NormalizeClassName"/>).
        /// </summary>
        public static bool MatchesOldIdentity(Type candidate, ManagedTypeName stored, string storedClass)
        {
            try
            {
                foreach (var attribute in candidate.GetCustomAttributes(inherit: false))
                {
                    var attributeType = attribute.GetType();
                    if (attributeType.Name != "MovedFromAttribute") continue;

                    var data = attributeType
                        .GetField("data", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        ?.GetValue(attribute);
                    if (data is null) continue;

                    var dataType = data.GetType();

                    // When a "*HasChanged" flag is false the old value equals the current type's value, so fall back to
                    // the candidate's own identity for that slot — exactly how Unity's updater resolves the old name.
                    var oldClass = NormalizeClassName(ReadMovedSlot(dataType, data, "className", "classHasChanged", candidate.Name));
                    if (!string.Equals(oldClass, storedClass, StringComparison.Ordinal)) continue;

                    if (!string.IsNullOrEmpty(stored.Namespace))
                    {
                        var oldNamespace = ReadMovedSlot(dataType, data, "nameSpace", "nameSpaceHasChanged", candidate.Namespace);
                        if (!string.Equals(oldNamespace ?? string.Empty, stored.Namespace, StringComparison.Ordinal)) continue;
                    }

                    if (!string.IsNullOrEmpty(stored.Assembly))
                    {
                        var oldAssembly = ReadMovedSlot(dataType, data, "assembly", "assemblyHasChanged", candidate.Assembly.GetName().Name);
                        if (!string.Equals(oldAssembly ?? string.Empty, stored.Assembly, StringComparison.Ordinal)) continue;
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                // The attribute data struct is not public API; any reflection failure simply means "no MovedFrom match".
            }

            return false;
        }

        /// <summary>
        /// Strips Unity's generic-arity/expansion decoration from a stored or live class name so both sides compare on
        /// the bare simple name: <c>"Modifier`1[[System.Single, mscorlib]]"</c> and <c>"Modifier`1"</c> both reduce to
        /// <c>"Modifier"</c>; a nested type (<c>"Outer/Inner"</c> or <c>"Outer+Inner"</c>) reduces to its innermost segment.
        /// </summary>
        public static string NormalizeClassName(string className)
        {
            if (string.IsNullOrEmpty(className)) return string.Empty;

            // Drop a bracketed generic expansion ("Foo`1[[...]]") and then the backtick-arity suffix ("Foo`1").
            var bracket = className.IndexOf('[');
            if (bracket >= 0) className = className[..bracket];

            var tick = className.IndexOf('`');
            if (tick >= 0) className = className[..tick];

            var slash = className.LastIndexOfAny(NestedSeparators);
            if (slash >= 0) className = className[(slash + 1)..];

            return className.Trim();
        }

        private static readonly char[] NestedSeparators = { '/', '+' };

        // Reads a string slot of MovedFromAttributeData, returning the recorded old value when its companion
        // "*HasChanged" flag is true and the current type's value otherwise (a slot that did not change).
        private static string ReadMovedSlot(Type dataType, object data, string valueField, string changedField, string current)
        {
            var changed = dataType.GetField(changedField, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var hasChanged = changed?.GetValue(data) is true;
            if (!hasChanged) return current;

            var value = dataType.GetField(valueField, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return value?.GetValue(data) as string;
        }
    }
}
