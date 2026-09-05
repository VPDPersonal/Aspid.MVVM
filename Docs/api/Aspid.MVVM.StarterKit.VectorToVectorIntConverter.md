---
title: "Class VectorToVectorIntConverter"
sidebar_label: "VectorToVectorIntConverter"
description: "Class VectorToVectorIntConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorToVectorIntConverter {#Aspid_MVVM_StarterKit_VectorToVectorIntConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Converts a vector to its integer form.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "To Vector Int", Tooltip = "Converts a vector to its integer form")]
public sealed class VectorToVectorIntConverter : ITwoWayConverter<Vector2, Vector2Int>, IConverter<Vector2, Vector2Int>, ITwoWayConverter<Vector3, Vector3Int>, IConverter<Vector3, Vector3Int>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorToVectorIntConverter](Aspid.MVVM.StarterKit.VectorToVectorIntConverter.md)

#### Implements

[ITwoWayConverter\<Vector2, Vector2Int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector2, Vector2Int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[ITwoWayConverter\<Vector3, Vector3Int\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector3, Vector3Int\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### VectorToVectorIntConverter\(\) {#Aspid_MVVM_StarterKit_VectorToVectorIntConverter__ctor}

```csharp
public VectorToVectorIntConverter()
```

#### Remarks

Default: rounding to nearest.

### VectorToVectorIntConverter\(RoundMode\) {#Aspid_MVVM_StarterKit_VectorToVectorIntConverter__ctor_Aspid_MVVM_StarterKit_RoundMode_}

```csharp
public VectorToVectorIntConverter(RoundMode mode)
```

#### Parameters

`mode` [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

Which way to drop the fraction.

## Methods

### Convert\(Vector2\) {#Aspid_MVVM_StarterKit_VectorToVectorIntConverter_Convert_UnityEngine_Vector2_}

Converts the specified vector to its integer form.

```csharp
public Vector2Int Convert(Vector2 value)
```

#### Parameters

`value` Vector2

The vector to convert.

#### Returns

 Vector2Int

The integer vector. An undeclared mode reports an error and rounds to nearest.

### ConvertBack\(Vector2Int\) {#Aspid_MVVM_StarterKit_VectorToVectorIntConverter_ConvertBack_UnityEngine_Vector2Int_}

Converts an integer vector back to a floating-point one.

```csharp
public Vector2 ConvertBack(Vector2Int value)
```

#### Parameters

`value` Vector2Int

The vector to convert.

#### Returns

 Vector2

The floating-point vector. The fraction dropped by [`VectorToVectorIntConverter.Convert`](Aspid.MVVM.StarterKit.VectorToVectorIntConverter.md#Aspid_MVVM_StarterKit_VectorToVectorIntConverter_Convert_UnityEngine_Vector2_) is not restored,
so a TwoWay binding quantizes the source.

