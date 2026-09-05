---
title: "Class ObservableCollectionMonoBinder<T>"
sidebar_label: "ObservableCollectionMonoBinder<T>"
description: "Class ObservableCollectionMonoBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableCollectionMonoBinder\<T\> {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that follows any `IObservableCollection<T>` (a set, a queue, a stack)
and reflects its changes onto a View. The hooks carry items only: these collections have no stable index.

```csharp
public abstract class ObservableCollectionMonoBinder<T> : MonoBinder, IMonoBinderValidatable, IRebindableBinder, IBinder<IObservableCollection<T>>, IBinder
```

#### Type Parameters

`T` 

The element type of the collection.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ObservableCollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.ObservableCollectionMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IObservableCollection\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObservableCollectionMonoBinder\<T\>\>\(ObservableCollectionMonoBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObservableCollectionMonoBinder\<T\>\>\(ObservableCollectionMonoBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObservableCollectionMonoBinder\<T\>\>\(ObservableCollectionMonoBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Collection {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_Collection}

Gets the bound collection, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is set.

```csharp
protected IObservableCollection<T> Collection { get; }
```

#### Property Value

 IObservableCollection\<T\>

### IsDebug {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAdded\(T\) {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_OnAdded__0_}

Called when an item was added.

```csharp
protected abstract void OnAdded(T newItem)
```

#### Parameters

`newItem` T

The added item.

### OnRemoved\(T\) {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_OnRemoved__0_}

Called when an item was removed.

```csharp
protected abstract void OnRemoved(T oldItem)
```

#### Parameters

`oldItem` T

The removed item.

### OnReset\(\) {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_OnReset}

Called when the collection was cleared or reordered; the View should drop every item.

```csharp
protected abstract void OnReset()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_OnUnbound}

Resets the View and unsubscribes from the bound collection.

```csharp
protected override void OnUnbound()
```

### SetValue\(IObservableCollection\<T\>\) {#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_SetValue_Aspid_Collections_Observable_IObservableCollection__0__}

Binds to <code class="paramref">value</code>: resets the previous one, then forwards the existing items to [`ObservableCollectionMonoBinder<T>.OnAdded`](Aspid.MVVM.StarterKit.ObservableCollectionMonoBinder-1.md#Aspid_MVVM_StarterKit_ObservableCollectionMonoBinder_1_OnAdded__0_).

```csharp
public void SetValue(IObservableCollection<T> value)
```

#### Parameters

`value` IObservableCollection\<T\>

The collection to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

