#nullable enable
using System;
using UnityEngine;
using System.Collections;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
// ReSharper disable NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
namespace Aspid.FastTools.Enums
{
    /// <summary>
    /// A serializable dictionary that maps each member of a chosen enum to a value of type
    /// <typeparamref name="TValue"/>. Supports both regular and <c>[Flags]</c> enums.
    /// </summary>
    /// <typeparam name="TValue">The type of the value associated with each enum member.</typeparam>
    /// <remarks>
    /// <para>
    /// The enum type is selected in the Inspector via a <see cref="TypeSelectorAttribute"/>
    /// and stored as an assembly-qualified name. All entries are initialized lazily on first access.
    /// When the enum type is already known at compile time, prefer
    /// <see cref="EnumValues{TEnum,TValue}"/> — its Inspector type-picker is read-only.
    /// </para>
    /// <para>
    /// For <c>[Flags]</c> enums <see cref="Equals(Enum,Enum)"/> uses flag-containment semantics
    /// with special handling for the zero (<c>None</c>) value — two values are considered equal
    /// only when both are zero or both are non-zero and one has all bits of the other set.
    /// </para>
    /// <para>
    /// <see cref="GetValue"/> returns the configured default value when no entry matches the lookup key.
    /// For <c>[Flags]</c> enums multiple entries may match a single lookup value; an exact-key entry
    /// always wins first, and only if none exists does the first entry (in serialized order) whose
    /// bits are all contained in the lookup value win.
    /// </para>
    /// <para>
    /// Iteration via <see cref="GetEnumerator"/> yields only the explicitly configured entries and
    /// does <b>not</b> include the default value.
    /// </para>
    /// <para>
    /// Internal hot paths are wrapped in profiler markers; define the
    /// <c>ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED</c> scripting symbol to compile them out.
    /// </para>
    /// </remarks>
    /// <example>
    /// Map a damage type to a color:
    /// <code>
    /// public class HitEffect : MonoBehaviour
    /// {
    ///     [SerializeField] private EnumValues&lt;Color&gt; _damageColors;
    ///
    ///     public Color GetColor(DamageType type) =>
    ///         _damageColors.GetValue(type);
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class EnumValues<TValue> : IEnumerable<KeyValuePair<Enum, TValue>>, ISerializationCallbackReceiver
    {
#pragma warning disable CS8618
        [TypeSelector(typeof(Enum), Required = true)]
        [SerializeField] private string _enumType;

        [SerializeField] private TValue _defaultValue;
        [SerializeField] private EnumValue<TValue>[] _values;
#pragma warning restore CS8618

        private Type? _type;
        private bool _isFlag;
        private bool _isInitialized;

        private void Initialize()
        {
            if (_isInitialized) return;

#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                _values ??= Array.Empty<EnumValue<TValue>>();

                // Unconfigured field — degrade to "no entries match" instead of crashing.
                // Reset the caches too: the type may have been configured before and cleared
                // since (OnAfterDeserialize only resets _isInitialized).
                if (string.IsNullOrWhiteSpace(_enumType))
                {
                    Debug.LogWarning($"[{nameof(EnumValues<TValue>)}] [{nameof(Initialize)}] " +
                        "No enum type configured — GetValue will always return the default value.");

                    Degrade();
                    return;
                }

                // Unresolvable type (renamed/moved since the asset was saved) — degrade the same
                // way instead of throwing on every lookup.
                if (Type.GetType(_enumType, throwOnError: false) is not { } type)
                {
                    Debug.LogError($"[{nameof(EnumValues<TValue>)}] [{nameof(Initialize)}] " +
                        $"Couldn't resolve enum type '{_enumType}' — GetValue will always return the default value.");

                    Degrade();
                    return;
                }

                // Resolvable but not an enum (e.g. the type was refactored into a class/struct
                // keeping the same name) — degrade the same way instead of throwing from
                // Enum.TryParse on every lookup.
                if (!type.IsEnum)
                {
                    Debug.LogError($"[{nameof(EnumValues<TValue>)}] [{nameof(Initialize)}] " +
                        $"Type '{_enumType}' is not an enum — GetValue will always return the default value.");

                    Degrade();
                    return;
                }

                foreach (var value in _values)
                    value.Initialize(type);

                _type = type;
                _isFlag = type.IsDefined(typeof(FlagsAttribute), false);
                _isInitialized = true;
            }
            return;

            void Degrade()
            {
                // Reset the entries too — keys resolved by a previous initialization would
                // otherwise keep matching lookups and being yielded by GetEnumerator even
                // though the type is no longer configured.
                foreach (var value in _values)
                    value.Reset();

                _type = null;
                _isFlag = false;
                _isInitialized = true;
            }
        }

        /// <summary>
        /// Returns the value mapped to <paramref name="enumValue"/>,
        /// or the configured default value if no mapping exists.
        /// A value of a different enum type than the configured one never matches.
        /// </summary>
        /// <param name="enumValue">The enum member to look up.</param>
        /// <returns>The mapped value, or the default value when no entry matches.</returns>
        public TValue GetValue(Enum enumValue)
        {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                Initialize();

                // Keys of another enum type could still collide numerically — never match them.
                // A null lookup on an unconfigured/empty collection degrades to the default too.
                if (enumValue is null || _type is null || enumValue.GetType() != _type)
                    return _defaultValue;

                var lookup = EnumInfo.ToInt64(enumValue);
                return EnumValueLookup.Find(_values, lookup, _isFlag, _defaultValue);
            }
        }

        /// <summary>
        /// Determines whether two enum values should be considered equal for lookup purposes.
        /// The first argument is the value being looked up; the second is the entry's stored key.
        /// </summary>
        /// <param name="enumValue1">The lookup value (must contain the entry's bits to match).</param>
        /// <param name="enumValue2">The stored entry key.</param>
        /// <returns>
        /// For regular enums: <see langword="true"/> when both values are identical.<br/>
        /// For <c>[Flags]</c> enums: <see langword="true"/> when <paramref name="enumValue1"/>
        /// has all bits of <paramref name="enumValue2"/> set, with the additional rule that
        /// the zero (<c>None</c>) value is only equal to another zero value.<br/>
        /// Values of different enum types are never equal.
        /// </returns>
        public bool Equals(Enum enumValue1, Enum enumValue2)
        {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                Initialize();

                // Unresolved key (see EnumValue.Initialize) — never matches.
                if (enumValue1 is null || enumValue2 is null)
                    return false;

                // Different enum types could still collide numerically — never equal.
                if (enumValue1.GetType() != enumValue2.GetType())
                    return false;

                var value1 = EnumInfo.ToInt64(enumValue1);
                var value2 = EnumInfo.ToInt64(enumValue2);

                return _isFlag ? EnumValueLookup.FlagsEquals(value1, value2) : value1 == value2;
            }
        }

        /// <summary>
        /// Returns a struct enumerator over the explicitly configured (key, value) pairs in
        /// serialized order — <c>foreach</c> binds to it directly and does not allocate.
        /// Does <b>not</b> include the default value or entries with an unresolved key.
        /// </summary>
        public EnumValuesEnumerator<Enum, TValue> GetEnumerator()
        {
            Initialize();
            return new EnumValuesEnumerator<Enum, TValue>(_values);
        }

        IEnumerator<KeyValuePair<Enum, TValue>> IEnumerable<KeyValuePair<Enum, TValue>>.GetEnumerator() =>
            GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <summary>
        /// Invalidates the resolved-key cache so the next lookup re-resolves it — otherwise
        /// entries added via an Inspector edit (e.g. "Populate Missing Enum Members") would stay
        /// unresolved.
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _isInitialized = false;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }

    /// <summary>
    /// A serializable dictionary that maps members of <typeparamref name="TEnum"/> to values of
    /// type <typeparamref name="TValue"/>. The typed counterpart of <see cref="EnumValues{TValue}"/>
    /// for the common case where the enum type is known at compile time — the Inspector type-picker
    /// is read-only, and lookups are compile-time safe.
    /// </summary>
    /// <typeparam name="TEnum">The enum type the entries are keyed by.</typeparam>
    /// <typeparam name="TValue">The type of the value associated with each enum member.</typeparam>
    /// <remarks>
    /// <para>
    /// Lookup semantics (including <c>[Flags]</c> handling) are identical to
    /// <see cref="EnumValues{TValue}"/> — see its remarks for details. The entries are the same
    /// <see cref="EnumValue{TValue}"/> instances, resolved once against
    /// <typeparamref name="TEnum"/>; steady-state <see cref="GetValue"/>, <see cref="Equals"/>
    /// and <c>foreach</c> (which binds to the struct <see cref="EnumValuesEnumerator{TKey,TValue}"/>)
    /// never allocate.
    /// </para>
    /// <para>
    /// In the editor the serialized layout is compatible with <see cref="EnumValues{TValue}"/>:
    /// the enum type is still stored in a hidden editor-only <c>_enumType</c> field, auto-filled
    /// with <typeparamref name="TEnum"/>'s assembly-qualified name on serialization. Switching a
    /// field between the two variants therefore migrates existing data, as long as the configured
    /// enum type matches <typeparamref name="TEnum"/>. Player builds strip the field — at runtime
    /// the enum type comes from the generic argument alone.
    /// </para>
    /// <para>
    /// Internal hot paths are wrapped in profiler markers; define the
    /// <c>ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED</c> scripting symbol to compile them out.
    /// </para>
    /// </remarks>
    /// <example>
    /// Map a damage type to a color, with the enum fixed at compile time:
    /// <code>
    /// public class HitEffect : MonoBehaviour
    /// {
    ///     [SerializeField] private EnumValues&lt;DamageType, Color&gt; _damageColors;
    ///
    ///     public Color GetColor(DamageType type) =>
    ///         _damageColors.GetValue(type);
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class EnumValues<TEnum, TValue> : IEnumerable<KeyValuePair<TEnum, TValue>>, ISerializationCallbackReceiver
        where TEnum : struct, Enum
    {
#if UNITY_EDITOR
        // Editor-only layout mirror of EnumValues<TValue>._enumType, auto-filled from TEnum on
        // serialization — keeps the two variants layout-compatible in the editor (where variant
        // switching happens) and feeds the per-element editor drawers. Never read at runtime
        // (the enum type comes from the generic argument), so it is stripped from player builds.
        // ReSharper disable once NotAccessedField.Local
        [SerializeField] private string? _enumType;
#endif

#pragma warning disable CS8618
        [SerializeField] private TValue _defaultValue;
        [SerializeField] private EnumValue<TValue>[] _values;
#pragma warning restore CS8618

        private bool _isInitialized;

        private void Initialize()
        {
            if (_isInitialized) return;

#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                _values ??= Array.Empty<EnumValue<TValue>>();

                foreach (var value in _values)
                    value.Initialize(typeof(TEnum));

                _isInitialized = true;
            }
        }

        /// <inheritdoc cref="EnumValues{TValue}.GetValue"/>
        public TValue GetValue(TEnum enumValue)
        {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                Initialize();

                var lookup = EnumInfo<TEnum>.ToInt64(enumValue);
                return EnumValueLookup.Find(_values, lookup, EnumInfo<TEnum>.IsFlags, _defaultValue);
            }
        }

        /// <inheritdoc cref="EnumValues{TValue}.Equals(Enum,Enum)"/>
        public bool Equals(TEnum enumValue1, TEnum enumValue2)
        {
            var value1 = EnumInfo<TEnum>.ToInt64(enumValue1);
            var value2 = EnumInfo<TEnum>.ToInt64(enumValue2);

            return EnumInfo<TEnum>.IsFlags ? EnumValueLookup.FlagsEquals(value1, value2) : value1 == value2;
        }

        /// <inheritdoc cref="EnumValues{TValue}.GetEnumerator"/>
        public EnumValuesEnumerator<TEnum, TValue> GetEnumerator()
        {
            Initialize();
            return new EnumValuesEnumerator<TEnum, TValue>(_values);
        }

        IEnumerator<KeyValuePair<TEnum, TValue>> IEnumerable<KeyValuePair<TEnum, TValue>>.GetEnumerator() =>
            GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        /// <summary>
        /// Invalidates the resolved-key cache so the next lookup re-resolves it — otherwise
        /// entries added via an Inspector edit (e.g. "Populate Missing Enum Members") would stay
        /// unresolved.
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _isInitialized = false;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
#if UNITY_EDITOR
            _enumType = typeof(TEnum).AssemblyQualifiedName;
#endif
        }
    }
}
