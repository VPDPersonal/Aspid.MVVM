---
title: "Class RoundNumberConverter"
sidebar_label: "RoundNumberConverter"
description: "Class RoundNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RoundNumberConverter {#Aspid_MVVM_StarterKit_RoundNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Rounds a number, in a way the caller chooses.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Round", Tooltip = "Rounds a number, in a way the caller chooses")]
public sealed class RoundNumberConverter : NumberConverter, IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[RoundNumberConverter](Aspid.MVVM.StarterKit.RoundNumberConverter.md)

#### Implements

[IConverter\<int, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The decimal-place count is ignored on the way to an int or long.

## Constructors

### RoundNumberConverter\(\) {#Aspid_MVVM_StarterKit_RoundNumberConverter__ctor}

```csharp
public RoundNumberConverter()
```

#### Remarks

Default: rounding to the nearest whole number.

### RoundNumberConverter\(RoundMode, int, MidpointRounding\) {#Aspid_MVVM_StarterKit_RoundNumberConverter__ctor_Aspid_MVVM_StarterKit_RoundMode_System_Int32_System_MidpointRounding_}

```csharp
public RoundNumberConverter(RoundMode mode, int digits = 0, MidpointRounding midpoint = MidpointRounding.ToEven)
```

#### Parameters

`mode` [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

Which way to drop the fraction.

`digits` [int](https://learn.microsoft.com/dotnet/api/system.int32)

How many decimal places to keep. Ignored on the way to an int or long.

`midpoint` [MidpointRounding](https://learn.microsoft.com/dotnet/api/system.midpointrounding)

Where an exact half goes. Only [`RoundMode.Round`](Aspid.MVVM.StarterKit.RoundMode.md) consults it: 2.5 becomes 2 under
[`ToEven`](https://learn.microsoft.com/dotnet/api/system.midpointrounding.toeven) and 3 under [`AwayFromZero`](https://learn.microsoft.com/dotnet/api/system.midpointrounding.awayfromzero).

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">digits</code> is negative.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_RoundNumberConverter_Apply_System_Double_}

Rounds the number to the configured number of decimal places.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to round.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The rounded number. An undeclared mode reports an error and returns the value unchanged.

