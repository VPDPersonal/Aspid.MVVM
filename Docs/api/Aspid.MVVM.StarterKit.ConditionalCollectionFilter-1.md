---
title: "Class ConditionalCollectionFilter<T>"
sidebar_label: "ConditionalCollectionFilter<T>"
description: "Class ConditionalCollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConditionalCollectionFilter\<T\> {#Aspid_MVVM_StarterKit_ConditionalCollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that applies the nested filter only while enabled.
When disabled, or with an empty slot, everything passes.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Composition", Name = "Conditional", Tooltip = "Applies the nested filter only while enabled")]
public class ConditionalCollectionFilter<T> : ICollectionFilter<T>
```

#### Type Parameters

`T` 

The element type being filtered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConditionalCollectionFilter\<T\>](Aspid.MVVM.StarterKit.ConditionalCollectionFilter-1.md)

#### Implements

[ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)



## Constructors

### ConditionalCollectionFilter\(\) {#Aspid_MVVM_StarterKit_ConditionalCollectionFilter_1__ctor}

```csharp
protected ConditionalCollectionFilter()
```

### ConditionalCollectionFilter\(ICollectionFilter\<T\>, bool\) {#Aspid_MVVM_StarterKit_ConditionalCollectionFilter_1__ctor_Aspid_MVVM_StarterKit_ICollectionFilter__0__System_Boolean_}

```csharp
public ConditionalCollectionFilter(ICollectionFilter<T> filter, bool isEnabled = true)
```

#### Parameters

`filter` [ICollectionFilter](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)\<T\>

The filter applied while enabled.

`isEnabled` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the nested filter is applied.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">filter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### IsEnabled {#Aspid_MVVM_StarterKit_ConditionalCollectionFilter_1_IsEnabled}

Gets or sets whether the nested filter is applied.

```csharp
public bool IsEnabled { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_ConditionalCollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
public bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

