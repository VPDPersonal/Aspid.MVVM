#nullable enable
using System;
using UnityEngine;
using System.Collections;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
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
    /// </para>
    /// <para>
    /// For <c>[Flags]</c> enums <see cref="Equals(Enum,Enum)"/> uses <c>HasFlag</c> semantics
    /// with special handling for the zero (<c>None</c>) value — two values are considered equal
    /// only when both are zero or both are non-zero and one has all bits of the other set.
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
    public sealed class EnumValues<TValue> : IEnumerable<KeyValuePair<Enum, TValue>>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [TypeSelector(typeof(Enum))]
        [SerializeField] private string _enumType;
        
        [SerializeField] private TValue _defaultValue;
        [SerializeField] private EnumValue<TValue>[] _values;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private bool _isFlag;
        private bool _isInitialized;
        
        private void Initialize()
        {
            if (_isInitialized) return;
            
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                var type = Type.GetType(_enumType, throwOnError: true);
            
                foreach (var value in _values)
                    value.Initialize(type);
            
                _isFlag = type.IsDefined(typeof(FlagsAttribute), false);
                _isInitialized = true;
            }
        }
        
        /// <summary>
        /// Returns the value mapped to <paramref name="enumValue"/>,
        /// or the configured default value if no mapping exists.
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
            
                foreach (var value in _values)
                {
                    if (Equals(enumValue, value.Key))
                        return value.Value; 
                }

                return _defaultValue;
            }
        }

        /// <summary>
        /// Determines whether two enum values should be considered equal for lookup purposes.
        /// </summary>
        /// <param name="enumValue1">The first enum value.</param>
        /// <param name="enumValue2">The second enum value.</param>
        /// <returns>
        /// For regular enums: <see langword="true"/> when both values are identical.<br/>
        /// For <c>[Flags]</c> enums: <see langword="true"/> when <paramref name="enumValue1"/>
        /// has all bits of <paramref name="enumValue2"/> set, with the additional rule that
        /// the zero (<c>None</c>) value is only equal to another zero value.
        /// </returns>
        public bool Equals(Enum enumValue1, Enum enumValue2)
        {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                Initialize();
                
                if (_isFlag)
                {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
                    using (this.Marker().WithName("Equals.HasFlag"))
#endif
                    {
                        if (!enumValue1.HasFlag(enumValue2)) return false;
                
                        var isEnum1None = Convert.ToInt32(enumValue1) is 0;
                        var isEnum2None = Convert.ToInt32(enumValue2) is 0;
                        return isEnum1None == isEnum2None;
                    }
                }
            
                return enumValue1.Equals(enumValue2);
            }
        }
        
        public IEnumerator<KeyValuePair<Enum, TValue>> GetEnumerator()
        {
            Initialize();
            
            foreach (var value in _values)
            {
                yield return new KeyValuePair<Enum, TValue>(value.Key, value.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
