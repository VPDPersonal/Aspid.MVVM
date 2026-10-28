using System;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
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
    public sealed class AccessAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the access modifier of the getter, overriding the constructor argument.
        /// </summary>
        public Access Get { get; set; }
        
        /// <summary>
        /// Gets or sets the access modifier of the setter, overriding the constructor argument.
        /// </summary>
        public Access Set { get; set; }
        
        /// <summary>
        /// Creates an attribute that applies <paramref name="access"/> to both accessors.
        /// </summary>
        /// <param name="access">The access modifier of the getter and the setter.</param>
        public AccessAttribute(Access access = Access.Private) { }
    }
}
