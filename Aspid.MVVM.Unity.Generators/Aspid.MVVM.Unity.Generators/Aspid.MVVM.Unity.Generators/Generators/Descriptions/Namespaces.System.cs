using Aspid.Generator.Helpers;

namespace Aspid.MVVM.Generators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText System = new(nameof(System));
    public static readonly NamespaceText System_Runtime = new("Runtime", System);
    public static readonly NamespaceText System_Collections = new("Collections", System);
    public static readonly NamespaceText System_ComponentModel = new("ComponentModel", System);
    public static readonly NamespaceText System_Collections_Generic = new("Generic", System_Collections);
    public static readonly NamespaceText System_Runtime_CompilerServices = new("CompilerServices", System_Runtime);
}