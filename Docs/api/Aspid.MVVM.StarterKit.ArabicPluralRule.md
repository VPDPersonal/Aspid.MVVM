---
title: "Class ArabicPluralRule"
sidebar_label: "ArabicPluralRule"
description: "Class ArabicPluralRule — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ArabicPluralRule {#Aspid_MVVM_StarterKit_ArabicPluralRule}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Six words, the widest grammar CLDR declares: Arabic.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Plural Rule", Name = "Arabic", Tooltip = "Six words, the widest grammar CLDR declares: Arabic")]
public sealed class ArabicPluralRule : PluralRule, IConverter<long, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PluralRule](Aspid.MVVM.StarterKit.PluralRule.md) ← 
[ArabicPluralRule](Aspid.MVVM.StarterKit.ArabicPluralRule.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The zero word is one of the six, not an override: this grammar words a count of none apart.

## Constructors

### ArabicPluralRule\(string, string, string, string, string, string?\) {#Aspid_MVVM_StarterKit_ArabicPluralRule__ctor_System_String_System_String_System_String_System_String_System_String_System_String_}

```csharp
public ArabicPluralRule(string one, string two, string few, string many, string other, string? zero = null)
```

#### Parameters

`one` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for exactly one.

`two` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for exactly two.

`few` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for a count whose last two digits are three to ten.

`many` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for a count whose last two digits are 11 to 99.

`other` [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for the round hundreds and the two counts after each.

`zero` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The word for a count of none. When <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, zero takes <code class="paramref">other</code>.

## Methods

### Word\(long\) {#Aspid_MVVM_StarterKit_ArabicPluralRule_Word_System_Int64_}

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

