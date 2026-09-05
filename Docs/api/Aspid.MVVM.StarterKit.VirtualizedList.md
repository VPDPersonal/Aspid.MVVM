---
title: "Class VirtualizedList"
sidebar_label: "VirtualizedList"
description: "Class VirtualizedList — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VirtualizedList {#Aspid_MVVM_StarterKit_VirtualizedList}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ScrollRect`](https://docs.unity3d.com/ScriptReference/UI-ScrollRect.html) that shows a list of ViewModels through a fixed set of recycled views,
instantiating only as many as fit the viewport.

```csharp
[AddComponentMenu("Aspid/MVVM/Components/UI/ScrollRect/VirtualizedList (Beta)")]
public class VirtualizedList : ScrollRect, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, IEventSystemHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
UIBehaviour ← 
ScrollRect ← 
[VirtualizedList](Aspid.MVVM.StarterKit.VirtualizedList.md)

#### Implements

IInitializePotentialDragHandler, 
IBeginDragHandler, 
IEndDragHandler, 
IDragHandler, 
IScrollHandler, 
IEventSystemHandler, 
ICanvasElement, 
ILayoutElement, 
ILayoutGroup, 
ILayoutController



## Remarks

Beta. Items share the prefab size, scroll in one direction only, without spacing or layout groups.

## Properties

### ItemsSource {#Aspid_MVVM_StarterKit_VirtualizedList_ItemsSource}

Gets or sets the ViewModels shown by the list. Observable and filtered lists are tracked for changes.

```csharp
public IReadOnlyList<IViewModel> ItemsSource { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

## Methods

### OnAdded\(IViewModel, int\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnAdded_Aspid_MVVM_IViewModel_System_Int32_}

Called when one item is inserted.

```csharp
protected virtual void OnAdded(IViewModel newItem, int newStartingIndex)
```

#### Parameters

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The inserted ViewModel.

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was inserted at.

### OnAdded\(IReadOnlyList\<IViewModel\>, int\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnAdded_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__System_Int32_}

Called when several items are inserted.

```csharp
protected virtual void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

The inserted ViewModels.

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the first inserted item.

### OnDisable\(\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnDisable}

```csharp
protected override void OnDisable()
```

### OnEnable\(\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnEnable}

```csharp
protected override void OnEnable()
```

### OnMove\(IViewModel, int, int\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnMove_Aspid_MVVM_IViewModel_System_Int32_System_Int32_}

Called when one item is moved.

```csharp
protected virtual void OnMove(IViewModel item, int oldStartingIndex, int newStartingIndex)
```

#### Parameters

`item` [IViewModel](Aspid.MVVM.IViewModel.md)

The moved ViewModel.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was moved from.

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was moved to.

### OnRemoved\(IViewModel, int\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnRemoved_Aspid_MVVM_IViewModel_System_Int32_}

Called when one item is removed.

```csharp
protected virtual void OnRemoved(IViewModel oldItem, int oldStartingIndex)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The removed ViewModel.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index it was removed from.

### OnRemoved\(IReadOnlyList\<IViewModel\>, int\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnRemoved_System_Collections_Generic_IReadOnlyList_Aspid_MVVM_IViewModel__System_Int32_}

Called when several items are removed.

```csharp
protected virtual void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<[IViewModel](Aspid.MVVM.IViewModel.md)\>

The removed ViewModels.

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the first removed item.

### OnReplace\(IViewModel, IViewModel, int\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnReplace_Aspid_MVVM_IViewModel_Aspid_MVVM_IViewModel_System_Int32_}

Called when one item is replaced in place.

```csharp
protected virtual void OnReplace(IViewModel oldItem, IViewModel newItem, int index)
```

#### Parameters

`oldItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The replaced ViewModel.

`newItem` [IViewModel](Aspid.MVVM.IViewModel.md)

The new ViewModel.

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of the replaced item.

### OnReset\(\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnReset}

Called when the source is reset or filtered anew.

```csharp
protected virtual void OnReset()
```

### OnValidate\(\) {#Aspid_MVVM_StarterKit_VirtualizedList_OnValidate}

```csharp
protected override void OnValidate()
```

