---
title: "Class OrCollectionFilter<T>"
sidebar_label: "OrCollectionFilter<T>"
description: "Class OrCollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OrCollectionFilter\<T\> {#Aspid_MVVM_StarterKit_OrCollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element when at least one nested filter passes it.
Empty slots are skipped; with no filter at all, everything passes.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Or", Tooltip = "Passes an element when at least one nested filter passes it")]
public class OrCollectionFilter<T> : ICollectionFilter<T>
```

#### Type Parameters

`T` 

The element type being filtered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OrCollectionFilter\<T\>](Aspid.MVVM.StarterKit.OrCollectionFilter-1.md)

#### Implements

[ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)



## Constructors

### OrCollectionFilter\(\) {#Aspid_MVVM_StarterKit_OrCollectionFilter_1__ctor}

```csharp
protected OrCollectionFilter()
```

### OrCollectionFilter\(params ICollectionFilter\<T\>?\[\]?\) {#Aspid_MVVM_StarterKit_OrCollectionFilter_1__ctor_Aspid_MVVM_StarterKit_ICollectionFilter__0____}

```csharp
public OrCollectionFilter(params ICollectionFilter<T>?[]? filters)
```

#### Parameters

`filters` [ICollectionFilter](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)\<T\>?\[\]?

The filters of which at least one must pass an element. Empty slots are skipped.

## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_OrCollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
public bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

