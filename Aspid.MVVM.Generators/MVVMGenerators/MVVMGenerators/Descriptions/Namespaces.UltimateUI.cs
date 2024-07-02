using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText UltimateUI = new(nameof(UltimateUI));
    public static readonly NamespaceText UltimateUI_MVVM = new("MVVM", UltimateUI);
    public static readonly NamespaceText UltimateUI_MVVM_ViewModels = new("ViewModels", UltimateUI_MVVM);
}