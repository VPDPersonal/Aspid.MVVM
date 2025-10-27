using Aspid.Generators.Helper;

namespace Aspid.MVVM.Generators.Generators.Descriptions;

// ReSharper disable InconsistentNaming
public static class Namespaces
{
    public static readonly NamespaceText Aspid = new(nameof(Aspid));
    
    public static readonly NamespaceText Aspid_MVVM = new("MVVM", Aspid);
    public static readonly NamespaceText Aspid_MVVM_UNITY = new("Unity", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_Generated = new("Generated", Aspid_MVVM);
}