// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

/// <summary>Polyfill: netstandard2.0 doesn't ship this attribute, the C# compiler only needs its shape.</summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
internal sealed class ModuleInitializerAttribute : Attribute;
