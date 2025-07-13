namespace Aspid.MVVM;

/// <summary>
/// Marker attribute for methods in partial classes or structures that implement <see cref="IBinder{T}"/>.
/// Applicable only to <c>SetValue</c> methods that are implicit implementations of the <see cref="IBinder{T}"/> interface.
/// Used for logging during development.
/// Used by the Source Generator to generate an explicit implementation of the <c>SetValue</c> method with added logging logic.
/// </summary>
#if UNITY_2022_1_OR_NEWER
[Conditional("UNITY_EDITOR")]
#endif
[AttributeUsage(AttributeTargets.Method)]
public sealed class BinderLogAttribute : Attribute { }