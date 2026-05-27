using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="Attribute"/> applied to fields or properties of a type carrying <see cref="ViewAttribute"/>;
    /// directs the Source Generator to emit binding code that wires the member to the supplied <see cref="IBinder"/> type.
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AsBinderAttribute : Attribute
    {
#if UNITY_EDITOR || DEBUG
        /// <summary>
        /// Gets the <see cref="IBinder"/> type used to bind the field or property.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public readonly Type Type;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="AsBinderAttribute"/> with the specified <see cref="IBinder"/> type and optional arguments.
        /// </summary>
        /// <param name="type">The type of <see cref="IBinder"/> that will be used to bind the field or property.</param>
        /// <param name="arguments">Additional arguments that can be passed to the constructor of the <see cref="IBinder"/> type.</param>
        public AsBinderAttribute(Type type, params object[] arguments)
        {
#if UNITY_EDITOR || DEBUG
            Type = type;
#endif
        }
    }
}
