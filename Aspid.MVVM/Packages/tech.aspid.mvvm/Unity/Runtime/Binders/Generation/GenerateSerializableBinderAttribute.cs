using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates a serializable binder from the annotated <see cref="MonoBinder"/> class.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public sealed class GenerateSerializableBinderAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether required base constructor parameters precede serialized options and stay required.
        /// </summary>
        [SuppressMessage(category: "ReSharper", checkId: "UnusedAutoPropertyAccessor.Global")]
        public bool PreserveRequiredBaseParameters { get; set; }
    }
}
