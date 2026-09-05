---
title: "Class StringToTimeSpanConverter"
sidebar_label: "StringToTimeSpanConverter"
description: "Class StringToTimeSpanConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class StringToTimeSpanConverter {#Aspid_MVVM_StarterKit_StringToTimeSpanConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a duration out of text.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/String/To Time", Name = "Parse Time Span", Tooltip = "Reads a duration out of text")]
public sealed class StringToTimeSpanConverter : ITwoWayConverter<string?, TimeSpan>, IConverter<string?, TimeSpan>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StringToTimeSpanConverter](Aspid.MVVM.StarterKit.StringToTimeSpanConverter.md)

#### Implements

[ITwoWayConverter\<string?, TimeSpan\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<string?, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A bare number is not seconds: [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) reads <code>"90"</code> as ninety days.

## Constructors

### StringToTimeSpanConverter\(\) {#Aspid_MVVM_StarterKit_StringToTimeSpanConverter__ctor}

```csharp
public StringToTimeSpanConverter()
```

#### Remarks

Default: accepting any format.

### StringToTimeSpanConverter\(string, TimeSpan?\) {#Aspid_MVVM_StarterKit_StringToTimeSpanConverter__ctor_System_String_System_Nullable_System_TimeSpan__}

```csharp
public StringToTimeSpanConverter(string format, TimeSpan? fallback = null)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

The exact TimeSpan format, e.g. <code>hh\:mm\:ss</code>. Empty accepts any format the culture understands.

`fallback` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)?

Returned when the text is not a duration. When omitted, [`Zero`](https://learn.microsoft.com/dotnet/api/system.timespan.zero).

## Methods

### Convert\(string?\) {#Aspid_MVVM_StarterKit_StringToTimeSpanConverter_Convert_System_String_}

Reads a duration out of the specified text.

```csharp
public TimeSpan Convert(string? value)
```

#### Parameters

`value` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The text to read.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration, or the fallback when the text is not one.

### ConvertBack\(TimeSpan\) {#Aspid_MVVM_StarterKit_StringToTimeSpanConverter_ConvertBack_System_TimeSpan_}

Writes the specified duration as text.

```csharp
public string ConvertBack(TimeSpan value)
```

#### Parameters

`value` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration to write.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The duration in the authored format, or in the culture's short form when none is authored or it is unusable.

