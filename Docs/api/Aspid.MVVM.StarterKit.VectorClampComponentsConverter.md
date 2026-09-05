---
title: "Class VectorClampComponentsConverter"
sidebar_label: "VectorClampComponentsConverter"
description: "Class VectorClampComponentsConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorClampComponentsConverter {#Aspid_MVVM_StarterKit_VectorClampComponentsConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Keeps every axis of a vector between two bounds.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Clamp Components", Tooltip = "Keeps every axis of a vector between two bounds")]
public sealed class VectorClampComponentsConverter : IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorClampComponentsConverter](Aspid.MVVM.StarterKit.VectorClampComponentsConverter.md)

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

### VectorClampComponentsConverter\(\) {#Aspid_MVVM_StarterKit_VectorClampComponentsConverter__ctor}

```csharp
public VectorClampComponentsConverter()
```

#### Remarks

Default: clamping to ±1.

### VectorClampComponentsConverter\(Vector4, Vector4\) {#Aspid_MVVM_StarterKit_VectorClampComponentsConverter__ctor_UnityEngine_Vector4_UnityEngine_Vector4_}

```csharp
public VectorClampComponentsConverter(Vector4 min, Vector4 max)
```

#### Parameters

`min` Vector4

The lowest each axis is allowed to be. Only the components the bound vector carries are
read, and bounds the wrong way round on an axis are reported and swapped.

`max` Vector4

The highest each axis is allowed to be. Only the components the bound vector carries are
read, and bounds the wrong way round on an axis are reported and swapped.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_VectorClampComponentsConverter_Convert_UnityEngine_Vector3_}

Clamps every axis of the specified vector.

```csharp
public Vector3 Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to clamp.

#### Returns

 Vector3

The clamped vector. An axis whose bounds are typed the wrong way round reports an error
and is clamped to the swapped pair.

