---
title: "Class DateTimeToUnixTimestampConverter"
sidebar_label: "DateTimeToUnixTimestampConverter"
description: "Class DateTimeToUnixTimestampConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class DateTimeToUnixTimestampConverter {#Aspid_MVVM_StarterKit_DateTimeToUnixTimestampConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a [`DateTime`](https://learn.microsoft.com/dotnet/api/system.datetime) to a Unix timestamp.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To Number", Name = "Date Time To Unix Timestamp", Tooltip = "Converts a DateTime to a Unix timestamp")]
public sealed class DateTimeToUnixTimestampConverter : ITwoWayConverter<DateTime, long>, IConverter<DateTime, long>, ITwoWayConverter<DateTime, int>, IConverter<DateTime, int>, ITwoWayConverter<DateTime, double>, IConverter<DateTime, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DateTimeToUnixTimestampConverter](Aspid.MVVM.StarterKit.DateTimeToUnixTimestampConverter.md)

#### Implements

[ITwoWayConverter\<DateTime, long\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<DateTime, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<DateTime, int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<DateTime, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<DateTime, double\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<DateTime, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

An [`Unspecified`](https://learn.microsoft.com/dotnet/api/system.datetimekind.unspecified) moment is read as local. A value out of range is clamped and reported, not thrown on.

## Constructors

### DateTimeToUnixTimestampConverter\(\) {#Aspid_MVVM_StarterKit_DateTimeToUnixTimestampConverter__ctor}

```csharp
public DateTimeToUnixTimestampConverter()
```

#### Remarks

Default: producing seconds, converting back to local time.

### DateTimeToUnixTimestampConverter\(bool, bool, ConverterFallback\<DateTime\>?\) {#Aspid_MVVM_StarterKit_DateTimeToUnixTimestampConverter__ctor_System_Boolean_System_Boolean_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback_System_DateTime___}

```csharp
public DateTimeToUnixTimestampConverter(bool milliseconds, bool utc = false, ConverterFallback<DateTime>? convertBackFallback = null)
```

#### Parameters

`milliseconds` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to produce milliseconds rather than seconds. An [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) holds only 25 days of them.

`utc` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to convert a timestamp back to a UTC time rather than a local one.

`convertBackFallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<[DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)\>?

Returned when the timestamp is not finite. When omitted, [`MinValue`](https://learn.microsoft.com/dotnet/api/system.datetime.minvalue).

## Methods

### Convert\(DateTime\) {#Aspid_MVVM_StarterKit_DateTimeToUnixTimestampConverter_Convert_System_DateTime_}

Converts the specified date and time to a timestamp.

```csharp
public long Convert(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The timestamp.

### ConvertBack\(long\) {#Aspid_MVVM_StarterKit_DateTimeToUnixTimestampConverter_ConvertBack_System_Int64_}

Converts a timestamp coming back from the View to a date and time.

```csharp
public DateTime ConvertBack(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The timestamp.

#### Returns

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The date and time, or the nearest bound when the timestamp is out of range.

