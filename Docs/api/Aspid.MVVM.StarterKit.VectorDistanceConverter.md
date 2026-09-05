---
title: "Class VectorDistanceConverter"
sidebar_label: "VectorDistanceConverter"
description: "Class VectorDistanceConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorDistanceConverter {#Aspid_MVVM_StarterKit_VectorDistanceConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Measures how far a position is from a target.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/To Number", Name = "Distance", Tooltip = "Measures how far a position is from a target")]
public sealed class VectorDistanceConverter : IConverter<Vector3, float>, IConverter<Vector2, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorDistanceConverter](Aspid.MVVM.StarterKit.VectorDistanceConverter.md)

#### Implements

[IConverter\<Vector3, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector2, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### VectorDistanceConverter\(\) {#Aspid_MVVM_StarterKit_VectorDistanceConverter__ctor}

```csharp
public VectorDistanceConverter()
```

#### Remarks

Default: measuring to the origin.

### VectorDistanceConverter\(Vector3, bool\) {#Aspid_MVVM_StarterKit_VectorDistanceConverter__ctor_UnityEngine_Vector3_System_Boolean_}

```csharp
public VectorDistanceConverter(Vector3 point, bool flattenY = false)
```

#### Parameters

`point` Vector3

The position the distance is measured to.

`flattenY` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to ignore the height difference.

### VectorDistanceConverter\(Transform, bool\) {#Aspid_MVVM_StarterKit_VectorDistanceConverter__ctor_UnityEngine_Transform_System_Boolean_}

```csharp
public VectorDistanceConverter(Transform target, bool flattenY = false)
```

#### Parameters

`target` Transform

The transform the distance is measured to.

`flattenY` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to ignore the height difference.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_VectorDistanceConverter_Convert_UnityEngine_Vector3_}

Measures the specified position against the target.

```csharp
public float Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The position to measure from.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The distance to the target, in world units.

