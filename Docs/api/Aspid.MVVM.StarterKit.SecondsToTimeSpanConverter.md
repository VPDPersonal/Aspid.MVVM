---
title: "Class SecondsToTimeSpanConverter"
sidebar_label: "SecondsToTimeSpanConverter"
description: "Class SecondsToTimeSpanConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SecondsToTimeSpanConverter {#Aspid_MVVM_StarterKit_SecondsToTimeSpanConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a number of seconds to a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Time", Name = "Seconds To Time Span", Tooltip = "Converts a number of seconds to a TimeSpan")]
public sealed class SecondsToTimeSpanConverter : ITwoWayConverter<float, TimeSpan>, IConverter<float, TimeSpan>, ITwoWayConverter<double, TimeSpan>, IConverter<double, TimeSpan>, ITwoWayConverter<int, TimeSpan>, IConverter<int, TimeSpan>, ITwoWayConverter<long, TimeSpan>, IConverter<long, TimeSpan>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SecondsToTimeSpanConverter](Aspid.MVVM.StarterKit.SecondsToTimeSpanConverter.md)

#### Implements

[ITwoWayConverter\<float, TimeSpan\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, TimeSpan\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<int, TimeSpan\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<int, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<long, TimeSpan\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<long, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A value [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) cannot hold is reported, not thrown on. Integers drop the fraction on the way back.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_SecondsToTimeSpanConverter_Convert_System_Single_}

Converts the specified seconds to a duration.

```csharp
public TimeSpan Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number of seconds.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration; [`Zero`](https://learn.microsoft.com/dotnet/api/system.timespan.zero) for a non-finite value, the nearest bound for one out of range.

### Convert\(double\) {#Aspid_MVVM_StarterKit_SecondsToTimeSpanConverter_Convert_System_Double_}

Converts the specified seconds to a duration.

```csharp
public TimeSpan Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number of seconds.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration; [`Zero`](https://learn.microsoft.com/dotnet/api/system.timespan.zero) for a non-finite value, the nearest bound for one out of range.

### Convert\(int\) {#Aspid_MVVM_StarterKit_SecondsToTimeSpanConverter_Convert_System_Int32_}

Converts the specified seconds to a duration.

```csharp
public TimeSpan Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of seconds.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration. No [`Int32`](https://learn.microsoft.com/dotnet/api/system.int32) count of seconds is out of range.

### Convert\(long\) {#Aspid_MVVM_StarterKit_SecondsToTimeSpanConverter_Convert_System_Int64_}

Converts the specified seconds to a duration.

```csharp
public TimeSpan Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number of seconds.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration, or the nearest bound for a count out of range.

### ConvertBack\(TimeSpan\) {#Aspid_MVVM_StarterKit_SecondsToTimeSpanConverter_ConvertBack_System_TimeSpan_}

Converts a duration back to seconds.

```csharp
public float ConvertBack(TimeSpan value)
```

#### Parameters

`value` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The number of seconds.

