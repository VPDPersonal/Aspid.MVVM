---
title: "Class CollectionTakeConverter<T>"
sidebar_label: "CollectionTakeConverter<T>"
description: "Class CollectionTakeConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionTakeConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionTakeConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Keeps a few items off one end of a sequence.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Take", Tooltip = "Keeps a few items off one end of a sequence")]
public class CollectionTakeConverter<T> : IConverter<IEnumerable<T?>?, IEnumerable<T?>>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionTakeConverter\<T\>](Aspid.MVVM.StarterKit.CollectionTakeConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, IEnumerable\<T?\>\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The result is one [`List<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1) refilled on every call, so read it at once.

## Constructors

### CollectionTakeConverter\(\) {#Aspid_MVVM_StarterKit_CollectionTakeConverter_1__ctor}

```csharp
public CollectionTakeConverter()
```

#### Remarks

Default: keeping the first three items.

### CollectionTakeConverter\(int, bool\) {#Aspid_MVVM_StarterKit_CollectionTakeConverter_1__ctor_System_Int32_System_Boolean_}

```csharp
public CollectionTakeConverter(int count, bool fromEnd = false)
```

#### Parameters

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many items to keep. Zero keeps none of them.

`fromEnd` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to take from the end rather than from the start.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">count</code> is negative.

## Methods

### Convert\(IEnumerable\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionTakeConverter_1_Convert_System_Collections_Generic_IEnumerable__0__}

Keeps the configured items of the specified sequence.

```csharp
public IEnumerable<T?> Convert(IEnumerable<T?>? value)
```

#### Parameters

`value` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>?

The sequence to shorten.

#### Returns

 [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T?\>

The kept items in their original order, in a list reused on the next call.

