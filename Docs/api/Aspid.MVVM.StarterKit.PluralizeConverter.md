---
title: "Class PluralizeConverter"
sidebar_label: "PluralizeConverter"
description: "Class PluralizeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PluralizeConverter {#Aspid_MVVM_StarterKit_PluralizeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Picks the right word form for a count.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To String", Name = "Pluralize", Tooltip = "Picks the right word form for a count")]
public sealed class PluralizeConverter : IConverter<int, string>, IConverter<long, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PluralizeConverter](Aspid.MVVM.StarterKit.PluralizeConverter.md)

#### Implements

[IConverter\<int, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The grammar and its words are a [`PluralRule`](Aspid.MVVM.StarterKit.PluralRule.md); the converter holds only the phrase.

## Constructors

### PluralizeConverter\(PluralRule, string?\) {#Aspid_MVVM_StarterKit_PluralizeConverter__ctor_Aspid_MVVM_StarterKit_PluralRule_System_String_}

```csharp
public PluralizeConverter(PluralRule rule, string? format = null)
```

#### Parameters

`rule` [PluralRule](Aspid.MVVM.StarterKit.PluralRule.md)

The grammar and the words it picks between.

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)?

A composite format: <code>\{0\}</code> is the count, <code>\{1\}</code> the word. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> uses <code>"\{0\} \{1\}"</code>;
blank or invalid writes the word alone.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">rule</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(int\) {#Aspid_MVVM_StarterKit_PluralizeConverter_Convert_System_Int32_}

Formats the specified count with the word its grammar calls for.

```csharp
public string Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The count, which keeps its sign in the phrase while the grammar reads its magnitude.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted text. A missing rule leaves the word out, an invalid format the count; both are reported.

