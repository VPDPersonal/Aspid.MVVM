---
title: "Class CzechPluralRule"
sidebar_label: "CzechPluralRule"
description: "Class CzechPluralRule — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CzechPluralRule {#Aspid_MVVM_StarterKit_CzechPluralRule}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

A word for one, a word for two to four, a word for the rest: Czech, Slovak.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Plural Rule", Name = "Czech", Tooltip = "A word for one, a word for two to four, a word for the rest: Czech, Slovak")]
public sealed class CzechPluralRule : PluralRule, IConverter<long, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PluralRule](Aspid.MVVM.StarterKit.PluralRule.md) ← 
[CzechPluralRule](Aspid.MVVM.StarterKit.CzechPluralRule.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The count itself decides, not its last digit: 22 takes the same word as 5.

## Constructors

### CzechPluralRule\(string, string, string, string?\) {#Aspid_MVVM_StarterKit_CzechPluralRule__ctor_System_String_System_String_System_String_System_String_}

```csharp
public CzechPluralRule(string one, string few, string other, string? zero = null)
```

#### Parameters

`one` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for exactly one.

`few` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for two, three and four.

`other` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for every other count, five and up included.

`zero` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Written for a count of none, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to word it like any other count.

## Methods

### Word\(long\) {#Aspid_MVVM_StarterKit_CzechPluralRule_Word_System_Int64_}

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

