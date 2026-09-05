---
title: "Class CollectionBinder<T>"
sidebar_label: "CollectionBinder<T>"
description: "Class CollectionBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionBinder\<T\> {#Aspid_MVVM_StarterKit_CollectionBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that receives a read-only collection and reflects its changes onto a View.
Observable and filtered lists are followed through their change notifications.

```csharp
public abstract class CollectionBinder<T> : Binder, IRebindableBinder, IBinder<IReadOnlyCollection<T>>, IBinder
```

#### Type Parameters

`T` 

The element type of the collection.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[CollectionBinder\<T\>](Aspid.MVVM.StarterKit.CollectionBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyCollection\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<CollectionBinder\<T\>\>\(CollectionBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<CollectionBinder\<T\>\>\(CollectionBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<CollectionBinder\<T\>\>\(CollectionBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### CollectionBinder\(BindMode\) {#Aspid_MVVM_StarterKit_CollectionBinder_1__ctor_Aspid_MVVM_BindMode_}

```csharp
protected CollectionBinder(BindMode mode = BindMode.OneWay)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

## Properties

### Collection {#Aspid_MVVM_StarterKit_CollectionBinder_1_Collection}

Gets the bound collection, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is set.

```csharp
protected IReadOnlyCollection<T>? Collection { get; }
```

#### Property Value

 [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T\>?

## Methods

### OnAdded\(IReadOnlyCollection\<T\>?\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection__0__}

Called with the whole collection on binding and after a filter reset.

```csharp
protected abstract void OnAdded(IReadOnlyCollection<T>? values)
```

#### Parameters

`values` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T\>?

The items to show.

### OnAdded\(T?\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnAdded__0_}

Called when one item was added.

```csharp
protected abstract void OnAdded(T? newItem)
```

#### Parameters

`newItem` T?

The added item.

### OnAdded\(IReadOnlyList\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnAdded_System_Collections_Generic_IReadOnlyList__0__}

Called when several items were added at once.

```csharp
protected abstract void OnAdded(IReadOnlyList<T?>? newItems)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>?

The added items.

### OnMoved\(T?, T?, int, int\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnMoved__0__0_System_Int32_System_Int32_}

Called when an item was moved.

```csharp
protected abstract void OnMoved(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex)
```

#### Parameters

`oldItem` T?

The item at <code class="paramref">oldStartingIndex</code> before the move.

`newItem` T?

The item at <code class="paramref">newStartingIndex</code> after the move.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index before the move.

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index after the move.

### OnRemoved\(T?\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnRemoved__0_}

Called when one item was removed.

```csharp
protected abstract void OnRemoved(T? oldItem)
```

#### Parameters

`oldItem` T?

The removed item.

### OnRemoved\(IReadOnlyList\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnRemoved_System_Collections_Generic_IReadOnlyList__0__}

Called when several items were removed at once.

```csharp
protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>?

The removed items.

### OnReplaced\(T?, T?, int\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnReplaced__0__0_System_Int32_}

Called when the item at <code class="paramref">index</code> was replaced.

```csharp
protected abstract void OnReplaced(T? oldItem, T? newItem, int index)
```

#### Parameters

`oldItem` T?

The item before replacement.

`newItem` T?

The item after replacement.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the replaced item.

### OnReset\(\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnReset}

Called when the collection was cleared or replaced; the View should drop every item.

```csharp
protected abstract void OnReset()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_OnUnbound}

Unsubscribes from the bound collection.

```csharp
protected override void OnUnbound()
```

### SetValue\(IReadOnlyCollection\<T\>?\) {#Aspid_MVVM_StarterKit_CollectionBinder_1_SetValue_System_Collections_Generic_IReadOnlyCollection__0__}

Binds to <code class="paramref">collection</code>: resets the previous one, then forwards the existing items to [`CollectionBinder<T>.OnAdded`](Aspid.MVVM.StarterKit.CollectionBinder-1.md#Aspid_MVVM_StarterKit_CollectionBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection__0__).

```csharp
public void SetValue(IReadOnlyCollection<T>? collection)
```

#### Parameters

`collection` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T\>?

The collection to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

