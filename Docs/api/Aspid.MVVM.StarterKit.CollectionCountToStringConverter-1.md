---
title: "Class CollectionCountToStringConverter<T>"
sidebar_label: "CollectionCountToStringConverter<T>"
description: "Class CollectionCountToStringConverter<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionCountToStringConverter\<T\> {#Aspid_MVVM_StarterKit_CollectionCountToStringConverter_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes how many items a collection holds, in words.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection/To String", Name = "Count To String", Tooltip = "Writes how many items a collection holds, in words")]
public class CollectionCountToStringConverter<T> : IConverter<IEnumerable<T?>?, string>, IConverter<IReadOnlyCollection<T?>?, string>, IConverter
```

#### Type Parameters

`T` 

The type of the items.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionCountToStringConverter\<T\>](Aspid.MVVM.StarterKit.CollectionCountToStringConverter-1.md)

#### Implements

[IConverter\<IEnumerable\<T?\>?, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<IReadOnlyCollection\<T?\>?, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Wording is delegated to [`PluralizeConverter`](Aspid.MVVM.StarterKit.PluralizeConverter.md); a sequence with no count of its own is walked on every push.

## Constructors

### CollectionCountToStringConverter\(\) {#Aspid_MVVM_StarterKit_CollectionCountToStringConverter_1__ctor}

```csharp
public CollectionCountToStringConverter()
```

#### Remarks

Default: writing English item counts.

### CollectionCountToStringConverter\(PluralizeConverter, string?\) {#Aspid_MVVM_StarterKit_CollectionCountToStringConverter_1__ctor_Aspid_MVVM_StarterKit_PluralizeConverter_System_String_}

```csharp
public CollectionCountToStringConverter(PluralizeConverter pluralize, string? zeroText = null)
```

#### Parameters

`pluralize` [PluralizeConverter](Aspid.MVVM.StarterKit.PluralizeConverter.md)

Words the count into the phrase.

`zeroText` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Written for an empty collection instead of <code class="paramref">pluralize</code>. Blank words zero like any count.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">pluralize</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(IReadOnlyCollection\<T?\>?\) {#Aspid_MVVM_StarterKit_CollectionCountToStringConverter_1_Convert_System_Collections_Generic_IReadOnlyCollection__0__}

Writes the size of the specified collection.

```csharp
public string Convert(IReadOnlyCollection<T?>? value)
```

#### Parameters

`value` [IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)\<T?\>?

The collection to describe.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The worded phrase, or the empty text for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or empty collection.

