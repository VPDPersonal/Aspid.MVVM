namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText UltimateUI = new(nameof(UltimateUI));
    public static readonly NamespaceText UltimateUI_MVVM = new("MVVM", UltimateUI);
    public static readonly NamespaceText UltimateUI_MVVM_Views = new("Views", UltimateUI_MVVM);
    public static readonly NamespaceText UltimateUI_MVVM_Commands = new("Commands", UltimateUI_MVVM);
    public static readonly NamespaceText UltimateUI_MVVM_ViewModels = new("ViewModels", UltimateUI_MVVM);
    
    public static readonly NamespaceText UltimateUI_MVVM_Unity = new("Unity", UltimateUI_MVVM);
    public static readonly NamespaceText UltimateUI_MVVM_Unity_Views = new("Views", UltimateUI_MVVM_Unity);
    public static readonly NamespaceText UltimateUI_MVVM_Unity_ViewModels = new("ViewModels", UltimateUI_MVVM_Unity);
}