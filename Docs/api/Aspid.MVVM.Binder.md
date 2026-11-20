---
title: "Class Binder"
sidebar_label: "Binder"
description: "Class Binder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Binder {#Aspid_MVVM_Binder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

[`IBinder`](Aspid.MVVM.IBinder.md) with a serialized mode and binding lifecycle hooks.

```csharp
[Serializable]
[SuppressMessage("ReSharper", "MemberInitializerValueIgnored")]
public abstract class Binder : IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Binder](Aspid.MVVM.Binder.md)

#### Derived

[AnyToStringCasterBinder](Aspid.MVVM.StarterKit.AnyToStringCasterBinder.md),
[AudioMixerSnapshotBinder](Aspid.MVVM.StarterKit.AudioMixerSnapshotBinder.md),
[Binder\<TProperty\>](Aspid.MVVM.StarterKit.Binder-1.md),
[CasterBinder\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterBinder-2.md),
[CasterBinder\<TTarget, TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterBinder-3.md),
[CollectionBinder\<T\>](Aspid.MVVM.StarterKit.CollectionBinder-1.md),
[DebugLogBinder](Aspid.MVVM.StarterKit.DebugLogBinder.md),
[DelegateOneWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayBinder-1.md),
[DelegateOneWayBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneWayBinder-2.md),
[DelegateOneWayToSourceBinder\<T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-1.md),
[DelegateOneWayToSourceBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateOneWayToSourceBinder-2.md),
[DelegateTwoWayBinder\<TTarget, T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-2.md),
[DelegateTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.DelegateTwoWayBinder-1.md),
[ObservableDictionaryBinder\<TKey, TValue\>](Aspid.MVVM.StarterKit.ObservableDictionaryBinder-2.md),
[ObservableListBinder\<T\>](Aspid.MVVM.StarterKit.ObservableListBinder-1.md),
[SwitcherBinder\<T\>](Aspid.MVVM.StarterKit.SwitcherBinder-1.md),
[TargetBinder\<TTarget\>](Aspid.MVVM.TargetBinder-1.md),
[ValueOneWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueOneWayBinder-1.md),
[ValueToStringCasterBinder\<T\>](Aspid.MVVM.StarterKit.ValueToStringCasterBinder-1.md),
[ValueTwoWayBinder\<T\>](Aspid.MVVM.StarterKit.ValueTwoWayBinder-1.md),
[ViewBinder](Aspid.MVVM.ViewBinder.md)

#### Implements

[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<Binder\>\(Binder?, in FindBindableMemberResult, object, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_),
[BinderExtensions.BindSafely\<Binder\>\(Binder?, in BindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_BindContext__System_String_),
[BinderLogger.Log\(IBinder, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_Log_Aspid_MVVM_IBinder_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderLogger.LogError\(IBinder, Exception, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogError_Aspid_MVVM_IBinder_System_Exception_System_String_UnityEngine_Object_),
[BinderLogger.LogWarning\(IBinder, string, string, Object?\)](Aspid.MVVM.StarterKit.BinderLogger.md#Aspid_MVVM_StarterKit_BinderLogger_LogWarning_Aspid_MVVM_IBinder_System_String_System_String_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.NonNegative\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_NonNegative_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector2, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector2_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector3, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector3_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Vector4, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Vector4_UnityEngine_Object_),
[BinderMath.RequireFinite\(IBinder, Rect, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_RequireFinite_Aspid_MVVM_IBinder_UnityEngine_Rect_UnityEngine_Object_),
[BinderMath.SafeClamp\(IBinder, float, float, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp_Aspid_MVVM_IBinder_System_Single_System_Single_System_Single_UnityEngine_Object_),
[BinderMath.SafeClamp01\(IBinder, float, Object?\)](Aspid.MVVM.StarterKit.BinderMath.md#Aspid_MVVM_StarterKit_BinderMath_SafeClamp01_Aspid_MVVM_IBinder_System_Single_UnityEngine_Object_),
[BinderExtensions.UnbindSafely\<Binder\>\(Binder?, in UnbindContext, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_Aspid_MVVM_UnbindContext__System_String_)

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

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">mode</code> is [`BindMode.None`](Aspid.MVVM.BindMode.md) or undefined.

## Properties

### CanBind {#Aspid_MVVM_Binder_CanBind}

Gets a value indicating whether [`Binder.Bind`](Aspid.MVVM.Binder.md) attaches the binder; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> unless
overridden.

```csharp
public virtual bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsBound {#Aspid_MVVM_Binder_IsBound}

Gets a value indicating whether binding is complete and cleanup has not started.

```csharp
public bool IsBound { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

A [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) binding remains bound until [`IBinder.Unbind`](Aspid.MVVM.IBinder.md).

### Mode {#Aspid_MVVM_Binder_Mode}

Gets the binding mode set by the constructor or in the Inspector.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### Owner {#Aspid_MVVM_Binder_Owner}

Gets the owner while this binder is being attached, bound or cleaned up; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> otherwise.

```csharp
protected object? Owner { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)?

## Methods

### Bind\(in BindContext\) {#Aspid_MVVM_Binder_Bind_Aspid_MVVM_BindContext__}

Attaches the binder through <code class="paramref">context</code>, calling [`Binder.OnBinding`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBinding) before and
[`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) after.

```csharp
public void Bind(in BindContext context)
```

#### Parameters

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and the owner acquiring the binding.

#### Remarks

Does nothing when [`Binder.CanBind`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_CanBind) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>. Installation failures roll back;
exceptions from [`Binder.OnBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnBound) propagate without rolling back the completed binding.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> is uninitialized.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The binder is already bound or a lifecycle operation is in progress.

### OnAttach\(\) {#Aspid_MVVM_Binder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected virtual void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`Binder.OnDetach`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnDetach) releases partially acquired resources.

### OnBinding\(\) {#Aspid_MVVM_Binder_OnBinding}

Called before attachment and the first value. Override to prepare for that value.

```csharp
protected virtual void OnBinding()
```

#### Remarks

Subscribe to the component in [`Binder.OnAttach`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_OnAttach) so the first value is not reported back as user input.

### OnBound\(\) {#Aspid_MVVM_Binder_OnBound}

Called with the binding complete and [`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) set. Override to notify or send an initial value.

```csharp
protected virtual void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnDetach\(\) {#Aspid_MVVM_Binder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected virtual void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnUnbinding\(\) {#Aspid_MVVM_Binder_OnUnbinding}

Called when cleanup starts, while reported values still reach the ViewModel. Override to send a last value.

```csharp
protected virtual void OnUnbinding()
```

#### Remarks

[`Binder.IsBound`](Aspid.MVVM.Binder.md#Aspid_MVVM_Binder_IsBound) is already <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>; the owner is retained until cleanup finishes.

### OnUnbound\(\) {#Aspid_MVVM_Binder_OnUnbound}

Called after cleanup releases ownership. Override to notify that the binder is unbound.

```csharp
protected virtual void OnUnbound()
```

#### Remarks

This hook may bind again; finish resource changes before invoking external code.

### Unbind\(in UnbindContext\) {#Aspid_MVVM_Binder_Unbind_Aspid_MVVM_UnbindContext__}

Detaches the binder when <code class="paramref">context</code> identifies its owner, calling the cleanup hooks.

```csharp
public void Unbind(in UnbindContext context)
```

#### Parameters

`context` [UnbindContext](Aspid.MVVM.UnbindContext.md)

The owner releasing the binding.

#### Remarks

Does nothing for a different owner or when cleanup is already in progress.
A single cleanup failure is rethrown with its original stack trace after the remaining steps finish.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> is uninitialized.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The owner requests cleanup before attachment finishes.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup operations fail.
