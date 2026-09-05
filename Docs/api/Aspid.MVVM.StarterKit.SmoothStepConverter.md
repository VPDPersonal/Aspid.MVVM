---
title: "Class SmoothStepConverter"
sidebar_label: "SmoothStepConverter"
description: "Class SmoothStepConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SmoothStepConverter {#Aspid_MVVM_StarterKit_SmoothStepConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a 0..1 position to a value in a range, eased in and out at the ends.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Smooth Step", Tooltip = "Converts a 0..1 position to a value in a range, eased in and out at the ends")]
public sealed class SmoothStepConverter : IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SmoothStepConverter](Aspid.MVVM.StarterKit.SmoothStepConverter.md)

#### Implements

[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The incoming position is always held inside 0..1; there is no unclamped mode.

## Constructors

### SmoothStepConverter\(\) {#Aspid_MVVM_StarterKit_SmoothStepConverter__ctor}

```csharp
public SmoothStepConverter()
```

#### Remarks

Default: over 0..1.

### SmoothStepConverter\(float, float\) {#Aspid_MVVM_StarterKit_SmoothStepConverter__ctor_System_Single_System_Single_}

```csharp
public SmoothStepConverter(float from, float to)
```

#### Parameters

`from` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value 0 maps to.

`to` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value 1 maps to.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_SmoothStepConverter_Convert_System_Single_}

Converts the specified position to an eased value in the range.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 position. A position outside it is held at the nearer end.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The eased value at that position.

