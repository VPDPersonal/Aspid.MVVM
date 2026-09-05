---
title: "Class ObservableListBinder<T>"
sidebar_label: "ObservableListBinder<T>"
description: "Class ObservableListBinder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableListBinder\<T\> {#Aspid_MVVM_StarterKit_ObservableListBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`Binder`](Aspid.MVVM.Binder.md) that follows a plain, observable or filtered list and reflects its
add, remove, replace, move and reset changes onto a View.

```csharp
public abstract class ObservableListBinder<T> : Binder, IRebindableBinder, IBinder<IReadOnlyList<T>>, IBinder<IReadOnlyFilteredList<T>>, IBinder<IReadOnlyObservableList<T>>, IBinder
```

#### Type Parameters

`T` 

The element type of the list.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[ObservableListBinder\<T\>](Aspid.MVVM.StarterKit.ObservableListBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyList\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IReadOnlyFilteredList\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IReadOnlyObservableList\<T\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObservableListBinder\<T\>\>\(ObservableListBinder\<T\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObservableListBinder\<T\>\>\(ObservableListBinder\<T\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObservableListBinder\<T\>\>\(ObservableListBinder\<T\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### ObservableListBinder\(BindMode\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1__ctor_Aspid_MVVM_BindMode_}

```csharp
protected ObservableListBinder(BindMode mode = BindMode.OneWay)
```

#### Parameters

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode.

## Properties

### List {#Aspid_MVVM_StarterKit_ObservableListBinder_1_List}

Gets the bound list, possibly wrapped by [`ObservableListBinder<T>.GetFilteredList`](Aspid.MVVM.StarterKit.ObservableListBinder-1.md#Aspid_MVVM_StarterKit_ObservableListBinder_1_GetFilteredList_System_Collections_Generic_IReadOnlyList__0__), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is set.

```csharp
protected IReadOnlyList<T?>? List { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>?

## Methods

### GetFilteredList\(IReadOnlyList\<T\>\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_GetFilteredList_System_Collections_Generic_IReadOnlyList__0__}

Called on binding to optionally wrap the list in a filtered view. Override to add a filter.

```csharp
protected virtual IReadOnlyFilteredList<T>? GetFilteredList(IReadOnlyList<T> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

The bound list.

#### Returns

 IReadOnlyFilteredList\<T\>?

The filtered view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use <code class="paramref">list</code> as-is.

### OnAdded\(T?, int\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnAdded__0_System_Int32_}

Called when one item was added.

```csharp
protected abstract void OnAdded(T? newItem, int index)
```

#### Parameters

`newItem` T?

The added item.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was inserted at.

### OnAdded\(IReadOnlyList\<T?\>?, int\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnAdded_System_Collections_Generic_IReadOnlyList__0__System_Int32_}

Called when several items were added at once, including the whole list on binding.

```csharp
protected abstract void OnAdded(IReadOnlyList<T?>? newItems, int index)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>?

The added items.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the first added item.

### OnMoved\(T?, T?, int, int\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnMoved__0__0_System_Int32_System_Int32_}

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

### OnRemoved\(T?, int\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnRemoved__0_System_Int32_}

Called when one item was removed.

```csharp
protected abstract void OnRemoved(T? oldItem, int oldStartingIndex)
```

#### Parameters

`oldItem` T?

The removed item.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was removed from.

### OnRemoved\(IReadOnlyList\<T?\>?, int\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnRemoved_System_Collections_Generic_IReadOnlyList__0__System_Int32_}

Called when several items were removed at once.

```csharp
protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems, int oldStartingIndex)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>?

The removed items.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the first removed item.

### OnReplaced\(T?, T?, int\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnReplaced__0__0_System_Int32_}

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

### OnReset\(\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnReset}

Called when the list was cleared or replaced; the View should drop every item.

```csharp
protected abstract void OnReset()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_OnUnbound}

Unsubscribes from the bound list and resets the View.

```csharp
protected override void OnUnbound()
```

### SetValue\(IReadOnlyList\<T\>?\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_SetValue_System_Collections_Generic_IReadOnlyList__0__}

Binds to a plain list; changes to it are not observed.

```csharp
public void SetValue(IReadOnlyList<T>? list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>?

The list to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

### SetValue\(IReadOnlyFilteredList\<T\>?\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_SetValue_Aspid_Collections_Observable_Filtered_IReadOnlyFilteredList__0__}

Binds to a filtered list; a filter change resets and replays the whole list.

```csharp
public void SetValue(IReadOnlyFilteredList<T>? list)
```

#### Parameters

`list` IReadOnlyFilteredList\<T\>?

The list to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

### SetValue\(IReadOnlyObservableList\<T\>?\) {#Aspid_MVVM_StarterKit_ObservableListBinder_1_SetValue_Aspid_Collections_Observable_IReadOnlyObservableList__0__}

Binds to an observable list and follows its granular changes.

```csharp
public void SetValue(IReadOnlyObservableList<T>? list)
```

#### Parameters

`list` IReadOnlyObservableList\<T\>?

The list to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear the binding.

