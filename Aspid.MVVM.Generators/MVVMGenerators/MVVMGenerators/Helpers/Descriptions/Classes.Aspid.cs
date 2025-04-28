namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Views
    public static readonly TypeText IView =
        new(nameof(IView), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewAttribute = 
        new("View", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText AsBinderAttribute =
        new("AsBinder", Namespaces.Aspid_MVVM_Generation);

    public static readonly TypeText ViewBinder =
        new (nameof(ViewBinder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText View =
        new(nameof(View), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoView =
        new(nameof(MonoView), Namespaces.Aspid_MVVM_Mono);
    
    public static readonly TypeText ScriptableView =
        new(nameof(ScriptableView), Namespaces.Aspid_MVVM_Mono);
    #endregion
    
    #region Binders
    public static readonly TypeText BindMode =
        new(nameof(BindMode), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IBinder =
        new(nameof(IBinder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IReverseBinder =
        new(nameof(IReverseBinder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoBinder =
        new(nameof(MonoBinder), Namespaces.Aspid_MVVM_Mono);
    
    public static readonly AttributeText BinderLogAttribute =
        new("BinderLog", Namespaces.Aspid_MVVM_Mono_Generation);
    #endregion
    
    #region View Models
    public static readonly TypeText IViewModel =
        new(nameof(IViewModel), Namespaces.Aspid_MVVM);

    public static readonly TypeText ViewModel =
        new(nameof(ViewModel), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoViewModel =
        new(nameof(MonoViewModel), Namespaces.Aspid_MVVM_Mono);
    
    public static readonly TypeText ScriptableViewModel =
        new(nameof(ScriptableViewModel), Namespaces.Aspid_MVVM_Mono);
    
    public static readonly TypeText ViewModelUtility =
        new(nameof(ViewModelUtility), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModel", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly TypeText BindResult =
        new(nameof(BindResult), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IRemoveBinderFromViewModel =
        new(nameof(IRemoveBinderFromViewModel), Namespaces.Aspid_MVVM);
    #endregion

    #region Bind Attributes
    public static readonly AttributeText AccessAttribute =
        new("Access", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText BindAlsoAttribute =
        new("BindAlso", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText BindAttribute =
        new("Bind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText OneWayBindAttribute =
        new("OneWayBind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText TwoWayBindAttribute =
        new("TwoWayBind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText OneTimeBindAttribute =
        new("OneTimeBind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText OneWayToSourceBindAttribute =
        new("OneWayToSourceBind", Namespaces.Aspid_MVVM_Generation);
    #endregion
    
    #region View Model Events
    public static readonly TypeText OneWayViewModelEvent =
        new(nameof(OneWayViewModelEvent), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText TwoWayViewModelEvent =
        new(nameof(TwoWayViewModelEvent), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneWayToSourceViewModelEvent =
        new(nameof(OneWayToSourceViewModelEvent), Namespaces.Aspid_MVVM);
    #endregion

    #region Commands
    public static readonly TypeText RelayCommand =
        new(nameof(RelayCommand), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IRelayCommand =
        new(nameof(IRelayCommand), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText RelayCommandAttribute =
        new("RelayCommand", Namespaces.Aspid_MVVM_Generation);
    #endregion

    public static readonly TypeText Ids =
        new("Ids", Namespaces.Aspid_MVVM_Generated);
    
    public static readonly AttributeText IdAttribute = 
        new("BindId", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText IgnoreAttribute = 
        new("IgnoreBind", Namespaces.Aspid_MVVM_Generation);
    
    public static readonly AttributeText CreateFromAttribute =
        new("CreateFrom", Namespaces.Aspid_MVVM_Generation);
}