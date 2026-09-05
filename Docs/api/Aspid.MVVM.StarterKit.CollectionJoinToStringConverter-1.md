---
title: "Class CollectionJoinToStringConverter<T>"
sidebar_label: "CollectionJoinToStringConverter<T>"
description: "Class CollectionJoinToStringConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionJoinToStringConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionJoinToStringConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Joins a collection into one string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To String", Name = "Join", Tooltip = "Joins a collection into one string")]
public class CollectionJoinToStringConverter<T> : IConverter<IEnumerable<T?>?, string>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionJoinToStringConverter\<T\>](Aspid.MVVM.StarterKit.CollectionJoinToStringConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> item renders as nothing: "a, , b".

## Constructors

### CollectionJoinToStringConverter\(\) {#Aspid_MVVM_StarterKit_CollectionJoinToStringConverter_1__ctor}

```csharp
public CollectionJoinToStringConverter()
```

#### Remarks

Default: joining with commas.

### CollectionJoinToStringConverter\(string, int, string, IConverter\<T?, string?\>?\) {#Aspid_MVVM_StarterKit_CollectionJoinToStringConverter_1__ctor_System_String_System_Int32_System_String_Aspid_MVVM_StarterKit_IConverter__0_System_String__}

```csharp
public CollectionJoinToStringConverter(string separator, int maxItems = 0, string emptyText = "", IConverter<T?, string?>? item = null)
```

#### Parameters

`separator` [string](https://learn.microsoft.com/dotnet/api/system.string)

Placed between items.

`maxItems` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many items to show. Zero shows all of them.

`emptyText` [string](https://learn.microsoft.com/dotnet/api/system.string)

Shown when the collection is empty.

`item` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<T?, [string](https://learn.microsoft.com/dotnet/api/system.string)?\>?

Writes each item. When omitted, the item is written with [`ToString`](https://learn.microsoft.com/dotnet/api/system.object.tostring).

## Methods

### Convert\(IEnumerable\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionJoinToStringConverter_1_Convert_System_Collections_Generic_IEnumerable__0__}

Joins the specified collection.

```csharp
public string Convert(IEnumerable<T?>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>?

The collection to join.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The joined text, or the empty text when there is nothing to join.
An invalid overflow format is reported and the overflow left out.

