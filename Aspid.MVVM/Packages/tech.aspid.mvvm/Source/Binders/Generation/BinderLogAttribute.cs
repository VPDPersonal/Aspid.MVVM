using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Generates an explicit <see cref="IBinder{T}.SetValue(T)"/> implementation that records each value in an Inspector log
    /// before calling the marked method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Mark a <c>SetValue</c> method with one parameter that implicitly implements <see cref="IBinder{T}"/>, or
    /// <see cref="IAnyBinder"/> when it is generic, in a <see langword="partial"/> type.
    /// </para>
    /// <para>
    /// The log records values while its Inspector toggle is on, in the Editor only; <c>ASPID_MVVM_BINDER_LOG_DISABLED</c>
    /// removes it.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// [BinderLog]
    /// public void SetValue(string value) => _text.text = value;
    /// </code>
    /// </example>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute { }
}
