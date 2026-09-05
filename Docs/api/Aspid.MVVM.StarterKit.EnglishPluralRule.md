---
title: "Class EnglishPluralRule"
sidebar_label: "EnglishPluralRule"
description: "Class EnglishPluralRule — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EnglishPluralRule {#Aspid_MVVM_StarterKit_EnglishPluralRule}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

One word for one, another for everything else: English, German, Dutch, Spanish, Italian,
Swedish, Greek.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Plural Rule", Name = "English", Tooltip = "One word for one, another for everything else: English, German, Spanish, Italian")]
public sealed class EnglishPluralRule : PluralRule, IConverter<long, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PluralRule](Aspid.MVVM.StarterKit.PluralRule.md) ← 
[EnglishPluralRule](Aspid.MVVM.StarterKit.EnglishPluralRule.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### EnglishPluralRule\(string, string, string?\) {#Aspid_MVVM_StarterKit_EnglishPluralRule__ctor_System_String_System_String_System_String_}

```csharp
public EnglishPluralRule(string one, string other, string? zero = null)
```

#### Parameters

`one` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for one item.

`other` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for every other count, zero included.

`zero` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Written for a count of none, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to word it like any other count.

## Methods

### Word\(long\) {#Aspid_MVVM_StarterKit_EnglishPluralRule_Word_System_Int64_}

Picks the word the grammar calls for.

```csharp
protected override string Word(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The count, as a magnitude.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for the count, or an empty string when it is not authored.

