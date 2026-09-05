---
title: "Class CollectionFirstConverter<T>"
sidebar_label: "CollectionFirstConverter<T>"
description: "Class CollectionFirstConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionFirstConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionFirstConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Takes the first item of a sequence.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To Value", Name = "First", Tooltip = "Takes the first item of a sequence")]
public class CollectionFirstConverter<T> : IConverter<IEnumerable<T?>?, T?>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionFirstConverter\<T\>](Aspid.MVVM.StarterKit.CollectionFirstConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Takes any sequence, indexer or not.

## Constructors

### CollectionFirstConverter\(\) {#Aspid_MVVM_StarterKit_CollectionFirstConverter_1__ctor}

```csharp
public CollectionFirstConverter()
```

#### Remarks

Default: falling back to the type default.

### CollectionFirstConverter\(T?\) {#Aspid_MVVM_StarterKit_CollectionFirstConverter_1__ctor__0_}

```csharp
public CollectionFirstConverter(T? fallback)
```

#### Parameters

`fallback` T?

Returned when the sequence is empty.

## Methods

### Convert\(IEnumerable\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionFirstConverter_1_Convert_System_Collections_Generic_IEnumerable__0__}

Takes the first item of the specified sequence.

```csharp
public T? Convert(IEnumerable<T?>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>?

The sequence to read.

#### Returns

 T?

The first item, or the fallback when there is none.

