---
title: "Class FloatToVectorConverter"
sidebar_label: "FloatToVectorConverter"
description: "Class FloatToVectorConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class FloatToVectorConverter {#Aspid_MVVM_StarterKit_FloatToVectorConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes one number into the chosen axes of a vector.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Vector", Name = "Float To Vector", Tooltip = "Writes one number into the chosen axes of a vector")]
public sealed class FloatToVectorConverter : IConverter<float, Vector3>, IConverter<float, Vector2>, IConverter<float, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FloatToVectorConverter](Aspid.MVVM.StarterKit.FloatToVectorConverter.md)

#### Implements

[IConverter\<float, Vector3\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, Vector2\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<float, Vector4\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### FloatToVectorConverter\(\) {#Aspid_MVVM_StarterKit_FloatToVectorConverter__ctor}

```csharp
public FloatToVectorConverter()
```

#### Remarks

Default: writing every axis.

### FloatToVectorConverter\(AxisMask, Vector4\) {#Aspid_MVVM_StarterKit_FloatToVectorConverter__ctor_Aspid_MVVM_StarterKit_AxisMask_UnityEngine_Vector4_}

```csharp
public FloatToVectorConverter(AxisMask axes, Vector4 @base = default)
```

#### Parameters

`axes` [AxisMask](Aspid.MVVM.StarterKit.AxisMask.md)

Which axes the number is written into.

`base` Vector4

The value used for the axes the number does not write, read as far as the bound vector goes.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_FloatToVectorConverter_Convert_System_Single_}

Writes the specified number into the chosen axes.

```csharp
public Vector3 Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The number to write.

#### Returns

 Vector3

The vector.

