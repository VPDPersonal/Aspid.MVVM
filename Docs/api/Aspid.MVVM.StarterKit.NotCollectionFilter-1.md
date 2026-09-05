---
title: "Class NotCollectionFilter<T>"
sidebar_label: "NotCollectionFilter<T>"
description: "Class NotCollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NotCollectionFilter\<T\> {#Aspid_MVVM_StarterKit_NotCollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element only when the nested filter rejects it.
An empty slot passes everything.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Not", Tooltip = "Passes an element only when the nested filter rejects it")]
public class NotCollectionFilter<T> : ICollectionFilter<T>
```

#### Type Parameters

`T` 

The element type being filtered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NotCollectionFilter\<T\>](Aspid.MVVM.StarterKit.NotCollectionFilter-1.md)

#### Implements

[ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)



## Constructors

### NotCollectionFilter\(\) {#Aspid_MVVM_StarterKit_NotCollectionFilter_1__ctor}

```csharp
protected NotCollectionFilter()
```

### NotCollectionFilter\(ICollectionFilter\<T\>\) {#Aspid_MVVM_StarterKit_NotCollectionFilter_1__ctor_Aspid_MVVM_StarterKit_ICollectionFilter__0__}

```csharp
public NotCollectionFilter(ICollectionFilter<T> filter)
```

#### Parameters

`filter` [ICollectionFilter](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)\<T\>

The filter whose verdict is inverted.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">filter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_NotCollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
public bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

