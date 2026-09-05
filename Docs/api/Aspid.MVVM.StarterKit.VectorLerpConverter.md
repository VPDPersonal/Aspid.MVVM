---
title: "Class VectorLerpConverter"
sidebar_label: "VectorLerpConverter"
description: "Class VectorLerpConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class VectorLerpConverter {#Aspid_MVVM_StarterKit_VectorLerpConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Moves between two vectors by a 0..1 amount.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Vector", Name = "Lerp", Tooltip = "Moves between two vectors by a 0..1 amount")]
public sealed class VectorLerpConverter : IConverter<float, Vector3>, IConverter<float, Vector2>, IConverter<float, Vector4>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VectorLerpConverter](Aspid.MVVM.StarterKit.VectorLerpConverter.md)

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

### VectorLerpConverter\(\) {#Aspid_MVVM_StarterKit_VectorLerpConverter__ctor}

```csharp
public VectorLerpConverter()
```

#### Remarks

Default: going zero to one.

### VectorLerpConverter\(Vector4, Vector4, AnimationCurve?\) {#Aspid_MVVM_StarterKit_VectorLerpConverter__ctor_UnityEngine_Vector4_UnityEngine_Vector4_UnityEngine_AnimationCurve_}

```csharp
public VectorLerpConverter(Vector4 from, Vector4 to, AnimationCurve? curve = null)
```

#### Parameters

`from` Vector4

The vector at 0. Only the components the bound vector carries are read.

`to` Vector4

The vector at 1. Only the components the bound vector carries are read.

`curve` AnimationCurve?

Shapes the amount before the move, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for an even one.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_VectorLerpConverter_Convert_System_Single_}

Reads the vector at the specified amount.

```csharp
public Vector3 Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 amount.

#### Returns

 Vector3

The vector there.

