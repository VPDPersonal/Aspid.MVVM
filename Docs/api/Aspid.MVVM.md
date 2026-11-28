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

Adds an Inspector context menu entry to the named serialized properties of a component.

 [AddBinderContextMenuByTypeAttribute](Aspid.MVVM.AddBinderContextMenuByTypeAttribute.md)

Adds an Inspector context menu entry to serialized properties matching a value type.

 [AsBinderAttribute](Aspid.MVVM.AsBinderAttribute.md)

Wraps a field or readable property of a [`[View]`](Aspid.MVVM.ViewAttribute.md) class in a binder.

 [BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md)

[`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) shared by [`BindAttribute`](Aspid.MVVM.BindAttribute.md) and the bind attributes with a fixed mode.

 [BindAlsoAttribute](Aspid.MVVM.BindAlsoAttribute.md)

Notifies the binders of another bindable property whenever the marked member of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class changes.

 [BindAttribute](Aspid.MVVM.BindAttribute.md)

Makes a field or property bindable in a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [BindIdAttribute](Aspid.MVVM.BindIdAttribute.md)

Sets the identifier used to match a View binder to a ViewModel member.

 [BindModeAttribute](Aspid.MVVM.BindModeAttribute.md)

Limits the binding modes that the Inspector offers for a [`BindMode`](Aspid.MVVM.BindMode.md) field.

 [BindModeExtensions](Aspid.MVVM.BindModeExtensions.md)

Provides extension methods for [`BindMode`](Aspid.MVVM.BindMode.md).

 [BindModeOverrideAttribute](Aspid.MVVM.BindModeOverrideAttribute.md)

Overrides the [`BindModeAttribute`](Aspid.MVVM.BindModeAttribute.md) restrictions for the class and its subclasses.

 [Binder](Aspid.MVVM.Binder.md)

[`IBinder`](Aspid.MVVM.IBinder.md) with a serialized mode and binding lifecycle hooks.

 [BinderExtensions](Aspid.MVVM.BinderExtensions.md)

Provides extension methods for [`IBinder`](Aspid.MVVM.IBinder.md) and binder collections.

 [BinderInvalidCastException](Aspid.MVVM.BinderInvalidCastException.md)

[`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) for a binder that cannot receive the bound value type from the ViewModel.

 [BinderLogAttribute](Aspid.MVVM.BinderLogAttribute.md)

Records incoming values in the Inspector before calling the marked <code>SetValue</code> method.

 [ComponentMonoBinder\<TComponent\>](Aspid.MVVM.ComponentMonoBinder-1.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) with an assigned target component or a fallback on the same GameObject.

 [GenerateSerializableBinderAttribute](Aspid.MVVM.GenerateSerializableBinderAttribute.md)

Generates a serializable binder from the annotated [`MonoBinder`](Aspid.MVVM.MonoBinder.md) class.

 [HeaderGroupAttribute](Aspid.MVVM.HeaderGroupAttribute.md)

Places only the decorated member in a named Inspector foldout.

 [HeaderGroupEndAttribute](Aspid.MVVM.HeaderGroupEndAttribute.md)

Ends the current Inspector foldout range before the decorated member.

 [HeaderGroupStartAttribute](Aspid.MVVM.HeaderGroupStartAttribute.md)

Starts an Inspector foldout range at the decorated member.

 [IgnoreBindAttribute](Aspid.MVVM.IgnoreBindAttribute.md)

Excludes the field or property of a [`[View]`](Aspid.MVVM.ViewAttribute.md) class from the generated binding code.

 [MonoBinder](Aspid.MVVM.MonoBinder.md)

[`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) binder with a serialized mode and binding lifecycle hooks.

 [MonoView](Aspid.MVVM.MonoView.md)

[`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) for views with serialized binder groups.

 [MonoViewBinder\<TView\>](Aspid.MVVM.MonoViewBinder-1.md)

[`ViewTargetBinder<T>`](Aspid.MVVM.ViewTargetBinder-1.md) restricted to [`Component`](https://docs.unity3d.com/ScriptReference/Component.html)-based views.

 [MonoViewBinder](Aspid.MVVM.MonoViewBinder.md)

Concrete [`MonoViewBinder<T>`](Aspid.MVVM.MonoViewBinder-1.md) for [`MonoView`](Aspid.MVVM.MonoView.md).

 [MonoViewModel](Aspid.MVVM.MonoViewModel.md)

[`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) for ViewModels that refresh bindings after Inspector changes.

 [MonoViewMonoBinder\<TView\>](Aspid.MVVM.MonoViewMonoBinder-1.md)

[`ComponentMonoBinder<T>`](Aspid.MVVM.ComponentMonoBinder-1.md) that binds a component view to an [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [MonoViewMonoBinder](Aspid.MVVM.MonoViewMonoBinder.md)

Concrete [`MonoViewMonoBinder<T>`](Aspid.MVVM.MonoViewMonoBinder-1.md) for [`MonoView`](Aspid.MVVM.MonoView.md).

 [OneTimeBindAttribute](Aspid.MVVM.OneTimeBindAttribute.md)

Binds the marked field or property in [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) using [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

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

Binds the marked field or property in [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) using [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

 [OneWayBindableMember\<T\>](Aspid.MVVM.OneWayBindableMember-1.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes every value change to its binders.

 [OneWayEnumBindableMember\<T\>](Aspid.MVVM.OneWayEnumBindableMember-1.md)

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [OneWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayStructBindableMember-2.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes every value change to its binders, boxing it for an
[`IBinder<T>`](Aspid.MVVM.IBinder-1.md) of <code class="typeparamref">TBoxed</code>.

 [OneWayStructBindableMember\<T\>](Aspid.MVVM.OneWayStructBindableMember-1.md)

Concrete [`OneWayStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

 [OneWayToSourceBindAttribute](Aspid.MVVM.OneWayToSourceBindAttribute.md)

Binds the marked field or property in [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) using [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

 [OneWayToSourceBindableMember\<T\>](Aspid.MVVM.OneWayToSourceBindableMember-1.md)

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) that passes values from its reverse binders to a setter.

 [OneWayToSourceEnumBindableMember\<T\>](Aspid.MVVM.OneWayToSourceEnumBindableMember-1.md)

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [OneWayToSourceStructBindableMember\<T\>](Aspid.MVVM.OneWayToSourceStructBindableMember-1.md)

Concrete [`OneWayToSourceStructBindableMember<T1, T2>`](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

 [OneWayToSourceStructBindableMember\<T, TBoxed\>](Aspid.MVVM.OneWayToSourceStructBindableMember-2.md)

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) that passes values from its reverse binders to a setter, unboxing them from an
[`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) of <code class="typeparamref">TBoxed</code>.

 [RelayCommand\<T1, T2, T3\>](Aspid.MVVM.RelayCommand-3.md)

[`IRelayCommand<T1, T2, T3>`](Aspid.MVVM.IRelayCommand-3.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.RelayCommand-4.md)

[`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

[`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand\<T\>](Aspid.MVVM.RelayCommand-1.md)

[`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommand\<T1, T2\>](Aspid.MVVM.RelayCommand-2.md)

[`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) that runs a delegate, optionally gated by a predicate.

 [RelayCommandAttribute](Aspid.MVVM.RelayCommandAttribute.md)

Generates a relay command for the marked method of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

 [RelayCommandExtensions](Aspid.MVVM.RelayCommandExtensions.md)

Provides extension methods for creating relay commands, binding arguments and supplying empty commands.

 [RequireBinderAttribute](Aspid.MVVM.RequireBinderAttribute.md)

Configures the binding ID and accepted value types for Inspector validation of a serialized binder field.

 [ReverseBinderInvalidCastException\<T\>](Aspid.MVVM.ReverseBinderInvalidCastException-1.md)

[`InvalidCastException`](https://learn.microsoft.com/dotnet/api/system.invalidcastexception) for a binder that cannot report the bound value type to the ViewModel.

 [ScriptableView](Aspid.MVVM.ScriptableView.md)

[`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) for views with generated binding lifecycle methods.

 [ScriptableViewBinder](Aspid.MVVM.ScriptableViewBinder.md)

Concrete [`ScriptableViewBinder<T>`](Aspid.MVVM.ScriptableViewBinder-1.md) for [`ScriptableView`](Aspid.MVVM.ScriptableView.md).

 [ScriptableViewBinder\<TView\>](Aspid.MVVM.ScriptableViewBinder-1.md)

[`ViewTargetBinder<T>`](Aspid.MVVM.ViewTargetBinder-1.md) restricted to [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html)-based views.

 [ScriptableViewModel](Aspid.MVVM.ScriptableViewModel.md)

[`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) for ViewModels that refresh bindings after Inspector changes.

 [ScriptableViewMonoBinder](Aspid.MVVM.ScriptableViewMonoBinder.md)

Concrete [`ScriptableViewMonoBinder<T>`](Aspid.MVVM.ScriptableViewMonoBinder-1.md) for [`ScriptableView`](Aspid.MVVM.ScriptableView.md).

 [ScriptableViewMonoBinder\<TView\>](Aspid.MVVM.ScriptableViewMonoBinder-1.md)

[`MonoBinder`](Aspid.MVVM.MonoBinder.md) that binds an assigned [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) view to an [`IViewModel`](Aspid.MVVM.IViewModel.md).

 [ShowDesignViewModelAttribute](Aspid.MVVM.ShowDesignViewModelAttribute.md)

Shows and configures the design ViewModel selector on a [`MonoView`](Aspid.MVVM.MonoView.md) Inspector.

 [TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md)

[`Binder`](Aspid.MVVM.Binder.md) with a serialized target.

 [TwoWayBindAttribute](Aspid.MVVM.TwoWayBindAttribute.md)

Binds the marked field or property in [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) using [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).

 [TwoWayBindableMember\<T\>](Aspid.MVVM.TwoWayBindableMember-1.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes value changes to its binders and passes values from its reverse binders
to a setter.

 [TwoWayEnumBindableMember\<T\>](Aspid.MVVM.TwoWayEnumBindableMember-1.md)

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`Enum`](https://learn.microsoft.com/dotnet/api/system.enum).

 [TwoWayStructBindableMember\<T, TBoxed\>](Aspid.MVVM.TwoWayStructBindableMember-2.md)

[`IBindableMember<T>`](Aspid.MVVM.IBindableMember-1.md) that pushes value changes to its binders and passes values from its reverse binders
to a setter, also through binders of <code class="typeparamref">TBoxed</code>.

 [TwoWayStructBindableMember\<T\>](Aspid.MVVM.TwoWayStructBindableMember-1.md)

Concrete [`TwoWayStructBindableMember<T1, T2>`](Aspid.MVVM.TwoWayStructBindableMember-2.md) that fixes <code>TBoxed</code> to [`ValueType`](https://learn.microsoft.com/dotnet/api/system.valuetype).

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

[`TargetBinder<T>`](Aspid.MVVM.TargetBinder-1.md) that binds a view to an [`IViewModel`](Aspid.MVVM.IViewModel.md) and deinitializes it on unbind.

### Structs

 [BindContext](Aspid.MVVM.BindContext.md)

Binding request containing a bindable member and its owner.

 [FindBindableMemberParameters](Aspid.MVVM.FindBindableMemberParameters.md)

Represents the query passed to [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

 [FindBindableMemberResult](Aspid.MVVM.FindBindableMemberResult.md)

Represents the result of [`IViewModel.FindBindableMember`](Aspid.MVVM.IViewModel.md).

 [UnbindContext](Aspid.MVVM.UnbindContext.md)

Cleanup request identifying the owner releasing a binder.

 [VersionStamp](Aspid.MVVM.VersionStamp.md)

Marks a revision of an operation so earlier work can detect when it is stale.

### Interfaces

 [IAnyBinder](Aspid.MVVM.IAnyBinder.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that receives values of any type from the ViewModel.

 [IAnyReverseBinder](Aspid.MVVM.IAnyReverseBinder.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that reports untyped values from the View back to the ViewModel.

 [IBindableMember\<T\>](Aspid.MVVM.IBindableMember-1.md)

[`IReadOnlyBindableMember<T>`](Aspid.MVVM.IReadOnlyBindableMember-1.md) whose value can also be set.

 [IBinder](Aspid.MVVM.IBinder.md)

Defines a binder that attaches to a bindable member of a ViewModel.

 [IBinder\<T\>](Aspid.MVVM.IBinder-1.md)

[`IBinder`](Aspid.MVVM.IBinder.md) that receives values of <code class="typeparamref">T</code> from the ViewModel.

 [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

Defines the side of a bindable member that accepts binders.

 [IBinderRemover](Aspid.MVVM.IBinderRemover.md)

Defines the side of a bindable member that detaches binders.

 [IReadOnlyBindableMember\<T\>](Aspid.MVVM.IReadOnlyBindableMember-1.md)

[`IReadOnlyValueBindableMember<T>`](Aspid.MVVM.IReadOnlyValueBindableMember-1.md) that reports value changes.

 [IReadOnlyValueBindableMember\<T\>](Aspid.MVVM.IReadOnlyValueBindableMember-1.md)

[`IBinderAdder`](Aspid.MVVM.IBinderAdder.md) that exposes its current value.

 [IRelayCommand\<T\>](Aspid.MVVM.IRelayCommand-1.md)

Defines a command with one parameter.

 [IRelayCommand\<T1, T2\>](Aspid.MVVM.IRelayCommand-2.md)

Defines a command with two parameters.

 [IRelayCommand\<T1, T2, T3\>](Aspid.MVVM.IRelayCommand-3.md)

Defines a command with three parameters.

 [IRelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.IRelayCommand-4.md)

Defines a command with four parameters.

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

Defines a command without parameters.

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
