---
title: "Class ColorVector4Converter"
sidebar_label: "ColorVector4Converter"
description: "Class ColorVector4Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorVector4Converter {#Aspid_MVVM_StarterKit_ColorVector4Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts between a color and a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html), in either direction.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color/To Vector", Name = "To Vector4", Tooltip = "Converts between a color and a Vector4, in either direction")]
public sealed class ColorVector4Converter : ITwoWayConverter<Color, Vector4>, IConverter<Color, Vector4>, ITwoWayConverter<Vector4, Color>, IConverter<Vector4, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorVector4Converter](Aspid.MVVM.StarterKit.ColorVector4Converter.md)

#### Implements

[ITwoWayConverter\<Color, Vector4\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Color, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Vector4, Color\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector4, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The channels are copied as they are, with no color-space conversion or clamping, which is
what makes the round trip exact.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorVector4Converter_Convert_UnityEngine_Color_}

Reads the specified color as a vector.

```csharp
public Vector4 Convert(Color value)
```

#### Parameters

`value` Color

The color to read.

#### Returns

 Vector4

Its red, green, blue and alpha as x, y, z and w.

### Convert\(Vector4\) {#Aspid_MVVM_StarterKit_ColorVector4Converter_Convert_UnityEngine_Vector4_}

Reads the specified vector as a color.

```csharp
public Color Convert(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to read.

#### Returns

 Color

Its x, y, z and w as red, green, blue and alpha, unclamped.

### ConvertBack\(Vector4\) {#Aspid_MVVM_StarterKit_ColorVector4Converter_ConvertBack_UnityEngine_Vector4_}

Reads a vector back as a color.

```csharp
public Color ConvertBack(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to read.

#### Returns

 Color

Its x, y, z and w as red, green, blue and alpha, unclamped.

### ConvertBack\(Color\) {#Aspid_MVVM_StarterKit_ColorVector4Converter_ConvertBack_UnityEngine_Color_}

Reads a color back as a vector.

```csharp
public Vector4 ConvertBack(Color value)
```

#### Parameters

`value` Color

The color to read.

#### Returns

 Vector4

Its red, green, blue and alpha as x, y, z and w.

