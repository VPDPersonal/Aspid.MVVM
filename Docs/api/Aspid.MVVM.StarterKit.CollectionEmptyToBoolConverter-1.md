---
title: "Class CollectionEmptyToBoolConverter<T>"
sidebar_label: "CollectionEmptyToBoolConverter<T>"
description: "Class CollectionEmptyToBoolConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionEmptyToBoolConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionEmptyToBoolConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reports whether a collection has anything in it.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To Bool", Name = "Is Empty", Tooltip = "Reports whether a collection has anything in it")]
public class CollectionEmptyToBoolConverter<T> : IConverter<IEnumerable<T?>?, bool>, IConverter<IReadOnlyCollection<T?>?, bool>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionEmptyToBoolConverter\<T\>](Aspid.MVVM.StarterKit.CollectionEmptyToBoolConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IReadOnlyCollection\<T?\>?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A sequence carrying no count of its own is asked for one item, never for all of them.

## Constructors

### CollectionEmptyToBoolConverter\(\) {#Aspid_MVVM_StarterKit_CollectionEmptyToBoolConverter_1__ctor}

```csharp
public CollectionEmptyToBoolConverter()
```

#### Remarks

Default: without inverting.

### CollectionEmptyToBoolConverter\(bool\) {#Aspid_MVVM_StarterKit_CollectionEmptyToBoolConverter_1__ctor_System_Boolean_}

```csharp
public CollectionEmptyToBoolConverter(bool isInvert)
```

#### Parameters

`isInvert` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, inverts the result.

## Methods

### Convert\(IReadOnlyCollection\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionEmptyToBoolConverter_1_Convert_System_Collections_Generic_IReadOnlyCollection__0__}

Tests whether the specified collection is empty.

```csharp
public bool Convert(IReadOnlyCollection<T?>? value)
```

#### Parameters

`value` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T?\>?

The collection to test.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or empty, inverted when configured.

