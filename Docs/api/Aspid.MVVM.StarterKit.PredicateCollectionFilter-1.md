---
title: "Class PredicateCollectionFilter<T>"
sidebar_label: "PredicateCollectionFilter<T>"
description: "Class PredicateCollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PredicateCollectionFilter\<T\> {#Aspid_MVVM_StarterKit_PredicateCollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that wraps a [`Predicate<T>`](https://learn.microsoft.com/dotnet/api/system.predicate-1) for code-built filters.

```csharp
[TypeSelectorDisplay(Hidden = true)]
public class PredicateCollectionFilter<T> : ICollectionFilter<T>
```

#### Type Parameters

`T` 

The element type being filtered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PredicateCollectionFilter\<T\>](Aspid.MVVM.StarterKit.PredicateCollectionFilter-1.md)

#### Implements

[ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)



## Constructors

### PredicateCollectionFilter\(Predicate\<T\>\) {#Aspid_MVVM_StarterKit_PredicateCollectionFilter_1__ctor_System_Predicate__0__}

```csharp
public PredicateCollectionFilter(Predicate<T> predicate)
```

#### Parameters

`predicate` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate-1)\<T\>

The predicate an element must satisfy.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">predicate</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_PredicateCollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
public bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

