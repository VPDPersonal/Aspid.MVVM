---
title: "Class AngleDifferenceConverter"
sidebar_label: "AngleDifferenceConverter"
description: "Class AngleDifferenceConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AngleDifferenceConverter {#Aspid_MVVM_StarterKit_AngleDifferenceConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Measures how far an angle is from a fixed one.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Angle Difference", Tooltip = "Measures how far an angle is from a fixed one")]
public sealed class AngleDifferenceConverter : IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AngleDifferenceConverter](Aspid.MVVM.StarterKit.AngleDifferenceConverter.md)

#### Implements

[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### AngleDifferenceConverter\(\) {#Aspid_MVVM_StarterKit_AngleDifferenceConverter__ctor}

```csharp
public AngleDifferenceConverter()
```

#### Remarks

Default: measuring from zero.

### AngleDifferenceConverter\(float, bool\) {#Aspid_MVVM_StarterKit_AngleDifferenceConverter__ctor_System_Single_System_Boolean_}

```csharp
public AngleDifferenceConverter(float reference, bool signed = true)
```

#### Parameters

`reference` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle the bound one is measured against, in degrees.

`signed` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to keep the sign of the difference.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_AngleDifferenceConverter_Convert_System_Single_}

Measures the specified angle against the reference.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The angle, in degrees.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The shortest way around from the reference to it, in degrees.

