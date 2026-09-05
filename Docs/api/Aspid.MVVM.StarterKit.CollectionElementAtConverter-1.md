---
title: "Class CollectionElementAtConverter<T>"
sidebar_label: "CollectionElementAtConverter<T>"
description: "Class CollectionElementAtConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionElementAtConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionElementAtConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Takes one item out of a list by index.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To Value", Name = "Element At", Tooltip = "Takes one item out of a list by index")]
public class CollectionElementAtConverter<T> : IConverter<IReadOnlyList<T?>?, T?>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionElementAtConverter\<T\>](Aspid.MVVM.StarterKit.CollectionElementAtConverter-1.md)

#### Implements

[IConverter\<IReadOnlyList\<T?\>?, T?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### CollectionElementAtConverter\(\) {#Aspid_MVVM_StarterKit_CollectionElementAtConverter_1__ctor}

```csharp
public CollectionElementAtConverter()
```

#### Remarks

Default: taking the first item.

### CollectionElementAtConverter\(int, bool, T?\) {#Aspid_MVVM_StarterKit_CollectionElementAtConverter_1__ctor_System_Int32_System_Boolean__0_}

```csharp
public CollectionElementAtConverter(int index, bool fromEnd = false, T? fallback = default)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

Which item to take. An index outside a non-empty list is reported.

`fromEnd` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to count from the end.

`fallback` T?

Returned when the index is outside the list.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">index</code> is negative.

## Methods

### Convert\(IReadOnlyList\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionElementAtConverter_1_Convert_System_Collections_Generic_IReadOnlyList__0__}

Takes the configured item.

```csharp
public T? Convert(IReadOnlyList<T?>? value)
```

#### Parameters

`value` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>?

The list to read.

#### Returns

 T?

The item, or the fallback when the index is outside the list. Only a non-empty list reports it.

