---
title: "Class TwoWayNumberConverter"
sidebar_label: "TwoWayNumberConverter"
description: "Class TwoWayNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayNumberConverter {#Aspid_MVVM_StarterKit_TwoWayNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`NumberConverter`](Aspid.MVVM.StarterKit.NumberConverter.md) that also converts back within the same numeric type.

```csharp
[Serializable]
public abstract class TwoWayNumberConverter : NumberConverter, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, ITwoWayConverter<int, int>, IConverter<int, int>, ITwoWayConverter<long, long>, IConverter<long, long>, ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md)

#### Derived

[ArithmeticNumberConverter](Aspid.MVVM.StarterKit.ArithmeticNumberConverter.md), 
[LerpNumberConverter](Aspid.MVVM.StarterKit.LerpNumberConverter.md), 
[OffsetThenScaleConverter](Aspid.MVVM.StarterKit.OffsetThenScaleConverter.md), 
[PowerNumberConverter](Aspid.MVVM.StarterKit.PowerNumberConverter.md), 
[RemapNumberConverter](Aspid.MVVM.StarterKit.RemapNumberConverter.md)

#### Implements

[IConverter\<int, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<int, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<long, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<int, int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<int, int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<long, long\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<long, long\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<float, float\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<double, double\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Methods

### ConvertBack\(int\) {#Aspid_MVVM_StarterKit_TwoWayNumberConverter_ConvertBack_System_Int32_}

Converts the specified number back.

```csharp
public int ConvertBack(int value)
```

#### Parameters

`value` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number to convert back.

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number the forward pass was given, saturated to the return type.

### ConvertBack\(long\) {#Aspid_MVVM_StarterKit_TwoWayNumberConverter_ConvertBack_System_Int64_}

Converts the specified number back.

```csharp
public long ConvertBack(long value)
```

#### Parameters

`value` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number to convert back.

#### Returns

 [long](https://learn.microsoft.com/dotnet/api/system.int64)

The number the forward pass was given, saturated to the return type.

### ConvertBack\(float\) {#Aspid_MVVM_StarterKit_TwoWayNumberConverter_ConvertBack_System_Single_}

Converts the specified number back.

```csharp
public float ConvertBack(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to convert back.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The number the forward pass was given, saturated to the return type.

### ConvertBack\(double\) {#Aspid_MVVM_StarterKit_TwoWayNumberConverter_ConvertBack_System_Double_}

Converts the specified number back.

```csharp
public double ConvertBack(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to convert back.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the forward pass was given, saturated to the return type.

### Undo\(double\) {#Aspid_MVVM_StarterKit_TwoWayNumberConverter_Undo_System_Double_}

Reverses [`NumberConverter.Apply`](Aspid.MVVM.StarterKit.NumberConverter.md#Aspid_MVVM_StarterKit_NumberConverter_Apply_System_Double_).

```csharp
protected abstract double Undo(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform back.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the forward pass was given, in [`Double`](https://learn.microsoft.com/dotnet/api/system.double).

