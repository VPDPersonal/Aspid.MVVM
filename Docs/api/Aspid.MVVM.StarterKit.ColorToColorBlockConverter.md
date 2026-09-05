---
title: "Class ColorToColorBlockConverter"
sidebar_label: "ColorToColorBlockConverter"
description: "Class ColorToColorBlockConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorToColorBlockConverter {#Aspid_MVVM_StarterKit_ColorToColorBlockConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Builds a full [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) out of one color.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "To Color Block", Tooltip = "Builds a full ColorBlock out of one color")]
public sealed class ColorToColorBlockConverter : IConverter<Color, ColorBlock>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorToColorBlockConverter](Aspid.MVVM.StarterKit.ColorToColorBlockConverter.md)

#### Implements

[IConverter\<Color, ColorBlock\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorToColorBlockConverter\(\) {#Aspid_MVVM_StarterKit_ColorToColorBlockConverter__ctor}

```csharp
public ColorToColorBlockConverter()
```

#### Remarks

Default: the state scaling a fresh [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html) is authored with.

### ColorToColorBlockConverter\(float, float, float, float, float, float, float\) {#Aspid_MVVM_StarterKit_ColorToColorBlockConverter__ctor_System_Single_System_Single_System_Single_System_Single_System_Single_System_Single_System_Single_}

```csharp
public ColorToColorBlockConverter(float highlightedMultiplier, float pressedMultiplier = 0.9, float selectedMultiplier = 1, float disabledMultiplier = 0.5, float disabledAlpha = 0.5, float fadeDuration = 0.1, float colorMultiplier = 1)
```

#### Parameters

`highlightedMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

Scales the color for the highlighted state.

`pressedMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

Scales the color for the pressed state.

`selectedMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

Scales the color for the selected state.

`disabledMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

Scales the color for the disabled state.

`disabledAlpha` [float](https://learn.microsoft.com/dotnet/api/system.single)

The alpha of the disabled state.

`fadeDuration` [float](https://learn.microsoft.com/dotnet/api/system.single)

How long a state change takes, in seconds. A duration that is negative or not a number is
reported as an error and zero is used instead.

`colorMultiplier` [float](https://learn.microsoft.com/dotnet/api/system.single)

The overall multiplier UGUI applies on top. A value outside 1..5 is reported and held to
that range.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorToColorBlockConverter_Convert_UnityEngine_Color_}

Builds a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) from the specified color.

```csharp
public ColorBlock Convert(Color value)
```

#### Parameters

`value` Color

The color the states are derived from.

#### Returns

 ColorBlock

The full block of state colors: the normal state is the bound color as it arrived, the
derived states are held to 0..1. The fade is instant when the configured duration is
negative or not a number, and a multiplier outside 1..5 is held to that range.

