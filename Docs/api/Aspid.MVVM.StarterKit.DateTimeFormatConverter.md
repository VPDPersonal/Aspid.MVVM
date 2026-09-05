---
title: "Class DateTimeFormatConverter"
sidebar_label: "DateTimeFormatConverter"
description: "Class DateTimeFormatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeFormatConverter {#Aspid_MVVM_StarterKit_DateTimeFormatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To String", Name = "Date Time Format", Tooltip = "Formats a DateTime")]
public sealed class DateTimeFormatConverter : IConverter<DateTime, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DateTimeFormatConverter](Aspid.MVVM.StarterKit.DateTimeFormatConverter.md)

#### Implements

[IConverter\<DateTime, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### DateTimeFormatConverter\(\) {#Aspid_MVVM_StarterKit_DateTimeFormatConverter__ctor}

```csharp
public DateTimeFormatConverter()
```

#### Remarks

Default: with the general format.

### DateTimeFormatConverter\(string, bool, CultureInfoMode\) {#Aspid_MVVM_StarterKit_DateTimeFormatConverter__ctor_System_String_System_Boolean_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public DateTimeFormatConverter(string format, bool toLocalTime = false, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) format string.

`toLocalTime` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to convert to local time before formatting.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the date is formatted with.

## Methods

### Convert\(DateTime\) {#Aspid_MVVM_StarterKit_DateTimeFormatConverter_Convert_System_DateTime_}

Formats the specified date and time.

```csharp
public string Convert(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted date, or the default rendering when the format is unusable.

