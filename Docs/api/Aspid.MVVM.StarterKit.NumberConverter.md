---
title: "Class NumberConverter"
sidebar_label: "NumberConverter"
description: "Class NumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class NumberConverter {#Aspid_MVVM_StarterKit_NumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base for a converter that transforms a number and accepts every numeric type.

```csharp
[Serializable]
public abstract class NumberConverter : IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md)

#### Derived

[RoundNumberConverter](Aspid.MVVM.StarterKit.RoundNumberConverter.md), 
[SnapToStepConverter](Aspid.MVVM.StarterKit.SnapToStepConverter.md), 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md), 
[UnaryMathConverter](Aspid.MVVM.StarterKit.UnaryMathConverter.md), 
[WrapNumberConverter](Aspid.MVVM.StarterKit.WrapNumberConverter.md)

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

Computed in [`Double`](https://learn.microsoft.com/dotnet/api/system.double): the int and long results truncate and saturate, the float result saturates.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_NumberConverter_Apply_System_Double_}

Transforms the specified number.

```csharp
protected abstract double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, in [`Double`](https://learn.microsoft.com/dotnet/api/system.double).

### Convert\(int\) {#Aspid_MVVM_StarterKit_NumberConverter_Convert_System_Int32_}

Converts the specified number.

```csharp
public int Convert(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to convert.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The result, saturated to the return type.

### Convert\(long\) {#Aspid_MVVM_StarterKit_NumberConverter_Convert_System_Int64_}

Converts the specified number.

```csharp
public long Convert(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number to convert.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The result, saturated to the return type.

### Convert\(float\) {#Aspid_MVVM_StarterKit_NumberConverter_Convert_System_Single_}

Converts the specified number.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to convert.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The result, saturated to the return type.

### Convert\(double\) {#Aspid_MVVM_StarterKit_NumberConverter_Convert_System_Double_}

Converts the specified number.

```csharp
public double Convert(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to convert.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, saturated to the return type.

