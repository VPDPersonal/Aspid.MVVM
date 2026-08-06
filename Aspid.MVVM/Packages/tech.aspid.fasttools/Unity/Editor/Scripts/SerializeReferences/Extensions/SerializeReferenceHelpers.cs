using System;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;
using Aspid.FastTools.Editors;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.Runtime.Serialization;
using Aspid.FastTools.Types.Editors;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Shared helpers for the <c>[TypeSelector]</c> drawer on <c>[SerializeReference]</c> fields: resolving the
    /// declared managed-reference field type, filtering candidate types, instantiating the selected type, and
    /// parsing Unity's managed-reference type-name format. The open-generic argument flow itself lives in
    /// the shared <see cref="Aspid.FastTools.Types.Editors.GenericTypeResolver"/> /
    /// <see cref="Aspid.FastTools.Types.Editors.TypeSelectorWindow"/>; <see cref="IsValidGenericArgument"/>
    /// is supplied to the selector as its argument filter.
    /// </summary>
    internal static class SerializeReferenceHelpers
    {
        /// <summary>
        /// Resolves the declared element type of a managed-reference property — the base type that
        /// constrains the candidate list. Uses <see cref="SerializedProperty.managedReferenceFieldTypename"/>,
        /// which already reports the element type for array/list entries.
        /// </summary>
        public static Type GetFieldType(SerializedProperty property) =>
            GetTypeFromTypename(property.managedReferenceFieldTypename) ?? typeof(object);

        /// <summary>
        /// Resolves the concrete type currently stored in the managed reference, or <see langword="null"/>
        /// when the reference is empty or its stored type can no longer be loaded.
        /// </summary>
        public static Type GetCurrentType(SerializedProperty property) =>
            property.managedReferenceValue?.GetType();

        #region Project scan helpers
        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="path"/> is a project asset whose extension can host
        /// managed references AND is not under a user-excluded folder. Layers SerializeReference's settings-based
        /// exclusion on top of the engine-level candidate test
        /// (<see cref="SerializeReferenceYaml.IsCandidateAssetPath"/>), which single-sources the .prefab/.asset/.unity
        /// set so the Repair window, the usage index, the breakage detector and the build/CI gate scan the same set.
        /// </summary>
        public static bool IsScanCandidate(string path) =>
            SerializeReferenceYaml.IsCandidateAssetPath(path) && !SerializeReferenceSettings.IsExcluded(path);

        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="path"/> is a Unity scene. Scenes cannot be read through
        /// <see cref="AssetDatabase.LoadAllAssetsAtPath"/> — it warns "Do not use ReadObjectThreaded on scene objects!"
        /// and returns nothing useful — so every object-loading scanner skips them and relies on the YAML pass instead.
        /// </summary>
        public static bool IsScene(string path) =>
            !string.IsNullOrEmpty(path) && path.EndsWith(".unity", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Stable grouping key for a stored type identity (class + namespace + assembly). <see cref="ManagedTypeName"/>
        /// carries no value equality, so the three fields are joined into a key string instead.
        /// </summary>
        public static string StoredTypeKey(ManagedTypeName type) =>
            $"{type.Assembly}|{type.Namespace}|{type.Class}";

        /// <summary>
        /// Open-generic identity key for a stored type: class name with its backtick arity but <b>without</b> the
        /// <c>[[…]]</c> closed-argument expansion, plus namespace and assembly. A script's open definition
        /// (<c>Modifier`1[[T]]</c>, what <see cref="MonoScript.GetClass"/> yields) and the closed forms YAML stores
        /// (<c>Modifier`1[[System.Single, …]]</c>) collapse to the same key, so the delete guard and usage index match
        /// every closed instantiation of a generic type to its script. A non-generic type has no <c>[[…]]</c> suffix and
        /// keys identically to <see cref="StoredTypeKey"/>.
        /// </summary>
        public static string OpenTypeKey(ManagedTypeName type) =>
            OpenTypeKey(StoredTypeKey(type));

        /// <summary>
        /// Reduces a <see cref="StoredTypeKey"/> string to its open-generic form by dropping the bracketed closed-argument
        /// expansion (<c>Foo`1[[…]]</c> -> <c>Foo`1</c>). The bracket only appears inside the class segment and the
        /// backtick arity is kept, so different arities never collapse and namespace/assembly stay intact.
        /// </summary>
        public static string OpenTypeKey(string storedTypeKey)
        {
            if (string.IsNullOrEmpty(storedTypeKey)) return storedTypeKey ?? string.Empty;

            var bracket = storedTypeKey.IndexOf('[');
            return bracket >= 0 ? storedTypeKey[..bracket] : storedTypeKey;
        }
        #endregion

        #region Multi-object editing
        /// <summary>
        /// Returns <see langword="true"/> when this property belongs to a <see cref="SerializedObject"/> editing more
        /// than one target object at once (a multi-selection in the inspector).
        /// </summary>
        public static bool IsEditingMultipleObjects(SerializedProperty property) =>
            property.serializedObject.isEditingMultipleObjects;

        /// <summary>
        /// Returns <see langword="true"/> when the selected targets do not all hold the same managed-reference type —
        /// either Unity reports <see cref="SerializedProperty.hasMultipleDifferentValues"/>, or the per-target
        /// <see cref="SerializedProperty.managedReferenceFullTypename"/> differs across
        /// <see cref="SerializedObject.targetObjects"/>. Always <see langword="false"/> for a single target, so the
        /// single-object paths are untouched. Used to drive the dropdown's mixed-value state and to suppress merging
        /// child field UIs of incompatible types.
        /// </summary>
        public static bool HasMixedTypes(SerializedProperty property)
        {
            if (!property.serializedObject.isEditingMultipleObjects) return false;

            // hasMultipleDifferentValues does not always flag the all-missing case, where every target reads back a
            // null value but the stored (unloadable) type names still differ — hence the explicit comparison below.
            if (property.hasMultipleDifferentValues) return true;

            // A non-null, agreed value means all targets share the concrete type — the per-target probe only matters
            // for the all-missing case (null value).
            if (property.managedReferenceValue is not null) return false;

            var first = property.managedReferenceFullTypename;
            var targets = property.serializedObject.targetObjects;
            if (targets.Length < 2) return false;

            // Memoise the per-target probe per selection: it allocates a SerializedObject per selected object on every
            // IMGUI repaint, while the all-missing state it measures is stable until the backing assets change.
            if (TryGetMixedCache(property.propertyPath, first, targets, out var cached)) return cached;

            var result = false;
            foreach (var target in targets)
            {
                if (target == null) continue;

                using var single = new SerializedObject(target);
                var other = single.FindProperty(property.propertyPath);
                if (other is null) continue;
                if (other.managedReferenceFullTypename != first) { result = true; break; }
            }

            StoreMixedCache(property.propertyPath, first, targets, result);
            return result;
        }

        // Per-selection memo backing HasMixedTypes' expensive all-missing multi-select probe. Entries are held per
        // property path so several empty fields drawn under ONE multi-selection all stay memoized across a repaint —
        // a single shared slot would be overwritten by each field and miss on the next. The map is scoped to one
        // selection snapshot (_mixedTargets) and resets whenever the selection changes, so it stays bounded by the
        // fields the current inspector actually draws.
        private static Object[] _mixedTargets;
        private static readonly Dictionary<string, (string first, bool result)> _mixedResults = new(StringComparer.Ordinal);

        // The memo is keyed by selection, not file state, so an EXTERNAL rewrite of the selected assets must drop it
        // explicitly (see SerializeReferenceEditorCacheInvalidator).
        public static void InvalidateMixedTypesCache()
        {
            _mixedTargets = null;
            _mixedResults.Clear();
        }

        private static bool TryGetMixedCache(string path, string first, UnityEngine.Object[] targets, out bool result)
        {
            result = false;
            if (!MixedTargetsMatch(targets)) return false;
            if (!_mixedResults.TryGetValue(path, out var entry) || entry.first != first) return false;

            result = entry.result;
            return true;
        }

        private static bool MixedTargetsMatch(UnityEngine.Object[] targets)
        {
            if (_mixedTargets is null || _mixedTargets.Length != targets.Length) return false;

            for (var i = 0; i < targets.Length; i++)
                if (!ReferenceEquals(_mixedTargets[i], targets[i])) return false;

            return true;
        }

        private static void StoreMixedCache(string path, string first, UnityEngine.Object[] targets, bool result)
        {
            if (!MixedTargetsMatch(targets))
            {
                _mixedResults.Clear();
                _mixedTargets = (Object[])targets.Clone(); // snapshot the references so a reused array can't alias
            }

            _mixedResults[path] = (first, result);
        }

        /// <summary>
        /// Applies a managed-reference change to <b>every</b> selected target independently, so each object receives its
        /// own instance rather than the shared reference that a single multi-object
        /// <see cref="SerializedProperty.managedReferenceValue"/> assignment would alias across all of them. For each
        /// target, <paramref name="factory"/> is invoked with that target's <i>previous</i> value (to support Keep-Data
        /// through <see cref="CreateInstancePreservingData"/>) and must return a fresh, independent instance (or
        /// <see langword="null"/> to clear). The whole batch is collapsed into a single Undo step so one Ctrl+Z reverts
        /// all targets together. The originating <paramref name="property"/>'s <see cref="SerializedObject"/> is then
        /// refreshed so the live inspector re-reads the new state.
        /// </summary>
        public static void ApplyManagedReferencePerTarget(SerializedProperty property, Func<object, object> factory)
        {
            var serializedObject = property.serializedObject;
            var targets = serializedObject.targetObjects;
            var propertyPath = property.propertyPath;

            Undo.IncrementCurrentGroup();
            var undoGroup = Undo.GetCurrentGroup();

            foreach (var target in targets)
            {
                if (target == null) continue;

                using var single = new SerializedObject(target);
                var singleProperty = single.FindProperty(propertyPath);
                if (singleProperty is null) continue;

                var previous = singleProperty.managedReferenceValue;
                var instance = factory(previous);

                singleProperty.managedReferenceValue = instance;
                singleProperty.isExpanded = instance is not null;
                single.ApplyModifiedProperties();
            }

            Undo.CollapseUndoOperations(undoGroup);

            // Update() pulls the per-target writes back into the live SerializedObject — applying it instead would
            // write the live SO's stale (pre-change) managed reference back over the per-target writes.
            serializedObject.Update();
        }

        /// <summary>
        /// Whether the per-asset missing/shared notices may be shown for this property. They are file-level operations
        /// keyed to a single backing asset (YAML rewrite, single-object cross-reference scan), so under a multi-object
        /// selection they would either misreport (the probes read only the first target) or apply to a single target
        /// while presenting as if they covered the selection. The conservative rule is therefore to surface a notice
        /// only for a single target; with several targets selected the notices are suppressed and the mixed/same-type
        /// hint takes their place. Returns <see langword="true"/> for the single-target case (notices allowed).
        /// </summary>
        public static bool NoticesApply(SerializedProperty property) =>
            !property.serializedObject.isEditingMultipleObjects;
        #endregion

        /// <summary>
        /// Returns <see langword="true"/> when this property holds a managed reference whose type can no longer be
        /// loaded (renamed / moved / deleted). Unity does not expose a missing type through the per-property API
        /// (the value reads back <see langword="null"/> and <see cref="SerializedProperty.managedReferenceFullTypename"/>
        /// is empty) and even drops it from the live object on prefabs / GameObjects, so detection reads the stored
        /// reference straight from the asset YAML: a null value whose recorded type cannot be resolved is missing.
        /// </summary>
        public static bool IsMissingType(SerializedProperty property) =>
            TryGetMissingType(property, out _, out _);

        // Per-frame memo: the probe runs several times per IMGUI repaint and every LEGITIMATELY EMPTY field pays the
        // full repair-location resolution plus a YAML parse per call. Repairs and imports land on later frames; the
        // mutation sites drop the memo explicitly for same-frame reads.
        private static int _missingProbeFrame = -1;
        private static readonly Dictionary<(int instanceId, string path), (bool missing, long referenceId, ManagedTypeName storedType)>
            _missingProbeMemo = new();

        // For mutations that must be visible to a read later in the SAME frame.
        public static void InvalidateMissingTypeMemo() => _missingProbeFrame = -1;

        // Reads the property's stored id and type from the asset YAML; missing when the type no longer resolves.
        private static bool TryGetMissingType(SerializedProperty property, out long referenceId, out ManagedTypeName storedType)
        {
            referenceId = 0;
            storedType = default;

            if (property.propertyType != SerializedPropertyType.ManagedReference) return false;
            if (property.managedReferenceValue is not null) return false;

            var frame = Time.frameCount;
            if (_missingProbeFrame != frame)
            {
                _missingProbeMemo.Clear();
                _missingProbeFrame = frame;
            }

            var target = property.serializedObject.targetObject;
            var key = (target != null ? target.GetInstanceID() : 0, property.propertyPath);

            if (_missingProbeMemo.TryGetValue(key, out var cached))
            {
                referenceId = cached.referenceId;
                storedType = cached.storedType;
                return cached.missing;
            }

            var missing = ProbeMissingType(property, out referenceId, out storedType);
            _missingProbeMemo[key] = (missing, referenceId, storedType);
            return missing;
        }

        private static bool ProbeMissingType(SerializedProperty property, out long referenceId, out ManagedTypeName storedType)
        {
            referenceId = 0;
            storedType = default;

            if (!TryGetRepairLocation(property, out var assetPath, out var fileId, out _)) return false;
            if (!SerializeReferenceYamlEditor.TryReadStoredType(assetPath, fileId, property.propertyPath, out referenceId, out storedType))
                return false;

            return !storedType.IsEmpty && !StoredTypeResolves(storedType);
        }

        // True when the YAML-recorded type identity can be loaded — i.e. the reference is intact, not missing.
        public static bool StoredTypeResolves(ManagedTypeName name)
        {
            if (string.IsNullOrEmpty(name.Class)) return false;

            var className = name.Class.Replace('/', '+');
            var fullName = string.IsNullOrEmpty(name.Namespace) ? className : $"{name.Namespace}.{className}";
            var assemblyQualified = string.IsNullOrEmpty(name.Assembly) ? fullName : $"{fullName}, {name.Assembly}";

            return Type.GetType(assemblyQualified, throwOnError: false) is not null;
        }

        /// <summary>
        /// Predicate identifying types that can legally be assigned to a <c>[SerializeReference]</c> field:
        /// concrete reference types that are neither <see cref="Object"/>, open generics, strings, nor delegates.
        /// </summary>
        public static bool IsAssignableManagedReference(Type type) =>
            type is { IsClass: true, IsAbstract: false, ContainsGenericParameters: false } &&
            type != typeof(string) &&
            !typeof(Object).IsAssignableFrom(type) &&
            !typeof(Delegate).IsAssignableFrom(type);

        /// <summary>
        /// Builds the candidate predicate for the type picker: the structural <see cref="IsAssignableManagedReference"/>
        /// check, optionally narrowed so only types assignable to one of <paramref name="baseTypes"/> qualify. A null or
        /// empty set — or one that only names <see cref="object"/> (the unconstrained <c>[TypeSelector]</c> default) —
        /// applies no extra narrowing, leaving every concrete type assignable to the field's declared type as a candidate.
        /// </summary>
        public static Func<Type, bool> BuildAssignableFilter(Type[] baseTypes)
        {
            var narrowing = FilterNarrowingTypes(baseTypes);
            if (narrowing is null) return IsAssignableManagedReference;

            return type => IsAssignableManagedReference(type) &&
                           Array.Exists(narrowing, baseType => baseType.IsAssignableFrom(type));
        }

        // Drops nulls and the unconstrained `object` sentinel; returns null when nothing meaningful narrows the set,
        // so the caller can fall back to the plain structural filter without allocating a predicate closure.
        private static Type[] FilterNarrowingTypes(Type[] baseTypes)
        {
            if (baseTypes is null || baseTypes.Length == 0) return null;

            var count = 0;
            foreach (var type in baseTypes)
                if (type is not null && type != typeof(object)) count++;

            if (count == 0) return null;

            var result = new Type[count];
            var index = 0;
            foreach (var type in baseTypes)
                if (type is not null && type != typeof(object)) result[index++] = type;

            return result;
        }

        /// <summary>
        /// Creates an instance of <paramref name="type"/> for assignment to a managed reference.
        /// Prefers a (public or non-public) parameterless constructor so field initializers run, and
        /// falls back to an uninitialized instance for types that expose no parameterless constructor.
        /// </summary>
        public static object CreateInstance(Type type)
        {
            if (type is null) return null;

            try
            {
                return Activator.CreateInstance(type, nonPublic: true);
            }
            catch (MissingMethodException)
            {
                return FormatterServices.GetUninitializedObject(type);
            }
        }

        /// <summary>
        /// Creates an instance of <paramref name="newType"/> and carries over the data of <paramref name="previous"/>
        /// for every field the two types share by name and serialized shape. Mirrors Unity's own type-change
        /// behaviour: the old value is serialized to JSON and overwritten onto the new instance, so matching fields
        /// survive a type switch (e.g. a shared <c>_radius</c>) while the rest fall back to the new type's defaults.
        /// A structural mismatch simply leaves the new instance untouched.
        /// </summary>
        public static object CreateInstancePreservingData(Type newType, object previous)
        {
            var instance = CreateInstance(newType);
            if (instance is null || previous is null) return instance;

            try
            {
                var json = JsonUtility.ToJson(previous);
                if (!string.IsNullOrEmpty(json) && json != "{}")
                    JsonUtility.FromJsonOverwrite(json, instance);
            }
            catch (Exception)
            {
                // Best effort: incompatible layouts just mean nothing is carried over.
            }

            // JsonUtility skips [SerializeReference] fields entirely, so nested managed references are carried over by
            // reflection — the very instances, not copies, keeping aliases onto them intact across a type switch. The
            // Make-unique flows deep-copy the result afterwards (see CloneManagedReferenceGraph).
            try
            {
                CarryManagedReferences(previous, instance);
            }
            catch (Exception)
            {
                // Same best-effort contract as the JSON pass.
            }

            return instance;
        }

        // Assigns every [SerializeReference] field the two shapes share by name (and whose value fits the target
        // field's declared type) from previous onto instance — including whole arrays / lists of references.
        private static void CarryManagedReferences(object previous, object instance)
        {
            Dictionary<string, FieldInfo> targets = null;

            foreach (var field in EnumerateManagedReferenceFields(previous.GetType()))
            {
                if (targets is null)
                {
                    targets = new Dictionary<string, FieldInfo>(StringComparer.Ordinal);
                    foreach (var target in EnumerateManagedReferenceFields(instance.GetType()))
                        targets[target.Name] = target;
                }

                if (!targets.TryGetValue(field.Name, out var into)) continue;

                var value = field.GetValue(previous);
                if (value is null || into.FieldType.IsInstanceOfType(value))
                    into.SetValue(instance, value);
            }
        }

        /// <summary>
        /// Deep-copies a managed-reference instance: value fields ride the same JSON round-trip as
        /// <see cref="CreateInstancePreservingData"/>, and every nested <c>[SerializeReference]</c> field — including
        /// arrays and lists of them — is recursively replaced with its own independent copy. Internal topology is
        /// preserved: two fields aliasing one nested instance alias one copy, and a cyclic graph clones without
        /// recursing forever (each copy registers in the map before its children are cloned). This is the Make-unique
        /// / de-alias copier — those flows promise an instance independent all the way down; the type-switch flows
        /// keep <see cref="CreateInstancePreservingData"/>, where reusing the nested instances is correct.
        /// </summary>
        public static object CloneManagedReferenceGraph(object source) =>
            CloneManagedReferenceGraph(source, new Dictionary<object, object>(ReferenceComparer.Instance));

        private static object CloneManagedReferenceGraph(object source, Dictionary<object, object> clones)
        {
            if (source is null) return null;
            if (clones.TryGetValue(source, out var existing)) return existing;

            var clone = CreateInstancePreservingData(source.GetType(), source);
            if (clone is null) return null;
            clones[source] = clone;

            foreach (var field in EnumerateManagedReferenceFields(source.GetType()))
                field.SetValue(clone, CloneManagedReferenceValue(field.GetValue(source), clones));

            return clone;
        }

        // Clones one [SerializeReference] field slot: a collection is rebuilt (never shared with the source) with
        // each element cloned; a single reference clones directly.
        private static object CloneManagedReferenceValue(object value, Dictionary<object, object> clones)
        {
            switch (value)
            {
                case null:
                    return null;

                case Array array:
                {
                    var copy = Array.CreateInstance(array.GetType().GetElementType()!, array.Length);
                    for (var i = 0; i < array.Length; i++)
                        copy.SetValue(CloneManagedReferenceGraph(array.GetValue(i), clones), i);
                    return copy;
                }

                case IList list:
                {
                    var copy = (IList)Activator.CreateInstance(value.GetType());
                    foreach (var element in list)
                        copy.Add(CloneManagedReferenceGraph(element, clones));
                    return copy;
                }

                default:
                    return CloneManagedReferenceGraph(value, clones);
            }
        }

        // The serialized fields Unity persists as managed references: instance fields, public or [SerializeField],
        // declared with [SerializeReference], walking the base chain (each level reports its own declared fields).
        private static IEnumerable<FieldInfo> EnumerateManagedReferenceFields(Type type)
        {
            const BindingFlags flags =
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

            for (var current = type; current is not null && current != typeof(object); current = current.BaseType)
                foreach (var field in current.GetFields(flags))
                {
                    if (field.IsStatic || field.IsInitOnly || field.IsNotSerialized) continue;
                    if (!field.IsPublic && !field.IsDefined(typeof(SerializeField), inherit: false)) continue;
                    if (field.IsDefined(typeof(SerializeReference), inherit: false)) yield return field;
                }
        }

        // Reference-identity comparer for the clone map: a user-defined Equals must not merge distinct instances
        // (or split one), and managed references are always classes, so no boxing is involved.
        private sealed class ReferenceComparer : IEqualityComparer<object>
        {
            public static readonly ReferenceComparer Instance = new();

            bool IEqualityComparer<object>.Equals(object x, object y) => ReferenceEquals(x, y);

            int IEqualityComparer<object>.GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
        }

        /// <summary>
        /// Parses Unity's managed-reference type-name format (<c>"AssemblyName Namespace.TypeName"</c>)
        /// into a <see cref="Type"/>, or <see langword="null"/> when it is empty or cannot be loaded.
        /// </summary>
        public static Type GetTypeFromTypename(string typename)
        {
            if (string.IsNullOrEmpty(typename)) return null;

            var separator = typename.IndexOf(' ');
            if (separator < 0) return Type.GetType(typename, throwOnError: false);

            var assembly = typename[..separator];
            var fullName = typename[(separator + 1)..];
            return Type.GetType($"{fullName}, {assembly}", throwOnError: false);
        }

        /// <summary>
        /// Predicate identifying types usable as a generic argument of a serialized managed reference:
        /// concrete, non-generic types Unity can serialize as a field value (primitives, <see cref="string"/>,
        /// enums, <see cref="Object"/>-derived references, or <c>[Serializable]</c> structs/classes). Passed to
        /// <see cref="Aspid.FastTools.Types.Editors.TypeSelectorWindow.Show"/> as the argument filter.
        /// </summary>
        public static bool IsValidGenericArgument(Type type)
        {
            if (type is null) return false;
            if (type.IsAbstract || type.IsInterface || type.ContainsGenericParameters) return false;
            if (typeof(Delegate).IsAssignableFrom(type)) return false;

            return type.IsPrimitive ||
                   type.IsEnum ||
                   type == typeof(string) ||
                   typeof(Object).IsAssignableFrom(type) ||
                   (type.IsValueType && type.IsSerializable) ||
                   (type.IsClass && type.IsSerializable);
        }

        #region Missing-type repair
        /// <summary>
        /// Resolves the stored (now unloadable) type identity of this property's missing managed reference, read from
        /// the asset YAML, for display in the caption / warning. Returns <see langword="default"/> when the property
        /// is not a recognised missing reference.
        /// </summary>
        public static ManagedTypeName GetMissingTypeName(SerializedProperty property) =>
            TryGetMissingType(property, out _, out var storedType) ? storedType : default;

        /// <summary>
        /// Human-readable <c>Namespace.Class</c> of this property's missing type, for the dropdown caption and the
        /// warning message, or an empty string when the property is not a recognised missing reference.
        /// </summary>
        public static string GetMissingTypeDisplayName(SerializedProperty property) =>
            GetMissingTypeName(property).DisplayName;

        /// <summary>
        /// Computes the best <b>Smart Fix</b> repair suggestion for this property's missing managed reference: the
        /// highest-scoring existing type the renamed/moved reference most likely became, ranked by
        /// <see cref="SerializeReferenceRepairSuggestions"/>. The suggestion is never applied automatically — the caller
        /// surfaces it as a one-click action. The candidate pool is constrained to types the picker itself would offer
        /// (assignable to the field's declared type, narrowed by <paramref name="baseTypes"/>), so a suggestion can
        /// never violate the field's constraint. Returns <see langword="false"/> when the property is not a recognised
        /// missing reference, the repair location is unavailable, or no candidate clears the confidence threshold.
        /// </summary>
        public static bool TryGetRepairSuggestion(SerializedProperty property, Type[] baseTypes,
            out SerializeReferenceRepairSuggestions.RepairCandidate suggestion)
        {
            suggestion = default;

            if (!TryGetMissingType(property, out var referenceId, out var storedType)) return false;
            if (!TryGetRepairLocation(property, out var assetPath, out var fileId, out var inMemory)) return false;

            var fieldType = GetFieldType(property);
            var pickerFilter = BuildAssignableFilter(baseTypes);

            var ranked = SerializeReferenceRepairSuggestions.GetCached(assetPath, fileId, referenceId,
                () => SerializeReferenceRepairSuggestions.Rank(
                    storedType,
                    GetMissingFieldNames(property, assetPath, fileId, referenceId, inMemory),
                    fieldType));

            foreach (var candidate in ranked)
            {
                if (!pickerFilter(candidate.Type)) continue;
                suggestion = candidate;
                return true;
            }

            return false;
        }

        /// <summary>
        /// The Smart Fix <paramref name="suggestion"/> label — the "<c>→ Name</c>" affordance. Shared by the
        /// UIToolkit and IMGUI notices and the Project References quick-apply button so the copy never drifts.
        /// The "<c>·</c>" that separates it from the inline Fix segment is NOT part of this label: it is decoration
        /// each notice renders itself, unclickable and never underlined.
        /// </summary>
        public static string GetSuggestionLabel(SerializeReferenceRepairSuggestions.RepairCandidate suggestion) =>
            $"→ {TypeSelectorHelpers.GetTypeSelectorTitle(suggestion.Type)}";

        /// <summary>
        /// The hover-tooltip detail for a Smart Fix <paramref name="suggestion"/> — the full type identity and the
        /// ranking reason. Shared by the UIToolkit and IMGUI notices so the two never drift.
        /// </summary>
        public static string GetSuggestionDetail(SerializeReferenceRepairSuggestions.RepairCandidate suggestion) =>
            $"Suggested: {suggestion.Type.FullName}, {suggestion.Type.Assembly.GetName().Name}.\n" +
            $"Reason: {suggestion.Reason}.\nClick to re-point this reference to it, keeping its data.";

        // Top-level serialized field names of the missing reference's orphaned payload, for the field-shape heuristic.
        // Saved assets read them straight from the rid's YAML data block; a Prefab Mode object has no committed block
        // for the live copy, so the flat payload Unity still exposes for the missing reference is parsed instead.
        private static List<string> GetMissingFieldNames(SerializedProperty property, string assetPath, long fileId, long referenceId, bool inMemory)
        {
            if (!inMemory)
                return SerializeReferenceYamlEditor.GetReferenceFieldNames(assetPath, fileId, referenceId);

            var target = property.serializedObject.targetObject;
            foreach (var entry in SerializationUtility.GetManagedReferencesWithMissingTypes(target))
                if (entry.referenceId == referenceId)
                    return SerializeReferenceYamlEditor.ParseTopLevelFieldNames(entry.serializedData);

            return new List<string>();
        }

        /// <summary>
        /// Resolves the on-disk asset path and the target object's local file id (the YAML document anchor) backing
        /// this property. Returns <see langword="false"/> for scene objects and prefab instances, which have no
        /// editable asset file — the YAML repair flow only applies to saved assets (ScriptableObjects, prefabs).
        /// </summary>
        public static bool TryGetAssetLocation(SerializedProperty property, out string assetPath, out long fileId)
        {
            fileId = 0;
            var target = property.serializedObject.targetObject;
            assetPath = AssetDatabase.GetAssetPath(target);

            if (string.IsNullOrEmpty(assetPath)) return false;
            return AssetDatabase.TryGetGUIDAndLocalFileIdentifier(target, out _, out fileId);
        }

        /// <summary>
        /// Resolves the YAML document backing this property's stored managed reference and reports whether the repair
        /// must be applied in memory. Saved assets (ScriptableObjects, prefab assets selected in the Project) resolve
        /// directly and are repaired by rewriting the file. Objects open in <b>Prefab Mode</b> have no asset path of
        /// their own — the path comes from the prefab stage and the document id is matched back to the asset on disk —
        /// and must be repaired in memory (<paramref name="inMemory"/> = <see langword="true"/>), because the open
        /// stage holds a separate copy that does not refresh on reimport and would overwrite a file rewrite on save.
        /// </summary>
        public static bool TryGetRepairLocation(SerializedProperty property, out string assetPath, out long fileId, out bool inMemory)
        {
            inMemory = false;
            if (TryGetAssetLocation(property, out assetPath, out fileId)) return true;

            assetPath = null;
            fileId = 0;

            var target = property.serializedObject.targetObject;
            var go = target as GameObject ?? (target as Component)?.gameObject;
            if (go is null) return false;

            var stage = PrefabStageUtility.GetPrefabStage(go);
            if (stage is not null)
            {
                if (!TryMatchAssetFileId(stage, target, go, out fileId)) return false;

                assetPath = stage.assetPath;
                inMemory = true;
                return true;
            }

            // A plain object in a saved scene: its scene file is the YAML document store and its scene-local file id is
            // the document anchor. Repaired in memory (a loaded scene must not be rewritten on disk under it).
            if (TryGetSceneLocation(target, go, out assetPath, out fileId))
            {
                inMemory = true;
                return true;
            }

            return false;
        }

        // GlobalObjectId.targetObjectId is the scene-local file identifier matching the YAML "--- !u!114 &<fileID>"
        // anchor. Bails for unsaved/dirty scenes (the on-disk YAML would not match the live object) and for
        // prefab-instance overrides (their data lives in the source prefab).
        private static bool TryGetSceneLocation(Object target, GameObject go, out string assetPath, out long fileId)
        {
            assetPath = null;
            fileId = 0;

            var scene = go.scene;
            if (!scene.IsValid() || string.IsNullOrEmpty(scene.path) || scene.isDirty) return false;

            var globalId = GlobalObjectId.GetGlobalObjectIdSlow(target);
            if (globalId.identifierType != 2) return false;     // 2 == scene object
            if (globalId.targetPrefabId != 0) return false;      // a prefab-instance override — defer to the source prefab

            assetPath = scene.path;
            fileId = unchecked((long)globalId.targetObjectId);
            return true;
        }

        /// <summary>
        /// Resolves the source prefab asset path for a nested prefab instance's <paramref name="target"/>, whose managed
        /// reference data lives in that source prefab rather than the host. Returns <see langword="false"/> for plain
        /// scene objects and saved assets.
        /// </summary>
        public static bool TryGetSourcePrefabPath(Object target, out string sourcePath)
        {
            sourcePath = null;
            if (target == null) return false;

            var go = target as GameObject ?? (target as Component)?.gameObject;
            if (go is null || !PrefabUtility.IsPartOfPrefabInstance(go)) return false;

            sourcePath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(go);
            return !string.IsNullOrEmpty(sourcePath);
        }

        // A Prefab Mode object is a copy in a preview scene and carries no file id of its own, so the matching
        // persisted object is located in the asset by replaying its child path from the stage root, and the document
        // id is read from the asset's component (or GameObject) there.
        private static bool TryMatchAssetFileId(PrefabStage stage, Object target, GameObject stageGo, out long fileId)
        {
            fileId = 0;

            // A dirty stage has diverged from the on-disk asset — the index replay would land on the WRONG asset
            // object and let the repair overwrite the wrong field. Mirrors TryGetSceneLocation's scene.isDirty guard.
            if (stage.scene.isDirty) return false;

            var indices = new List<int>();
            var transform = stageGo.transform;
            var root = stage.prefabContentsRoot.transform;
            while (transform != root)
            {
                if (transform.parent is null) return false; // object is not under the stage root
                indices.Insert(0, transform.GetSiblingIndex());
                transform = transform.parent;
            }

            var assetRoot = AssetDatabase.LoadAssetAtPath<GameObject>(stage.assetPath);
            if (assetRoot is null) return false;

            var assetTransform = assetRoot.transform;
            foreach (var index in indices)
            {
                if (index < 0 || index >= assetTransform.childCount) return false;
                assetTransform = assetTransform.GetChild(index);
            }

            if (target is not Component component)
                return AssetDatabase.TryGetGUIDAndLocalFileIdentifier(assetTransform.gameObject, out _, out fileId);

            // Disambiguate by component index in case the object carries several components of the same type.
            var stageComponents = stageGo.GetComponents(component.GetType());
            var componentIndex = Array.IndexOf(stageComponents, component);
            var assetComponents = assetTransform.GetComponents(component.GetType());
            if (componentIndex < 0 || componentIndex >= assetComponents.Length) return false;

            return AssetDatabase.TryGetGUIDAndLocalFileIdentifier(assetComponents[componentIndex], out _, out fileId);
        }

        /// <summary>
        /// Finds the <c>RefIds</c> id of the missing managed reference this property points at, read from the asset
        /// YAML. Detection is strict and per-property: only a field whose own recorded type fails to resolve counts
        /// as missing, so legitimately-empty fields are never flagged.
        /// </summary>
        public static bool TryGetMissingReferenceId(SerializedProperty property, out long referenceId) =>
            TryGetMissingType(property, out referenceId, out _);

        /// <summary>
        /// Opens the same hierarchical type picker the dropdown uses, anchored at <paramref name="screenRect"/>, to
        /// choose the existing type a missing reference should resolve to. <paramref name="baseTypes"/> narrows the
        /// candidates the same way the live dropdown does, so a repair cannot pick a type the attribute excludes. The
        /// chosen type is written into the asset YAML (re-pointing the reference and keeping its stored data);
        /// <paramref name="onFixed"/> runs on success.
        /// </summary>
        public static void ShowFixTypeSelector(SerializedProperty property, Rect screenRect, Action onFixed, Type[] baseTypes = null)
        {
            var fieldType = GetFieldType(property);

            TypeSelectorWindow.Show(
                screenRect: screenRect,
                filter: new TypeSelectorFilter
                {
                    Types = new[] { fieldType },
                    Predicate = BuildAssignableFilter(baseTypes),
                    AdditionalTypes = GenericTypeResolver.GetAssignableGenericDefinitions(fieldType, baseTypes),
                    ArgumentFilter = IsValidGenericArgument,
                },
                currentAqn: null, // a missing-type Fix has no current value — nothing (not even <None>) wears the check
                onSelected: assemblyQualifiedName =>
                {
                    var type = string.IsNullOrEmpty(assemblyQualifiedName)
                        ? null
                        : Type.GetType(assemblyQualifiedName, throwOnError: false);

                    if (type is not null && TryFixMissingType(property, type))
                        onFixed?.Invoke();
                });
        }

        /// <summary>
        /// Re-points this property's missing managed reference to <paramref name="newType"/>, keeping its stored data.
        /// Saved assets are repaired by rewriting the type in the YAML and reimporting; objects open in Prefab Mode are
        /// repaired in memory (see <see cref="TryGetRepairLocation"/>). Returns <see langword="true"/> on success; the
        /// caller refreshes the inspector.
        /// </summary>
        public static bool TryFixMissingType(SerializedProperty property, Type newType)
        {
            if (newType is null) return false;
            if (!TryGetRepairLocation(property, out var assetPath, out var fileId, out var inMemory)) return false;
            if (!TryGetMissingReferenceId(property, out var referenceId)) return false;

            bool repaired;
            if (inMemory)
            {
                repaired = TryFixMissingTypeInMemory(property, newType, referenceId);
            }
            else
            {
                repaired = SerializeReferenceYamlEditor.TryRewriteType(assetPath, fileId, referenceId, ManagedTypeName.FromType(newType));
                // ForceUpdate reloads the asset and invalidates the live SerializedObject, so the property must not be
                // touched afterwards — the inspector is rebuilt below from a fresh selection instead.
                if (repaired) AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }

            // The repair leaves the cached ranking, cached YAML lines and the per-frame memos stale — an IMGUI
            // repaint can land in the same Time.frameCount as this click.
            if (repaired)
            {
                SerializeReferenceRepairSuggestions.ClearCache();
                SerializeReferenceYamlProbeCache.ClearCache();
                InvalidateSharedReferenceCache();
                InvalidateMissingTypeMemo();
            }

            if (repaired) ScheduleInspectorRebuild();
            return repaired;
        }

        // Unity's object-level "contains missing SerializeReference types" banner is drawn from a flag cached when
        // the editor is built and only clears on a genuine reselection, so the current objects are deselected and
        // reselected on the next ticks to rebuild the editors from scratch.
        private static void ScheduleInspectorRebuild()
        {
            var selection = Selection.objects;
            if (selection is null || selection.Length == 0) return;

            EditorApplication.delayCall += () =>
            {
                Selection.objects = Array.Empty<Object>();
                EditorApplication.delayCall += () => Selection.objects = selection;
            };
        }

        // Prefab Mode: the open stage holds its own copy that does not refresh on reimport and overwrites a file
        // rewrite on save, so the reference is reassigned on the live object (recovering the orphaned field data)
        // and the now-unused missing-type entry is cleared.
        private static bool TryFixMissingTypeInMemory(SerializedProperty property, Type newType, long referenceId)
        {
            var target = property.serializedObject.targetObject;
            var instance = CreateInstance(newType);
            if (instance is null) return false;

            foreach (var entry in SerializationUtility.GetManagedReferencesWithMissingTypes(target))
            {
                if (entry.referenceId != referenceId) continue;
                RecoverManagedReferenceData(entry.serializedData, instance);
                break;
            }

            property.SetManagedReferenceAndApply(instance);
            ClearMissingSubtree(target, referenceId);
            EditorUtility.SetDirty(target);
            property.serializedObject.Update();

            // Mark the owning scene dirty so the in-memory repair is offered for save.
            var scene = (target as Component)?.gameObject.scene ?? (target as GameObject)?.scene ?? default;
            if (scene.IsValid()) EditorSceneManager.MarkSceneDirty(scene);

            return true;
        }

        /// <summary>
        /// Clears a missing managed reference (id <paramref name="rid"/>, stored type <paramref name="storedType"/>) to
        /// <see langword="null"/> on the live object of an asset open in Prefab Mode or a loaded scene — the in-memory
        /// counterpart of the YAML clear. The Project References uses it when a direct file rewrite would be clobbered by the
        /// open copy on save. Marks the owning scene dirty so the change is offered for save (the on-disk file, and so
        /// the audit listing, only updates once saved). Returns whether a matching live entry was found and cleared.
        /// </summary>
        public static bool TryClearMissingReferenceInMemory(string assetPath, long rid, ManagedTypeName storedType)
        {
            if (string.IsNullOrEmpty(assetPath)) return false;

            foreach (var target in EnumerateOpenMissingTypeTargets(assetPath))
            {
                var matched = false;
                foreach (var entry in SerializationUtility.GetManagedReferencesWithMissingTypes(target))
                {
                    if (entry.referenceId != rid) continue;
                    // Guard against a colliding rid on another live object: also match the stored class when it is known.
                    if (!string.IsNullOrEmpty(storedType.Class) && entry.className != storedType.Class) continue;
                    matched = true;
                    break;
                }

                if (!matched) continue;

                ClearMissingSubtree(target, rid);
                EditorUtility.SetDirty(target);
                InvalidateMissingTypeMemo();

                var scene = (target as Component)?.gameObject.scene ?? default;
                if (scene.IsValid()) EditorSceneManager.MarkSceneDirty(scene);

                return true;
            }

            return false;
        }

        // The live MonoBehaviours of an open (unsafe-to-rewrite) asset: a prefab in Prefab Mode, or a loaded scene.
        // Matched by missing-reference identity, not file id (the open stage remaps ids). Only MonoBehaviours are
        // probed — GetManagedReferencesWithMissingTypes errors on unsupported types.
        private static IEnumerable<Object> EnumerateOpenMissingTypeTargets(string assetPath)
        {
            var stage = PrefabStageUtility.GetCurrentPrefabStage();
            if (stage != null && string.Equals(stage.assetPath, assetPath, StringComparison.Ordinal) && stage.prefabContentsRoot != null)
                foreach (var mb in stage.prefabContentsRoot.GetComponentsInChildren<MonoBehaviour>(true))
                    if (mb != null) yield return mb;

            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByPath(assetPath);
            if (scene.IsValid() && scene.isLoaded)
                foreach (var root in scene.GetRootGameObjects())
                    foreach (var mb in root.GetComponentsInChildren<MonoBehaviour>(true))
                        if (mb != null) yield return mb;
        }

        // Clears the fixed missing-type entry and any missing-type entries it transitively referenced — otherwise
        // they linger as unreachable orphans and keep Unity's object-level missing-types banner raised. A member
        // referenced from OUTSIDE the subtree is kept (clearing it would leave the other pointer unrepairable),
        // along with everything only reachable through it.
        private static void ClearMissingSubtree(Object target, long rootReferenceId)
        {
            var dataByRid = new Dictionary<long, string>();
            foreach (var entry in SerializationUtility.GetManagedReferencesWithMissingTypes(target))
                dataByRid[entry.referenceId] = entry.serializedData;

            // The transitive closure of the fixed entry: what we would LIKE to clear.
            var closure = new HashSet<long>();
            var pending = new Stack<long>();
            pending.Push(rootReferenceId);

            while (pending.Count > 0)
            {
                var rid = pending.Pop();
                if (!closure.Add(rid)) continue;
                if (!dataByRid.TryGetValue(rid, out var data)) continue; // a resolvable reference, or already cleared

                foreach (var child in EnumerateRidPointers(data, rid))
                    pending.Push(child);
            }

            // Seed protection with every closure member referenced from outside it: by another missing entry's
            // payload, or by a live field still holding the rid (the repaired field itself now points at the fresh
            // instance, so it no longer counts).
            var keep = new HashSet<long>();

            foreach (var pair in dataByRid)
            {
                if (closure.Contains(pair.Key)) continue;
                foreach (var child in EnumerateRidPointers(pair.Value, pair.Key))
                    if (closure.Contains(child))
                        keep.Add(child);
            }

            using (var serializedObject = new SerializedObject(target))
                TraverseManagedReferences(serializedObject, property =>
                {
                    var id = property.managedReferenceId;
                    if (closure.Contains(id)) keep.Add(id);
                    return false;
                });

            // A kept entry's payload still points at its own children — clearing those would break it the same way,
            // so protection propagates down through the closure.
            foreach (var rid in keep) pending.Push(rid);
            while (pending.Count > 0)
            {
                var rid = pending.Pop();
                if (!dataByRid.TryGetValue(rid, out var data)) continue;

                foreach (var child in EnumerateRidPointers(data, rid))
                    if (closure.Contains(child) && keep.Add(child))
                        pending.Push(child);
            }

            foreach (var rid in closure)
            {
                if (!keep.Contains(rid) && dataByRid.ContainsKey(rid))
                    SerializationUtility.ClearManagedReferenceWithMissingType(target, rid);
            }
        }

        // The rid pointers inside a missing entry's payload block. The look-behind keeps a field that merely ENDS in
        // "rid" (e.g. "_hybrid: 15") from reading as a pointer — the same discipline as the scanner's RidPointer regex.
        private static IEnumerable<long> EnumerateRidPointers(string data, long self)
        {
            foreach (Match match in Regex.Matches(data ?? string.Empty, @"(?<!\w)rid:\s*(-?\d+)"))
            {
                if (long.TryParse(match.Groups[1].Value, out var child) && child != self)
                    yield return child;
            }
        }

        // Best-effort recovery of a missing reference's stored data: Unity surfaces the orphaned payload as YAML
        // scalars; the flat top-level ones are mapped to JSON and overwritten onto the instance. Nested mappings
        // and sequences are skipped and left at the new type's defaults.
        private static void RecoverManagedReferenceData(string serializedData, object instance)
        {
            if (string.IsNullOrEmpty(serializedData)) return;

            try
            {
                var json = new StringBuilder("{");
                var first = true;

                foreach (var raw in serializedData.Split('\n'))
                {
                    var line = raw.TrimEnd('\r');
                    // Only top-level scalars: skip blanks, indented (nested) lines and sequence items.
                    if (line.Length == 0 || char.IsWhiteSpace(line[0]) || line[0] == '-') continue;

                    var separator = line.IndexOf(':');
                    if (separator <= 0) continue;

                    var key = line[..separator].Trim();
                    var value = line[(separator + 1)..].Trim();

                    // Empty value = a mapping/array header (e.g. "_nested:"); complex flow values are not flat scalars.
                    if (key.Length == 0 || value.Length == 0 || value[0] is '{' or '[') continue;

                    if (!first) json.Append(',');
                    first = false;

                    json.Append('"').Append(key).Append("\":");
                    json.Append(IsJsonNumber(value) ? value : Quote(UnquoteYaml(value)));
                }

                json.Append('}');
                if (!first) JsonUtility.FromJsonOverwrite(json.ToString(), instance);
            }
            catch (Exception)
            {
                // Best effort: an unparseable payload simply leaves the new instance at its defaults.
            }
        }

        private static bool IsJsonNumber(string value) => Regex.IsMatch(value, @"^-?\d+(\.\d+)?$");

        // Unity single-quotes YAML scalars that contain reserved characters, doubling embedded quotes.
        private static string UnquoteYaml(string value) =>
            value.Length >= 2 && value[0] == '\'' && value[^1] == '\''
                ? value[1..^1].Replace("''", "'")
                : value;

        private static string Quote(string value) =>
            $"\"{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}\"";
        #endregion

        #region Constraint map
        /// <summary>
        /// Maps every managed reference in <paramref name="assetPath"/> to the declared field type that holds it, keyed
        /// by the owning object document (its local file id) and the reference's <c>RefIds</c> id. A missing reference
        /// reads back <see langword="null"/> through the serialization API, but its field still reports the declared
        /// element type via <see cref="SerializedProperty.managedReferenceFieldTypename"/>, and the orphaned rid survives
        /// in the YAML — so the two together recover the constraint the picker should honour. References nested inside a
        /// missing parent are unreachable here (the parent is null) and simply fall back to an unconstrained picker, as do
        /// orphaned rids no field points at.
        /// </summary>
        /// <remarks>
        /// Shared by the asset-level Repair window (per-entry and project-wide group constraints) and the Managed
        /// References graph window, so a single declared-type recovery backs every embedded picker.
        /// </remarks>
        public static Dictionary<(long fileId, long rid), Type> BuildConstraintMap(string assetPath)
        {
            var map = new Dictionary<(long, long), Type>();
            if (string.IsNullOrEmpty(assetPath)) return map;

            // Scenes cannot be read through LoadAllAssetsAtPath (see IsScene); an unconstrained picker is the fallback.
            if (IsScene(assetPath)) return map;

            // A managed-reference graph may be cyclic, so descending into a rid already on this document's walk would
            // loop forever. Cleared per document — rids are only unique within a document.
            var visited = new HashSet<long>();

            foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(assetPath))
            {
                if (obj == null) continue;
                if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out _, out var fileId)) continue;

                visited.Clear();

                using var serialized = new SerializedObject(obj);
                var iterator = serialized.GetIterator();

                var enterChildren = true;
                while (iterator.Next(enterChildren))
                {
                    enterChildren = true;

                    if (iterator.propertyType != SerializedPropertyType.ManagedReference) continue;

                    long rid;
                    if (iterator.managedReferenceValue is not null)
                        rid = iterator.managedReferenceId;
                    else if (!SerializeReferenceYamlEditor.TryReadReferenceId(assetPath, fileId, iterator.propertyPath, out rid))
                        continue;

                    // A rid already walked is a back-edge in a cyclic graph; record the constraint but do not descend
                    // into its subtree again, or the iterator would never terminate.
                    if (rid >= 0 && !visited.Add(rid)) enterChildren = false;

                    var fieldType = GetFieldType(iterator);
                    if (fieldType is null || fieldType == typeof(object)) continue;

                    map[(fileId, rid)] = fieldType;
                }
            }

            return map;
        }
        #endregion

        #region Cross references
        /// <summary>
        /// Returns <see langword="true"/> when another managed-reference property in the same object aliases this
        /// one (shares its <see cref="SerializedProperty.managedReferenceId"/>) — which happens after duplicating an
        /// array element or pasting, leaving two fields backed by a single instance so edits to one bleed into the other.
        /// </summary>
        public static bool HasSharedReference(SerializedProperty property)
        {
            if (property.managedReferenceValue is null) return false;

            var id = property.managedReferenceId;

            // The id → use-count map is built once per object per frame — a naive per-property full-object walk would
            // be 2·N walks per repaint (GetHeight and Draw each ask this for every managed-reference field).
            return GetReferenceIdCounts(property.serializedObject).TryGetValue(id, out var count) && count > 1;
        }

        /// <summary>
        /// The 1-based ordinal of this property's shared-reference group within its object — a small, stable badge
        /// number ("Shared reference #1", "#2", …) so two fields aliasing the same instance show the same number and
        /// two distinct shared groups show different numbers. Numbering follows each rid's first appearance in document
        /// order and is shared by the IMGUI and UIToolkit notices, so the same reference reads the same on both. Returns
        /// <c>0</c> when the property is empty or not part of a shared group.
        /// </summary>
        public static int GetSharedReferenceIndex(SerializedProperty property)
        {
            if (property.managedReferenceValue is null) return 0;

            var id = property.managedReferenceId;
            return GetSharedReferenceIndices(property.serializedObject).TryGetValue(id, out var index) ? index : 0;
        }

        // Per-object, per-frame memo of how many managed-reference fields carry each id, built by a single full-object
        // walk and shared across every HasSharedReference call in the same repaint.
        private static int _aliasFrame = -1;
        private static SerializedObject _aliasSerializedObject;
        private static readonly Dictionary<long, int> AliasCounts = new();

        // Each id's first-sighting order during the walk above, so the shared-reference badge numbers follow document
        // order (first shared group in the inspector → (1)) rather than the dictionary's incidental iteration order.
        private static readonly List<long> AliasOrder = new();

        private static Dictionary<long, int> GetReferenceIdCounts(SerializedObject serializedObject)
        {
            var frame = Time.frameCount;
            if (_aliasFrame == frame && ReferenceEquals(_aliasSerializedObject, serializedObject))
                return AliasCounts;

            AliasCounts.Clear();
            AliasOrder.Clear();
            TraverseManagedReferences(serializedObject, other =>
            {
                // Negative ids are sentinels, not instances: every EMPTY field reports RefIdNull (-2), so counting
                // them would form a phantom "shared group".
                var id = other.managedReferenceId;
                if (id < 0) return false;

                if (!AliasCounts.TryGetValue(id, out var count)) AliasOrder.Add(id); // first sighting → record its order
                AliasCounts[id] = count + 1;
                return false;
            });

            // The counts (and their order) were rebuilt — the maps derived from them are now stale.
            _sharedIndicesFrame = -1;
            _sharedPathsFrame = -1;
            _aliasFrame = frame;
            _aliasSerializedObject = serializedObject;
            return AliasCounts;
        }

        // Per-object, per-frame memo mapping each shared (count > 1) id to its 1-based badge number. Kept separate
        // from the counts memo so it is built only when a notice actually asks for a badge.
        private static int _sharedIndicesFrame = -1;
        private static SerializedObject _sharedIndicesObject;
        private static readonly Dictionary<long, int> SharedIndices = new();

        private static Dictionary<long, int> GetSharedReferenceIndices(SerializedObject serializedObject)
        {
            // Refresh the counts/order for this frame first (this also resets _sharedIndicesFrame when it rebuilds).
            var counts = GetReferenceIdCounts(serializedObject);

            var frame = Time.frameCount;
            if (_sharedIndicesFrame == frame && ReferenceEquals(_sharedIndicesObject, serializedObject))
                return SharedIndices;

            SharedIndices.Clear();
            var next = 1;
            foreach (var id in AliasOrder)
            {
                if (counts.TryGetValue(id, out var count) && count > 1)
                    SharedIndices[id] = next++;
            }

            _sharedIndicesFrame = frame;
            _sharedIndicesObject = serializedObject;
            return SharedIndices;
        }

        /// <summary>
        /// The property paths of the OTHER managed-reference fields in the same object aliasing this property's
        /// instance (sharing its rid), in document order — what the shared-reference notice lists in its tooltip and
        /// navigates between on click. Empty when the property is empty or not part of a shared group.
        /// </summary>
        public static List<string> GetSharedReferenceAliasPaths(SerializedProperty property)
        {
            var result = new List<string>();
            if (property.managedReferenceValue is null) return result;

            if (!GetSharedReferencePathsById(property.serializedObject)
                    .TryGetValue(property.managedReferenceId, out var paths))
            {
                return result;
            }

            var selfPath = property.propertyPath;
            foreach (var path in paths)
            {
                if (path != selfPath)
                    result.Add(path);
            }

            return result;
        }

        /// <summary>
        /// Every property path in this property's shared-reference group — its own included — in document order;
        /// empty when the property is not part of a shared group. This canonical order backs the notice's
        /// click-to-navigate cycling in both drawers, so they walk the members the same way. The list is a per-frame
        /// memo — read it immediately, do not cache it.
        /// </summary>
        public static IReadOnlyList<string> GetSharedReferenceGroupPaths(SerializedProperty property)
        {
            if (property.managedReferenceValue is null) return Array.Empty<string>();

            return GetSharedReferencePathsById(property.serializedObject)
                .TryGetValue(property.managedReferenceId, out var paths)
                ? paths
                : (IReadOnlyList<string>)Array.Empty<string>();
        }

        // The shared-reference tooltip lists at most this many alias paths before folding the rest into "…and N more".
        private const int MaxDetailAliasPaths = 6;

        /// <summary>
        /// Builds the shared-reference notice's hover detail for both drawers: which other fields alias this instance
        /// (by display path), what sharing means, and what the notice's two affordances do. Kept here so the IMGUI and
        /// UIToolkit notices always tell the same story.
        /// </summary>
        public static string BuildSharedReferenceDetail(SerializedProperty property)
        {
            var builder = new StringBuilder(
                "This reference is shared — editing it in one place changes every field that uses it.");

            var others = GetSharedReferenceAliasPaths(property);
            if (others.Count > 0)
            {
                builder.Append("\nAlso used by:");
                var shown = Mathf.Min(others.Count, MaxDetailAliasPaths);
                for (var i = 0; i < shown; i++)
                    builder.Append("\n• ").Append(GetPropertyDisplayPath(others[i]));

                if (others.Count > shown)
                    builder.Append("\n• …and ").Append(others.Count - shown).Append(" more");
            }

            builder.Append("\n\nClick the message to highlight the other fields; " +
                           "Make unique gives this field its own independent copy.");
            return builder.ToString();
        }

        // propertyPath → display cache ("sidearms.Array.data[1]" → "Sidearms › Element 1"); the same paths recur on
        // every IMGUI repaint, so the nicified form is built once.
        private static readonly Dictionary<string, string> DisplayPathCache = new();

        /// <summary>
        /// Human-readable form of a serialized property path, matching the labels the inspector itself shows:
        /// "sidearms.Array.data[1].onHitEffect" → "Sidearms › Element 1 › On Hit Effect". Used by the
        /// shared-reference notice to list the other fields aliasing an instance.
        /// </summary>
        public static string GetPropertyDisplayPath(string propertyPath)
        {
            if (string.IsNullOrEmpty(propertyPath)) return string.Empty;
            if (DisplayPathCache.TryGetValue(propertyPath, out var cached)) return cached;

            var builder = new StringBuilder();

            var segments = SerializePropertyExtensions.SimplifyPropertyPath(propertyPath).Split('.');

            foreach (var segment in segments)
            {
                if (builder.Length > 0) builder.Append(" › ");

                var bracket = segment.IndexOf('[');
                builder.Append(ObjectNames.NicifyVariableName(bracket < 0 ? segment : segment[..bracket]));

                // Each "[i]" becomes the inspector's own "Element i" caption.
                for (var open = bracket; open >= 0; open = segment.IndexOf('[', open + 1))
                {
                    var close = segment.IndexOf(']', open);
                    if (close < 0) break;
                    builder.Append(" › Element ").Append(segment, open + 1, close - open - 1);
                }
            }

            return DisplayPathCache[propertyPath] = builder.ToString();
        }

        // Per-object, per-frame memo of each shared (count > 1) id's member property paths, in document order. Built
        // only when a notice actually needs the paths; invalidated with the counts memo above.
        private static int _sharedPathsFrame = -1;
        private static SerializedObject _sharedPathsObject;
        private static readonly Dictionary<long, List<string>> SharedPathsById = new();

        private static Dictionary<long, List<string>> GetSharedReferencePathsById(SerializedObject serializedObject)
        {
            // Refresh the counts for this frame first (this also resets _sharedPathsFrame when it rebuilds).
            var counts = GetReferenceIdCounts(serializedObject);

            var frame = Time.frameCount;
            if (_sharedPathsFrame == frame && ReferenceEquals(_sharedPathsObject, serializedObject))
                return SharedPathsById;

            SharedPathsById.Clear();
            TraverseManagedReferences(serializedObject, other =>
            {
                var id = other.managedReferenceId;
                if (!counts.TryGetValue(id, out var count) || count <= 1) return false;

                if (!SharedPathsById.TryGetValue(id, out var paths)) SharedPathsById[id] = paths = new List<string>();
                paths.Add(other.propertyPath);
                return false;
            });

            _sharedPathsFrame = frame;
            _sharedPathsObject = serializedObject;
            return SharedPathsById;
        }

        /// <summary>
        /// Drops the per-frame managed-reference-id alias memo (see <see cref="HasSharedReference"/>) so the next call
        /// rebuilds it from the current object. Call after a same-frame reassignment (Make unique, type pick, paste, …):
        /// the memo is keyed by frame, so a synchronous re-query right after the mutation would otherwise return this
        /// frame's pre-mutation snapshot and still report the just-broken alias as shared.
        /// </summary>
        public static void InvalidateSharedReferenceCache()
        {
            _aliasFrame = -1;
            _sharedIndicesFrame = -1;
            _sharedPathsFrame = -1;
        }

        // The alias memo is keyed by frame, not content, so a same-frame repaint after an undo would read the pre-undo
        // snapshot. Registered at domain load — before any per-field handler subscribes — so it always runs first.
        [InitializeOnLoadMethod]
        private static void InvalidateAliasMemoOnUndoRedo() =>
            Undo.undoRedoPerformed += InvalidateSharedReferenceCache;

        /// <summary>
        /// Breaks an aliased managed reference by replacing it with an independent clone that carries the same data
        /// (a fresh instance gets a new <see cref="SerializedProperty.managedReferenceId"/> on assignment), so the
        /// two formerly shared fields no longer affect each other.
        /// </summary>
        public static void MakeReferenceUnique(SerializedProperty property)
        {
            var persistent = property.Persistent();
            var current = persistent.managedReferenceValue;
            if (current is null) return;

            // Deep copy: "Make unique" promises an instance independent all the way down — a shallow clone would
            // keep sharing the nested [SerializeReference] children with the alias it just split from.
            persistent.SetManagedReferenceAndApply(CloneManagedReferenceGraph(current));

            // The per-frame alias memo is keyed by frame, not content: an IMGUI repaint in this same Time.frameCount
            // would still read the pre-split snapshot and keep painting the shared notice on both ex-members.
            InvalidateSharedReferenceCache();
        }

        // Visits every managed-reference property in the object, descending into nested values; stops early when the
        // visitor returns true. A cyclic graph would loop forever, so — mirroring BuildConstraintMap — a revisited rid
        // is still reported but its children are not re-entered.
        private static void TraverseManagedReferences(SerializedObject serializedObject, Func<SerializedProperty, bool> visit)
        {
            using var iterator = serializedObject.GetIterator();
            if (!iterator.Next(enterChildren: true)) return;

            var visited = new HashSet<long>();
            bool enterChildren;

            do
            {
                enterChildren = true;

                if (iterator.propertyType == SerializedPropertyType.ManagedReference)
                {
                    if (visit(iterator)) return;

                    var rid = iterator.managedReferenceId;
                    if (rid >= 0 && !visited.Add(rid)) enterChildren = false;
                }
            }
            while (iterator.Next(enterChildren));
        }
        #endregion
    }
}
