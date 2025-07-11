using Aspid.Generator.Helpers;

namespace Aspid.MVVM.Generators.Descriptions;

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
    public static readonly TypeText IBindableMember =
        new(nameof(IBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IReadOnlyBindableMember =
        new(nameof(IReadOnlyBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IReadOnlyValueBindableMember =
        new(nameof(IReadOnlyValueBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IBinderAdder =
        new(nameof(IBinderAdder), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IBinderRemover =
        new(nameof(IBinderRemover), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayBindableMember =
        new(nameof(OneWayBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayStructBindableMember =
        new(nameof(OneWayStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayEnumBindableMember =
        new(nameof(OneWayEnumBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? TwoWayBindableMember =
        new(nameof(TwoWayBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? TwoWayStructBindableMember =
        new(nameof(TwoWayStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? TwoWayEnumBindableMember =
        new(nameof(TwoWayEnumBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneTimeBindableMember =
        new(nameof(OneTimeBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneTimeStructBindableMember =
        new(nameof(OneTimeStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneTimeEnumBindableMember =
        new(nameof(OneTimeEnumBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayToSourceBindableMember =
        new(nameof(OneWayToSourceBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayToSourceStructBindableMember =
        new(nameof(OneWayToSourceStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText? OneWayToSourceEnumBindableMember =
        new(nameof(OneWayToSourceEnumBindableMember), Namespaces.Aspid_MVVM);
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
    
    public static readonly AttributeText AddComponentContextMenuAttribute =
        new ("AddComponentContextMenu", Namespaces.Aspid_MVVM_UNITY);
}