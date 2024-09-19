namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText AspidUI = new(nameof(AspidUI));
    public static readonly NamespaceText AspidUI_MVVM = new("MVVM", AspidUI);
    public static readonly NamespaceText AspidUI_MVVM_Views = new("Views", AspidUI_MVVM);
    public static readonly NamespaceText AspidUI_MVVM_Views_Generation = new("Generation", AspidUI_MVVM_Views);
    public static readonly NamespaceText AspidUI_MVVM_Commands = new("Commands", AspidUI_MVVM);
    public static readonly NamespaceText AspidUI_MVVM_Generation = new("Generation", AspidUI_MVVM);
    public static readonly NamespaceText AspidUI_MVVM_ViewModels = new("ViewModels", AspidUI_MVVM);
    public static readonly NamespaceText AspidUI_MVVM_ViewModels_Generation = new("Generation", AspidUI_MVVM_ViewModels);
    
    public static readonly NamespaceText AspidUI_MVVM_Unity = new("Unity", AspidUI_MVVM);
    public static readonly NamespaceText AspidUI_MVVM_Unity_Generation = new("Generation", AspidUI_MVVM_Unity);
    public static readonly NamespaceText AspidUI_MVVM_Unity_Views = new("Views", AspidUI_MVVM_Unity);
    public static readonly NamespaceText AspidUI_MVVM_Unity_ViewModels = new("ViewModels", AspidUI_MVVM_Unity);
}