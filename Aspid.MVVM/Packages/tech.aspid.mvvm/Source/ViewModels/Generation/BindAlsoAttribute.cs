using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Notifies the binders of another bindable property whenever the marked member of a <see cref="ViewModelAttribute">[ViewModel]</see> class changes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The marked member needs <see cref="BindAttribute"/>, <see cref="OneWayBindAttribute"/> or
    /// <see cref="TwoWayBindAttribute"/> with a mode that pushes values to the View.
    /// </para>
    /// <para>A property of the class without a bind attribute becomes a <see cref="BindMode.OneWay"/> bindable member when named here.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// [Bind]
    /// [BindAlso(nameof(FullName))]
    /// private string _firstName;
    ///
    /// private string FullName => $"{FirstName} {LastName}";
    /// </code>
    /// </example>
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class BindAlsoAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// Gets the name of the property whose binders are also notified.
        /// </summary>
        /// <remarks>
        /// Exists only when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string PropertyName { get; }
#endif

        /// <summary>
        /// Creates an attribute that also notifies the property named <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the generated or declared property to notify.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is empty or whitespace.</exception>
        public BindAlsoAttribute(string propertyName)
        {
            if (propertyName is null)
                throw new ArgumentNullException(nameof(propertyName));
            
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("A dependent property name cannot be empty or whitespace.", nameof(propertyName));

#if UNITY_EDITOR || DEBUG
            PropertyName = propertyName;
#endif
        }
    }
}
