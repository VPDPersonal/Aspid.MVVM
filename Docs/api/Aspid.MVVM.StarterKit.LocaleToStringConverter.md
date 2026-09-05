---
title: "Class LocaleToStringConverter"
sidebar_label: "LocaleToStringConverter"
description: "Class LocaleToStringConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LocaleToStringConverter {#Aspid_MVVM_StarterKit_LocaleToStringConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes the name of a locale.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Localization", Name = "Locale To String", Tooltip = "Writes the name of a locale")]
public sealed class LocaleToStringConverter : IConverter<Locale?, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LocaleToStringConverter](Aspid.MVVM.StarterKit.LocaleToStringConverter.md)

#### Implements

[IConverter\<Locale?, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### LocaleToStringConverter\(\) {#Aspid_MVVM_StarterKit_LocaleToStringConverter__ctor}

```csharp
public LocaleToStringConverter()
```

#### Remarks

Default: the locale's own name for itself, and an empty string for no locale.

### LocaleToStringConverter\(bool, string?\) {#Aspid_MVVM_StarterKit_LocaleToStringConverter__ctor_System_Boolean_System_String_}

```csharp
public LocaleToStringConverter(bool nativeName, string? fallback = null)
```

#### Parameters

`nativeName` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to use the locale's own name for itself rather than its English name. A locale with
no culture behind it is named by its own [`LocaleName`](https://docs.unity3d.com/ScriptReference/Localization-Locale-LocaleName.html) either way.

`fallback` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Shown when there is no locale, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to show nothing.

## Methods

### Convert\(Locale?\) {#Aspid_MVVM_StarterKit_LocaleToStringConverter_Convert_UnityEngine_Localization_Locale_}

Writes the name of the specified locale.

```csharp
public string Convert(Locale? value)
```

#### Parameters

`value` Locale?

The locale to name.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

Its native or English name; its [`LocaleName`](https://docs.unity3d.com/ScriptReference/Localization-Locale-LocaleName.html) when no culture stands
behind it; or the fallback when the locale is missing or destroyed.

