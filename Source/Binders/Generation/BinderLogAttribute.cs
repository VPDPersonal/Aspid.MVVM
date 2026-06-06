using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Instructs the Source Generator to generate an explicit <see cref="IBinder{T}"/> <see cref="IBinder{T}.SetValue(T)"/> implementation
    /// with added logging, wrapping the annotated method.
    /// </summary>
    /// <remarks>
    /// Must be applied only to <see cref="IBinder{T}.SetValue(T)"/> methods that implicitly implement <see cref="IBinder{T}"/>
    /// in a <see langword="partial"/> class.
    /// The attribute is stripped in non-<c>UNITY_EDITOR</c> builds, so no logging is generated outside the Editor.
    /// </remarks>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute { }
}
