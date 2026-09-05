---
title: "Class UnixTimestampToDateTimeConverter"
sidebar_label: "UnixTimestampToDateTimeConverter"
description: "Class UnixTimestampToDateTimeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class UnixTimestampToDateTimeConverter {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a Unix timestamp to a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Time", Name = "Unix Timestamp To Date Time", Tooltip = "Converts a Unix timestamp to a DateTime")]
public sealed class UnixTimestampToDateTimeConverter : ITwoWayConverter<long, DateTime>, IConverter<long, DateTime>, ITwoWayConverter<int, DateTime>, IConverter<int, DateTime>, ITwoWayConverter<double, DateTime>, IConverter<double, DateTime>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UnixTimestampToDateTimeConverter](Aspid.MVVM.StarterKit.UnixTimestampToDateTimeConverter.md)

#### Implements

[ITwoWayConverter\<long, DateTime\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<long, DateTime\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<int, DateTime\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<int, DateTime\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, DateTime\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, DateTime\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

An [`Unspecified`](https://learn.microsoft.com/dotnet/api/system.datetimekind.unspecified) moment is read as local. A value out of range is clamped and reported, not thrown on.

## Constructors

### UnixTimestampToDateTimeConverter\(\) {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter__ctor}

```csharp
public UnixTimestampToDateTimeConverter()
```

#### Remarks

Default: reading local seconds.

### UnixTimestampToDateTimeConverter\(bool, bool\) {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter__ctor_System_Boolean_System_Boolean_}

```csharp
public UnixTimestampToDateTimeConverter(bool milliseconds, bool utc = false)
```

#### Parameters

`milliseconds` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether the timestamp is in milliseconds. An [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) holds only 25 days of them.

`utc` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to produce a UTC time.

## Methods

### Convert\(long\) {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter_Convert_System_Int64_}

Converts the specified timestamp to a date and time.

```csharp
public DateTime Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The timestamp.

#### Returns

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time, or the nearest bound when the timestamp is out of range.

### Convert\(int\) {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter_Convert_System_Int32_}

Converts the specified timestamp to a date and time.

```csharp
public DateTime Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The timestamp.

#### Returns

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time. A millisecond timestamp in an [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) is reported.

### Convert\(double\) {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter_Convert_System_Double_}

Converts the specified timestamp to a date and time.

```csharp
public DateTime Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The timestamp, carrying a fraction of a second.

#### Returns

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time; the Unix epoch for a non-finite value, the nearest bound for one out of range.

### ConvertBack\(DateTime\) {#Aspid_MVVM_StarterKit_UnixTimestampToDateTimeConverter_ConvertBack_System_DateTime_}

Converts a date and time back to a timestamp.

```csharp
public long ConvertBack(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time. An [`Unspecified`](https://learn.microsoft.com/dotnet/api/system.datetimekind.unspecified) one is read as local.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The timestamp.

