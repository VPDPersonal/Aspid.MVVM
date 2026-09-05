---
title: "Class ObservableListViewModelMonoBinder<TView>"
sidebar_label: "ObservableListViewModelMonoBinder<TView>"
description: "Class ObservableListViewModelMonoBinder<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableListViewModelMonoBinder\<TView\> {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ObservableListMonoBinder<T>`](Aspid.MVVM.StarterKit.ObservableListMonoBinder-1.md) that creates a view per ViewModel in list order, with an optional filter and sort order.

```csharp
public abstract class ObservableListViewModelMonoBinder<TView> : ObservableListMonoBinder<IViewModel>, IMonoBinderValidatable, IRebindableBinder, IBinder<IReadOnlyList<IViewModel>>, IBinder<IReadOnlyFilteredList<IViewModel>>, IBinder<IReadOnlyObservableList<IViewModel>>, IBinder where TView : MonoBehaviour, IView
```

#### Type Parameters

`TView` 

The type of view created per item.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[MonoBinder](Aspid.MVVM.MonoBinder.md) ← 
[ObservableListMonoBinder\<IViewModel\>](Aspid.MVVM.StarterKit.ObservableListMonoBinder-1.md) ← 
[ObservableListViewModelMonoBinder\<TView\>](Aspid.MVVM.StarterKit.ObservableListViewModelMonoBinder-1.md)

#### Implements

[IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md), 
[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyList\<IViewModel\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IReadOnlyFilteredList\<IViewModel\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder\<IReadOnlyObservableList\<IViewModel\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<ObservableListViewModelMonoBinder\<TView\>\>\(ObservableListViewModelMonoBinder\<TView\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<ObservableListViewModelMonoBinder\<TView\>\>\(ObservableListViewModelMonoBinder\<TView\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<ObservableListViewModelMonoBinder\<TView\>\>\(ObservableListViewModelMonoBinder\<TView\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Methods

### GetFilteredList\(IReadOnlyList\<IViewModel\>\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_GetFilteredList_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__}

Called on binding to optionally wrap the list in a filtered view. Override to add a filter.

```csharp
protected override sealed IReadOnlyFilteredList<IViewModel> GetFilteredList(IReadOnlyList<IViewModel> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

The bound list.

#### Returns

 IReadOnlyFilteredList\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

The filtered view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use <code class="paramref">list</code> as-is.

### OnAdded\(IViewModel, int\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnAdded_Aspid_MVVM_IViewModel_System_Int32_}

Called when one item was added.

```csharp
protected override sealed void OnAdded(IViewModel newItem, int index)
```

#### Parameters

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The added item.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was inserted at.

### OnAdded\(IReadOnlyList\<IViewModel\>, int\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnAdded_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__System_Int32_}

Called when several items were added at once, including the whole list on binding.

```csharp
protected override sealed void OnAdded(IReadOnlyList<IViewModel> newItems, int index)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

The added items.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the first added item.

### OnMoved\(IViewModel, IViewModel, int, int\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnMoved_Aspid_MVVM_IViewModel_Aspid_MVVM_IViewModel_System_Int32_System_Int32_}

Called when an item was moved.

```csharp
protected override sealed void OnMoved(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The item at <code class="paramref">oldStartingIndex</code> before the move.

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The item at <code class="paramref">newStartingIndex</code> after the move.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index before the move.

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index after the move.

### OnRemoved\(IViewModel, int\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnRemoved_Aspid_MVVM_IViewModel_System_Int32_}

Called when one item was removed.

```csharp
protected override sealed void OnRemoved(IViewModel oldItem, int oldStartingIndex)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The removed item.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was removed from.

### OnRemoved\(IReadOnlyList\<IViewModel\>, int\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnRemoved_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__System_Int32_}

Called when several items were removed at once.

```csharp
protected override sealed void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

The removed items.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the first removed item.

### OnReplaced\(IViewModel, IViewModel, int\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnReplaced_Aspid_MVVM_IViewModel_Aspid_MVVM_IViewModel_System_Int32_}

Called when the item at <code class="paramref">index</code> was replaced.

```csharp
protected override sealed void OnReplaced(IViewModel oldItem, IViewModel newItem, int index)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The item before replacement.

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The item after replacement.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the replaced item.

### OnReset\(\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnReset}

Called when the list was cleared or replaced; the View should drop every item.

```csharp
protected override sealed void OnReset()
```

### OnUnbound\(\) {#Aspid_MVVM_StarterKit_ObservableListViewModelMonoBinder_1_OnUnbound}

Disposes the filtered view before the base class detaches from the list.

```csharp
protected override void OnUnbound()
```

#### Remarks

When overriding, always call <code>base.OnUnbound()</code>.

