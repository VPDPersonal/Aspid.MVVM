namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Views
    public static readonly TypeText IView =
        new("IView", Namespaces.UltimateUI_MVVM_Views);
    
    public static readonly AttributeText ViewAttribute = 
        new("View", Namespaces.UltimateUI_MVVM_Views_Generation);
    
    public static readonly AttributeText AsBinderAttribute =
        new("AsBinder", Namespaces.UltimateUI_MVVM_Views_Generation);

    public static readonly TypeText ViewBinder =
        new ("ViewBinder", Namespaces.UltimateUI_MVVM_Views);
    
    public static readonly TypeText MonoView =
        new("MonoView", Namespaces.UltimateUI_MVVM_Unity_Views);
    #endregion
    
    #region Binders
    public static readonly TypeText IBinder =
        new("IBinder", Namespaces.UltimateUI_MVVM);
    
    public static readonly TypeText IReverseBinder =
        new("IReverseBinder", Namespaces.UltimateUI_MVVM);
    
    public static readonly TypeText MonoBinder =
        new("MonoBinder", Namespaces.UltimateUI_MVVM_Unity);
    
    public static readonly AttributeText BinderLogAttribute =
        new("BinderLog", Namespaces.UltimateUI_MVVM_Unity_Generation);
    #endregion

    #region View Models
    public static readonly TypeText IViewModel =
        new("IViewModel", Namespaces.UltimateUI_MVVM_ViewModels);

    public static readonly TypeText ViewModelUtility =
        new("ViewModelUtility", Namespaces.UltimateUI_MVVM_ViewModels);
    
    public static readonly AttributeText BindAttribute =
        new("Bind", Namespaces.UltimateUI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText BindAlsoAttribute =
        new("BindAlso", Namespaces.UltimateUI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText ReadOnlyBindAttribute =
        new("ReadOnlyBind", Namespaces.UltimateUI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText RelayCommandAttribute =
        new("RelayCommand", Namespaces.UltimateUI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText AccessAttribute =
        new("Access", Namespaces.UltimateUI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModel", Namespaces.UltimateUI_MVVM_ViewModels_Generation);
    #endregion

    #region Commands
    public static readonly TypeText RelayCommand =
        new("RelayCommand", Namespaces.UltimateUI_MVVM_Commands);
    #endregion
}