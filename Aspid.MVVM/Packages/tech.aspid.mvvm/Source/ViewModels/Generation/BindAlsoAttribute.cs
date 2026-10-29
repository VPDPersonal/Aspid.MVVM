using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
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
        public BindAlsoAttribute(string propertyName)
        {
#if UNITY_EDITOR || DEBUG
            PropertyName = propertyName;
#endif
        }
    }
}
