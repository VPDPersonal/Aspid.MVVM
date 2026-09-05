---
title: "Class SnapToStepConverter"
sidebar_label: "SnapToStepConverter"
description: "Class SnapToStepConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SnapToStepConverter {#Aspid_MVVM_StarterKit_SnapToStepConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Snaps a number to the nearest multiple of a step.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Snap To Step", Tooltip = "Snaps a number to the nearest multiple of a step")]
public sealed class SnapToStepConverter : NumberConverter, IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>, IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>, IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>, IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NumberConverter](Aspid.MVVM.StarterKit.NumberConverter.md) ← 
[SnapToStepConverter](Aspid.MVVM.StarterKit.SnapToStepConverter.md)

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

An exact half goes to the even step: 0.5 snaps to 0, 1.5 to 2.

## Constructors

### SnapToStepConverter\(\) {#Aspid_MVVM_StarterKit_SnapToStepConverter__ctor}

```csharp
public SnapToStepConverter()
```

#### Remarks

Default: snapping to whole numbers.

### SnapToStepConverter\(float, float\) {#Aspid_MVVM_StarterKit_SnapToStepConverter__ctor_System_Single_System_Single_}

```csharp
public SnapToStepConverter(float step, float offset = 0)
```

#### Parameters

`step` [float](https://learn.microsoft.com/dotnet/api/system.single)

The size of one step. Zero reports an error and passes the value through.

`offset` [float](https://learn.microsoft.com/dotnet/api/system.single)

Shifts where the steps fall.

## Methods

### Apply\(double\) {#Aspid_MVVM_StarterKit_SnapToStepConverter_Apply_System_Double_}

Snaps the number to the nearest step.

```csharp
protected override double Apply(double value)
```

#### Parameters

`value` [double](https://learn.microsoft.com/dotnet/api/system.double)

The number to snap.

#### Returns

 [double](https://learn.microsoft.com/dotnet/api/system.double)

The nearest multiple of the step. A zero step reports an error and returns the value unchanged.

