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

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
overrides the default <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/private">private</a> access modifier of the generated property's get and set
accessors. Requires a companion [`BindAttribute`](Aspid.MVVM.BindAttribute.md), [`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md),
[`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md), [`OneTimeBindAttribute`](Aspid.MVVM.OneTimeBindAttribute.md), or [`OneWayToSourceBindAttribute`](Aspid.MVVM.OneWayToSourceBindAttribute.md)
on the same field.

 [AddBinderContextMenuAttribute](Aspid.MVVM.AddBinderContextMenuAttribute.md)

Editor-only attribute that offers a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) in the "Add Binder" context menu of a
component, and of the specific serialized properties it names.

 [AddBinderContextMenuByTypeAttribute](Aspid.MVVM.AddBinderContextMenuByTypeAttribute.md)

Editor-only attribute that registers a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) class in the "Add Binder" context menu
based solely on the target component type. Unlike [`AddBinderContextMenuAttribute`](Aspid.MVVM.AddBinderContextMenuAttribute.md),
this attribute does not support property auto-population or custom menu paths.
Can be applied multiple times to associate a binder with several component types.

 [AsBinderAttribute](Aspid.MVVM.AsBinderAttribute.md)

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to fields or properties of a type carrying [`ViewAttribute`](Aspid.MVVM.ViewAttribute.md);
directs the Source Generator to emit binding code that wires the member to the supplied [`IBinder`](Aspid.MVVM.IBinder.md) type.

 [BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md)

Serves as the base class for all binding-related attributes.
Derive from this class to implement custom binding attributes for use with ViewModels.
This class itself does not contain any logic and is used primarily as a marker for attribute hierarchy.
Classes that inherit from [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) must be manually added to the Source Generator 
to generate the appropriate binding logic. This process does not happen automatically.

 [BindAlsoAttribute](Aspid.MVVM.BindAlsoAttribute.md)

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to also raise the change event of the property named [`BindAlsoAttribute.PropertyName`](Aspid.MVVM.BindAlsoAttribute.md#Aspid_MVVM_BindAlsoAttribute_PropertyName)
when the decorated field changes. Requires a companion [`BindAttribute`](Aspid.MVVM.BindAttribute.md), [`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md),
[`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md), or [`OneWayToSourceBindAttribute`](Aspid.MVVM.OneWayToSourceBindAttribute.md) on the same field.

 [BindAttribute](Aspid.MVVM.BindAttribute.md)

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property for the field.
The default constructor selects [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) for mutable fields and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md)
for <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields. When the mode-taking constructor is used on a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field,
[`BindMode.OneTime`](Aspid.MVVM.BindMode.md) and [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) both resolve to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md);
any other mode is rejected.

 [BindIdAttribute](Aspid.MVVM.BindIdAttribute.md)

Attribute used to override the binding ID for fields, properties, or [RelayCommand] in a ViewModel and View.

 [BindModeAttribute](Aspid.MVVM.BindModeAttribute.md)

Attribute used to specify allowed binding modes for a property in the Unity Editor.
This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.

 [BindModeExtensions](Aspid.MVVM.BindModeExtensions.md)

Provides extension methods for [`BindMode`](Aspid.MVVM.BindMode.md) providing mode classification checks and validation helpers.

 [BindModeOverrideAttribute](Aspid.MVVM.BindModeOverrideAttribute.md)

Attribute used to override allowed binding modes for a class.
This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.

 [BindSafelyNullReferenceException](Aspid.MVVM.BindSafelyNullReferenceException.md)

Exception thrown when a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder is encountered during a
[`BinderExtensions.BindSafely%60<T>`](Aspid.MVVM.BinderExtensions.md) operation.

 [Binder](Aspid.MVVM.Binder.md)

Abstract base class for binder implementations.
Manages the binding lifecycle — binding to and unbinding from an [`IViewModel`](Aspid.MVVM.IViewModel.md).
Derived classes must implement [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) to define the specific binding behavior.

 [BinderExtensions](Aspid.MVVM.BinderExtensions.md)

Provides extension methods for safely binding and unbinding [`IBinder`](Aspid.MVVM.IBinder.md) instances to [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) targets.
Null-safe variants guard against <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binders or collections.

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

Exception thrown when a binder is not of the expected type during a binding operation.
Provides factory methods for generating descriptive error messages for class and struct binders.

 [BinderLogAttribute](Aspid.MVVM.BinderLogAttribute.md)

Instructs the Source Generator to generate an explicit [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) [`IBinder<T>.SetValue`](Aspid.MVVM.IBinder-1.md#Aspid_MVVM_IBinder_1_SetValue__0_) implementation
with added logging, wrapping the annotated method.

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

Attribute used to indicate that a field or property should be ignored by the source code generator for binding in View.

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

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

 [OneTimeBindableMember\<T\>](Aspid.MVVM.OneTimeBindableMember-1.md)

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) exposed as a per-type singleton that pushes a single [`OneTimeBindableMember<T>.Value`](Aspid.MVVM.OneTimeBindableMember-1.md#Aspid_MVVM_OneTimeBindableMember_1_Value)
to the binder once and then releases it; rejects every [`BindMode`](Aspid.MVVM.BindMode.md) other than
[`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

 [OneTimeEnumBindableMember\<T\>](Aspid.MVVM.OneTimeEnumBindableMember-1.md)

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum),
exposed as a per-type singleton via [`OneTimeEnumBindableMember<T>.Get`](Aspid.MVVM.OneTimeEnumBindableMember-1.md#Aspid_MVVM_OneTimeEnumBindableMember_1_Get__0_) for one-time enum bindings.

 [OneTimeStructBindableMember\<T\>](Aspid.MVVM.OneTimeStructBindableMember-1.md)

Concrete [`OneTimeStructBindableMember<T1, T2>`](Aspid.MVVM.OneTimeStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype),
exposed as a per-type singleton via [`OneTimeStructBindableMember<T>.Get`](Aspid.MVVM.OneTimeStructBindableMember-1.md#Aspid_MVVM_OneTimeStructBindableMember_1_Get__0_).

 [OneTimeStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneTimeStructBindableMember-2.md)

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued one-time bindings that pushes a single
[`OneTimeStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneTimeStructBindableMember-2.md#Aspid_MVVM_OneTimeStructBindableMember_2_Value) to the binder and then releases it; supports binders typed against
<code class="typeparamref">T</code>, <code class="typeparamref">TBoxed</code>, or [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md).

 [OneWayBindAttribute](Aspid.MVVM.OneWayBindAttribute.md)

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).
On <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields the mode resolves to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md), matching
[`BindAttribute`](Aspid.MVVM.BindAttribute.md) with [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

 [OneWayBindableMember\<T\>](Aspid.MVVM.OneWayBindableMember-1.md)

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that pushes value changes from the ViewModel to subscribed
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) instances; additionally exposes a get/set
[`OneWayBindableMember<T>.Value`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Value) and a [`OneWayBindableMember<T>.Changed`](Aspid.MVVM.OneWayBindableMember-1.md#Aspid_MVVM_OneWayBindableMember_1_Changed) event. Accepts only [`BindMode.OneWay`](Aspid.MVVM.BindMode.md)
and [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) binders.

 [OneWayEnumBindableMember\<T\>](Aspid.MVVM.OneWayEnumBindableMember-1.md)

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum),
allowing enum-typed binders ([`IBinder<T>`](Aspid.MVVM.IBinder-1.md)) to receive the boxed enum value alongside
the strongly-typed <code class="typeparamref">T</code>.

 [OneWayStructBindableMember\<T\>](Aspid.MVVM.OneWayStructBindableMember-1.md)

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype)
for any value-type payload that does not need a more specific boxing target.

 [OneWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayStructBindableMember-2.md)

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued one-way bindings that dispatches changes to
both [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) and [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) subscribers — the latter receive the
value pre-boxed as <code class="typeparamref">TBoxed</code>. Additionally exposes a get/set [`OneWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Value)
and a [`OneWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayStructBindableMember-2.md#Aspid_MVVM_OneWayStructBindableMember_2_Changed) event. Accepts only [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) and
[`BindMode.OneTime`](Aspid.MVVM.BindMode.md) binders.

 [OneWayToSourceBindAttribute](Aspid.MVVM.OneWayToSourceBindAttribute.md)

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).
Cannot be applied to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields.

 [OneWayToSourceBindableMember\<T\>](Aspid.MVVM.OneWayToSourceBindableMember-1.md)

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that forwards View-side value changes back to the ViewModel through a
captured setter [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1); additionally exposes the latest [`OneWayToSourceBindableMember<T>.Value`](Aspid.MVVM.OneWayToSourceBindableMember-1.md#Aspid_MVVM_OneWayToSourceBindableMember_1_Value) and a
[`OneWayToSourceBindableMember<T>.Changed`](Aspid.MVVM.OneWayToSourceBindableMember-1.md#Aspid_MVVM_OneWayToSourceBindableMember_1_Changed) event. Accepts only [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) and
[`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) reverse binders.

 [OneWayToSourceEnumBindableMember\<T\>](Aspid.MVVM.OneWayToSourceEnumBindableMember-1.md)

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)
for one-way-to-source enum bindings, forwarding View-side enum changes back to the ViewModel.

 [OneWayToSourceStructBindableMember\<T\>](Aspid.MVVM.OneWayToSourceStructBindableMember-1.md)

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype)
for any value-type payload that does not need a more specific boxing target.

 [OneWayToSourceStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md)

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued one-way-to-source bindings that forwards
View-side value changes back to the ViewModel through a captured setter [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1);
additionally exposes the latest [`OneWayToSourceStructBindableMember<T1, T2>.Value`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Value) and a [`OneWayToSourceStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md#Aspid_MVVM_OneWayToSourceStructBindableMember_2_Changed) event.
Accepts [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md), [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md), and
[`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md) in [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) or [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) mode.

 [RebindableBinderExtensions](Aspid.MVVM.RebindableBinderExtensions.md)

Provides extension methods for [`IBinder`](Aspid.MVVM.IBinder.md) instances that implement [`IRebindableBinder`](Aspid.MVVM.IRebindableBinder.md).

 [RelayCommand\<T\>](Aspid.MVVM.RelayCommand-1.md)

Sealed [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) implementation that wraps an [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1) as the execute callback
and an optional [`Func<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.func-2) predicate to gate execution against the supplied parameter.

 [RelayCommand\<T1, T2\>](Aspid.MVVM.RelayCommand-2.md)

Sealed [`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) implementation that wraps an [`Action<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.action-2) as the execute
callback and an optional [`Func<T1, T2, T3>`](https://learn.microsoft.com/dotnet/api/system.func-3) predicate to gate execution against the supplied parameters.

 [RelayCommand\<T1, T2, T3\>](Aspid.MVVM.RelayCommand-3.md)

Sealed [`IRelayCommand<T1, T2, T3>`](Aspid.MVVM.IRelayCommand-3.md) implementation that wraps an [`Action<T1, T2, T3>`](https://learn.microsoft.com/dotnet/api/system.action-3) as the
execute callback and an optional [`Func<T1, T2, T3, T4>`](https://learn.microsoft.com/dotnet/api/system.func-4) predicate to gate execution against the supplied parameters.

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

Sealed [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) implementation that wraps an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) as the execute callback
and an optional [`Func<T>`](https://learn.microsoft.com/dotnet/api/system.func-1) predicate to gate execution.

 [RelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.RelayCommand-4.md)

Sealed [`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) implementation that wraps an [`Action<T1, T2, T3, T4>`](https://learn.microsoft.com/dotnet/api/system.action-4) as the
execute callback and an optional [`Func<T1, T2, T3, T4, T5>`](https://learn.microsoft.com/dotnet/api/system.func-5) predicate to gate execution against the supplied parameters.

 [RelayCommandAttribute](Aspid.MVVM.RelayCommandAttribute.md)

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to methods of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a matching [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) (or one of its generic
overloads, picked by the method's parameter count) that wraps the decorated method.

 [RelayCommandExtensions](Aspid.MVVM.RelayCommandExtensions.md)

Provides extension methods for [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) and its generic variants
for null-safe fallback to empty commands and for creating commands from delegates.

 [RequireBinderAttribute](Aspid.MVVM.RequireBinderAttribute.md)

Editor-only attribute applied to serialized fields to declare the required binder association
in the Unity Inspector. Enables the editor to validate that a [`MonoBinder`](Aspid.MVVM.MonoBinder.md)
of the expected type is assigned to the field.
Stripped from builds outside of <code>DEBUG</code> and <code>UNITY_EDITOR</code> configurations.

 [ReverseBinderInvalidCastException\<T\>](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)

Exception thrown when a binder is not of the expected reverse binder type during a one-way-to-source binding operation.
Provides factory methods for generating descriptive error messages for class and struct reverse binders.

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

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that provides a typed <code class="typeparamref">TTarget</code> reference
available to derived classes for binding logic.

 [TwoWayBindAttribute](Aspid.MVVM.TwoWayBindAttribute.md)

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).
Cannot be applied to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields.

 [TwoWayBindableMember\<T\>](Aspid.MVVM.TwoWayBindableMember-1.md)

Sealed [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that supports every [`BindMode`](Aspid.MVVM.BindMode.md) except [`BindMode.None`](Aspid.MVVM.BindMode.md),
dispatching forward updates through [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) and reverse updates
through [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) / [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md); additionally exposes a get/set
[`TwoWayBindableMember<T>.Value`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Value) and a [`TwoWayBindableMember<T>.Changed`](Aspid.MVVM.TwoWayBindableMember-1.md#Aspid_MVVM_TwoWayBindableMember_1_Changed) event.

 [TwoWayEnumBindableMember\<T\>](Aspid.MVVM.TwoWayEnumBindableMember-1.md)

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum)
for two-way enum bindings, supporting both strongly-typed and boxed-enum binders.

 [TwoWayStructBindableMember\<T\>](Aspid.MVVM.TwoWayStructBindableMember-1.md)

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code class="typeparamref">TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype)
for any value-type payload that does not need a more specific boxing target.

 [TwoWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.TwoWayStructBindableMember-2.md)

Abstract base [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) for struct-valued two-way bindings that supports every
[`BindMode`](Aspid.MVVM.BindMode.md) except [`BindMode.None`](Aspid.MVVM.BindMode.md), dispatching forward updates through
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) / [`IAnyBinder`](Aspid.MVVM.IAnyBinder.md) and reverse updates
through [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) / [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) / [`IAnyReverseBinder`](Aspid.MVVM.IAnyReverseBinder.md).
Additionally exposes a get/set [`TwoWayStructBindableMember<T1, T2>.Value`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Value) and a [`TwoWayStructBindableMember<T1, T2>.Changed`](Aspid.MVVM.TwoWayStructBindableMember-2.md#Aspid_MVVM_TwoWayStructBindableMember_2_Changed) event.

 [UnbindSafelyNullReferenceException](Aspid.MVVM.UnbindSafelyNullReferenceException.md)

Exception thrown when a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> binder is encountered during an
[`BinderExtensions.UnbindSafely%60<T>`](Aspid.MVVM.BinderExtensions.md) operation.

 [UsedInModesAttribute](Aspid.MVVM.UsedInModesAttribute.md)

Marks a serialized field as used only under the specified binding modes, so the Inspector
can disable it while the hosting binder is bound in any other.
This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.

 [ViewAttribute](Aspid.MVVM.ViewAttribute.md)

Sealed marker [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) that drives the Source Generator to emit an [`IView`](Aspid.MVVM.IView.md)
implementation for the decorated class or struct and to analyze code blocks within the type.

 [ViewBinder](Aspid.MVVM.ViewBinder.md)

[`Binder`](Aspid.MVVM.Binder.md) that initializes an [`IView`](Aspid.MVVM.IView.md) when a bound [`IViewModel`](Aspid.MVVM.IViewModel.md) is received,
and deinitializes it on unbind.

 [ViewExtensions](Aspid.MVVM.ViewExtensions.md)

Provides extension methods for [`IView`](Aspid.MVVM.IView.md) providing helpers for reinitialization and safe disposal.

 [ViewModelAttribute](Aspid.MVVM.ViewModelAttribute.md)

Sealed marker [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) that drives the Source Generator to emit an [`IViewModel`](Aspid.MVVM.IViewModel.md)
implementation for the decorated class or struct and to analyze code blocks within the type.

 [ViewModelExtensions](Aspid.MVVM.ViewModelExtensions.md)

Provides extension methods for [`IViewModel`](Aspid.MVVM.IViewModel.md) providing lifecycle helpers such as disposal.

 [ViewTargetBinder\<TView\>](Aspid.MVVM.ViewTargetBinder-1.md)

Abstract base [`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that initializes the target view with the bound [`IViewModel`](Aspid.MVVM.IViewModel.md)
and deinitializes it on unbind.

### Structs

 [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

Represents the parameters used to search for a bindable member in a ViewModel.

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

Represents the result of a binding operation, indicating whether a bindable member was successfully located.

### Interfaces

 [IAnyBinder](Aspid.MVVM.IAnyBinder.md)

Extends [`IBinder`](Aspid.MVVM.IBinder.md) with the ability to receive values of any type from the ViewModel.

 [IAnyReverseBinder](Aspid.MVVM.IAnyReverseBinder.md)

Extends [`IBinder`](Aspid.MVVM.IBinder.md) with reverse data binding capability — propagating non-generic values from the View back to the ViewModel.

 [IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md)

Represents a bindable member that allows setting a value and notifies listeners when the value changes.

 [IBinder](Aspid.MVVM.IBinder.md)

Base interface for binder implementations.
Defines the binding lifecycle — binding to and unbinding from an [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [IBinder\<T\>](Aspid.MVVM.IBinder-1.md)

Extends [`IBinder`](Aspid.MVVM.IBinder.md) with the ability to receive typed values from the ViewModel.

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

Interface for adding event bindings to a bindable member.

 [IBinderRemover](Aspid.MVVM.IBinderRemover.md)

Interface for removing event bindings from a bindable member.

 [IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md)

Represents a read-only bindable member that exposes a value and notifies listeners when the value changes.

 [IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md)

Represents a bindable member that exposes a read-only value and allows binders to be added.

 [IRebindableBinder](Aspid.MVVM.IRebindableBinder.md)

Provides the ability to force a rebind, re-establishing the connection to the current ViewModel.

 [IRelayCommand\<T\>](Aspid.MVVM.IRelayCommand-1.md)

An interface for a command that can be executed with a parameter.

 [IRelayCommand\<T1, T2, T3\>](Aspid.MVVM.IRelayCommand-3.md)

An interface for a command that can be executed with three parameters.

 [IRelayCommand\<T1, T2\>](Aspid.MVVM.IRelayCommand-2.md)

An interface for a command that can be executed with two parameters.

 [IRelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.IRelayCommand-4.md)

An interface for a command that can be executed with four parameters.

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

An interface for a command that can be executed without parameters.

 [IReverseBinder\<T\>](Aspid.MVVM.IReverseBinder-1.md)

Extends [`IBinder`](Aspid.MVVM.IBinder.md) with reverse data binding capability — propagating values from the View back to the ViewModel.

 [IView\<T\>](Aspid.MVVM.IView-1.md)

Generic interface for initializing a View with a strongly-typed ViewModel.

 [IView](Aspid.MVVM.IView.md)

Interface for initializing a View using a specified ViewModel.

 [IViewModel](Aspid.MVVM.IViewModel.md)

Interface for a ViewModel that supports data binding functionality.

### Enums

 [Access](Aspid.MVVM.Access.md)

Defines access modifiers for properties generated from fields decorated with [`BindAttribute`](Aspid.MVVM.BindAttribute.md),
[`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md), [`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md), [`OneTimeBindAttribute`](Aspid.MVVM.OneTimeBindAttribute.md),
or [`OneWayToSourceBindAttribute`](Aspid.MVVM.OneWayToSourceBindAttribute.md).
Each value corresponds to a value from <code>Microsoft.CodeAnalysis.CSharp.SyntaxKind</code>.

 [BindMode](Aspid.MVVM.BindMode.md)

Represents the binding mode that determines the direction of data flow between the ViewModel and the View.

