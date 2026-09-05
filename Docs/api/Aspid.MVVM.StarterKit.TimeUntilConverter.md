---
title: "Class TimeUntilConverter"
sidebar_label: "TimeUntilConverter"
description: "Class TimeUntilConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TimeUntilConverter {#Aspid_MVVM_StarterKit_TimeUntilConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Measures how long there is until a moment.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time", Name = "Time Until", Tooltip = "Measures how long there is until a moment")]
public sealed class TimeUntilConverter : IConverter<DateTime, TimeSpan>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TimeUntilConverter](Aspid.MVVM.StarterKit.TimeUntilConverter.md)

#### Implements

[IConverter\<DateTime, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The result is only as fresh as the last push; the ViewModel still has to tick.

## Constructors

### TimeUntilConverter\(\) {#Aspid_MVVM_StarterKit_TimeUntilConverter__ctor}

```csharp
public TimeUntilConverter()
```

#### Remarks

Default: measuring against local time.

### TimeUntilConverter\(bool, bool\) {#Aspid_MVVM_StarterKit_TimeUntilConverter__ctor_System_Boolean_System_Boolean_}

```csharp
public TimeUntilConverter(bool useUtcNow, bool clampToZero = true)
```

#### Parameters

`useUtcNow` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether an [`Unspecified`](https://learn.microsoft.com/dotnet/api/system.datetimekind.unspecified) moment is measured against UTC rather than local time.

`clampToZero` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, reports a moment already past as zero.

## Methods

### Convert\(DateTime\) {#Aspid_MVVM_StarterKit_TimeUntilConverter_Convert_System_DateTime_}

Measures how long there is until the specified moment.

```csharp
public TimeSpan Convert(DateTime value)
```

#### Parameters

`value` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

The moment to measure to.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration remaining, negative once the moment has passed unless clamped.

