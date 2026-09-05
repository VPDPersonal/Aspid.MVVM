---
title: "Class AnimationCurveConverter"
sidebar_label: "AnimationCurveConverter"
description: "Class AnimationCurveConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class AnimationCurveConverter {#Aspid_MVVM_StarterKit_AnimationCurveConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Passes a number through an [`AnimationCurve`](https://docs.unity3d.com/ScriptReference/AnimationCurve.html).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Animation Curve", Tooltip = "Passes a number through an AnimationCurve")]
public sealed class AnimationCurveConverter : IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AnimationCurveConverter](Aspid.MVVM.StarterKit.AnimationCurveConverter.md)

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

### AnimationCurveConverter\(\) {#Aspid_MVVM_StarterKit_AnimationCurveConverter__ctor}

```csharp
public AnimationCurveConverter()
```

#### Remarks

Default: with a linear curve.

### AnimationCurveConverter\(AnimationCurve\) {#Aspid_MVVM_StarterKit_AnimationCurveConverter__ctor_UnityEngine_AnimationCurve_}

```csharp
public AnimationCurveConverter(AnimationCurve curve)
```

#### Parameters

`curve` AnimationCurve

The curve the value is passed through. One with no keys is reported as an error and the
value passes through unchanged.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_AnimationCurveConverter_Convert_System_Single_}

Evaluates the curve at the specified value.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to evaluate at.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The curve's value there. A curve with no keys is reported as an error and the input passes
through unchanged.

