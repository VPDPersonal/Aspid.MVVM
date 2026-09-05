---
title: "Class RectVector4Converter"
sidebar_label: "RectVector4Converter"
description: "Class RectVector4Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RectVector4Converter {#Aspid_MVVM_StarterKit_RectVector4Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts between a rectangle and a four-component vector, in either direction.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Rect/To Vector", Name = "To Vector4", Tooltip = "Converts between a rectangle and a four-component vector, in either direction")]
public sealed class RectVector4Converter : ITwoWayConverter<Rect, Vector4>, IConverter<Rect, Vector4>, ITwoWayConverter<Vector4, Rect>, IConverter<Vector4, Rect>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RectVector4Converter](Aspid.MVVM.StarterKit.RectVector4Converter.md)

#### Implements

[ITwoWayConverter\<Rect, Vector4\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Rect, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Vector4, Rect\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector4, Rect\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The four numbers are a corner plus a size (x, y, width, height), and neither direction
normalizes, so the round trip is exact.

## Methods

### Convert\(Rect\) {#Aspid_MVVM_StarterKit_RectVector4Converter_Convert_UnityEngine_Rect_}

Reads the specified rectangle as a vector.

```csharp
public Vector4 Convert(Rect value)
```

#### Parameters

`value` Rect

The rectangle to read.

#### Returns

 Vector4

The vector, as x, y, width, height.

### Convert\(Vector4\) {#Aspid_MVVM_StarterKit_RectVector4Converter_Convert_UnityEngine_Vector4_}

Reads the specified vector as a rectangle.

```csharp
public Rect Convert(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to read, as x, y, width, height.

#### Returns

 Rect

The rectangle.

### ConvertBack\(Vector4\) {#Aspid_MVVM_StarterKit_RectVector4Converter_ConvertBack_UnityEngine_Vector4_}

Reads a vector back as a rectangle.

```csharp
public Rect ConvertBack(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to read, as x, y, width, height.

#### Returns

 Rect

The rectangle.

### ConvertBack\(Rect\) {#Aspid_MVVM_StarterKit_RectVector4Converter_ConvertBack_UnityEngine_Rect_}

Reads a rectangle back as a vector.

```csharp
public Vector4 ConvertBack(Rect value)
```

#### Parameters

`value` Rect

The rectangle to read.

#### Returns

 Vector4

The vector, as x, y, width, height.

