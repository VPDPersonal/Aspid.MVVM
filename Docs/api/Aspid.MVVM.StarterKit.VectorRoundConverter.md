---
title: "Class VectorRoundConverter"
sidebar_label: "VectorRoundConverter"
description: "Class VectorRoundConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorRoundConverter {#Aspid_MVVM_StarterKit_VectorRoundConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Rounds every axis of a vector.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Round", Tooltip = "Rounds every axis of a vector")]
public sealed class VectorRoundConverter : IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorRoundConverter](Aspid.MVVM.StarterKit.VectorRoundConverter.md)

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

### VectorRoundConverter\(\) {#Aspid_MVVM_StarterKit_VectorRoundConverter__ctor}

```csharp
public VectorRoundConverter()
```

#### Remarks

Default: rounding to whole numbers.

### VectorRoundConverter\(RoundMode, float\) {#Aspid_MVVM_StarterKit_VectorRoundConverter__ctor_Aspid_MVVM_StarterKit_RoundMode_System_Single_}

```csharp
public VectorRoundConverter(RoundMode mode, float step = 0)
```

#### Parameters

`mode` [RoundMode](Aspid.MVVM.StarterKit.RoundMode.md)

Which way to drop the fraction.

`step` [float](https://learn.microsoft.com/dotnet/api/system.single)

The size of one grid step. Zero rounds to whole numbers. A negative step reports an error
and its size is used.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_VectorRoundConverter_Convert_UnityEngine_Vector3_}

Rounds every axis of the specified vector.

```csharp
public Vector3 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to round.

#### Returns

 Vector3

The rounded vector. A negative grid step reports an error and snaps to a grid of its size.
Reports an error and returns the value unchanged when the mode is not a declared value.

