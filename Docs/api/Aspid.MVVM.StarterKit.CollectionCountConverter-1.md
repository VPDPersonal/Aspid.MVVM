---
title: "Class CollectionCountConverter<T>"
sidebar_label: "CollectionCountConverter<T>"
description: "Class CollectionCountConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionCountConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionCountConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Counts the items in a collection.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To Number", Name = "Count", Tooltip = "Counts the items in a collection")]
public class CollectionCountConverter<T> : IConverter<IEnumerable<T?>?, int>, IConverter<IEnumerable<T?>?, long>, IConverter<IEnumerable<T?>?, float>, IConverter<IEnumerable<T?>?, double>, IConverter<IReadOnlyCollection<T?>?, int>, IConverter<IReadOnlyCollection<T?>?, long>, IConverter<IReadOnlyCollection<T?>?, float>, IConverter<IReadOnlyCollection<T?>?, double>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionCountConverter\<T\>](Aspid.MVVM.StarterKit.CollectionCountConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<T?\>?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<T?\>?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IEnumerable\<T?\>?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IReadOnlyCollection\<T?\>?, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IReadOnlyCollection\<T?\>?, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IReadOnlyCollection\<T?\>?, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IReadOnlyCollection\<T?\>?, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A sequence carrying no count of its own is walked on every push.

## Methods

### Convert\(IReadOnlyCollection\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionCountConverter_1_Convert_System_Collections_Generic_IReadOnlyCollection__0__}

Counts the specified collection.

```csharp
public int Convert(IReadOnlyCollection<T?>? value)
```

#### Parameters

`value` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T?\>?

The collection to count.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of items, or zero when the collection is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

