---
title: "Class InverseCollectionOrder<T>"
sidebar_label: "InverseCollectionOrder<T>"
description: "Class InverseCollectionOrder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class InverseCollectionOrder\<T\> {#Aspid_MVVM_StarterKit_InverseCollectionOrder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionOrder<T>`](Aspid.MVVM.StarterKit.ICollectionOrder-1.md) that runs the nested order in the opposite direction.
An empty slot keeps the source order.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Inverse", Tooltip = "Runs an order in the opposite direction")]
public class InverseCollectionOrder<T> : ICollectionOrder<T>, IComparer<T>
```

#### Type Parameters

`T` 

The element type being ordered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InverseCollectionOrder\<T\>](Aspid.MVVM.StarterKit.InverseCollectionOrder-1.md)

#### Implements

[ICollectionOrder\<T\>](Aspid.MVVM.StarterKit.ICollectionOrder-1.md), 
[IComparer\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)



## Constructors

### InverseCollectionOrder\(\) {#Aspid_MVVM_StarterKit_InverseCollectionOrder_1__ctor}

```csharp
protected InverseCollectionOrder()
```

### InverseCollectionOrder\(ICollectionOrder\<T\>\) {#Aspid_MVVM_StarterKit_InverseCollectionOrder_1__ctor_Aspid_MVVM_StarterKit_ICollectionOrder__0__}

```csharp
public InverseCollectionOrder(ICollectionOrder<T> order)
```

#### Parameters

`order` [ICollectionOrder](Aspid.MVVM.StarterKit.ICollectionOrder-1.md)\<T\>

The order to run in the opposite direction.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">order</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Compare\(T, T\) {#Aspid_MVVM_StarterKit_InverseCollectionOrder_1_Compare__0__0_}

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

