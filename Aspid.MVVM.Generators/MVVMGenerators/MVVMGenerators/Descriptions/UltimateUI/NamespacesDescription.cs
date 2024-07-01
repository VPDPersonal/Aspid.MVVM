namespace MVVMGenerators.Descriptions.UltimateUI;

// ReSharper disable InconsistentNaming
public static class NamespacesDescription
{
    public static readonly NamespaceDescription UltimateUI = new(nameof(UltimateUI));
    public static readonly NamespaceDescription UltimateUI_MVVM = new("MVVM", UltimateUI);
    public static readonly NamespaceDescription UltimateUI_MVVM_ViewModels = new("ViewModels", UltimateUI_MVVM);
}