---
title: "Class DefaultStringConverter"
sidebar_label: "DefaultStringConverter"
description: "Class DefaultStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DefaultStringConverter {#Aspid_MVVM_StarterKit_DefaultStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Substitutes a placeholder for a blank string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String", Name = "Default", Tooltip = "Substitutes a placeholder for a blank string")]
public sealed class DefaultStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DefaultStringConverter](Aspid.MVVM.StarterKit.DefaultStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### DefaultStringConverter\(\) {#Aspid_MVVM_StarterKit_DefaultStringConverter__ctor}

```csharp
public DefaultStringConverter()
```

#### Remarks

Default: with an em dash.

### DefaultStringConverter\(string?\) {#Aspid_MVVM_StarterKit_DefaultStringConverter__ctor_System_String_}

```csharp
public DefaultStringConverter(string? fallback)
```

#### Parameters

`fallback` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Shown when the bound string is blank. A string of spaces counts as blank.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_DefaultStringConverter_Convert_System_String_}

Returns the specified string, or the placeholder when it is blank.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string to check.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The string, or the placeholder when the string is blank, spaces included.

