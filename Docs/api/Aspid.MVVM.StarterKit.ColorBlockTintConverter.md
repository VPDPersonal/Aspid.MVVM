---
title: "Class ColorBlockTintConverter"
sidebar_label: "ColorBlockTintConverter"
description: "Class ColorBlockTintConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorBlockTintConverter {#Aspid_MVVM_StarterKit_ColorBlockTintConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Tints the chosen colors of a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Color Block Tint", Tooltip = "Tints the chosen colors of a ColorBlock")]
public sealed class ColorBlockTintConverter : IConverter<ColorBlock, ColorBlock>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorBlockTintConverter](Aspid.MVVM.StarterKit.ColorBlockTintConverter.md)

#### Implements

[IConverter\<ColorBlock, ColorBlock\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorBlockTintConverter\(\) {#Aspid_MVVM_StarterKit_ColorBlockTintConverter__ctor}

```csharp
public ColorBlockTintConverter()
```

#### Remarks

Default: a multiply by white over every state, which changes nothing.

### ColorBlockTintConverter\(Color, ColorBlendMode, SelectableStates, float\) {#Aspid_MVVM_StarterKit_ColorBlockTintConverter__ctor_UnityEngine_Color_Aspid_MVVM_StarterKit_ColorBlendMode_Aspid_MVVM_StarterKit_SelectableStates_System_Single_}

```csharp
public ColorBlockTintConverter(Color tint, ColorBlendMode blend = ColorBlendMode.Multiply, SelectableStates states = SelectableStates.All, float amount = 1)
```

#### Parameters

`tint` Color

The color the chosen states are combined with.

`blend` [ColorBlendMode](Aspid.MVVM.StarterKit.ColorBlendMode.md)

How the two are combined.

`states` [SelectableStates](Aspid.MVVM.StarterKit.SelectableStates.md)

Which states are tinted. The rest pass through untouched.

`amount` [float](https://learn.microsoft.com/dotnet/api/system.single)

How far toward the tint to move, for [`ColorBlendMode.Lerp`](Aspid.MVVM.StarterKit.ColorBlendMode.md).

## Methods

### Convert\(ColorBlock\) {#Aspid_MVVM_StarterKit_ColorBlockTintConverter_Convert_UnityEngine_UI_ColorBlock_}

Tints the chosen states of the specified block.

```csharp
public ColorBlock Convert(ColorBlock value)
```

#### Parameters

`value` ColorBlock

The block to tint.

#### Returns

 ColorBlock

The tinted block, with the states outside the mask unchanged. A blend that is not a
declared [`ColorBlendMode`](Aspid.MVVM.StarterKit.ColorBlendMode.md) value reports an error and the colors pass through
untinted.

