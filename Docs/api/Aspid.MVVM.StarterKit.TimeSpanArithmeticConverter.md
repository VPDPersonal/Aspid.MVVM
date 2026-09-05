---
title: "Class TimeSpanArithmeticConverter"
sidebar_label: "TimeSpanArithmeticConverter"
description: "Class TimeSpanArithmeticConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TimeSpanArithmeticConverter {#Aspid_MVVM_StarterKit_TimeSpanArithmeticConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies arithmetic to a duration.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Time", Name = "Time Span Arithmetic", Tooltip = "Applies arithmetic to a duration")]
public sealed class TimeSpanArithmeticConverter : IConverter<TimeSpan, TimeSpan>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TimeSpanArithmeticConverter](Aspid.MVVM.StarterKit.TimeSpanArithmeticConverter.md)

#### Implements

[IConverter\<TimeSpan, TimeSpan\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The operand is seconds, except a factor for [`NumberOperation.Multiply`](Aspid.MVVM.StarterKit.NumberOperation.md) and [`NumberOperation.Divide`](Aspid.MVVM.StarterKit.NumberOperation.md)
and an exponent for [`NumberOperation.Power`](Aspid.MVVM.StarterKit.NumberOperation.md). Power and ReverseDivide treat the duration as seconds.

## Constructors

### TimeSpanArithmeticConverter\(\) {#Aspid_MVVM_StarterKit_TimeSpanArithmeticConverter__ctor}

```csharp
public TimeSpanArithmeticConverter()
```

#### Remarks

Default: adding zero seconds, which leaves the duration unchanged.

### TimeSpanArithmeticConverter\(NumberOperation, float, ConverterFallback\<TimeSpan\>?\) {#Aspid_MVVM_StarterKit_TimeSpanArithmeticConverter__ctor_Aspid_MVVM_StarterKit_NumberOperation_System_Single_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback_System_TimeSpan___}

```csharp
public TimeSpanArithmeticConverter(NumberOperation operation, float operand, ConverterFallback<TimeSpan>? fallback = null)
```

#### Parameters

`operation` [NumberOperation](Aspid.MVVM.StarterKit.NumberOperation.md)

The arithmetic applied to the duration.

`operand` [float](https://learn.microsoft.com/dotnet/api/system.single)

Seconds, except a factor for Multiply and Divide and an exponent for Power.

`fallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<[TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)\>?

Returned when the operand is not finite, the operation is undeclared, divides by zero or has no real result.
When omitted, returns the duration unchanged.

## Methods

### Convert\(TimeSpan\) {#Aspid_MVVM_StarterKit_TimeSpanArithmeticConverter_Convert_System_TimeSpan_}

Applies the configured arithmetic to the specified duration.

```csharp
public TimeSpan Convert(TimeSpan value)
```

#### Parameters

`value` [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The duration to transform.

#### Returns

 [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan)

The result, saturated on overflow, or the fallback when the arithmetic cannot be done.

