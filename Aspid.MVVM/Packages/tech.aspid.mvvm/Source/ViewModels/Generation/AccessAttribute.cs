using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sets the access modifiers of the property generated for a bind field of a <see cref="ViewModelAttribute">[ViewModel]</see> class.
    /// </summary>
    /// <remarks>
    /// Without it, both accessors are <see langword="private"/>; on a field without a bind attribute, it is ignored.
    /// </remarks>
    /// <example>
    /// <code>
    /// [Bind]
    /// [Access(Access.Public)]
    /// private int _health;
    /// </code>
    /// <code>
    /// [Bind]
    /// [Access(Get = Access.Public, Set = Access.Protected)]
    /// private int _health;
    /// </code>
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Field)]
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
    public sealed class AccessAttribute : Attribute
    {
        private Access _get;
        private Access _set;

        /// <summary>
        /// Creates an attribute that applies <paramref name="access"/> to both accessors.
        /// </summary>
        /// <param name="access">The access modifier of the getter and the setter.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="access"/> is not a declared access modifier.</exception>
        public AccessAttribute(Access access = Access.Private)
        {
            EnsureValid(access, nameof(access));
            _get = access;
            _set = access;
        }

        /// <summary>
        /// Gets or sets the access modifier of the getter, overriding the constructor argument.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value is not a declared access modifier.</exception>
        public Access Get
        {
            get => _get;
            set
            {
                EnsureValid(value, nameof(value));
                _get = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the access modifier of the setter, overriding the constructor argument.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value is not a declared access modifier.</exception>
        public Access Set
        {
            get => _set;
            set
            {
                EnsureValid(value, nameof(value));
                _set = value;
            }
        }
        
        private static void EnsureValid(Access access, string parameterName)
        {
            if (access is Access.Private or Access.Protected or Access.Public) return;
            throw new ArgumentOutOfRangeException(parameterName, access, "The access modifier must be Private, Protected or Public.");
        }
    }
}
