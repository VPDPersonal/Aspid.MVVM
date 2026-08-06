#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// A wrapper around <see cref="System.Type"/> that supports Unity Inspector serialization.
    /// The type is stored by its <c>AssemblyQualifiedName</c> and resolved lazily on first access.
    /// </summary>
    /// <example>
    /// Declare a serializable type field and use the resolved type at runtime:
    /// <code>
    /// public class MyComponent : MonoBehaviour
    /// {
    ///     [SerializeField] private SerializableType _targetType;
    ///
    ///     private void Start()
    ///     {
    ///         Type type = _targetType;  // implicit conversion
    ///         if (type != null)
    ///             Debug.Log(type.FullName);
    ///     }
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class SerializableType : ISerializableType, ISerializationCallbackReceiver
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [SerializeField] private string _assemblyQualifiedName;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private Type? _type;

        /// <inheritdoc />
        public Type BaseType => typeof(object);

        /// <inheritdoc />
        public Type? Type
        {
            get
            {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
                using (this.Marker())
#endif
                {
                    return _type ??= GetTypeFromAssemblyQualifiedName(_assemblyQualifiedName);
                }
            }
        }

        /// <summary>
        /// Resolves and returns the wrapped type; equivalent to <see cref="Type"/>.
        /// A <see langword="null"/> wrapper converts to <see langword="null"/>.
        /// </summary>
        public static implicit operator Type?(SerializableType? type) => type?.Type;

        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _type = null;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        // Unity deserialization coerces the field to "", but a code-constructed instance still holds
        // null — and Type.GetType(null) throws ArgumentNullException regardless of throwOnError,
        // breaking the "or null" contract above for a plain `new SerializableType()`.
        internal static Type? GetTypeFromAssemblyQualifiedName(string? assemblyQualifiedName) => string.IsNullOrWhiteSpace(assemblyQualifiedName)
            ? null
            : Type.GetType(assemblyQualifiedName, throwOnError: false);
    }

    /// <summary>
    /// A wrapper around <see cref="System.Type"/> that supports Unity Inspector serialization,
    /// constrained to types assignable to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base constraint type. The editor picker offers only types assignable to it.</typeparam>
    /// <example>
    /// Constrain the picker to <c>MonoBehaviour</c> subtypes only:
    /// <code>
    /// public class MyComponent : MonoBehaviour
    /// {
    ///     [SerializeField] private SerializableType&lt;MonoBehaviour&gt; _behaviourType;
    ///
    ///     private void Start()
    ///     {
    ///         Type type = _behaviourType;  // always a MonoBehaviour subtype or null
    ///         if (type != null)
    ///             gameObject.AddComponent(type);
    ///     }
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public sealed class SerializableType<T> : ISerializableType, ISerializationCallbackReceiver
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [SerializeField] private string _assemblyQualifiedName;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private Type? _type;

        /// <inheritdoc />
        public Type BaseType => typeof(T);

        /// <inheritdoc />
        public Type? Type
        {
            get
            {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
                using (this.Marker())
#endif
                {
                    return _type ??= SerializableType.GetTypeFromAssemblyQualifiedName(_assemblyQualifiedName);
                }
            }
        }

        /// <summary>
        /// Resolves and returns the wrapped type; equivalent to <see cref="Type"/>.
        /// A <see langword="null"/> wrapper converts to <see langword="null"/>.
        /// </summary>
        public static implicit operator Type?(SerializableType<T>? type) => type?.Type;

        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _type = null;

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }
}
