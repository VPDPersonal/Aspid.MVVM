namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Views
    public static readonly TypeText IView =
        new("IView", Namespaces.Aspid_UI_MVVM_Views);
    
    public static readonly AttributeText ViewAttribute = 
        new("View", Namespaces.Aspid_UI_MVVM_Views_Generation);
    
    public static readonly AttributeText AsBinderAttribute =
        new("AsBinder", Namespaces.Aspid_UI_MVVM_Views_Generation);

    public static readonly TypeText ViewBinder =
        new ("ViewBinder", Namespaces.Aspid_UI_MVVM_Views);
    
    public static readonly TypeText MonoView =
        new("MonoView", Namespaces.Aspid_UI_MVVM_Mono_Views);
    #endregion
    
    #region Binders
    public static readonly TypeText IBinder =
        new("IBinder", Namespaces.Aspid_UI_MVVM);
    
    public static readonly TypeText IReverseBinder =
        new("IReverseBinder", Namespaces.Aspid_UI_MVVM);
    
    public static readonly TypeText MonoBinder =
        new("MonoBinder", Namespaces.Aspid_UI_MVVM_Mono);
    
    public static readonly AttributeText BinderLogAttribute =
        new("BinderLog", Namespaces.Aspid_UI_MVVM_Mono_Generation);
    #endregion

    #region View Models
    public static readonly TypeText IViewModel =
        new("IViewModel", Namespaces.Aspid_UI_MVVM_ViewModels);

    public static readonly TypeText ViewModelUtility =
        new("ViewModelUtility", Namespaces.Aspid_UI_MVVM_ViewModels);
    
    public static readonly AttributeText BindAttribute =
        new("Bind", Namespaces.Aspid_UI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText BindAlsoAttribute =
        new("BindAlso", Namespaces.Aspid_UI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText ReadOnlyBindAttribute =
        new("ReadOnlyBind", Namespaces.Aspid_UI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText RelayCommandAttribute =
        new("RelayCommand", Namespaces.Aspid_UI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText AccessAttribute =
        new("Access", Namespaces.Aspid_UI_MVVM_ViewModels_Generation);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModel", Namespaces.Aspid_UI_MVVM_ViewModels_Generation);
    #endregion

    #region Commands
    public static readonly TypeText RelayCommand =
        new("RelayCommand", Namespaces.Aspid_UI_MVVM_Commands);
    #endregion
    
    public static readonly AttributeText CreateFromAttribute =
        new("CreateFrom", Namespaces.Aspid_UI_MVVM_Generation);
}