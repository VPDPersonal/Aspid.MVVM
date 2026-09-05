---
title: "Class HdrIntensityConverter"
sidebar_label: "HdrIntensityConverter"
description: "Class HdrIntensityConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class HdrIntensityConverter {#Aspid_MVVM_StarterKit_HdrIntensityConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Pushes a color above white by an exposure value.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "HDR Intensity", Tooltip = "Pushes a color above white by an exposure value")]
public sealed class HdrIntensityConverter : IConverter<Color, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HdrIntensityConverter](Aspid.MVVM.StarterKit.HdrIntensityConverter.md)

#### Implements

[IConverter\<Color, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The result leaves the 0..1 range on purpose, bind it to a material color or a light. A UGUI
[`Graphic`](https://docs.unity3d.com/ScriptReference/UI-Graphic.html) clamps it and shows no difference above white.

## Constructors

### HdrIntensityConverter\(\) {#Aspid_MVVM_StarterKit_HdrIntensityConverter__ctor}

```csharp
public HdrIntensityConverter()
```

#### Remarks

Default: no exposure, which changes nothing.

### HdrIntensityConverter\(float\) {#Aspid_MVVM_StarterKit_HdrIntensityConverter__ctor_System_Single_}

```csharp
public HdrIntensityConverter(float intensity)
```

#### Parameters

`intensity` [float](https://learn.microsoft.com/dotnet/api/system.single)

The exposure applied to the color, in stops. Each whole step doubles its brightness; zero
changes nothing.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_HdrIntensityConverter_Convert_UnityEngine_Color_}

Applies the exposure to the specified color.

```csharp
public Color Convert(Color value)
```

#### Parameters

`value` Color

The color to brighten.

#### Returns

 Color

The color scaled by two to the power of the intensity, with its alpha untouched. The
channels are not clamped, an HDR color above one is the point.

