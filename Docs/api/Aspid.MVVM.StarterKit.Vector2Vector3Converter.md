---
title: "Class Vector2Vector3Converter"
sidebar_label: "Vector2Vector3Converter"
description: "Class Vector2Vector3Converter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Vector2Vector3Converter {#Aspid_MVVM_StarterKit_Vector2Vector3Converter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Maps a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html)'s components onto two axes of a [`Vector3`](https://docs.unity3d.com/ScriptReference/Vector3.html), filling
the third with a constant, and reads the same two back.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 To Vector3", Tooltip = "Maps a Vector2's components onto two axes of a Vector3, filling the third with a constant")]
public sealed class Vector2Vector3Converter : ITwoWayConverter<Vector2, Vector3>, IConverter<Vector2, Vector3>, ITwoWayConverter<Vector3, Vector2>, IConverter<Vector3, Vector2>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Vector2Vector3Converter](Aspid.MVVM.StarterKit.Vector2Vector3Converter.md)

#### Implements

[ITwoWayConverter\<Vector2, Vector3\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector2, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Vector3, Vector2\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector3, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### Vector2Vector3Converter\(\) {#Aspid_MVVM_StarterKit_Vector2Vector3Converter__ctor}

```csharp
public Vector2Vector3Converter()
```

#### Remarks

Default: X and Y kept, with a zero Z.

### Vector2Vector3Converter\(Mode, float\) {#Aspid_MVVM_StarterKit_Vector2Vector3Converter__ctor_Aspid_MVVM_StarterKit_Vector2Vector3Converter_Mode_System_Single_}

```csharp
public Vector2Vector3Converter(Vector2Vector3Converter.Mode mode, float thirdValue = 0)
```

#### Parameters

`mode` [Vector2Vector3Converter](Aspid.MVVM.StarterKit.Vector2Vector3Converter.md).[Mode](Aspid.MVVM.StarterKit.Vector2Vector3Converter.Mode.md)

Which axes of the 3D vector the 2D components are written into.

`thirdValue` [float](https://learn.microsoft.com/dotnet/api/system.single)

The constant written into the axis the mode leaves out. When omitted, zero.

## Methods

### Convert\(Vector2\) {#Aspid_MVVM_StarterKit_Vector2Vector3Converter_Convert_UnityEngine_Vector2_}

Maps the specified vector onto the configured axes.

```csharp
public Vector3 Convert(Vector2 value)
```

#### Parameters

`value` Vector2

The 2D vector to convert.

#### Returns

 Vector3

The converted 3D vector. Reports an error and returns a zero vector when the mode is not
a declared value.

### ConvertBack\(Vector3\) {#Aspid_MVVM_StarterKit_Vector2Vector3Converter_ConvertBack_UnityEngine_Vector3_}

Reads the two mapped axes back out of the specified vector.

```csharp
public Vector2 ConvertBack(Vector3 value)
```

#### Parameters

`value` Vector3

The 3D vector to convert.

#### Returns

 Vector2

The two axes the mode names, in the order it names them; the constant axis is dropped.
Reports an error and returns a zero vector when the mode is not a declared value.

