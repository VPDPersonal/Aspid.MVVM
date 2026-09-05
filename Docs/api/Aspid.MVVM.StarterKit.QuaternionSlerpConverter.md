---
title: "Class QuaternionSlerpConverter"
sidebar_label: "QuaternionSlerpConverter"
description: "Class QuaternionSlerpConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class QuaternionSlerpConverter {#Aspid_MVVM_StarterKit_QuaternionSlerpConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Turns between two rotations by a 0..1 amount.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Quaternion", Name = "Slerp", Tooltip = "Turns between two rotations by a 0..1 amount")]
public sealed class QuaternionSlerpConverter : IConverter<float, Quaternion>, IConverter<double, Quaternion>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[QuaternionSlerpConverter](Aspid.MVVM.StarterKit.QuaternionSlerpConverter.md)

#### Implements

[IConverter\<float, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, Quaternion\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### QuaternionSlerpConverter\(\) {#Aspid_MVVM_StarterKit_QuaternionSlerpConverter__ctor}

```csharp
public QuaternionSlerpConverter()
```

#### Remarks

Default: both endpoints are identity, so every amount reads as no rotation.

### QuaternionSlerpConverter\(Vector3, Vector3, AnimationCurve?\) {#Aspid_MVVM_StarterKit_QuaternionSlerpConverter__ctor_UnityEngine_Vector3_UnityEngine_Vector3_UnityEngine_AnimationCurve_}

```csharp
public QuaternionSlerpConverter(Vector3 fromEuler, Vector3 toEuler, AnimationCurve? curve = null)
```

#### Parameters

`fromEuler` Vector3

The rotation at 0, in Euler degrees.

`toEuler` Vector3

The rotation at 1, in Euler degrees.

`curve` AnimationCurve?

Shapes the amount before the turn, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for an even sweep.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_QuaternionSlerpConverter_Convert_System_Single_}

Reads the rotation at the specified amount.

```csharp
public Quaternion Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 amount.

#### Returns

 Quaternion

The rotation there.

