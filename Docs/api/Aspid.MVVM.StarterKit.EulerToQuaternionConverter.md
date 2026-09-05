---
title: "Class EulerToQuaternionConverter"
sidebar_label: "EulerToQuaternionConverter"
description: "Class EulerToQuaternionConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EulerToQuaternionConverter {#Aspid_MVVM_StarterKit_EulerToQuaternionConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Turns Euler angles into a rotation.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Vector/To Quaternion", Name = "Euler To Quaternion", Tooltip = "Turns Euler angles into a rotation")]
public sealed class EulerToQuaternionConverter : ITwoWayConverter<Vector3, Quaternion>, IConverter<Vector3, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EulerToQuaternionConverter](Aspid.MVVM.StarterKit.EulerToQuaternionConverter.md)

#### Implements

[ITwoWayConverter\<Vector3, Quaternion\>](Aspid.MVVM.StarterKit.ITwoWayConverter-2.md), 
[IConverter\<Vector3, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The pair names the same rotation both ways, but not the same numbers: Unity reports Euler
angles in 0..360, so -10° goes out and 350° comes back.

## Methods

### Convert\(Vector3\) {#Aspid_MVVM_StarterKit_EulerToQuaternionConverter_Convert_UnityEngine_Vector3_}

Turns the specified angles into a rotation.

```csharp
public Quaternion Convert(Vector3 value)
```

#### Parameters

`value` Vector3

The Euler angles, in degrees.

#### Returns

 Quaternion

The rotation.

### ConvertBack\(Quaternion\) {#Aspid_MVVM_StarterKit_EulerToQuaternionConverter_ConvertBack_UnityEngine_Quaternion_}

Reads Euler angles off a rotation.

```csharp
public Vector3 ConvertBack(Quaternion value)
```

#### Parameters

`value` Quaternion

The rotation to read.

#### Returns

 Vector3

The angles, in degrees, each folded into 0..360.

