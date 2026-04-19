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
    public sealed class SerializableType
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [SerializeField] private string _assemblyQualifiedName;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private Type? _type;

        /// <summary>
        /// The base constraint type. Always <see cref="object"/> for unconstrained <see cref="SerializableType"/>.
        /// </summary>
        public Type BaseType => typeof(object);

        /// <summary>
        /// Returns the resolved <see cref="System.Type"/>, or <c>null</c>
        /// if the stored assembly-qualified name could not be matched to any loaded assembly.
        /// </summary>
        public Type? Type
        {
            get
            {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
                using (this.Marker())
#endif
                {
                    return _type ??= Type.GetType(_assemblyQualifiedName, throwOnError: false);
                }
            }
        }

        /// <summary>
        /// Implicitly converts to <see cref="System.Type"/>. Equivalent to accessing <see cref="Type"/>.
        /// </summary>
        public static implicit operator Type?(SerializableType type) => type.Type;
    }

    /// <summary>
    /// A wrapper around <see cref="System.Type"/> that supports Unity Inspector serialization,
    /// constrained to types assignable to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The base constraint type. Only subtypes will be offered in the editor picker.</typeparam>
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
    public sealed class SerializableType<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [SerializeField] private string _assemblyQualifiedName;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private Type? _type;

        /// <summary>
        /// The base constraint type. Always <typeparamref name="T"/>.
        /// </summary>
        public Type BaseType => typeof(T);

        /// <summary>
        /// Returns the resolved <see cref="System.Type"/>, or <c>null</c>
        /// if the stored assembly-qualified name could not be matched to any loaded assembly.
        /// </summary>
        public Type? Type
        {
            get
            {
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
                using (this.Marker())
#endif
                {
                    return _type ??= Type.GetType(_assemblyQualifiedName, throwOnError: false);
                }
            }
        }

        /// <summary>
        /// Implicitly converts to <see cref="System.Type"/>. Equivalent to accessing <see cref="Type"/>.
        /// </summary>
        public static implicit operator Type?(SerializableType<T> type) => type.Type;
    }
}
