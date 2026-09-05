---
title: "Class ColorBlockFadeDurationConverter"
sidebar_label: "ColorBlockFadeDurationConverter"
description: "Class ColorBlockFadeDurationConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorBlockFadeDurationConverter {#Aspid_MVVM_StarterKit_ColorBlockFadeDurationConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Sets how long a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) takes to fade between states.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Color Block Fade Duration", Tooltip = "Sets how long a ColorBlock takes to fade between states")]
public sealed class ColorBlockFadeDurationConverter : IConverter<ColorBlock, ColorBlock>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorBlockFadeDurationConverter](Aspid.MVVM.StarterKit.ColorBlockFadeDurationConverter.md)

#### Implements

[IConverter\<ColorBlock, ColorBlock\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorBlockFadeDurationConverter\(\) {#Aspid_MVVM_StarterKit_ColorBlockFadeDurationConverter__ctor}

```csharp
public ColorBlockFadeDurationConverter()
```

#### Remarks

Default: a tenth of a second, the same as a fresh [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html).

### ColorBlockFadeDurationConverter\(float\) {#Aspid_MVVM_StarterKit_ColorBlockFadeDurationConverter__ctor_System_Single_}

```csharp
public ColorBlockFadeDurationConverter(float fadeDuration)
```

#### Parameters

`fadeDuration` [float](https://learn.microsoft.com/dotnet/api/system.single)

How long a state change takes, in seconds. A duration that is negative or not a number is
reported as an error and zero is used instead.

## Methods

### Convert\(ColorBlock\) {#Aspid_MVVM_StarterKit_ColorBlockFadeDurationConverter_Convert_UnityEngine_UI_ColorBlock_}

Sets the fade duration of the specified block.

```csharp
public ColorBlock Convert(ColorBlock value)
```

#### Parameters

`value` ColorBlock

The block to adjust.

#### Returns

 ColorBlock

The adjusted block, or the block with an instant fade when the configured duration is
negative or not a number.

