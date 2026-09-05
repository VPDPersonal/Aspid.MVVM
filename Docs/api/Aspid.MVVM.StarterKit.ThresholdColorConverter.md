---
title: "Class ThresholdColorConverter"
sidebar_label: "ThresholdColorConverter"
description: "Class ThresholdColorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ThresholdColorConverter {#Aspid_MVVM_StarterKit_ThresholdColorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Picks a color by which threshold a number has passed.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Color", Name = "Threshold", Tooltip = "Picks a color by which threshold a number has passed")]
public sealed class ThresholdColorConverter : IConverter<float, Color>, IConverter<double, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ThresholdColorConverter](Aspid.MVVM.StarterKit.ThresholdColorConverter.md)

#### Implements

[IConverter\<float, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ThresholdColorConverter\(ColorStop\[\]?, Color, bool\) {#Aspid_MVVM_StarterKit_ThresholdColorConverter__ctor_Aspid_MVVM_StarterKit_ColorStop___UnityEngine_Color_System_Boolean_}

```csharp
public ThresholdColorConverter(ColorStop[]? stops, Color fallback, bool interpolate = false)
```

#### Parameters

`stops` [ColorStop](Aspid.MVVM.StarterKit.ColorStop.md)\[\]?

Colors by threshold. With none the converter has nothing to pick from, which is reported
as an error.

`fallback` Color

Used when the value is below every threshold.

`interpolate` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to blend toward the next stop up.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_ThresholdColorConverter_Convert_System_Single_}

Picks the color for the specified value.

```csharp
public Color Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to place.

#### Returns

 Color

The color of the highest qualifying stop, or the fallback. When blending, the color
between that stop and the next one up, by how far the value has traveled between them.
With no stops authored the fallback is returned and the failure is reported as an error.

