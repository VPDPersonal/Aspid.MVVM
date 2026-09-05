---
title: "Interface ICollectionOrder<T>"
sidebar_label: "ICollectionOrder<T>"
description: "Interface ICollectionOrder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface ICollectionOrder\<T\> {#Aspid_MVVM_StarterKit_ICollectionOrder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Serializable sort order for collection binders.

```csharp
public interface ICollectionOrder<in T> : IComparer<T>
```

#### Type Parameters

`T` 

The element type being ordered.

#### Implements

[IComparer\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)

