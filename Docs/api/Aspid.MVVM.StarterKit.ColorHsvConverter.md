---
title: "Class ColorHsvConverter"
sidebar_label: "ColorHsvConverter"
description: "Class ColorHsvConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorHsvConverter {#Aspid_MVVM_StarterKit_ColorHsvConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Shifts a color in HSV space.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "HSV", Tooltip = "Shifts a color in HSV space")]
public sealed class ColorHsvConverter : IConverter<Color, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorHsvConverter](Aspid.MVVM.StarterKit.ColorHsvConverter.md)

#### Implements

[IConverter\<Color, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Saturation and brightness are held inside 0..1, so an HDR color comes back at white level.

## Constructors

### ColorHsvConverter\(\) {#Aspid_MVVM_StarterKit_ColorHsvConverter__ctor}

```csharp
public ColorHsvConverter()
```

#### Remarks

Default: no shift and no scaling, which changes nothing.

### ColorHsvConverter\(float, float, float\) {#Aspid_MVVM_StarterKit_ColorHsvConverter__ctor_System_Single_System_Single_System_Single_}

```csharp
public ColorHsvConverter(float hueShift, float saturationMultiplier = 1, float valueMultiplier = 1)
```

#### Parameters

`hueShift` [float](https://learn.microsoft.com/dotnet/api/system.single)

How far to rotate the hue, in turns. 0.5 is the opposite color.

`saturationMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

Scales the saturation. The result is held to 0..1.

`valueMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

Scales the brightness. The result is held to 0..1, so an HDR color comes back at white level.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorHsvConverter_Convert_UnityEngine_Color_}

Shifts the specified color.

```csharp
public Color Convert(Color value)
```

#### Parameters

`value` Color

The color to shift.

#### Returns

 Color

The shifted color, with its alpha untouched.

