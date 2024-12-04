namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText Aspid = new(nameof(Aspid));
    
    public static readonly NamespaceText Aspid_MVVM = new("MVVM", Aspid);
    public static readonly NamespaceText Aspid_MVVM_Views = new("Views", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_Extensions = new("Extensions", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_Views_Generation = new("Generation", Aspid_MVVM_Views);
    public static readonly NamespaceText Aspid_MVVM_Commands = new("Commands", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_Generation = new("Generation", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_ViewModels = new("ViewModels", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_ViewModels_Generation = new("Generation", Aspid_MVVM_ViewModels);
    
    public static readonly NamespaceText Aspid_MVVM_Mono = new("Mono", Aspid_MVVM);
    public static readonly NamespaceText Aspid_MVVM_Mono_Generation = new("Generation", Aspid_MVVM_Mono);
    public static readonly NamespaceText Aspid_MVVM_Mono_Views = new("Views", Aspid_MVVM_Mono);
    public static readonly NamespaceText Aspid_MVVM_Mono_ViewModels = new("ViewModels", Aspid_MVVM_Mono);
}