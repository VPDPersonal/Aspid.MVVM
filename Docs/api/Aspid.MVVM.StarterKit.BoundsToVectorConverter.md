---
title: "Class BoundsToVectorConverter"
sidebar_label: "BoundsToVectorConverter"
description: "Class BoundsToVectorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BoundsToVectorConverter {#Aspid_MVVM_StarterKit_BoundsToVectorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads one vector of a bounding box: its middle, its size or its half-size.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Bounds/To Vector", Name = "Bounds To Vector", Tooltip = "Reads the middle, the size or the half-size of a bounding box")]
public sealed class BoundsToVectorConverter : IConverter<Bounds, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BoundsToVectorConverter](Aspid.MVVM.StarterKit.BoundsToVectorConverter.md)

#### Implements

[IConverter\<Bounds, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### BoundsToVectorConverter\(\) {#Aspid_MVVM_StarterKit_BoundsToVectorConverter__ctor}

```csharp
public BoundsToVectorConverter()
```

#### Remarks

Default: reading the middle.

### BoundsToVectorConverter\(BoundsVector\) {#Aspid_MVVM_StarterKit_BoundsToVectorConverter__ctor_Aspid_MVVM_StarterKit_BoundsVector_}

```csharp
public BoundsToVectorConverter(BoundsVector vector)
```

#### Parameters

`vector` [BoundsVector](Aspid.MVVM.StarterKit.BoundsVector.md)

Which vector of the box to read.

## Methods

### Convert\(Bounds\) {#Aspid_MVVM_StarterKit_BoundsToVectorConverter_Convert_UnityEngine_Bounds_}

Reads the configured vector of the specified box.

```csharp
public Vector3 Convert(Bounds value)
```

#### Parameters

`value` Bounds

The box to read.

#### Returns

 Vector3

The middle, the size or the half-size, in the space the bounds were measured in. Reports an
error and returns the middle when the configured vector is not a declared
[`BoundsVector`](Aspid.MVVM.StarterKit.BoundsVector.md) value.

