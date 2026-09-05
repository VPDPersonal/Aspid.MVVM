---
title: "Class MonoBinder"
sidebar_label: "MonoBinder"
description: "Class MonoBinder — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoBinder {#Aspid_MVVM_MonoBinder}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Abstract base [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) binder that manages binding to and unbinding from an [`IViewModel`](Aspid.MVVM.IViewModel.md).
Derived classes implement [`IBinder<T>`](Aspid.MVVM.IBinder-1.md) to define what is bound.

```csharp
public abstract class MonoBinder : MonoBehaviour, IMonoBinderValidatable, IBinder, IRebindableBinder
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
[AggregatorInputMonoBinder\<TInput, TResult\>](Aspid.MVVM.StarterKit.AggregatorInputMonoBinder-2.md), 
[AnyToStringCasterMonoBinder](Aspid.MVVM.StarterKit.AnyToStringCasterMonoBinder.md), 
[AudioMixerSnapshotMonoBinder](Aspid.MVVM.StarterKit.AudioMixerSnapshotMonoBinder.md), 
[BehaviourEnabledByBindMonoBinder](Aspid.MVVM.StarterKit.BehaviourEnabledByBindMonoBinder.md), 
[CasterMonoBinder\<TFrom, TTo\>](Aspid.MVVM.StarterKit.CasterMonoBinder-2.md), 
[CollectionCountMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionCountMonoBinder-1.md), 
[CollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md), 
[CommandMonoBinder](Aspid.MVVM.StarterKit.CommandMonoBinder.md), 
[CommandMonoBinder\<T1, T2, T3, T4\>](Aspid.MVVM.StarterKit.CommandMonoBinder-4.md), 
[CommandMonoBinder\<T1, T2, T3\>](Aspid.MVVM.StarterKit.CommandMonoBinder-3.md), 
[CommandMonoBinder\<T1, T2\>](Aspid.MVVM.StarterKit.CommandMonoBinder-2.md), 
[CommandMonoBinder\<T\>](Aspid.MVVM.StarterKit.CommandMonoBinder-1.md), 
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
[IBinder](Aspid.MVVM.IBinder.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<MonoBinder\>\(MonoBinder?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<MonoBinder\>\(MonoBinder?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<MonoBinder\>\(MonoBinder?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Properties

### CanBind {#Aspid_MVVM_MonoBinder_CanBind}

Indicates whether binding is allowed. The default is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public virtual bool CanBind { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### DefaultMode {#Aspid_MVVM_MonoBinder_DefaultMode}

Gets the binding mode a freshly added binder starts in. The default is [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).
Override in binders whose <code>[BindModeOverride]</code> excludes it.

```csharp
protected virtual BindMode DefaultMode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

### IsBound {#Aspid_MVVM_MonoBinder_IsBound}

Indicates whether the binder is currently bound to a ViewModel.

```csharp
public bool IsBound { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Mode {#Aspid_MVVM_MonoBinder_Mode}

Gets the binding mode.

```csharp
public BindMode Mode { get; }
```

#### Property Value

 [BindMode](Aspid.MVVM.BindMode.md)

## Methods

### Bind\(IBinderAdder\) {#Aspid_MVVM_MonoBinder_Bind_Aspid_MVVM_IBinderAdder_}

Binds this binder using the specified [`IBinderAdder`](Aspid.MVVM.IBinderAdder.md).

```csharp
public void Bind(IBinderAdder binderAdder)
```

#### Parameters

`binderAdder` [IBinderAdder](Aspid.MVVM.IBinderAdder.md)

The binder adder that registers this binder with the ViewModel.

### OnBinding\(\) {#Aspid_MVVM_MonoBinder_OnBinding}

Called before binding is established. Override to add pre-binding logic.

```csharp
protected virtual void OnBinding()
```

#### Remarks

The ViewModel pushes its first value after this hook, so subscribe to the component in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound),
not here: a subscription taken here hears that first value as if the user had entered it.

### OnBound\(\) {#Aspid_MVVM_MonoBinder_OnBound}

Called after binding is established and the first value is applied. Override to subscribe to the component.

```csharp
protected virtual void OnBound()
```

### OnDestroy\(\) {#Aspid_MVVM_MonoBinder_OnDestroy}

Called by Unity when the component is destroyed. Unbinds so the ViewModel drops its reference to this binder.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

When overriding, always call <code>base.OnDestroy()</code>.

### OnUnbinding\(\) {#Aspid_MVVM_MonoBinder_OnUnbinding}

Called before unbinding, while the binder is still attached to the ViewModel. Override to add pre-unbinding logic.

```csharp
protected virtual void OnUnbinding()
```

### OnUnbound\(\) {#Aspid_MVVM_MonoBinder_OnUnbound}

Called after unbinding. Override to release a subscription taken in [`MonoBinder.OnBound`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_OnBound).

```csharp
protected virtual void OnUnbound()
```

### Reset\(\) {#Aspid_MVVM_MonoBinder_Reset}

Called by Unity when the component is added or reset in the Editor. Applies [`MonoBinder.DefaultMode`](Aspid.MVVM.MonoBinder.md#Aspid_MVVM_MonoBinder_DefaultMode).

```csharp
protected virtual void Reset()
```

#### Remarks

When overriding, always call <code>base.Reset()</code>.

### Unbind\(\) {#Aspid_MVVM_MonoBinder_Unbind}

Unbinds this binder from the bound [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public void Unbind()
```

