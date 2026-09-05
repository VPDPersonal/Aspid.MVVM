---
title: "Class DictionaryLookupConverter<TKey, TValue>"
sidebar_label: "DictionaryLookupConverter<TKey, TValue>"
description: "Class DictionaryLookupConverter<TKey, TValue> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DictionaryLookupConverter\<TKey, TValue\> {#Aspid_MVVM_StarterKit_DictionaryLookupConverter_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Looks a key up in an authored table.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Dictionary Lookup", Tooltip = "Looks a key up in an authored table")]
public class DictionaryLookupConverter<TKey, TValue> : IConverter<TKey, TValue?>, IConverter
```

#### Type Parameters

`TKey` 

The type of the key being looked up.

`TValue` 

The type of the value the key names.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DictionaryLookupConverter\<TKey, TValue\>](Aspid.MVVM.StarterKit.DictionaryLookupConverter-2.md)

#### Implements

[IConverter\<TKey, TValue?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Keys are matched with the type's own equality: for a string, ordinal and case-sensitive.
A duplicate key is reported and answered by its first row.

## Constructors

### DictionaryLookupConverter\(\) {#Aspid_MVVM_StarterKit_DictionaryLookupConverter_2__ctor}

```csharp
public DictionaryLookupConverter()
```

#### Remarks

Default: an empty table, answering every key with the type default.

### DictionaryLookupConverter\(LookupEntry\<TKey, TValue?\>\[\]?, TValue?\) {#Aspid_MVVM_StarterKit_DictionaryLookupConverter_2__ctor_Aspid_MVVM_StarterKit_LookupEntry__0__1_____1_}

```csharp
public DictionaryLookupConverter(LookupEntry<TKey, TValue?>[]? map, TValue? fallback = default)
```

#### Parameters

`map` [LookupEntry](Aspid.MVVM.StarterKit.LookupEntry-2.md)\<TKey, TValue?\>\[\]?

The value for each key. A duplicate key is reported, its first row wins. The array is copied.

`fallback` TValue?

Returned for a key <code class="paramref">map</code> does not list.

## Methods

### Convert\(TKey\) {#Aspid_MVVM_StarterKit_DictionaryLookupConverter_2_Convert__0_}

Looks the specified key up in the table.

```csharp
public TValue? Convert(TKey value)
```

#### Parameters

`value` TKey

The key to look up.

#### Returns

 TValue?

The value for that key, or the fallback when it is not listed.

