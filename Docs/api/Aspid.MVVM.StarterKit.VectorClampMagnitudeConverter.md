---
title: "Class VectorClampMagnitudeConverter"
sidebar_label: "VectorClampMagnitudeConverter"
description: "Class VectorClampMagnitudeConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorClampMagnitudeConverter {#Aspid_MVVM_StarterKit_VectorClampMagnitudeConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Keeps a vector inside a length.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Clamp Magnitude", Tooltip = "Keeps a vector inside a length")]
public sealed class VectorClampMagnitudeConverter : IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorClampMagnitudeConverter](Aspid.MVVM.StarterKit.VectorClampMagnitudeConverter.md)

#### Implements

[IConverter\<Vector2, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector3, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector4, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### VectorClampMagnitudeConverter\(\) {#Aspid_MVVM_StarterKit_VectorClampMagnitudeConverter__ctor}

```csharp
public VectorClampMagnitudeConverter()
```

#### Remarks

Default: clamping to one.

### VectorClampMagnitudeConverter\(float, float\) {#Aspid_MVVM_StarterKit_VectorClampMagnitudeConverter__ctor_System_Single_System_Single_}

```csharp
public VectorClampMagnitudeConverter(float maxMagnitude, float minMagnitude = 0)
```

#### Parameters

`maxMagnitude` [float](https://learn.microsoft.com/dotnet/api/system.single)

The longest the vector is allowed to be. Bounds typed the wrong way round are reported and
swapped, and a negative bound reads as zero.

`minMagnitude` [float](https://learn.microsoft.com/dotnet/api/system.single)

The shortest the vector is allowed to be. Zero disables the lower bound; bounds typed the
wrong way round are reported and swapped.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_VectorClampMagnitudeConverter_Convert_UnityEngine_Vector3_}

Clamps the length of the specified vector.

```csharp
public Vector3 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to clamp.

#### Returns

 Vector3

The clamped vector, with a zero vector left as it is. A pair typed the wrong way round,
or with a negative length in it, reports an error and is read in the order that holds the
vector inside both bounds, with a negative bound reading as zero.

