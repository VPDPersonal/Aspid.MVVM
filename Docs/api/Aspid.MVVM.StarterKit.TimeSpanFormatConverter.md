---
title: "Class TimeSpanFormatConverter"
sidebar_label: "TimeSpanFormatConverter"
description: "Class TimeSpanFormatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TimeSpanFormatConverter {#Aspid_MVVM_StarterKit_TimeSpanFormatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) with a real [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) format string.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To String", Name = "Time Span Format", Tooltip = "Formats a TimeSpan with a real TimeSpan format string")]
public sealed class TimeSpanFormatConverter : IConverter<TimeSpan, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TimeSpanFormatConverter](Aspid.MVVM.StarterKit.TimeSpanFormatConverter.md)

#### Implements

[IConverter\<TimeSpan, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The pattern is taken directly, the way [`ToString`](https://learn.microsoft.com/dotnet/api/system.timespan.tostring) takes it.

## Constructors

### TimeSpanFormatConverter\(\) {#Aspid_MVVM_StarterKit_TimeSpanFormatConverter__ctor}

```csharp
public TimeSpanFormatConverter()
```

#### Remarks

Default: writing mm:ss.

### TimeSpanFormatConverter\(string, CultureInfoMode\) {#Aspid_MVVM_StarterKit_TimeSpanFormatConverter__ctor_System_String_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public TimeSpanFormatConverter(string format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) format string.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the duration is formatted with.

## Methods

### Convert\(TimeSpan\) {#Aspid_MVVM_StarterKit_TimeSpanFormatConverter_Convert_System_TimeSpan_}

Formats the specified duration.

```csharp
public string Convert(TimeSpan value)
```

#### Parameters

`value` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted duration, or the default rendering when the format is unusable.

