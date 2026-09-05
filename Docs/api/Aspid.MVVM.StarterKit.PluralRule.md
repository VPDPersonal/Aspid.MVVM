---
title: "Class PluralRule"
sidebar_label: "PluralRule"
description: "Class PluralRule — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PluralRule {#Aspid_MVVM_StarterKit_PluralRule}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Words a count in one language: the grammar and the words it picks between.

```csharp
[Serializable]
public abstract class PluralRule : IConverter<long, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PluralRule](Aspid.MVVM.StarterKit.PluralRule.md)

#### Derived

[ArabicPluralRule](Aspid.MVVM.StarterKit.ArabicPluralRule.md), 
[CzechPluralRule](Aspid.MVVM.StarterKit.CzechPluralRule.md), 
[EastSlavicPluralRule](Aspid.MVVM.StarterKit.EastSlavicPluralRule.md), 
[EnglishPluralRule](Aspid.MVVM.StarterKit.EnglishPluralRule.md), 
[FrenchPluralRule](Aspid.MVVM.StarterKit.FrenchPluralRule.md), 
[PolishPluralRule](Aspid.MVVM.StarterKit.PolishPluralRule.md), 
[SingleFormPluralRule](Aspid.MVVM.StarterKit.SingleFormPluralRule.md)

#### Implements

[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A subclass declares only the words its grammar uses.

## Constructors

### PluralRule\(string?\) {#Aspid_MVVM_StarterKit_PluralRule__ctor_System_String_}

```csharp
protected PluralRule(string? zero = null)
```

#### Parameters

`zero` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Written for a count of none, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to word it like any other count.

## Methods

### Convert\(long\) {#Aspid_MVVM_StarterKit_PluralRule_Convert_System_Int64_}

Words the specified count.

```csharp
public string Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The count, as a magnitude.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The zero word for a count of none when authored, otherwise the grammar's word. A blank word is reported.

### Word\(long\) {#Aspid_MVVM_StarterKit_PluralRule_Word_System_Int64_}

Picks the word the grammar calls for.

```csharp
protected abstract string Word(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The count, as a magnitude.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The word for the count, or an empty string when it is not authored.

