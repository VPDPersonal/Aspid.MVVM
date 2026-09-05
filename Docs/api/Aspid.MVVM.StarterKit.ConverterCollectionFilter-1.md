---
title: "Class ConverterCollectionFilter<T>"
sidebar_label: "ConverterCollectionFilter<T>"
description: "Class ConverterCollectionFilter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ConverterCollectionFilter\<T\> {#Aspid_MVVM_StarterKit_ConverterCollectionFilter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICollectionFilter<T>`](Aspid.MVVM.StarterKit.ICollectionFilter-1.md) that passes an element when an [`IConverter<T1, T2>`](Aspid.MVVM.StarterKit.IConverter-2.md)
to [`Boolean`](https://learn.microsoft.com/dotnet/api/system.boolean) answers <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> for it. An empty slot passes everything.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid", Name = "Converter", Tooltip = "Passes an element when a converter to bool answers true for it")]
public class ConverterCollectionFilter<T> : ICollectionFilter<T>
```

#### Type Parameters

`T` 

The element type being filtered.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ConverterCollectionFilter\<T\>](Aspid.MVVM.StarterKit.ConverterCollectionFilter-1.md)

#### Implements

[ICollectionFilter\<T\>](Aspid.MVVM.StarterKit.ICollectionFilter-1.md)



## Constructors

### ConverterCollectionFilter\(\) {#Aspid_MVVM_StarterKit_ConverterCollectionFilter_1__ctor}

```csharp
protected ConverterCollectionFilter()
```

### ConverterCollectionFilter\(IConverter\<T, bool\>\) {#Aspid_MVVM_StarterKit_ConverterCollectionFilter_1__ctor_Aspid_MVVM_StarterKit_IConverter__0_System_Boolean__}

```csharp
public ConverterCollectionFilter(IConverter<T, bool> converter)
```

#### Parameters

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

The converter that decides whether an element passes.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">converter</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Matches\(T\) {#Aspid_MVVM_StarterKit_ConverterCollectionFilter_1_Matches__0_}

Returns whether the element is shown.

```csharp
public bool Matches(T item)
```

#### Parameters

`item` T

The element to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

