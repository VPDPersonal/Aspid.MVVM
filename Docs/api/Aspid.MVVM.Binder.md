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

Serializable [`IBinder`](Aspid.MVVM.IBinder.md) that tracks its binding state and calls lifecycle hooks around binding.

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

## Remarks

A derived class implements [`IBinder<T>`](Aspid.MVVM.IBinder-1.md), [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) or both to exchange values.

## Constructors

### Binder\(\) {#Aspid_MVVM_Binder__ctor}

Creates a [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) binder for Unity deserialization, which then restores the serialized mode.

```csharp
protected Binder()
```

### Binder\(BindMode\) {#Aspid_MVVM_Binder__ctor_Aspid_MVVM_BindMode_}

Creates a binder with the given binding mode.

```csharp
protected Binder(BindMode mode = BindMode.OneWay)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

## Properties

### CanBind {#Aspid_MVVM_Binder_CanBind}

Gets a value indicating whether [`Binder.Bind`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_Bind_Aspid_MVVM_IBinderAdder_) attaches the binder; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> unless
overridden.

```csharp
public virtual bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsBound {#Aspid_MVVM_Binder_IsBound}

Gets a value indicating whether the binder is attached to a bindable member.

```csharp
public bool IsBound { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Mode {#Aspid_MVVM_Binder_Mode}

Gets the binding mode set by the constructor or in the Inspector.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### Bind\(IBinderAdder\) {#Aspid_MVVM_Binder_Bind_Aspid_MVVM_IBinderAdder_}

Attaches the binder through <code class="paramref">binderAdder</code>, calling [`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) before and
[`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) after.

```csharp
public void Bind(IBinderAdder binderAdder)
```

#### Parameters

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The bindable member to attach to.

#### Remarks

Does nothing when [`Binder.CanBind`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_CanBind) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>, and logs an error when the binder is already bound.

### OnBinding\(\) {#Aspid_MVVM_Binder_OnBinding}

Called before the binder attaches and receives the first value. Override to prepare for that value.

```csharp
protected virtual void OnBinding()
```

#### Remarks

Subscribe to the component in [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) instead: a subscription made here reports the first value from the
ViewModel back as if the user had entered it.

### OnBound\(\) {#Aspid_MVVM_Binder_OnBound}

Called after the binder attaches and receives the first value. Override to subscribe to the component.

```csharp
protected virtual void OnBound()
```

### OnUnbinding\(\) {#Aspid_MVVM_Binder_OnUnbinding}

Called before the binder detaches, while values it reports still reach the ViewModel. Override to report a last value.

```csharp
protected virtual void OnUnbinding()
```

### OnUnbound\(\) {#Aspid_MVVM_Binder_OnUnbound}

Called after the binder detaches. Override to release what [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) subscribed to.

```csharp
protected virtual void OnUnbound()
```

### Unbind\(\) {#Aspid_MVVM_Binder_Unbind}

Detaches the binder, calling [`Binder.OnUnbinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnUnbinding) before and [`Binder.OnUnbound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnUnbound) after; does nothing when it
is not bound.

```csharp
public void Unbind()
```

