---
title: "Class StringFormatConverter"
sidebar_label: "StringFormatConverter"
description: "Class StringFormatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringFormatConverter {#Aspid_MVVM_StarterKit_StringFormatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ValueToStringConverter<T>`](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md) for strings, with optional handling of empty values.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Format", Tooltip = "Formats a string into a template, skipping empty input by default")]
public sealed class StringFormatConverter : ValueToStringConverter<string>, IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ValueToStringConverter\<string\>](Aspid.MVVM.StarterKit.ValueToStringConverter-1.md) ← 
[StringFormatConverter](Aspid.MVVM.StarterKit.StringFormatConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

By default, a blank input passes through and <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> stays <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Constructors

### StringFormatConverter\(\) {#Aspid_MVVM_StarterKit_StringFormatConverter__ctor}

```csharp
public StringFormatConverter()
```

#### Remarks

Default: no format, passing the string through.

### StringFormatConverter\(string, bool, CultureInfoMode\) {#Aspid_MVVM_StarterKit_StringFormatConverter__ctor_System_String_System_Boolean_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public StringFormatConverter(string format, bool formatEmptyValues = false, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

Composite format string such as <code>"HP: \{0\}"</code>.

`formatEmptyValues` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, applies the format to a blank value too, reading <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> as an empty string.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the format is applied with.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringFormatConverter_Convert_System_String_}

Converts the specified string, reading <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> as an empty one when empty values are formatted.

```csharp
public override string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The value to convert.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The formatted string, or the value unchanged when the format does not apply.

### Format\(string, string\) {#Aspid_MVVM_StarterKit_StringFormatConverter_Format_System_String_System_String_}

Applies the format unless the value is blank and blank values are not being formatted.

```csharp
protected override string Format(string value, string format)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)

The non-null value to format.

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

The composite format string, never blank.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted string, or the value unchanged.

