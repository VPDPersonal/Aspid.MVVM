---
title: "Class EasingConverter"
sidebar_label: "EasingConverter"
description: "Class EasingConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class EasingConverter {#Aspid_MVVM_StarterKit_EasingConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reshapes a 0..1 value along an easing curve.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Number", Name = "Easing", Tooltip = "Reshapes a 0..1 value along an easing curve")]
public sealed class EasingConverter : IConverter<float, float>, IConverter<double, double>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[EasingConverter](Aspid.MVVM.StarterKit.EasingConverter.md)

#### Implements

[IConverter\<float, float\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter\<double, double\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Clamps what goes in, never what comes out: Back and Elastic overshoot 0..1 on purpose. Evaluated in <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types">float</a>.

## Constructors

### EasingConverter\(\) {#Aspid_MVVM_StarterKit_EasingConverter__ctor}

```csharp
public EasingConverter()
```

#### Remarks

Default: easing out quadratically.

### EasingConverter\(EaseType, bool\) {#Aspid_MVVM_StarterKit_EasingConverter__ctor_Aspid_MVVM_StarterKit_EaseType_System_Boolean_}

```csharp
public EasingConverter(EaseType ease, bool clamp = true)
```

#### Parameters

`ease` [EaseType](Aspid.MVVM.StarterKit.EaseType.md)

The curve applied to the value.

`clamp` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>, holds the incoming value inside 0..1.

## Methods

### Convert\(float\) {#Aspid_MVVM_StarterKit_EasingConverter_Convert_System_Single_}

Eases the specified value.

```csharp
public float Convert(float value)
```

#### Parameters

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The 0..1 position along the curve.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The eased value. An undeclared curve reports an error and returns the value unchanged.

