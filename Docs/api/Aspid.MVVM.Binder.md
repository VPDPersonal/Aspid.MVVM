---
title: "Class Binder"
sidebar_label: "Binder"
description: "Class Binder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Binder {#Aspid_MVVM_Binder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Abstract base class for binder implementations.
Manages the binding lifecycle — binding to and unbinding from an [`IViewModel`](Aspid.MVVM.IViewModel.md).
Derived classes must implement [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) to define the specific binding behavior.

```csharp
[Serializable]
public abstract class Binder : IBinder, IRebindableBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md)

#### Derived

[AnyToStringCasterBinder](Aspid.MVVM.StarterKit.AnyToStringCasterBinder.md), 
[AudioMixerSnapshotBinder](Aspid.MVVM.StarterKit.AudioMixerSnapshotBinder.md), 
[Binder\<TProperty\>](Aspid.MVVM.StarterKit.Binder-1.md), 
[CasterBinder\<TTarget, TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterBinder-3.md), 
[CasterBinder\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterBinder-2.md), 
[CollectionBinder\<T\>](Aspid.MVVM.StarterKit.CollectionBinder-1.md), 
[DebugLogBinder](Aspid.MVVM.StarterKit.DebugLogBinder.md), 
[DelegateOneWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayBinder-1.md), 
[DelegateOneWayBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneWayBinder-2.md), 
[DelegateOneWayToSourceBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-1.md), 
[DelegateOneWayToSourceBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-2.md), 
[DelegateTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-1.md), 
[DelegateTwoWayBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-2.md), 
[ObservableDictionaryBinder\<TKey, TValue\>](Aspid.MVVM.StarterKit.ObservableDictionaryBinder-2.md), 
[ObservableListBinder\<T\>](Aspid.MVVM.StarterKit.ObservableListBinder-1.md), 
[SwitcherBinder\<T\>](Aspid.MVVM.StarterKit.SwitcherBinder-1.md), 
[TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md), 
[ValueOneWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md), 
[ValueToStringCasterBinder\<T\>](Aspid.MVVM.StarterKit.ValueToStringCasterBinder-1.md), 
[ValueTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md), 
[ViewBinder](Aspid.MVVM.ViewBinder.md)

#### Implements

[IBinder](Aspid.MVVM.IBinder.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<Binder\>\(Binder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<Binder\>\(Binder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_), 
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[RebindableBinderExtensions.Rebind\(IBinder\)](Aspid.MVVM.RebindableBinderExtensions.md#Aspid_MVVM_RebindableBinderExtensions_Rebind_Aspid_MVVM_IBinder_), 
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_), 
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_), 
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_), 
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_), 
[BinderExtensions.UnbindSafely\<Binder\>\(Binder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### Binder\(\) {#Aspid_MVVM_Binder__ctor}

```csharp
protected Binder()
```

#### Remarks

For deserialization only: Unity builds a serialized instance without running a constructor's
arguments and assigns the fields itself.

### Binder\(BindMode\) {#Aspid_MVVM_Binder__ctor_Aspid_MVVM_BindMode_}

```csharp
protected Binder(BindMode mode = BindMode.OneWay)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode to use for the binder.

## Properties

### CanBind {#Aspid_MVVM_Binder_CanBind}

Indicates whether binding is allowed.
The default value is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public virtual bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsBound {#Aspid_MVVM_Binder_IsBound}

Indicates whether the binder is currently bound to a ViewModel.

```csharp
public bool IsBound { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Mode {#Aspid_MVVM_Binder_Mode}

Gets the binding mode that determines the direction of data flow.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### Bind\(IBinderAdder\) {#Aspid_MVVM_Binder_Bind_Aspid_MVVM_IBinderAdder_}

Binds this binder using the specified [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md).

```csharp
public void Bind(IBinderAdder binderAdder)
```

#### Parameters

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The binder adder that registers this binder with the ViewModel.

### OnBinding\(\) {#Aspid_MVVM_Binder_OnBinding}

Called before binding is established. Override to add pre-binding logic.

```csharp
protected virtual void OnBinding()
```

#### Remarks

The order is: [`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding), then the ViewModel pushes its current value, then
[`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) becomes <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, then [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound).

<p></p>

That first push happens <em>after</em> this hook, which is why a binder that listens to its component
subscribes in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) and not here: subscribing here means hearing the ViewModel's own
first value come back as if the user had entered it.

### OnBound\(\) {#Aspid_MVVM_Binder_OnBound}

Called after binding is established. Override to add post-binding logic.

```csharp
protected virtual void OnBound()
```

#### Remarks

Runs after the ViewModel's first value has been applied and after [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. This is where a binder subscribes to its component — see
[`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) for why the earlier hook is the wrong place.

### OnUnbinding\(\) {#Aspid_MVVM_Binder_OnUnbinding}

Called before unbinding. Override to add pre-unbinding logic.

```csharp
protected virtual void OnUnbinding()
```

#### Remarks

Runs while [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is still <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> and the binder is still
attached to the ViewModel, so anything sent from here still arrives.

### OnUnbound\(\) {#Aspid_MVVM_Binder_OnUnbound}

Called after unbinding. Override to add post-unbinding logic.

```csharp
protected virtual void OnUnbound()
```

#### Remarks

Runs once the binder is detached and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.
This is where a subscription taken in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) is released.

### Unbind\(\) {#Aspid_MVVM_Binder_Unbind}

Unbinds this binder from the bound [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public void Unbind()
```

