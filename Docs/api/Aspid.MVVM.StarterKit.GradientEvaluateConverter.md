---
title: "Class GradientEvaluateConverter"
sidebar_label: "GradientEvaluateConverter"
description: "Class GradientEvaluateConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class GradientEvaluateConverter {#Aspid_MVVM_StarterKit_GradientEvaluateConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reads a color off a [`Gradient`](https://docs.unity3d.com/ScriptReference/Gradient.html).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number/To Color", Name = "Gradient Evaluate", Tooltip = "Reads a color off a Gradient")]
public sealed class GradientEvaluateConverter : IConverter<float, Color>, IConverter<double, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GradientEvaluateConverter](Aspid.MVVM.StarterKit.GradientEvaluateConverter.md)

#### Implements

[IConverter\<float, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### GradientEvaluateConverter\(\) {#Aspid_MVVM_StarterKit_GradientEvaluateConverter__ctor}

```csharp
public GradientEvaluateConverter()
```

#### Remarks

Default: over 0..1.

### GradientEvaluateConverter\(Gradient, float, float\) {#Aspid_MVVM_StarterKit_GradientEvaluateConverter__ctor_UnityEngine_Gradient_System_Single_System_Single_}

```csharp
public GradientEvaluateConverter(Gradient gradient, float inputMin = 0, float inputMax = 1)
```

#### Parameters

`gradient` Gradient

The gradient the value is read from.

`inputMin` [float](https://learn.microsoft.com/dotnet/api/system.single)

The input value that maps to the start of the gradient. Equal to <code class="paramref">inputMax</code>,
the range is reported as an error and the gradient is read at its start.

`inputMax` [float](https://learn.microsoft.com/dotnet/api/system.single)

The input value that maps to the end of the gradient. Equal to <code class="paramref">inputMin</code>,
the range is reported as an error and the gradient is read at its start.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">gradient</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_GradientEvaluateConverter_Convert_System_Single_}

Reads the color at the specified value.

```csharp
public Color Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value to read at.

#### Returns

 Color

The color there, or white when no gradient is assigned.

