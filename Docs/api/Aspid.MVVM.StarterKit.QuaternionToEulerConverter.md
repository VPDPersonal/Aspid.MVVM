---
title: "Class QuaternionToEulerConverter"
sidebar_label: "QuaternionToEulerConverter"
description: "Class QuaternionToEulerConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class QuaternionToEulerConverter {#Aspid_MVVM_StarterKit_QuaternionToEulerConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads Euler angles off a rotation.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Quaternion/To Vector", Name = "To Euler", Tooltip = "Reads Euler angles off a rotation")]
public sealed class QuaternionToEulerConverter : IConverter<Quaternion, Vector3>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[QuaternionToEulerConverter](Aspid.MVVM.StarterKit.QuaternionToEulerConverter.md)

#### Implements

[IConverter\<Quaternion, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Unity reports Euler angles in 0..360, so an un-normalized read gives 359 where -1 is meant.

## Constructors

### QuaternionToEulerConverter\(\) {#Aspid_MVVM_StarterKit_QuaternionToEulerConverter__ctor}

```csharp
public QuaternionToEulerConverter()
```

#### Remarks

Default: normalizing to ±180.

### QuaternionToEulerConverter\(bool\) {#Aspid_MVVM_StarterKit_QuaternionToEulerConverter__ctor_System_Boolean_}

```csharp
public QuaternionToEulerConverter(bool normalizeToSigned180)
```

#### Parameters

`normalizeToSigned180` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to report angles as -180..180.

## Methods

### Convert\(Quaternion\) {#Aspid_MVVM_StarterKit_QuaternionToEulerConverter_Convert_UnityEngine_Quaternion_}

Reads the angles off the specified rotation.

```csharp
public Vector3 Convert(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to read.

#### Returns

 Vector3

The angles, in degrees.

