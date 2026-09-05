---
title: "Class StringToBoolConverter"
sidebar_label: "StringToBoolConverter"
description: "Class StringToBoolConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToBoolConverter {#Aspid_MVVM_StarterKit_StringToBoolConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a boolean out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Bool", Name = "Parse", Tooltip = "Reads a boolean out of text")]
public sealed class StringToBoolConverter : ITwoWayConverter<string?, bool>, IConverter<string?, bool>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToBoolConverter](Aspid.MVVM.StarterKit.StringToBoolConverter.md)

#### Implements

[ITwoWayConverter\<string?, bool\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, bool\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### StringToBoolConverter\(\) {#Aspid_MVVM_StarterKit_StringToBoolConverter__ctor}

```csharp
public StringToBoolConverter()
```

#### Remarks

Default: with the usual spellings.

### StringToBoolConverter\(string\[\]?, string\[\]?, bool\) {#Aspid_MVVM_StarterKit_StringToBoolConverter__ctor_System_String___System_String___System_Boolean_}

```csharp
public StringToBoolConverter(string[]? trueTokens, string[]? falseTokens = null, bool fallback = false)
```

#### Parameters

`trueTokens` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

The spellings read as <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>; empty keeps the usual ones. The first is written back.

`falseTokens` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

The spellings read as <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>; empty makes unmatched text take the fallback quietly.

`fallback` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Returned when the text matches nothing.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToBoolConverter_Convert_System_String_}

Reads a boolean out of the specified text.

```csharp
public bool Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the text matches an accepted spelling, or the fallback when it matches none.

### ConvertBack\(bool\) {#Aspid_MVVM_StarterKit_StringToBoolConverter_ConvertBack_System_Boolean_}

Writes the specified boolean as text.

```csharp
public string ConvertBack(bool value)
```

#### Parameters

`value` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The boolean to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The first spelling authored for it, or the plain word when none is authored.

