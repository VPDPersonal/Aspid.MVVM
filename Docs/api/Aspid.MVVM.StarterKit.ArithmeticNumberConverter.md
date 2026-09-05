---
title: "Class ArithmeticNumberConverter"
sidebar_label: "ArithmeticNumberConverter"
description: "Class ArithmeticNumberConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ArithmeticNumberConverter {#Aspid_MVVM_StarterKit_ArithmeticNumberConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies an arithmetic operation with an authored coefficient.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Arithmetic", Tooltip = "Applies an arithmetic operation with an authored coefficient")]
public sealed class ArithmeticNumberConverter : TwoWayNumberConverter, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, ITwoWayConverter<int, int>, IConverter<int, int>, ITwoWayConverter<long, long>, IConverter<long, long>, ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md) ← 
[ArithmeticNumberConverter](Aspid.MVVM.StarterKit.ArithmeticNumberConverter.md)

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

## Constructors

### ArithmeticNumberConverter\(\) {#Aspid_MVVM_StarterKit_ArithmeticNumberConverter__ctor}

```csharp
public ArithmeticNumberConverter()
```

#### Remarks

Default: adding a coefficient of one.

### ArithmeticNumberConverter\(NumberOperation, double, ConverterFallback\<double\>?\) {#Aspid_MVVM_StarterKit_ArithmeticNumberConverter__ctor_Aspid_MVVM_StarterKit_NumberOperation_System_Double_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback_System_Double___}

```csharp
public ArithmeticNumberConverter(NumberOperation operation, double coefficient, ConverterFallback<double>? fallback = null)
```

#### Parameters

`operation` [NumberOperation](Aspid.MVVM.StarterKit.NumberOperation.md)

The arithmetic applied to the number.

`coefficient` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the operation is applied with. Dividing by zero falls back.

`fallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>?

Returned when the operation is undeclared, divides by zero, or cannot be undone.
When omitted, returns the input value unchanged.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_ArithmeticNumberConverter_Apply_System_Double_}

Applies the authored arithmetic.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result, or the fallback for an undeclared operation or a division by zero.

### Undo\(double\) {#Aspid_MVVM_StarterKit_ArithmeticNumberConverter_Undo_System_Double_}

Reverses the authored arithmetic.

```csharp
protected override double Undo(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform back.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the forward pass was given, or the fallback where the operation cannot be undone.
[`NumberOperation.Modulo`](Aspid.MVVM.StarterKit.NumberOperation.md) returns the value unchanged without reporting.

