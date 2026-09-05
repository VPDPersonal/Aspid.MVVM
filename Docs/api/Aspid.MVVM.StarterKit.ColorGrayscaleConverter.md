---
title: "Class ColorGrayscaleConverter"
sidebar_label: "ColorGrayscaleConverter"
description: "Class ColorGrayscaleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorGrayscaleConverter {#Aspid_MVVM_StarterKit_ColorGrayscaleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Desaturates a color.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Grayscale", Tooltip = "Desaturates a color")]
public sealed class ColorGrayscaleConverter : IConverter<Color, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorGrayscaleConverter](Aspid.MVVM.StarterKit.ColorGrayscaleConverter.md)

#### Implements

[IConverter\<Color, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Gray is computed with the luminance weights, not a flat channel average.

## Constructors

### ColorGrayscaleConverter\(\) {#Aspid_MVVM_StarterKit_ColorGrayscaleConverter__ctor}

```csharp
public ColorGrayscaleConverter()
```

#### Remarks

Default: fully gray.

### ColorGrayscaleConverter\(float\) {#Aspid_MVVM_StarterKit_ColorGrayscaleConverter__ctor_System_Single_}

```csharp
public ColorGrayscaleConverter(float saturation)
```

#### Parameters

`saturation` [float](https://learn.microsoft.com/dotnet/api/system.single)

How much color to keep. Zero is fully gray, one leaves the color untouched; a value
outside that range is held to it.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorGrayscaleConverter_Convert_UnityEngine_Color_}

Desaturates the specified color.

```csharp
public Color Convert(Color value)
```

#### Parameters

`value` Color

The color to desaturate.

#### Returns

 Color

The desaturated color, with its alpha untouched.

