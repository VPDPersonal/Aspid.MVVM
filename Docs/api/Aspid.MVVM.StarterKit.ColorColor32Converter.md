---
title: "Class ColorColor32Converter"
sidebar_label: "ColorColor32Converter"
description: "Class ColorColor32Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorColor32Converter {#Aspid_MVVM_StarterKit_ColorColor32Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts between a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) and a [`Color32`](https://docs.unity3d.com/ScriptReference/Color32.html), in either direction.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "To Color32", Tooltip = "Converts between a Color and a Color32, in either direction")]
public sealed class ColorColor32Converter : ITwoWayConverter<Color, Color32>, IConverter<Color, Color32>, ITwoWayConverter<Color32, Color>, IConverter<Color32, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorColor32Converter](Aspid.MVVM.StarterKit.ColorColor32Converter.md)

#### Implements

[ITwoWayConverter\<Color, Color32\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Color, Color32\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Color32, Color\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Color32, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Narrowing clamps each channel to 0..1 and quantizes it to a byte, so an HDR color loses
everything above white and the round trip through the byte color is not exact.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorColor32Converter_Convert_UnityEngine_Color_}

Narrows the specified color.

```csharp
public Color32 Convert(Color value)
```

#### Parameters

`value` Color

The color to narrow.

#### Returns

 Color32

The same color with each channel as a byte.

### Convert\(Color32\) {#Aspid_MVVM_StarterKit_ColorColor32Converter_Convert_UnityEngine_Color32_}

Widens the specified byte color.

```csharp
public Color Convert(Color32 value)
```

#### Parameters

`value` Color32

The byte color to widen.

#### Returns

 Color

The same color with each channel as a 0..1 float.

### ConvertBack\(Color32\) {#Aspid_MVVM_StarterKit_ColorColor32Converter_ConvertBack_UnityEngine_Color32_}

Widens a byte color back.

```csharp
public Color ConvertBack(Color32 value)
```

#### Parameters

`value` Color32

The byte color to widen.

#### Returns

 Color

The same color with each channel as a 0..1 float.

### ConvertBack\(Color\) {#Aspid_MVVM_StarterKit_ColorColor32Converter_ConvertBack_UnityEngine_Color_}

Narrows a color back.

```csharp
public Color32 ConvertBack(Color value)
```

#### Parameters

`value` Color

The color to narrow.

#### Returns

 Color32

The same color with each channel as a byte.

