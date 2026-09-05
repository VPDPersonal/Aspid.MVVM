---
title: "Class OffsetThenScaleConverter"
sidebar_label: "OffsetThenScaleConverter"
description: "Class OffsetThenScaleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OffsetThenScaleConverter {#Aspid_MVVM_StarterKit_OffsetThenScaleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Adds a constant to a number and scales the sum.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Offset Then Scale", Tooltip = "Adds a constant to a number and scales the sum")]
public sealed class OffsetThenScaleConverter : TwoWayNumberConverter, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, ITwoWayConverter<int, int>, IConverter<int, int>, ITwoWayConverter<long, long>, IConverter<long, long>, ITwoWayConverter<float, float>, IConverter<float, float>, ITwoWayConverter<double, double>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[TwoWayNumberConverter](Aspid.MVVM.StarterKit.TwoWayNumberConverter.md) ← 
[OffsetThenScaleConverter](Aspid.MVVM.StarterKit.OffsetThenScaleConverter.md)

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

### OffsetThenScaleConverter\(\) {#Aspid_MVVM_StarterKit_OffsetThenScaleConverter__ctor}

```csharp
public OffsetThenScaleConverter()
```

#### Remarks

Default: no offset and a scale of one.

### OffsetThenScaleConverter\(float, float, ConverterFallback\<double\>?\) {#Aspid_MVVM_StarterKit_OffsetThenScaleConverter__ctor_System_Single_System_Single_System_Nullable_Aspid_MVVM_StarterKit_ConverterFallback_System_Double___}

```csharp
public OffsetThenScaleConverter(float offset, float scale = 1, ConverterFallback<double>? convertBackFallback = null)
```

#### Parameters

`offset` [float](https://learn.microsoft.com/dotnet/api/system.single)

Added to the value first.

`scale` [float](https://learn.microsoft.com/dotnet/api/system.single)

Multiplies the sum. A scale of zero cannot be reversed.

`convertBackFallback` [ConverterFallback](Aspid.MVVM.StarterKit.ConverterFallback-1.md)\<[double](https://learn.microsoft.com/dotnet/api/system.double)\>?

Returned from <code>ConvertBack</code> when the scale is zero. When omitted, returns the input value unchanged.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_OffsetThenScaleConverter_Apply_System_Double_}

Adds the offset and scales the sum.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The transformed number.

### Undo\(double\) {#Aspid_MVVM_StarterKit_OffsetThenScaleConverter_Undo_System_Double_}

Divides by the scale and removes the offset.

```csharp
protected override double Undo(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to transform back.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The number the forward pass was given, or the fallback for a zero scale.

