---
title: "Class AndCollectionFilter<T>"
sidebar_label: "AndCollectionFilter<T>"
description: "Class AndCollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AndCollectionFilter\<T\> {#Aspid_MVVM_StarterKit_AndCollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element only when every nested filter passes it.
Empty slots are skipped.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "And", Tooltip = "Passes an element only when every nested filter passes it")]
public class AndCollectionFilter<T> : ICollectionFilter<T>
```

#### Type Parameters

`T` 

The element type being filtered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AndCollectionFilter\<T\>](Aspid.MVVM.StarterKit.AndCollectionFilter-1.md)

#### Implements

[ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)



## Constructors

### AndCollectionFilter\(\) {#Aspid_MVVM_StarterKit_AndCollectionFilter_1__ctor}

```csharp
protected AndCollectionFilter()
```

### AndCollectionFilter\(params ICollectionFilter\<T\>?\[\]?\) {#Aspid_MVVM_StarterKit_AndCollectionFilter_1__ctor_Aspid_MVVM_StarterKit_ICollectionFilter__0____}

```csharp
public AndCollectionFilter(params ICollectionFilter<T>?[]? filters)
```

#### Parameters

`filters` [ICollectionFilter](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)\<T\>?\[\]?

The filters that must all pass an element. Empty slots are skipped.

## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_AndCollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
public bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

