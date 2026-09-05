---
title: "Class CollectionMonoBinder<T>"
sidebar_label: "CollectionMonoBinder<T>"
description: "Class CollectionMonoBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionMonoBinder\<T\> {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`MonoBinder`](Aspid.MVVM.MonoBinder.md) that receives a read-only collection and reflects it onto a View.
Observable and filtered lists are rebuilt on every change: [`CollectionMonoBinder<T>.OnReset`](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnReset), then [`CollectionMonoBinder<T>.OnAdded`](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection__0__) with the current items.

```csharp
public abstract class CollectionMonoBinder<T> : MonoBinder, IMonoBinderValidatable, IRebindableBinder, IBinder<IReadOnlyCollection<T>>, IBinder
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
[CollectionMonoBinder\<T\>](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyCollection\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<CollectionMonoBinder\<T\>\>\(CollectionMonoBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<CollectionMonoBinder\<T\>\>\(CollectionMonoBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<CollectionMonoBinder\<T\>\>\(CollectionMonoBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Fields

### SetValueMarker {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_SetValueMarker}

```csharp
protected static readonly ProfilerMarker SetValueMarker
```

#### Field Value

 ProfilerMarker

## Properties

### Collection {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_Collection}

Gets the bound collection, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is set.

```csharp
protected IReadOnlyCollection<T> Collection { get; }
```

#### Property Value

 [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T\>

### IsDebug {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_IsDebug}

```csharp
protected bool IsDebug { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### AddLog\(string\) {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_AddLog_System_String_}

```csharp
protected void AddLog(string log)
```

#### Parameters

`log` [string](https://learn.microsoft.com/dotnet/api/system.string)

### OnAdded\(IReadOnlyCollection\<T\>\) {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection__0__}

Called with the whole collection on binding and after every change.

```csharp
protected abstract void OnAdded(IReadOnlyCollection<T> values)
```

#### Parameters

`values` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T\>

The items to show.

### OnReset\(\) {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnReset}

Called before the items are shown again; the View should drop every item.

```csharp
protected abstract void OnReset()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnUnbound}

Unsubscribes from the bound collection.

```csharp
protected override void OnUnbound()
```

### SetValue\(IReadOnlyCollection\<T\>\) {#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_SetValue_System_Collections_Generic_IReadOnlyCollection__0__}

Binds to <code class="paramref">collection</code>: resets the previous one, then forwards the existing items to [`CollectionMonoBinder<T>.OnAdded`](Aspid.MVVM.StarterKit.CollectionMonoBinder-1.md#Aspid_MVVM_StarterKit_CollectionMonoBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection__0__).

```csharp
public void SetValue(IReadOnlyCollection<T> collection)
```

#### Parameters

`collection` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T\>

The collection to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

