namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Views
    public static readonly TypeText IView =
        new("IView", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewAttribute = 
        new("View", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText AsBinderAttribute =
        new("AsBinder", Namespaces.Aspid_MVVM_Generation);

    public static readonly TypeText ViewBinder =
        new ("ViewBinder", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText View =
        new("View", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoView =
        new("MonoView", Namespaces.Aspid_MVVM_Mono);
    
    public static readonly TypeText ScriptableView =
        new("ScriptableView", Namespaces.Aspid_MVVM_Mono);
    #endregion
    
    #region Binders
    public static readonly TypeText IBinder =
        new("IBinder", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IReverseBinder =
        new("IReverseBinder", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoBinder =
        new("MonoBinder", Namespaces.Aspid_MVVM_Mono);
    
    public static readonly AttributeText BinderLogAttribute =
        new("BinderLog", Namespaces.Aspid_MVVM_Mono_Generation);
    #endregion

    #region View Models
    public static readonly TypeText IViewModel =
        new("IViewModel", Namespaces.Aspid_MVVM);

    public static readonly TypeText ViewModel =
        new("ViewModel", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoViewModel =
        new("MonoViewModel", Namespaces.Aspid_MVVM_Mono);
    
    public static readonly TypeText ScriptableViewModel =
        new("ScriptableViewModel", Namespaces.Aspid_MVVM_Mono);
    
    public static readonly TypeText ViewModelUtility =
        new("ViewModelUtility", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText BindAttribute =
        new("Bind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText BindAlsoAttribute =
        new("BindAlso", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText ReadOnlyBindAttribute =
        new("ReadOnlyBind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText RelayCommandAttribute =
        new("RelayCommand", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText AccessAttribute =
        new("Access", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModel", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly TypeText ViewModelEvent =
        new("ViewModelEvent", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IRemoveBinderFromViewModel =
        new("IRemoveBinderFromViewModel", Namespaces.Aspid_MVVM);
    #endregion

    #region Commands
    public static readonly TypeText IRelayCommand =
        new("IRelayCommand", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText RelayCommand =
        new("RelayCommand", Namespaces.Aspid_MVVM);
    #endregion

    public static readonly AttributeText IdAttribute = 
        new("BindId", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText IgnoreAttribute = 
        new("IgnoreBind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText CreateFromAttribute =
        new("CreateFrom", Namespaces.Aspid_MVVM_Generation);
}