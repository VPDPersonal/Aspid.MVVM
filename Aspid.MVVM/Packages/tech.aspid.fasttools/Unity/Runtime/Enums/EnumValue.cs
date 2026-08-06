using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums
{
    /// <summary>
    /// A single serializable entry in an <see cref="EnumValues{TValue}"/> collection,
    /// associating one enum member with a value of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value mapped to the enum member.</typeparam>
    /// <remarks>
    /// The enum key is stored as a string and resolved to an <see cref="Enum"/> instance
    /// (plus its numeric value for lookups) lazily via <see cref="Initialize"/>. This class is
    /// managed entirely by <see cref="EnumValues{TValue}"/> / <see cref="EnumValues{TEnum,TValue}"/>
    /// and is not intended for direct instantiation.
    /// </remarks>
    [Serializable]
    internal sealed class EnumValue<TValue>
    {
        [SerializeField] private string _key;
        [SerializeField] private TValue _value;

#if UNITY_EDITOR
        // Mirrors EnumValues._enumType so per-element drawers can render the correct
        // EnumField/EnumFlagsField without walking up the SerializedProperty hierarchy.
        [SerializeField] private string _enumType;
#endif

        /// <summary>
        /// The value mapped to <see cref="Key"/>.
        /// </summary>
        public TValue Value => _value;

        /// <summary>
        /// The resolved enum member. Valid only when <see cref="IsResolved"/> is <see langword="true"/>.
        /// </summary>
        public Enum Key { get; private set; }

        /// <summary>
        /// The resolved key's underlying integral value (see <see cref="EnumInfo.ToInt64"/>).
        /// Valid only when <see cref="IsResolved"/> is <see langword="true"/>.
        /// </summary>
        public long NumericKey { get; private set; }

        /// <summary>
        /// Whether <see cref="Initialize"/> successfully parsed the serialized key.
        /// Unresolved entries never match a lookup.
        /// </summary>
        public bool IsResolved => Key is not null;

        /// <summary>
        /// Resolves the serialized string key to an <see cref="Enum"/> instance of the supplied type.
        /// Logs an error (without throwing) if the key cannot be parsed.
        /// </summary>
        /// <param name="type">The enum type to parse the key against.</param>
        public void Initialize(Type type)
        {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                if (Enum.TryParse(type, _key, out var parsedEnum))
                {
                    Key = (Enum)parsedEnum;
                    NumericKey = EnumInfo.ToInt64(Key);
                }
                else
                {
                    // A stale Key from a previous type would silently keep matching lookups,
                    // so reset the entry instead of leaving it as-is.
                    Reset();

                    Debug.LogError($"[{nameof(EnumValue<TValue>)}] [{nameof(Initialize)}] " +
                        $"Couldn't parse key '{_key}' to Enum '{type.FullName}'");
                }
            }
        }

        /// <summary>
        /// Clears the resolved key so the entry no longer matches lookups and is skipped by
        /// enumeration — used when the owning collection degrades to an unconfigured state.
        /// </summary>
        public void Reset()
        {
            Key = null;
            NumericKey = 0L;
        }
    }
}
