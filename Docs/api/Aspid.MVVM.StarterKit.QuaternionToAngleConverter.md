---
title: "Class QuaternionToAngleConverter"
sidebar_label: "QuaternionToAngleConverter"
description: "Class QuaternionToAngleConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class QuaternionToAngleConverter {#Aspid_MVVM_StarterKit_QuaternionToAngleConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads the angle a rotation carries around one axis.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Quaternion/To Number", Name = "To Angle", Tooltip = "Reads the angle a rotation carries around one axis")]
public sealed class QuaternionToAngleConverter : IConverter<Quaternion, float>, IConverter<Quaternion, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[QuaternionToAngleConverter](Aspid.MVVM.StarterKit.QuaternionToAngleConverter.md)

#### Implements

[IConverter\<Quaternion, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<Quaternion, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The angle is folded into 0..360, or ±180 when signed.

## Constructors

### QuaternionToAngleConverter\(\) {#Aspid_MVVM_StarterKit_QuaternionToAngleConverter__ctor}

```csharp
public QuaternionToAngleConverter()
```

#### Remarks

Default: reading Z.

### QuaternionToAngleConverter\(RotationAxis, bool\) {#Aspid_MVVM_StarterKit_QuaternionToAngleConverter__ctor_Aspid_MVVM_StarterKit_RotationAxis_System_Boolean_}

```csharp
public QuaternionToAngleConverter(RotationAxis axis, bool signed = true)
```

#### Parameters

`axis` [RotationAxis](Aspid.MVVM.StarterKit.RotationAxis.md)

The axis the angle is read around.

`signed` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to report the angle as -180..180.

### QuaternionToAngleConverter\(Vector3, bool\) {#Aspid_MVVM_StarterKit_QuaternionToAngleConverter__ctor_UnityEngine_Vector3_System_Boolean_}

```csharp
public QuaternionToAngleConverter(Vector3 customAxis, bool signed = true)
```

#### Parameters

`customAxis` Vector3

The axis the angle is read around. A zero vector reports an error and the angle reads zero.

`signed` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to report the angle as -180..180.

## Methods

### Convert\(Quaternion\) {#Aspid_MVVM_StarterKit_QuaternionToAngleConverter_Convert_UnityEngine_Quaternion_}

Reads the angle off the specified rotation.

```csharp
public float Convert(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to read.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees. A zero custom axis, or an axis that is not a declared
[`RotationAxis`](Aspid.MVVM.StarterKit.RotationAxis.md), reports an error and reads zero.

