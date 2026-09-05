---
title: "Class ComparisonCollectionOrder<T>"
sidebar_label: "ComparisonCollectionOrder<T>"
description: "Class ComparisonCollectionOrder<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ComparisonCollectionOrder\<T\> {#Aspid_MVVM_StarterKit_ComparisonCollectionOrder_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionOrder<T>`](Aspid.MVVM.StarterKit.ICollectionOrder-1.md) that wraps a [`Comparison<T>`](https://learn.microsoft.com/dotnet/api/system.comparison-1) or an
[`IComparer<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1) for code-built sort orders.

```csharp
[TypeSelectorDisplay(Hidden = true)]
public class ComparisonCollectionOrder<T> : ICollectionOrder<T>, IComparer<T>
```

#### Type Parameters

`T` 

The element type being ordered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ComparisonCollectionOrder\<T\>](Aspid.MVVM.StarterKit.ComparisonCollectionOrder-1.md)

#### Implements

[ICollectionOrder\<T\>](Aspid.MVVM.StarterKit.ICollectionOrder-1.md), 
[IComparer\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)



## Constructors

### ComparisonCollectionOrder\(IComparer\<T?\>\) {#Aspid_MVVM_StarterKit_ComparisonCollectionOrder_1__ctor_System_Collections_Generic_IComparer__0__}

```csharp
public ComparisonCollectionOrder(IComparer<T?> comparer)
```

#### Parameters

`comparer` [IComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)\<T?\>

The comparer to wrap.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">comparer</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### ComparisonCollectionOrder\(Comparison\<T?\>\) {#Aspid_MVVM_StarterKit_ComparisonCollectionOrder_1__ctor_System_Comparison__0__}

```csharp
public ComparisonCollectionOrder(Comparison<T?> comparison)
```

#### Parameters

`comparison` [Comparison](https://learn.microsoft.com/dotnet/api/system.comparison-1)\<T?\>

The comparison to wrap.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">comparison</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Compare\(T?, T?\) {#Aspid_MVVM_StarterKit_ComparisonCollectionOrder_1_Compare__0__0_}

Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.

```csharp
public int Compare(T? x, T? y)
```

#### Parameters

`x` T?

The first object to compare.

`y` T?

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

