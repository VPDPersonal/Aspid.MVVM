using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Records incoming values in the Inspector before calling the marked <c>SetValue</c> method.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The method must implicitly implement <see cref="IBinder{T}"/> or <see cref="IAnyBinder"/> in a <see langword="partial"/> type.
    /// </para>
    /// <para>
    /// Recording runs only in the Editor while enabled in the Inspector. Define <c>ASPID_MVVM_BINDER_LOG_DISABLED</c> to omit the log.
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
