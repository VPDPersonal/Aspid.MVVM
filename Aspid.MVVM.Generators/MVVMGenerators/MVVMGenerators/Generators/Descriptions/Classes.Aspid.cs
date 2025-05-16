namespace MVVMGenerators.Helpers.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Classes
{
    #region Views
    public static readonly TypeText IView =
        new(nameof(IView), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewAttribute = 
        new("View", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText AsBinderAttribute =
        new("AsBinder", Namespaces.Aspid_MVVM);

    public static readonly TypeText ViewBinder =
        new (nameof(ViewBinder), Namespaces.Aspid_MVVM);
    #endregion
    
    #region Binders
    public static readonly TypeText BindMode =
        new(nameof(BindMode), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IBinder =
        new(nameof(IBinder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IReverseBinder =
        new(nameof(IReverseBinder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText MonoBinder =
        new(nameof(MonoBinder), Namespaces.Aspid_MVVM_UNITY);
    
    public static readonly AttributeText BinderLogAttribute =
        new("BinderLog", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText RequireBinderAttribute =
        new("RequireBinder", Namespaces.Aspid_MVVM);
    #endregion
    
    #region View Models
    public static readonly TypeText IViewModel =
        new(nameof(IViewModel), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModel", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText FindBindableMemberResult =
        new(nameof(FindBindableMemberResult), Namespaces.Aspid_MVVM);  
    
    public static readonly TypeText FindBindableMemberParameters =
        new(nameof(FindBindableMemberParameters), Namespaces.Aspid_MVVM);
    #endregion

    #region Bind Attributes
    public static readonly AttributeText BindAttribute =
        new("Bind", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText IdAttribute = 
        new("BindId", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText AccessAttribute =
        new("Access", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText BindAlsoAttribute =
        new("BindAlso", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText IgnoreAttribute = 
        new("IgnoreBind", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText OneWayBindAttribute =
        new("OneWayBind", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText TwoWayBindAttribute =
        new("TwoWayBind", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText OneTimeBindAttribute =
        new("OneTimeBind", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText OneWayToSourceBindAttribute =
        new("OneWayToSourceBind", Namespaces.Aspid_MVVM);
    #endregion
    
    #region Bindable Member Events
    public static readonly TypeText IBindableMemberEvent =
        new(nameof(IBindableMemberEvent), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IBindableMemberEventAdder =
        new(nameof(IBindableMemberEventAdder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IBindableMemberEventRemover =
        new(nameof(IBindableMemberEventRemover), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayBindableMemberEvent =
        new(nameof(OneWayBindableMemberEvent), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? TwoWayBindableMemberEvent =
        new(nameof(TwoWayBindableMemberEvent), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneTimeBindableMemberEvent =
        new(nameof(OneTimeBindableMemberEvent), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayToSourceBindableMemberEvent =
        new(nameof(OneWayToSourceBindableMemberEvent), Namespaces.Aspid_MVVM);
    #endregion

    #region Commands
    public static readonly TypeText RelayCommand =
        new(nameof(RelayCommand), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IRelayCommand =
        new(nameof(IRelayCommand), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText RelayCommandAttribute =
        new("RelayCommand", Namespaces.Aspid_MVVM);
    #endregion
    
    public static readonly TypeText Unsafe =
        new(nameof(Unsafe), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText Ids =
        new(nameof(Ids), Namespaces.Aspid_MVVM_Generated);
    
    public static readonly AttributeText CreateFromAttribute =
        new("CreateFrom", Namespaces.Aspid_MVVM);
}