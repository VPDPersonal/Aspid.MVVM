---
title: "Class ColorAlphaConverter"
sidebar_label: "ColorAlphaConverter"
description: "Class ColorAlphaConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorAlphaConverter {#Aspid_MVVM_StarterKit_ColorAlphaConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Changes the alpha of a color, leaving its hue alone.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Alpha", Tooltip = "Changes the alpha of a color, leaving its hue alone")]
public sealed class ColorAlphaConverter : IConverter<Color, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorAlphaConverter](Aspid.MVVM.StarterKit.ColorAlphaConverter.md)

#### Implements

[IConverter\<Color, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorAlphaConverter\(\) {#Aspid_MVVM_StarterKit_ColorAlphaConverter__ctor}

```csharp
public ColorAlphaConverter()
```

#### Remarks

Default: setting the alpha to one, which leaves an opaque color opaque.

### ColorAlphaConverter\(float, AlphaMode\) {#Aspid_MVVM_StarterKit_ColorAlphaConverter__ctor_System_Single_Aspid_MVVM_StarterKit_AlphaMode_}

```csharp
public ColorAlphaConverter(float alpha, AlphaMode mode = AlphaMode.Set)
```

#### Parameters

`alpha` [float](https://learn.microsoft.com/dotnet/api/system.single)

The alpha applied to the color. The result is held to 0..1 whichever mode is used.

`mode` [AlphaMode](Aspid.MVVM.StarterKit.AlphaMode.md)

How the alpha is applied.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorAlphaConverter_Convert_UnityEngine_Color_}

Applies the configured alpha to the specified color.

```csharp
public Color Convert(Color value)
```

#### Parameters

`value` Color

The color to adjust.

#### Returns

 Color

The color with its alpha changed, held to 0..1. A mode that is not a declared
[`AlphaMode`](Aspid.MVVM.StarterKit.AlphaMode.md) value reports an error and the alpha is left as it arrived.

