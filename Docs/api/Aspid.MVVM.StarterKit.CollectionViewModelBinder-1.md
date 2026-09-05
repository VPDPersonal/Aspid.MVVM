---
title: "Class CollectionViewModelBinder<TView>"
sidebar_label: "CollectionViewModelBinder<TView>"
description: "Class CollectionViewModelBinder<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionViewModelBinder\<TView\> {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`CollectionBinder<T>`](Aspid.MVVM.StarterKit.CollectionBinder-1.md) that shows bound ViewModels in a fixed set of pre-placed views, in order.
Views beyond the item count are deactivated; items beyond the view count are not shown. Every change rebuilds the whole set.

```csharp
[Serializable]
public class CollectionViewModelBinder<TView> : CollectionBinder<IViewModel>, IRebindableBinder, IBinder<IReadOnlyCollection<IViewModel>>, IBinder where TView : MonoBehaviour, IView
```

#### Type Parameters

`TView` 

The type of the pre-placed views.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Binder](Aspid.MVVM.Binder.md) ← 
[CollectionBinder\<IViewModel\>](Aspid.MVVM.StarterKit.CollectionBinder-1.md) ← 
[CollectionViewModelBinder\<TView\>](Aspid.MVVM.StarterKit.CollectionViewModelBinder-1.md)

#### Implements

[IRebindableBinder](Aspid.MVVM.IRebindableBinder.md), 
[IBinder\<IReadOnlyCollection\<IViewModel\>\>](Aspid.MVVM.IBinder-1.md), 
[IBinder](Aspid.MVVM.IBinder.md)


#### Extension Methods

[BinderExtensions.BindSafely\<CollectionViewModelBinder\<TView\>\>\(CollectionViewModelBinder\<TView\>?, in FindBindableMemberResult, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_FindBindableMemberResult__System_Object_System_String_), 
[BinderExtensions.BindSafely\<CollectionViewModelBinder\<TView\>\>\(CollectionViewModelBinder\<TView\>?, IBinderAdder, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_BindSafely__1___0_Aspid_MVVM_IBinderAdder_System_Object_System_String_), 
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
[BinderExtensions.UnbindSafely\<CollectionViewModelBinder\<TView\>\>\(CollectionViewModelBinder\<TView\>?, object?, string?\)](Aspid.MVVM.BinderExtensions.md#Aspid_MVVM_BinderExtensions_UnbindSafely__1___0_System_Object_System_String_)

## Constructors

### CollectionViewModelBinder\(\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1__ctor}

```csharp
protected CollectionViewModelBinder()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### CollectionViewModelBinder\(TView\[\], BindMode\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1__ctor__0___Aspid_MVVM_BindMode_}

```csharp
public CollectionViewModelBinder(TView[] views, BindMode mode = BindMode.OneWay)
```

#### Parameters

`views` TView\[\]

The views the items are shown in, in order. Extra items are not shown.

`mode` [BindMode](Aspid.MVVM.BindMode.md)

The binding mode. Must not be [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">views</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when <code class="paramref">mode</code> is [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) or [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

## Methods

### OnAdded\(IReadOnlyCollection\<IViewModel\>?\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnAdded_System_Collections_Generic_IReadOnlyCollection_Aspid_MVVM_IViewModel__}

Called with the whole collection on binding and after a filter reset.

```csharp
protected override void OnAdded(IReadOnlyCollection<IViewModel>? values)
```

#### Parameters

`values` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>?

The items to show.

### OnAdded\(IViewModel?\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnAdded_Aspid_MVVM_IViewModel_}

Called when one item was added.

```csharp
protected override void OnAdded(IViewModel? newItem)
```

#### Parameters

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)?

The added item.

### OnAdded\(IReadOnlyList\<IViewModel?\>?\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnAdded_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__}

Called when several items were added at once.

```csharp
protected override void OnAdded(IReadOnlyList<IViewModel?>? newItems)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)?\>?

The added items.

### OnMoved\(IViewModel?, IViewModel?, int, int\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnMoved_Aspid_MVVM_IViewModel_Aspid_MVVM_IViewModel_System_Int32_System_Int32_}

Called when an item was moved.

```csharp
protected override void OnMoved(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)?

The item at <code class="paramref">oldStartingIndex</code> before the move.

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)?

The item at <code class="paramref">newStartingIndex</code> after the move.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index before the move.

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index after the move.

### OnRemoved\(IViewModel?\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnRemoved_Aspid_MVVM_IViewModel_}

Called when one item was removed.

```csharp
protected override void OnRemoved(IViewModel? oldItem)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)?

The removed item.

### OnRemoved\(IReadOnlyList\<IViewModel?\>?\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnRemoved_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__}

Called when several items were removed at once.

```csharp
protected override void OnRemoved(IReadOnlyList<IViewModel?>? oldItems)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)?\>?

The removed items.

### OnReplaced\(IViewModel?, IViewModel?, int\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnReplaced_Aspid_MVVM_IViewModel_Aspid_MVVM_IViewModel_System_Int32_}

Called when the item at <code class="paramref">index</code> was replaced.

```csharp
protected override void OnReplaced(IViewModel? oldItem, IViewModel? newItem, int index)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)?

The item before replacement.

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)?

The item after replacement.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the replaced item.

### OnReset\(\) {#Aspid_MVVM_StarterKit_CollectionViewModelBinder_1_OnReset}

Called when the collection was cleared or replaced; the View should drop every item.

```csharp
protected override void OnReset()
```

