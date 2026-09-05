---
title: "Class SequenceCollectionOrder<T>"
sidebar_label: "SequenceCollectionOrder<T>"
description: "Class SequenceCollectionOrder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SequenceCollectionOrder\<T\> {#Aspid_MVVM_StarterKit_SequenceCollectionOrder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionOrder<T>`](Aspid.MVVM.StarterKit.ICollectionOrder-1.md) that applies multiple orders in sequence: the first one that tells
two elements apart decides. Empty slots are skipped.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Sequence", Tooltip = "Applies orders in sequence: the first one that tells two elements apart decides")]
public class SequenceCollectionOrder<T> : ICollectionOrder<T>, IComparer<T>
```

#### Type Parameters

`T` 

The element type being ordered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SequenceCollectionOrder\<T\>](Aspid.MVVM.StarterKit.SequenceCollectionOrder-1.md)

#### Implements

[ICollectionOrder\<T\>](Aspid.MVVM.StarterKit.ICollectionOrder-1.md), 
[IComparer\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)



## Constructors

### SequenceCollectionOrder\(\) {#Aspid_MVVM_StarterKit_SequenceCollectionOrder_1__ctor}

```csharp
protected SequenceCollectionOrder()
```

### SequenceCollectionOrder\(params ICollectionOrder\<T\>?\[\]?\) {#Aspid_MVVM_StarterKit_SequenceCollectionOrder_1__ctor_Aspid_MVVM_StarterKit_ICollectionOrder__0____}

```csharp
public SequenceCollectionOrder(params ICollectionOrder<T>?[]? orders)
```

#### Parameters

`orders` [ICollectionOrder](Aspid.MVVM.StarterKit.ICollectionOrder-1.md)\<T\>?\[\]?

The orders applied in sequence. Empty slots are skipped.

## Methods

### Compare\(T, T\) {#Aspid_MVVM_StarterKit_SequenceCollectionOrder_1_Compare__0__0_}

Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.

```csharp
public int Compare(T x, T y)
```

#### Parameters

`x` T

The first object to compare.

`y` T

The second object to compare.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

A signed integer that indicates the relative values of <code class="paramref">x</code> and <code class="paramref">y</code>, as shown in the following table.  
  Value  

  Meaning  

  Less than zero  

 <code class="paramref">x</code> is less than <code class="paramref">y</code>.  

  Zero  

 <code class="paramref">x</code> equals <code class="paramref">y</code>.  

  Greater than zero  

 <code class="paramref">x</code> is greater than <code class="paramref">y</code>.

