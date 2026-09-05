---
title: "Class ColorTintConverter"
sidebar_label: "ColorTintConverter"
description: "Class ColorTintConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorTintConverter {#Aspid_MVVM_StarterKit_ColorTintConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Combines a bound color with an authored one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Tint", Tooltip = "Combines a bound color with an authored one")]
public sealed class ColorTintConverter : IConverter<Color, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorTintConverter](Aspid.MVVM.StarterKit.ColorTintConverter.md)

#### Implements

[IConverter\<Color, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorTintConverter\(\) {#Aspid_MVVM_StarterKit_ColorTintConverter__ctor}

```csharp
public ColorTintConverter()
```

#### Remarks

Default: a multiply by white, which changes nothing.

### ColorTintConverter\(Color, ColorBlendMode, float\) {#Aspid_MVVM_StarterKit_ColorTintConverter__ctor_UnityEngine_Color_Aspid_MVVM_StarterKit_ColorBlendMode_System_Single_}

```csharp
public ColorTintConverter(Color tint, ColorBlendMode blend = ColorBlendMode.Multiply, float amount = 1)
```

#### Parameters

`tint` Color

The color the bound one is combined with.

`blend` [ColorBlendMode](Aspid.MVVM.StarterKit.ColorBlendMode.md)

How the two are combined.

`amount` [float](https://learn.microsoft.com/dotnet/api/system.single)

How far toward the tint to move, for [`ColorBlendMode.Lerp`](Aspid.MVVM.StarterKit.ColorBlendMode.md).

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorTintConverter_Convert_UnityEngine_Color_}

Combines the specified color with the authored tint.

```csharp
public Color Convert(Color value)
```

#### Parameters

`value` Color

The color to tint.

#### Returns

 Color

The combined color. Its alpha follows the blend: [`ColorBlendMode.Multiply`](Aspid.MVVM.StarterKit.ColorBlendMode.md) and
[`ColorBlendMode.Lerp`](Aspid.MVVM.StarterKit.ColorBlendMode.md) take the tint's alpha into account, the other two leave the
bound color's alpha alone. A blend that is not a declared [`ColorBlendMode`](Aspid.MVVM.StarterKit.ColorBlendMode.md) value
reports an error and the color passes through unchanged.

