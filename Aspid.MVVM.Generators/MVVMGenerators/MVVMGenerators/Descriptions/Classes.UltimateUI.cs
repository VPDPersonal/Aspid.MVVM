using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Binders
    public static readonly TypeText IBinder =
        new("IBinder", Namespaces.UltimateUI_MVVM);
    
    public static readonly TypeText IAnyBinder =
        new("IAnyBinder", Namespaces.UltimateUI_MVVM);
    
    public static readonly TypeText MonoBinder =
        new("MonoBinder", Namespaces.UltimateUI_MVVM);
    
    public static readonly AttributeText BinderLogAttribute =
        new("BinderLog", Namespaces.UltimateUI_MVVM);
    
    public static readonly AttributeText BindInheritorsAlsoAttribute =
        new("BindInheritorsAlso", Namespaces.UltimateUI_MVVM);
    #endregion
    
    public static readonly TypeText ViewAttribute = 
        new("ViewAttribute", Namespaces.UltimateUI_MVVM_Views);

    #region View Models
    public static readonly TypeText IViewModel =
        new("IViewModel", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly AttributeText BindAttribute =
        new("Bind", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModel", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly TypeText BindMethods =
        new("BindMethods", Namespaces.UltimateUI_MVVM_ViewModels);

    public static readonly TypeText ViewModelUtility =
        new("ViewModelUtility", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly TypeText IReadOnlyBindsMethods =
        new("IReadOnlyBindsMethods", Namespaces.UltimateUI_MVVM_ViewModels); 
   #endregion
}