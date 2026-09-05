---
title: "Class LocalizedStringConverter"
sidebar_label: "LocalizedStringConverter"
description: "Class LocalizedStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LocalizedStringConverter {#Aspid_MVVM_StarterKit_LocalizedStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Looks a key up in a localization table.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Localization", Name = "Localized String", Tooltip = "Looks a key up in a localization table")]
public sealed class LocalizedStringConverter : IConverter<string?, string?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LocalizedStringConverter](Aspid.MVVM.StarterKit.LocalizedStringConverter.md)

#### Implements

[IConverter\<string?, string?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Only compiled when <code>com.unity.localization</code> is installed.

## Constructors

### LocalizedStringConverter\(\) {#Aspid_MVVM_StarterKit_LocalizedStringConverter__ctor}

```csharp
public LocalizedStringConverter()
```

#### Remarks

Default: showing the key itself when it has no entry.

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_LocalizedStringConverter_Convert_System_String_}

Looks the specified key up.

```csharp
public string? Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The key to look up.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

The localized text, the key itself, or the missing format, whichever the settings call
for. A blank key, spaces included, comes back unchanged. A lookup with no table assigned
is reported as an error.

