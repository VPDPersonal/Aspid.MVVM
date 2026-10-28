---
title: "Namespace Aspid.MVVM"
sidebar_label: "Aspid.MVVM"
description: "Namespace Aspid.MVVM — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Namespace Aspid.MVVM {#Aspid_MVVM}

### Namespaces

 [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)

 [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md)

### Classes

 [AccessAttribute](Aspid.MVVM.AccessAttribute.md)

Sets the access modifiers of the property generated for a bind field of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [AddBinderContextMenuAttribute](Aspid.MVVM.AddBinderContextMenuAttribute.md)

Editor-only attribute that offers a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) in the "Add Binder" context menu of a
component, and of the specific serialized properties it names.

 [AddBinderContextMenuByTypeAttribute](Aspid.MVVM.AddBinderContextMenuByTypeAttribute.md)

Editor-only attribute that registers a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) class in the "Add Binder" context menu
based solely on the target component type. Unlike [`AddBinderContextMenuAttribute`](Aspid.MVVM.AddBinderContextMenuAttribute.md),
this attribute does not support property auto-population or custom menu paths.
Can be applied multiple times to associate a binder with several component types.

 [AsBinderAttribute](Aspid.MVVM.AsBinderAttribute.md)

Wraps the field or property of a [`[View]`](Aspid.MVVM.ViewAttribute.md) class in a binder of [`AsBinderAttribute.Type`](Aspid.MVVM.AsBinderAttribute.md#Aspid_MVVM_AsBinderAttribute_Type), created with the member as its first
constructor argument.

 [BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md)

[`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) shared by [`BindAttribute`](Aspid.MVVM.BindAttribute.md) and the bind attributes with a fixed mode.

 [BindAlsoAttribute](Aspid.MVVM.BindAlsoAttribute.md)

Notifies the binders of another bindable property whenever the marked member of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class changes.

 [BindAttribute](Aspid.MVVM.BindAttribute.md)

Generates a bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class, and a property for a field.

 [BindIdAttribute](Aspid.MVVM.BindIdAttribute.md)

Sets the identifier that pairs a ViewModel member with the View binders of the same identifier.

 [BindModeAttribute](Aspid.MVVM.BindModeAttribute.md)

Limits the binding modes that the Inspector offers for a [`BindMode`](Aspid.MVVM.BindMode.md) field.

 [BindModeExtensions](Aspid.MVVM.BindModeExtensions.md)

Provides extension methods for [`BindMode`](Aspid.MVVM.BindMode.md).

 [BindModeOverrideAttribute](Aspid.MVVM.BindModeOverrideAttribute.md)

Replaces the [`BindModeAttribute`](Aspid.MVVM.BindModeAttribute.md) limits of every [`BindMode`](Aspid.MVVM.BindMode.md) field of the class and its subclasses in the Inspector.

 [Binder](Aspid.MVVM.Binder.md)

Serializable [`IBinder`](Aspid.MVVM.IBinder.md) that tracks its binding state and calls lifecycle hooks around binding.

 [BinderExtensions](Aspid.MVVM.BinderExtensions.md)

Provides extension methods for binding and unbinding [`IBinder`](Aspid.MVVM.IBinder.md) instances and their collections.

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

[`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) thrown when a binder lacks the interface a bindable member pushes values through.

 [BinderLogAttribute](Aspid.MVVM.BinderLogAttribute.md)

Generates an explicit [`IBinder<T>.SetValue`](Aspid.MVVM.IBinder-1.md#Aspid_MVVM_IBinder_1_SetValue__0_) implementation that records each value in an Inspector log
before calling the marked method.

 [ComponentMonoBinder\<TComponent\>](Aspid.MVVM.ComponentMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that targets a <code class="typeparamref">TComponent</code>, taken from the
serialized field or found on the same GameObject.

 [HeaderGroupAttribute](Aspid.MVVM.HeaderGroupAttribute.md)

Editor-only marker that places the decorated binder field into a collapsible foldout with
the supplied title. Fields decorated with the same title merge into a single foldout
regardless of declaration order. Unlike [`HeaderGroupStartAttribute`](Aspid.MVVM.HeaderGroupStartAttribute.md), this
attribute does not open a range — subsequent fields without their own grouping fall back to
the surrounding range (or the root, if no enclosing range is open).
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

 [HeaderGroupEndAttribute](Aspid.MVVM.HeaderGroupEndAttribute.md)

Editor-only marker that closes the currently open foldout group before the decorated
binder field is processed. The decorated field itself is rendered outside of the closed group.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

 [HeaderGroupStartAttribute](Aspid.MVVM.HeaderGroupStartAttribute.md)

Editor-only marker that opens a collapsible foldout starting at the decorated binder field.
The foldout continues until either [`HeaderGroupEndAttribute`](Aspid.MVVM.HeaderGroupEndAttribute.md), another
[`HeaderGroupAttribute`](Aspid.MVVM.HeaderGroupAttribute.md) / [`HeaderGroupStartAttribute`](Aspid.MVVM.HeaderGroupStartAttribute.md),
or the end of the inspector list is reached.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

 [IgnoreBindAttribute](Aspid.MVVM.IgnoreBindAttribute.md)

Excludes the field or property of a [`[View]`](Aspid.MVVM.ViewAttribute.md) class from the generated binding code.

 [MonoBinder](Aspid.MVVM.MonoBinder.md)

Abstract base [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) binder that manages binding to and unbinding from an [`IViewModel`](Aspid.MVVM.IViewModel.md).
Derived classes implement [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) to define what is bound.

 [MonoView](Aspid.MVVM.MonoView.md)

Represents a base class for views in a Unity context that are derived from [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html).
Implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) to allow cleanup of resources, including the destruction of the associated GameObject.

 [MonoViewBinder\<TView\>](Aspid.MVVM.MonoViewBinder-1.md)

[`ViewTargetBinder<T>`](Aspid.MVVM.ViewTargetBinder-1.md) restricted to [`Component`](https://docs.unity3d.com/ScriptReference/Component.html)-based views.

 [MonoViewBinder](Aspid.MVVM.MonoViewBinder.md)

[`MonoViewBinder<T>`](Aspid.MVVM.MonoViewBinder-1.md) for [`MonoView`](Aspid.MVVM.MonoView.md).

 [MonoViewExtensions](Aspid.MVVM.MonoViewExtensions.md)

Provides extension methods for the [`IView`](Aspid.MVVM.IView.md) interface.

 [MonoViewModel](Aspid.MVVM.MonoViewModel.md)

Represents a base class for ViewModels in a Unity context that are derived from [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html).
Implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) to allow cleanup of resources, including the destruction of the component.

 [MonoViewModelExtensions](Aspid.MVVM.MonoViewModelExtensions.md)

Provides extension methods for the [`IViewModel`](Aspid.MVVM.IViewModel.md) interface.

 [MonoViewMonoBinder](Aspid.MVVM.MonoViewMonoBinder.md)

[`MonoViewMonoBinder<T>`](Aspid.MVVM.MonoViewMonoBinder-1.md) for [`MonoView`](Aspid.MVVM.MonoView.md).

 [MonoViewMonoBinder\<TView\>](Aspid.MVVM.MonoViewMonoBinder-1.md)

Abstract base [`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that initializes the target view with the bound [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [OneTimeBindAttribute](Aspid.MVVM.OneTimeBindAttribute.md)

Generates a [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [OneTimeBindableMember\<T\>](Aspid.MVVM.OneTimeBindableMember-1.md)

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that pushes its value to a binder once and keeps no subscription.

 [OneTimeEnumBindableMember\<T\>](Aspid.MVVM.OneTimeEnumBindableMember-1.md)

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [OneTimeStructBindableMember\<T\>](Aspid.MVVM.OneTimeStructBindableMember-1.md)

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

 [OneTimeStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneTimeStructBindableMember-2.md)

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that pushes its value to a binder once and keeps no subscription,
boxing it for an [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) of <code class="typeparamref">TBoxed</code>.

 [OneWayBindAttribute](Aspid.MVVM.OneWayBindAttribute.md)

Generates a [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [OneWayBindableMember\<T\>](Aspid.MVVM.OneWayBindableMember-1.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes every value change to its binders.

 [OneWayEnumBindableMember\<T\>](Aspid.MVVM.OneWayEnumBindableMember-1.md)

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [OneWayStructBindableMember\<T\>](Aspid.MVVM.OneWayStructBindableMember-1.md)

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

 [OneWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayStructBindableMember-2.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes every value change to its binders, boxing it for an
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) of <code class="typeparamref">TBoxed</code>.

 [OneWayToSourceBindAttribute](Aspid.MVVM.OneWayToSourceBindAttribute.md)

Generates a [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [OneWayToSourceBindableMember\<T\>](Aspid.MVVM.OneWayToSourceBindableMember-1.md)

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) that passes values from its reverse binders to a setter.

 [OneWayToSourceEnumBindableMember\<T\>](Aspid.MVVM.OneWayToSourceEnumBindableMember-1.md)

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [OneWayToSourceStructBindableMember\<T\>](Aspid.MVVM.OneWayToSourceStructBindableMember-1.md)

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

 [OneWayToSourceStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md)

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) that passes values from its reverse binders to a setter, unboxing them from an
[`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) of <code class="typeparamref">TBoxed</code>.

 [RebindableBinderExtensions](Aspid.MVVM.RebindableBinderExtensions.md)

Provides extension methods for rebinding an [`IBinder`](Aspid.MVVM.IBinder.md).

 [RelayCommand\<T\>](Aspid.MVVM.RelayCommand-1.md)

[`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand\<T1, T2\>](Aspid.MVVM.RelayCommand-2.md)

[`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand\<T1, T2, T3\>](Aspid.MVVM.RelayCommand-3.md)

[`IRelayCommand<T1, T2, T3>`](Aspid.MVVM.IRelayCommand-3.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

[`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.RelayCommand-4.md)

[`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommandAttribute](Aspid.MVVM.RelayCommandAttribute.md)

Generates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) property, named after the method with a <code>Command</code> suffix, for the method of
a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [RelayCommandExtensions](Aspid.MVVM.RelayCommandExtensions.md)

Provides extension methods for creating relay commands and for replacing a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command with an empty one.

 [RequireBinderAttribute](Aspid.MVVM.RequireBinderAttribute.md)

Editor-only attribute applied to serialized fields to declare the required binder association
in the Unity Inspector. Enables the editor to validate that a [`MonoBinder`](Aspid.MVVM.MonoBinder.md)
of the expected type is assigned to the field.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

 [ReverseBinderInvalidCastException\<T\>](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)

[`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) thrown when a binder lacks the interface a bindable member receives values through.

 [ScriptableView](Aspid.MVVM.ScriptableView.md)

Represents a base class for views in a Unity context derived from [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

 [ScriptableViewBinder](Aspid.MVVM.ScriptableViewBinder.md)

[`ScriptableViewBinder<T>`](Aspid.MVVM.ScriptableViewBinder-1.md) for [`ScriptableView`](Aspid.MVVM.ScriptableView.md).

 [ScriptableViewBinder\<TView\>](Aspid.MVVM.ScriptableViewBinder-1.md)

[`ViewTargetBinder<T>`](Aspid.MVVM.ViewTargetBinder-1.md) restricted to [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html)-based views.

 [ScriptableViewModel](Aspid.MVVM.ScriptableViewModel.md)

Represents a base class for ViewModels in a Unity context derived from [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

 [ScriptableViewMonoBinder](Aspid.MVVM.ScriptableViewMonoBinder.md)

[`ScriptableViewMonoBinder<T>`](Aspid.MVVM.ScriptableViewMonoBinder-1.md) for [`ScriptableView`](Aspid.MVVM.ScriptableView.md).

 [ScriptableViewMonoBinder\<TView\>](Aspid.MVVM.ScriptableViewMonoBinder-1.md)

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that initializes a serialized [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) view with the bound [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [ShowDesignViewModelAttribute](Aspid.MVVM.ShowDesignViewModelAttribute.md)

Specifies which ViewModel types are available as design-time ViewModels for a View in the Unity Editor.
Apply this attribute to a [`MonoView`](Aspid.MVVM.MonoView.md) or [`ScriptableView`](Aspid.MVVM.ScriptableView.md) class to restrict
or extend the list of types shown in the design ViewModel selector.

 [TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) that operates on a serialized <code class="typeparamref">TTarget</code>.

 [TwoWayBindAttribute](Aspid.MVVM.TwoWayBindAttribute.md)

Generates a [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [TwoWayBindableMember\<T\>](Aspid.MVVM.TwoWayBindableMember-1.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes value changes to its binders and passes values from its reverse binders
to a setter.

 [TwoWayEnumBindableMember\<T\>](Aspid.MVVM.TwoWayEnumBindableMember-1.md)

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [TwoWayStructBindableMember\<T\>](Aspid.MVVM.TwoWayStructBindableMember-1.md)

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

 [TwoWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.TwoWayStructBindableMember-2.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes value changes to its binders and passes values from its reverse binders
to a setter, also through binders of <code class="typeparamref">TBoxed</code>.

 [UsedInModesAttribute](Aspid.MVVM.UsedInModesAttribute.md)

Disables the field in the Inspector while the binder that holds it has a mode outside [`UsedInModesAttribute.Modes`](Aspid.MVVM.UsedInModesAttribute.md#Aspid_MVVM_UsedInModesAttribute_Modes).

 [ViewAttribute](Aspid.MVVM.ViewAttribute.md)

Generates the [`IView`](Aspid.MVVM.IView.md) implementation of the class, which binds its binder fields and properties.

 [ViewBinder](Aspid.MVVM.ViewBinder.md)

[`Binder`](Aspid.MVVM.Binder.md) that initializes an [`IView`](Aspid.MVVM.IView.md) with each ViewModel it receives.

 [ViewExtensions](Aspid.MVVM.ViewExtensions.md)

Provides extension methods for [`IView`](Aspid.MVVM.IView.md).

 [ViewModelAttribute](Aspid.MVVM.ViewModelAttribute.md)

Generates the [`IViewModel`](Aspid.MVVM.IViewModel.md) implementation of the class from its bind members and
[`[RelayCommand]`](Aspid.MVVM.RelayCommandAttribute.md) methods.

 [ViewModelExtensions](Aspid.MVVM.ViewModelExtensions.md)

Provides extension methods for [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [ViewTargetBinder\<TView\>](Aspid.MVVM.ViewTargetBinder-1.md)

Abstract base [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that initializes the target view with the bound [`IViewModel`](Aspid.MVVM.IViewModel.md)
and deinitializes it on unbind.

### Structs

 [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

Represents the query passed to [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

Represents the result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

### Interfaces

 [IAnyBinder](Aspid.MVVM.IAnyBinder.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that receives values of any type from the ViewModel.

 [IAnyReverseBinder](Aspid.MVVM.IAnyReverseBinder.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that reports untyped values from the View back to the ViewModel.

 [IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md)

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) whose value can also be set.

 [IBinder\<T\>](Aspid.MVVM.IBinder-1.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that receives values of <code class="typeparamref">T</code> from the ViewModel.

 [IBinder](Aspid.MVVM.IBinder.md)

Defines a binder that attaches to a bindable member of a ViewModel.

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

Defines the side of a bindable member that accepts binders.

 [IBinderRemover](Aspid.MVVM.IBinderRemover.md)

Defines the side of a bindable member that detaches binders.

 [IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md)

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that reports value changes.

 [IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md)

[`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that exposes its current value.

 [IRebindableBinder](Aspid.MVVM.IRebindableBinder.md)

Defines a binder that can attach again to its current bindable member.

 [IRelayCommand\<T1, T2, T3\>](Aspid.MVVM.IRelayCommand-3.md)

Defines a command with three parameters.

 [IRelayCommand\<T1, T2\>](Aspid.MVVM.IRelayCommand-2.md)

Defines a command with two parameters.

 [IRelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.IRelayCommand-4.md)

Defines a command with four parameters.

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

Defines a command without parameters.

 [IRelayCommand\<T\>](Aspid.MVVM.IRelayCommand-1.md)

Defines a command with one parameter.

 [IReverseBinder\<T\>](Aspid.MVVM.IReverseBinder-1.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that reports values of <code class="typeparamref">T</code> from the View back to the ViewModel.

 [IView\<T\>](Aspid.MVVM.IView-1.md)

[`IView`](Aspid.MVVM.IView.md) that accepts a ViewModel of <code class="typeparamref">T</code>.

 [IView](Aspid.MVVM.IView.md)

Defines a View that binds its binders to a ViewModel.

 [IViewModel](Aspid.MVVM.IViewModel.md)

Defines a ViewModel whose bindable members a View looks up by identifier.

### Enums

 [Access](Aspid.MVVM.Access.md)

Specifies the access modifier of an accessor of a property generated for a bind field.

 [BindMode](Aspid.MVVM.BindMode.md)

Specifies the direction of data flow between the ViewModel and the View.

