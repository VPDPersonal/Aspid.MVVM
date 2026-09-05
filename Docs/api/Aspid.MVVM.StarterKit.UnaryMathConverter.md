---
title: "Class UnaryMathConverter"
sidebar_label: "UnaryMathConverter"
description: "Class UnaryMathConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class UnaryMathConverter {#Aspid_MVVM_StarterKit_UnaryMathConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies a single-argument mathematical function.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Unary Math", Tooltip = "Applies a single-argument mathematical function")]
public sealed class UnaryMathConverter : NumberConverter, IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[UnaryMathConverter](Aspid.MVVM.StarterKit.UnaryMathConverter.md)

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

Functions with a domain return zero or clamp outside it rather than yielding NaN or infinity.

## Constructors

### UnaryMathConverter\(\) {#Aspid_MVVM_StarterKit_UnaryMathConverter__ctor}

```csharp
public UnaryMathConverter()
```

#### Remarks

Default: applying [`UnaryMathOperation.Abs`](Aspid.MVVM.StarterKit.UnaryMathOperation.md).

### UnaryMathConverter\(UnaryMathOperation\) {#Aspid_MVVM_StarterKit_UnaryMathConverter__ctor_Aspid_MVVM_StarterKit_UnaryMathOperation_}

```csharp
public UnaryMathConverter(UnaryMathOperation operation)
```

#### Parameters

`operation` [UnaryMathOperation](Aspid.MVVM.StarterKit.UnaryMathOperation.md)

The function to apply.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_UnaryMathConverter_Apply_System_Double_}

Applies the configured function.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The result. An undeclared operation reports an error and returns the value unchanged.

