using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Attribute used to override the binding ID for fields, properties, or [RelayCommand] in a ViewModel and View.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class BindIdAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BindIdAttribute"/> class with a specified ID.
        /// </summary>
        /// <param name="id">The binding ID to be associated with the target field, property, or [RelayCommand].</param>
        public BindIdAttribute(string id) { }
    }
}