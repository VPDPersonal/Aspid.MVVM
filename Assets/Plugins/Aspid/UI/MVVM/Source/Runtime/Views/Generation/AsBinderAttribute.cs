using System;

namespace Aspid.UI.MVVM.Views.Generation
{
    /// <summary>
    /// Marker attribute for fields and properties within a class or structure marked with the <see cref="ViewAttribute"/>.
    /// Used by the Source Generator to generate binding code based on the provided <see cref="IBinder"/> type in the View.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AsBinderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsBinderAttribute"/> with the specified <see cref="IBinder"/> type and optional arguments.
        /// </summary>
        /// <param name="type">The type of <see cref="IBinder"/> that will be used to bind the field or property.</param>
        /// <param name="args">Additional arguments that can be passed to the constructor of the <see cref="IBinder"/> type.</param>
        public AsBinderAttribute(Type type, params object[] args) { }
    }
}