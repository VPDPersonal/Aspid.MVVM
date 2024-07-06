using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Views
    public static readonly TypeText IView =
        new("IView", Namespaces.UltimateUI_MVVM_Views);
    
    public static readonly AttributeText ViewAttribute = 
        new("View", Namespaces.UltimateUI_MVVM_Views);

    public static readonly TypeText MonoView =
        new("MonoView", Namespaces.UltimateUI_MVVM_Views);
    
    public static readonly TypeText BindersCollectionById =
        new("BindersCollectionById", Namespaces.UltimateUI_MVVM_Views);
    
    public static readonly TypeText IReadOnlyBindersCollectionById =
        new("IReadOnlyBindersCollectionById", Namespaces.UltimateUI_MVVM_Views);
    #endregion
    
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