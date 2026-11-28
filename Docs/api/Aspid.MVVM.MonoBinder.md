---
title: "Class MonoBinder"
sidebar_label: "MonoBinder"
description: "Class MonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoBinder {#Aspid_MVVM_MonoBinder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) binder with a serialized mode and binding lifecycle hooks.

```csharp
[SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public abstract class MonoBinder : MonoBehaviour, IMonoBinderValidatable, IBinder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
Object ←
Component ←
Behaviour ←
MonoBehaviour ←
[MonoBinder](Aspid.MVVM.MonoBinder.md)

#### Derived

[AddressableMonoBinder\<TAsset\>](Aspid.MVVM.StarterKit.AddressableMonoBinder-1.md),
[AnyToStringCasterMonoBinder](Aspid.MVVM.StarterKit.AnyToStringCasterMonoBinder.md),
[AudioMixerSnapshotMonoBinder](Aspid.MVVM.StarterKit.AudioMixerSnapshotMonoBinder.md),
[BehaviourEnabledByBindMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledByBindMonoBinder.md),
[CasterMonoBinder\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md),
[CollectionCountMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionCountMonoBinder-1.md),
[CollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md),
[CommandMonoBinder\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.CommandMonoBinder-4.md),
[CommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.CommandMonoBinder-3.md),
[CommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md),
[CommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md),
[CommandMonoBinder](Aspid.MVVM.StarterKit.CommandMonoBinder.md),
[ComponentMonoBinder\<TComponent\>](Aspid.MVVM.ComponentMonoBinder-1.md),
[ConditionalMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.ConditionalMonoBinder-1.md),
[DebugLogMonoBinder](Aspid.MVVM.StarterKit.DebugLogMonoBinder.md),
[EnumGroupMonoBinder\<TElement\>](Aspid.MVVM.StarterKit.EnumGroupMonoBinder-1.md),
[EnumMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.EnumMonoBinder-1.md),
[GameObjectToSourceMonoBinder](Aspid.MVVM.StarterKit.GameObjectToSourceMonoBinder.md),
[GameObjectVisibleByBindMonoBinder](Aspid.MVVM.StarterKit.GameObjectVisibleByBindMonoBinder.md),
[MonoBinder\<TProperty\>](Aspid.MVVM.StarterKit.MonoBinder-1.md),
[ObservableCollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.ObservableCollectionMonoBinder-1.md),
[ObservableDictionaryMonoBinder\<TKey, TValue\>](Aspid.MVVM.StarterKit.ObservableDictionaryMonoBinder-2.md),
[ObservableListMonoBinder\<T\>](Aspid.MVVM.StarterKit.ObservableListMonoBinder-1.md),
[RateLimitedMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.RateLimitedMonoBinder-1.md),
[ScriptableViewMonoBinder\<TView\>](Aspid.MVVM.ScriptableViewMonoBinder-1.md),
[SwitcherMonoBinder\<T\>](Aspid.MVVM.StarterKit.SwitcherMonoBinder-1.md),
[TweenMonoBinder\<TValue\>](Aspid.MVVM.StarterKit.TweenMonoBinder-1.md),
[UnityEventBoolByBindMonoBinder](Aspid.MVVM.StarterKit.UnityEventBoolByBindMonoBinder.md),
[UnityEventBoolMonoBinder](Aspid.MVVM.StarterKit.UnityEventBoolMonoBinder.md),
[UnityEventColorMonoBinder](Aspid.MVVM.StarterKit.UnityEventColorMonoBinder.md),
[UnityEventDoubleMonoBinder](Aspid.MVVM.StarterKit.UnityEventDoubleMonoBinder.md),
[UnityEventFloatMonoBinder](Aspid.MVVM.StarterKit.UnityEventFloatMonoBinder.md),
[UnityEventIntMonoBinder](Aspid.MVVM.StarterKit.UnityEventIntMonoBinder.md),
[UnityEventLongMonoBinder](Aspid.MVVM.StarterKit.UnityEventLongMonoBinder.md),
[UnityEventNumberConditionMonoBinder](Aspid.MVVM.StarterKit.UnityEventNumberConditionMonoBinder.md),
[UnityEventNumberConditionSwitcherMonoBinder](Aspid.MVVM.StarterKit.UnityEventNumberConditionSwitcherMonoBinder.md),
[UnityEventQuaternionMonoBinder](Aspid.MVVM.StarterKit.UnityEventQuaternionMonoBinder.md),
[UnityEventStringMonoBinder](Aspid.MVVM.StarterKit.UnityEventStringMonoBinder.md),
[UnityEventVector2MonoBinder](Aspid.MVVM.StarterKit.UnityEventVector2MonoBinder.md),
[UnityEventVector3MonoBinder](Aspid.MVVM.StarterKit.UnityEventVector3MonoBinder.md),
[VisualElementMonoBinder\<TElement\>](Aspid.MVVM.StarterKit.VisualElementMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md),
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\(IBinder?, in BindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_BindContext__),
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
[BinderExtensions.UnbindSafely\(IBinder?, in UnbindContext\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely_Aspid_MVVM_IBinder_Aspid_MVVM_UnbindContext__)

## Remarks

A derived class implements [`IBinder<T>`](Aspid.MVVM.IBinder-1.md), [`IReverseBinder<T>`](Aspid.MVVM.IReverseBinder-1.md) or both to exchange values.

## Properties

### BindingVersion {#Aspid_MVVM_MonoBinder_BindingVersion}

Gets a read-only reference to the stamp that changes when binding or unbinding starts.

```csharp
protected ref readonly VersionStamp BindingVersion { get; }
```

#### Property Value

 [VersionStamp](Aspid.MVVM.VersionStamp.md)

#### Remarks

Copy the stamp before calling user code and compare it with the current stamp afterward.

### CanBind {#Aspid_MVVM_MonoBinder_CanBind}

Gets a value indicating whether [`MonoBinder.Bind`](Aspid.MVVM.MonoBinder.md) should attempt to attach the binder.

```csharp
public virtual bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> for an intentional skip. Validate required configuration in [`MonoBinder.OnBinding`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBinding).
The default is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

### DefaultMode {#Aspid_MVVM_MonoBinder_DefaultMode}

Gets the initial binding mode. Defaults to [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

```csharp
protected virtual BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

#### Remarks

Override if [`BindModeOverrideAttribute`](Aspid.MVVM.BindModeOverrideAttribute.md) excludes [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).

### IsBound {#Aspid_MVVM_MonoBinder_IsBound}

Gets a value indicating whether binding is complete and cleanup has not started.

```csharp
public bool IsBound { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

A [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) binding remains bound until [`IBinder.Unbind`](Aspid.MVVM.IBinder.md).

### Mode {#Aspid_MVVM_MonoBinder_Mode}

Gets or sets the binding mode. An unconfigured binder uses [`MonoBinder.DefaultMode`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_DefaultMode).

```csharp
public BindMode Mode { get; set; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The binder is bound or a lifecycle operation is in progress.

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

The value is not a supported binding mode.

### Owner {#Aspid_MVVM_MonoBinder_Owner}

Gets the owner while this binder is being attached, bound or cleaned up; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> otherwise.

```csharp
protected object Owner { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

## Methods

### Bind\(in BindContext\) {#Aspid_MVVM_MonoBinder_Bind_Aspid_MVVM_BindContext__}

Attaches the binder through <code class="paramref">context</code>, calling [`MonoBinder.OnBinding`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBinding) before and
[`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound) after.

```csharp
public void Bind(in BindContext context)
```

#### Parameters

`context` [BindContext](Aspid.MVVM.BindContext.md)

The bindable member and the owner acquiring the binding.

#### Remarks

Does nothing when [`MonoBinder.CanBind`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_CanBind) is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>. Installation failures roll back;
exceptions from [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound) or [`MonoBinder.OnBoundToSource`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBoundToSource) propagate without rolling back the completed binding.

#### Exceptions

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

<code class="paramref">context</code> is uninitialized.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The binder is destroyed, already bound or a lifecycle operation is in progress.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Binding and rollback both fail. The binding exception is first.

### OnAttach\(\) {#Aspid_MVVM_MonoBinder_OnAttach}

Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.

```csharp
protected virtual void OnAttach()
```

#### Remarks

An exception rolls back the binding; [`MonoBinder.OnDetach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnDetach) releases partially acquired resources.

### OnBinding\(\) {#Aspid_MVVM_MonoBinder_OnBinding}

Called before attachment and the first value. Override to prepare for that value.

```csharp
protected virtual void OnBinding()
```

#### Remarks

Subscribe to the component in [`MonoBinder.OnAttach`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnAttach) so the first value is not reported back as user input.

### OnBound\(\) {#Aspid_MVVM_MonoBinder_OnBound}

Called with the binding complete and [`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) set. Override to notify or send an initial value.

```csharp
protected virtual void OnBound()
```

#### Remarks

Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.

### OnBoundToSource\(\) {#Aspid_MVVM_MonoBinder_OnBoundToSource}

Called after [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound) if the same [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) binding is still active.
Override to send the initial value to the ViewModel.

```csharp
protected virtual void OnBoundToSource()
```

### OnDestroy\(\) {#Aspid_MVVM_MonoBinder_OnDestroy}

Called when Unity destroys the component to release its binding.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

Overrides must call the base implementation.

### OnDetach\(\) {#Aspid_MVVM_MonoBinder_OnDetach}

Called after removal, before ownership is released. Override to release subscriptions and other resources.

```csharp
protected virtual void OnDetach()
```

#### Remarks

Also called after a failed installation; tolerate partially initialized resources.

### OnUnbinding\(\) {#Aspid_MVVM_MonoBinder_OnUnbinding}

Called when cleanup starts, while reported values still reach the ViewModel. Override to send a last value.

```csharp
protected virtual void OnUnbinding()
```

#### Remarks

[`MonoBinder.IsBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_IsBound) is already <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>; the owner is retained until cleanup finishes.

### OnUnbound\(\) {#Aspid_MVVM_MonoBinder_OnUnbound}

Called after cleanup releases ownership. Override to notify that the binder is unbound.

```csharp
protected virtual void OnUnbound()
```

#### Remarks

This hook may bind again; finish resource changes before invoking external code.

### Reset\(\) {#Aspid_MVVM_MonoBinder_Reset}

Called when Unity adds or resets the component to apply [`MonoBinder.DefaultMode`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_DefaultMode).

```csharp
protected virtual void Reset()
```

#### Remarks

Overrides must call the base implementation.

### Unbind\(in UnbindContext\) {#Aspid_MVVM_MonoBinder_Unbind_Aspid_MVVM_UnbindContext__}

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
