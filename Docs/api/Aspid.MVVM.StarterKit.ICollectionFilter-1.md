---
title: "Interface ICollectionFilter<T>"
sidebar_label: "ICollectionFilter<T>"
description: "Interface ICollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface ICollectionFilter\<T\> {#Aspid_MVVM_StarterKit_ICollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Serializable filter for collection binders.

```csharp
public interface ICollectionFilter<in T>
```

#### Type Parameters

`T` 

The element type being filtered.


## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_ICollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

