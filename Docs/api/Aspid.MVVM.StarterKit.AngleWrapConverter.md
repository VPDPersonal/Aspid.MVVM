---
title: "Class AngleWrapConverter"
sidebar_label: "AngleWrapConverter"
description: "Class AngleWrapConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AngleWrapConverter {#Aspid_MVVM_StarterKit_AngleWrapConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Folds an angle into a standard range.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Angle Wrap", Tooltip = "Folds an angle into a standard range")]
public sealed class AngleWrapConverter : IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AngleWrapConverter](Aspid.MVVM.StarterKit.AngleWrapConverter.md)

#### Implements

[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### AngleWrapConverter\(\) {#Aspid_MVVM_StarterKit_AngleWrapConverter__ctor}

```csharp
public AngleWrapConverter()
```

#### Remarks

Default: reporting 0..360.

### AngleWrapConverter\(AngleRange, float\) {#Aspid_MVVM_StarterKit_AngleWrapConverter__ctor_Aspid_MVVM_StarterKit_AngleRange_System_Single_}

```csharp
public AngleWrapConverter(AngleRange range, float offset = 0)
```

#### Parameters

`range` [AngleRange](Aspid.MVVM.StarterKit.AngleRange.md)

Which range to report in.

`offset` [float](https://learn.microsoft.com/dotnet/api/system.single)

Added before wrapping.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_AngleWrapConverter_Convert_System_Single_}

Folds the specified angle into the configured range.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The folded angle. A range that is not a declared [`AngleRange`](Aspid.MVVM.StarterKit.AngleRange.md) is reported and
the bound angle is returned unchanged, without the offset.

