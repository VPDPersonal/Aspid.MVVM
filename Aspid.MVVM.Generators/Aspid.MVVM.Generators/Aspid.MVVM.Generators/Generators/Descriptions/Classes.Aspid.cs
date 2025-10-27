using Aspid.Generators.Helper;

namespace Aspid.MVVM.Generators.Generators.Descriptions;

// ReSharper disable InconsistentNaming
public static class Classes
{
    #region Views
    public static readonly TypeText IView =
        new(nameof(IView), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewAttribute = 
        new("ViewAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText AsBinderAttribute =
        new("AsBinderAttribute", Namespaces.Aspid_MVVM);

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
        new("BinderLogAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText RequireBinderAttribute =
        new("RequireBinderAttribute", Namespaces.Aspid_MVVM);
    #endregion
    
    #region View Models
    public static readonly TypeText IViewModel =
        new(nameof(IViewModel), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText ViewModelAttribute =
        new("ViewModelAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly TypeText FindBindableMemberResult =
        new(nameof(FindBindableMemberResult), Namespaces.Aspid_MVVM);  
    
    public static readonly TypeText FindBindableMemberParameters =
        new(nameof(FindBindableMemberParameters), Namespaces.Aspid_MVVM);
    #endregion

    #region Bind Attributes
    public static readonly AttributeText BindAttribute =
        new("BindAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText IdAttribute = 
        new("BindIdAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText AccessAttribute =
        new("AccessAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText BindAlsoAttribute =
        new("BindAlsoAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText IgnoreAttribute = 
        new("IgnoreBindAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText OneWayBindAttribute =
        new("OneWayBindAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText TwoWayBindAttribute =
        new("TwoWayBindAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText OneTimeBindAttribute =
        new("OneTimeBindAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText OneWayToSourceBindAttribute =
        new("OneWayToSourceBindAttribute", Namespaces.Aspid_MVVM);
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
    
    public static readonly TypeText OneWayBindableMember =
        new(nameof(OneWayBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneWayStructBindableMember =
        new(nameof(OneWayStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneWayEnumBindableMember =
        new(nameof(OneWayEnumBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText TwoWayBindableMember =
        new(nameof(TwoWayBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText TwoWayStructBindableMember =
        new(nameof(TwoWayStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText TwoWayEnumBindableMember =
        new(nameof(TwoWayEnumBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneTimeBindableMember =
        new(nameof(OneTimeBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneTimeStructBindableMember =
        new(nameof(OneTimeStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneTimeEnumBindableMember =
        new(nameof(OneTimeEnumBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneWayToSourceBindableMember =
        new(nameof(OneWayToSourceBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneWayToSourceStructBindableMember =
        new(nameof(OneWayToSourceStructBindableMember), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText OneWayToSourceEnumBindableMember =
        new(nameof(OneWayToSourceEnumBindableMember), Namespaces.Aspid_MVVM);
    #endregion

    #region Commands
    public static readonly TypeText RelayCommand =
        new(nameof(RelayCommand), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText IRelayCommand =
        new(nameof(IRelayCommand), Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText RelayCommandAttribute =
        new("RelayCommandAttribute", Namespaces.Aspid_MVVM);
    #endregion
    
    public static readonly TypeText Unsafe =
        new(nameof(Unsafe), Namespaces.Aspid_MVVM);
    
    public static readonly TypeText Ids =
        new(nameof(Ids), Namespaces.Aspid_MVVM_Generated);
    
    public static readonly AttributeText CreateFromAttribute =
        new("CreateFromAttribute", Namespaces.Aspid_MVVM);
    
    public static readonly AttributeText AddComponentContextMenuAttribute =
        new ("AddComponentContextMenuAttribute", Namespaces.Aspid_MVVM_UNITY);
}