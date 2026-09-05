---
title: "Class SingleFormPluralRule"
sidebar_label: "SingleFormPluralRule"
description: "Class SingleFormPluralRule — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SingleFormPluralRule {#Aspid_MVVM_StarterKit_SingleFormPluralRule}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

One word for every count: Chinese, Japanese, Korean, Thai, Vietnamese, Turkish, Indonesian.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Plural Rule", Name = "Single Form", Tooltip = "One word for every count: Chinese, Japanese, Korean, Thai, Vietnamese, Turkish")]
public sealed class SingleFormPluralRule : PluralRule, IConverter<long, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PluralRule](Aspid.MVVM.StarterKit.PluralRule.md) ← 
[SingleFormPluralRule](Aspid.MVVM.StarterKit.SingleFormPluralRule.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### SingleFormPluralRule\(string, string?\) {#Aspid_MVVM_StarterKit_SingleFormPluralRule__ctor_System_String_System_String_}

```csharp
public SingleFormPluralRule(string word, string? zero = null)
```

#### Parameters

`word` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for every count.

`zero` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Written for a count of none, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to word it like any other count.

## Methods

### Word\(long\) {#Aspid_MVVM_StarterKit_SingleFormPluralRule_Word_System_Int64_}

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

