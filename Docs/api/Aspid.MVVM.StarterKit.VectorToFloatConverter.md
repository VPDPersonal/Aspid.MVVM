---
title: "Class VectorToFloatConverter"
sidebar_label: "VectorToFloatConverter"
description: "Class VectorToFloatConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorToFloatConverter {#Aspid_MVVM_StarterKit_VectorToFloatConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Measures one number out of a vector.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/To Number", Name = "To Float", Tooltip = "Measures one number out of a vector")]
public sealed class VectorToFloatConverter : IConverter<Vector3, float>, IConverter<Vector2, float>, IConverter<Vector4, float>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorToFloatConverter](Aspid.MVVM.StarterKit.VectorToFloatConverter.md)

#### Implements

[IConverter\<Vector3, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector2, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Vector4, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

A component the bound vector does not carry, Z on a [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html), W on anything
narrower than a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html), is reported on every push and reads as zero.

## Constructors

### VectorToFloatConverter\(\) {#Aspid_MVVM_StarterKit_VectorToFloatConverter__ctor}

```csharp
public VectorToFloatConverter()
```

#### Remarks

Default: measuring length.

### VectorToFloatConverter\(VectorComponent\) {#Aspid_MVVM_StarterKit_VectorToFloatConverter__ctor_Aspid_MVVM_StarterKit_VectorComponent_}

```csharp
public VectorToFloatConverter(VectorComponent component)
```

#### Parameters

`component` [VectorComponent](Aspid.MVVM.StarterKit.VectorComponent.md)

Which number to take.

### VectorToFloatConverter\(Vector4\) {#Aspid_MVVM_StarterKit_VectorToFloatConverter__ctor_UnityEngine_Vector4_}

```csharp
public VectorToFloatConverter(Vector4 dotAgainst)
```

#### Parameters

`dotAgainst` Vector4

The direction to measure along, read as far as the bound vector goes. Keep it unit length
to read a plain distance.

#### Remarks

Selects [`VectorComponent.Dot`](Aspid.MVVM.StarterKit.VectorComponent.md).

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_VectorToFloatConverter_Convert_UnityEngine_Vector3_}

Measures the specified vector.

```csharp
public float Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The vector to measure.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The measurement. [`VectorComponent.Dot`](Aspid.MVVM.StarterKit.VectorComponent.md) is the raw dot product, so a unit
direction reads as the signed distance along it and a longer one scales that reading. Reports
an error and returns zero when the component is not one this vector carries.

