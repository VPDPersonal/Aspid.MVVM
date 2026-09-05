---
title: "Class ReplaceStringConverter"
sidebar_label: "ReplaceStringConverter"
description: "Class ReplaceStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ReplaceStringConverter {#Aspid_MVVM_StarterKit_ReplaceStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Replaces occurrences of one piece of text with another.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Replace", Tooltip = "Replaces occurrences of one piece of text with another")]
public sealed class ReplaceStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ReplaceStringConverter](Aspid.MVVM.StarterKit.ReplaceStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ReplaceStringConverter\(\) {#Aspid_MVVM_StarterKit_ReplaceStringConverter__ctor}

```csharp
public ReplaceStringConverter()
```

#### Remarks

Default: with nothing to look for, which leaves the string as it is.

### ReplaceStringConverter\(string, string, bool\) {#Aspid_MVVM_StarterKit_ReplaceStringConverter__ctor_System_String_System_String_System_Boolean_}

```csharp
public ReplaceStringConverter(string search, string replacement, bool ignoreCase = false)
```

#### Parameters

`search` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text to look for. When empty, the string passes through.

`replacement` [string](https://learn.microsoft.com/dotnet/api/system.string)

The text put in its place.

`ignoreCase` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, matches without regard to case.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_ReplaceStringConverter_Convert_System_String_}

Replaces every occurrence in the specified string.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to search.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string with replacements made.

