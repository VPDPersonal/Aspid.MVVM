---
title: "Class DateTimeOffsetFormatConverter"
sidebar_label: "DateTimeOffsetFormatConverter"
description: "Class DateTimeOffsetFormatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeOffsetFormatConverter {#Aspid_MVVM_StarterKit_DateTimeOffsetFormatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Formats a [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To String", Name = "Date Time Offset Format", Tooltip = "Formats a DateTimeOffset")]
public sealed class DateTimeOffsetFormatConverter : IConverter<DateTimeOffset, string>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DateTimeOffsetFormatConverter](Aspid.MVVM.StarterKit.DateTimeOffsetFormatConverter.md)

#### Implements

[IConverter\<DateTimeOffset, string\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### DateTimeOffsetFormatConverter\(\) {#Aspid_MVVM_StarterKit_DateTimeOffsetFormatConverter__ctor}

```csharp
public DateTimeOffsetFormatConverter()
```

#### Remarks

Default: with the general format.

### DateTimeOffsetFormatConverter\(string, OffsetSource, CultureInfoMode\) {#Aspid_MVVM_StarterKit_DateTimeOffsetFormatConverter__ctor_System_String_Aspid_MVVM_StarterKit_OffsetSource_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public DateTimeOffsetFormatConverter(string format, OffsetSource offsetSource = OffsetSource.AsGiven, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) format string.

`offsetSource` [OffsetSource](Aspid.MVVM.StarterKit.OffsetSource.md)

The offset the moment is shown at. [`OffsetSource.Override`](Aspid.MVVM.StarterKit.OffsetSource.md) here means zero.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the date is formatted with.

### DateTimeOffsetFormatConverter\(string, TimeSpan, CultureInfoMode\) {#Aspid_MVVM_StarterKit_DateTimeOffsetFormatConverter__ctor_System_String_System_TimeSpan_Aspid_MVVM_StarterKit_CultureInfoMode_}

```csharp
public DateTimeOffsetFormatConverter(string format, TimeSpan offsetOverride, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
```

#### Parameters

`format` [string](https://learn.microsoft.com/dotnet/api/system.string)

A [`DateTimeOffset`](https://learn.microsoft.com/dotnet/api/system.datetimeoffset) format string.

`offsetOverride` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The offset to show the moment at, within ±14 hours.

`culture` [CultureInfoMode](Aspid.MVVM.StarterKit.CultureInfoMode.md)

The culture the date is formatted with.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">offsetOverride</code> is past ±14 hours.

## Methods

### Convert\(DateTimeOffset\) {#Aspid_MVVM_StarterKit_DateTimeOffsetFormatConverter_Convert_System_DateTimeOffset_}

Formats the specified moment.

```csharp
public string Convert(DateTimeOffset value)
```

#### Parameters

`value` [DateTimeOffset](https://learn.microsoft.com/dotnet/api/system.datetimeoffset)

The moment to format.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The formatted moment, or the default rendering when the format is unusable. An undeclared source reports an error and keeps the offset.

