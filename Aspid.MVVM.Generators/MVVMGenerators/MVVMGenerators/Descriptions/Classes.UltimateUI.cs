using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    public static readonly TypeText IViewModel =
        new("IViewModel", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly TypeText BindAttribute =
        new("BindAttribute", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly TypeText ViewModelAttribute =
        new("ViewModelAttribute", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly TypeText BindMethods =
        new("BindMethods", Namespaces.UltimateUI_MVVM_ViewModels);

    public static readonly TypeText ViewModelUtility =
        new("ViewModelUtility", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly TypeText IReadOnlyBindsMethods =
        new("IReadOnlyBindsMethods", Namespaces.UltimateUI_MVVM_ViewModels); 
}