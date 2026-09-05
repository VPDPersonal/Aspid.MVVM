---
title: "Class ColorLerpConverter"
sidebar_label: "ColorLerpConverter"
description: "Class ColorLerpConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorLerpConverter {#Aspid_MVVM_StarterKit_ColorLerpConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Moves between two colors by a 0..1 amount.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Color", Name = "Lerp", Tooltip = "Moves between two colors by a 0..1 amount")]
public sealed class ColorLerpConverter : IConverter<float, Color>, IConverter<double, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorLerpConverter](Aspid.MVVM.StarterKit.ColorLerpConverter.md)

#### Implements

[IConverter\<float, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The curve applies only while the clamp is on: a curve cannot answer past its own ends.

## Constructors

### ColorLerpConverter\(\) {#Aspid_MVVM_StarterKit_ColorLerpConverter__ctor}

```csharp
public ColorLerpConverter()
```

#### Remarks

Default: going red to green.

### ColorLerpConverter\(Color, Color, AnimationCurve?\) {#Aspid_MVVM_StarterKit_ColorLerpConverter__ctor_UnityEngine_Color_UnityEngine_Color_UnityEngine_AnimationCurve_}

```csharp
public ColorLerpConverter(Color from, Color to, AnimationCurve? curve = null)
```

#### Parameters

`from` Color

The color at 0.

`to` Color

The color at 1.

`curve` AnimationCurve?

Shapes the travel between the two colors, while the amount is clamped. Leave it out to
move evenly.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_ColorLerpConverter_Convert_System_Single_}

Reads the color at the specified amount.

```csharp
public Color Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 amount.

#### Returns

 Color

The color there, after the curve has shaped the amount. With the clamp cleared the amount
reaches the two colors as it arrived and the curve takes no part, so an amount outside
0..1 carries past them.

