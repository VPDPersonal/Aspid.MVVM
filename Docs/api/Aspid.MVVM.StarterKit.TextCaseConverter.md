---
title: "Class TextCaseConverter"
sidebar_label: "TextCaseConverter"
description: "Class TextCaseConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TextCaseConverter {#Aspid_MVVM_StarterKit_TextCaseConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Changes the casing of a string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Text Case", Tooltip = "Changes the casing of a string")]
public sealed class TextCaseConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TextCaseConverter](Aspid.MVVM.StarterKit.TextCaseConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### TextCaseConverter\(\) {#Aspid_MVVM_StarterKit_TextCaseConverter__ctor}

```csharp
public TextCaseConverter()
```

#### Remarks

Default: upper-casing.

### TextCaseConverter\(TextCase, CultureInfoMode\) {#Aspid_MVVM_StarterKit_TextCaseConverter__ctor_Aspid_MVVM_StarterKit_TextCase_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public TextCaseConverter(TextCase textCase, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`textCase` [TextCase](Aspid.MVVM.StarterKit.TextCase.md)

Which casing to apply.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture whose casing rules apply. Turkish and Azeri differ from the rest.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_TextCaseConverter_Convert_System_String_}

Applies the configured casing.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to recase.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The recased string. An undeclared casing reports an error and returns the value unchanged.

