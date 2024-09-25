namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText Aspid = new(nameof(Aspid));
    
    public static readonly NamespaceText Aspid_UI = new("UI", Aspid);
    public static readonly NamespaceText Aspid_UI_MVVM = new("MVVM", Aspid_UI);
    public static readonly NamespaceText Aspid_UI_MVVM_Views = new("Views", Aspid_UI_MVVM);
    public static readonly NamespaceText Aspid_UI_MVVM_Extensions = new("Extensions", Aspid_UI_MVVM);
    public static readonly NamespaceText Aspid_UI_MVVM_Views_Generation = new("Generation", Aspid_UI_MVVM_Views);
    public static readonly NamespaceText Aspid_UI_MVVM_Commands = new("Commands", Aspid_UI_MVVM);
    public static readonly NamespaceText Aspid_UI_MVVM_Generation = new("Generation", Aspid_UI_MVVM);
    public static readonly NamespaceText Aspid_UI_MVVM_ViewModels = new("ViewModels", Aspid_UI_MVVM);
    public static readonly NamespaceText Aspid_UI_MVVM_ViewModels_Generation = new("Generation", Aspid_UI_MVVM_ViewModels);
    
    public static readonly NamespaceText Aspid_UI_MVVM_Mono = new("Mono", Aspid_UI_MVVM);
    public static readonly NamespaceText Aspid_UI_MVVM_Mono_Generation = new("Generation", Aspid_UI_MVVM_Mono);
    public static readonly NamespaceText Aspid_UI_MVVM_Mono_Views = new("Views", Aspid_UI_MVVM_Mono);
    public static readonly NamespaceText Aspid_UI_MVVM_Mono_ViewModels = new("ViewModels", Aspid_UI_MVVM_Mono);
}