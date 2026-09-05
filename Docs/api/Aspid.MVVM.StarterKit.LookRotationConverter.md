---
title: "Class LookRotationConverter"
sidebar_label: "LookRotationConverter"
description: "Class LookRotationConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LookRotationConverter {#Aspid_MVVM_StarterKit_LookRotationConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Builds a rotation that looks along a direction.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/To Quaternion", Name = "Look Rotation", Tooltip = "Builds a rotation that looks along a direction")]
public sealed class LookRotationConverter : IConverter<Vector3, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LookRotationConverter](Aspid.MVVM.StarterKit.LookRotationConverter.md)

#### Implements

[IConverter\<Vector3, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### LookRotationConverter\(\) {#Aspid_MVVM_StarterKit_LookRotationConverter__ctor}

```csharp
public LookRotationConverter()
```

#### Remarks

Default: with world up.

### LookRotationConverter\(Vector3, bool\) {#Aspid_MVVM_StarterKit_LookRotationConverter__ctor_UnityEngine_Vector3_System_Boolean_}

```csharp
public LookRotationConverter(Vector3 up, bool flatten = false)
```

#### Parameters

`up` Vector3

Which way is up for the produced rotation. A zero vector reports an error and world up is
used.

`flatten` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to drop the vertical component before looking.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_LookRotationConverter_Convert_UnityEngine_Vector3_}

Builds a rotation looking along the specified direction.

```csharp
public Quaternion Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The direction to look along.

#### Returns

 Quaternion

The rotation, or the identity for a zero-length direction.

