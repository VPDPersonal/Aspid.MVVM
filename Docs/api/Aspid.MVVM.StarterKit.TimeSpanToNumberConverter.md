---
title: "Class TimeSpanToNumberConverter"
sidebar_label: "TimeSpanToNumberConverter"
description: "Class TimeSpanToNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TimeSpanToNumberConverter {#Aspid_MVVM_StarterKit_TimeSpanToNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Measures a [`TimeSpan`](https://learn.microsoft.com/dotnet/api/system.timespan) as a number.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time/To Number", Name = "Time Span To Number", Tooltip = "Measures a TimeSpan as a number")]
public sealed class TimeSpanToNumberConverter : IConverter<TimeSpan, int>, IConverter<TimeSpan, long>, IConverter<TimeSpan, float>, IConverter<TimeSpan, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TimeSpanToNumberConverter](Aspid.MVVM.StarterKit.TimeSpanToNumberConverter.md)

#### Implements

[IConverter\<TimeSpan, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<TimeSpan, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<TimeSpan, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<TimeSpan, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### TimeSpanToNumberConverter\(\) {#Aspid_MVVM_StarterKit_TimeSpanToNumberConverter__ctor}

```csharp
public TimeSpanToNumberConverter()
```

#### Remarks

Default: measuring in seconds.

### TimeSpanToNumberConverter\(TimeUnit\) {#Aspid_MVVM_StarterKit_TimeSpanToNumberConverter__ctor_Aspid_MVVM_StarterKit_TimeUnit_}

```csharp
public TimeSpanToNumberConverter(TimeUnit unit)
```

#### Parameters

`unit` [TimeUnit](Aspid.MVVM.StarterKit.TimeUnit.md)

Which unit to measure in.

## Methods

### Convert\(TimeSpan\) {#Aspid_MVVM_StarterKit_TimeSpanToNumberConverter_Convert_System_TimeSpan_}

Measures the specified duration.

```csharp
public float Convert(TimeSpan value)
```

#### Parameters

`value` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration to measure.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The measurement; total seconds when the unit is not a declared value.

