---
title: "Class Vector4ToRectOffsetConverter"
sidebar_label: "Vector4ToRectOffsetConverter"
description: "Class Vector4ToRectOffsetConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector4ToRectOffsetConverter {#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Turns the four numbers of a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) into a padding.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/To Rect Offset", Name = "Vector4 To Rect Offset", Tooltip = "Turns the four numbers of a Vector4 into a padding")]
public sealed class Vector4ToRectOffsetConverter : ITwoWayConverter<Vector4, RectOffset>, IConverter<Vector4, RectOffset>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector4ToRectOffsetConverter](Aspid.MVVM.StarterKit.Vector4ToRectOffsetConverter.md)

#### Implements

[ITwoWayConverter\<Vector4, RectOffset\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector4, RectOffset\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### Vector4ToRectOffsetConverter\(\) {#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverter__ctor}

```csharp
public Vector4ToRectOffsetConverter()
```

#### Remarks

Default: rounding to nearest.

### Vector4ToRectOffsetConverter\(RoundMode\) {#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverter__ctor_Aspid_MVVM_StarterKit_RoundMode_}

```csharp
public Vector4ToRectOffsetConverter(RoundMode rounding)
```

#### Parameters

`rounding` [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

Which way to drop the fraction.

## Methods

### Convert\(Vector4\) {#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverter_Convert_UnityEngine_Vector4_}

Turns the specified vector into a padding, reading x, y, z and w as left, right, top and bottom.

```csharp
public RectOffset Convert(Vector4 value)
```

#### Parameters

`value` Vector4

The vector to convert.

#### Returns

 RectOffset

The padding. The same instance is returned every call, so copy it if it must outlive the
next push. A component too large for a whole number is held at the nearest one, and a NaN
reads as zero. A rounding that is not a declared [`RoundMode`](Aspid.MVVM.StarterKit.RoundMode.md) value reports an
error and the fraction is truncated.

### ConvertBack\(RectOffset?\) {#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverter_ConvertBack_UnityEngine_RectOffset_}

Reads the specified padding back as four numbers.

```csharp
public Vector4 ConvertBack(RectOffset? value)
```

#### Parameters

`value` RectOffset?

The padding to read, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to read no padding at all.

#### Returns

 Vector4

The vector, reading left, right, top and bottom as x, y, z and w. The fraction dropped by
[`Vector4ToRectOffsetConverter.Convert`](Aspid.MVVM.StarterKit.Vector4ToRectOffsetConverter.md#Aspid_MVVM_StarterKit_Vector4ToRectOffsetConverter_Convert_UnityEngine_Vector4_) is not restored, so a TwoWay binding quantizes the source.

