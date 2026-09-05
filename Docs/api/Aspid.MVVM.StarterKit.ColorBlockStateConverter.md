---
title: "Class ColorBlockStateConverter"
sidebar_label: "ColorBlockStateConverter"
description: "Class ColorBlockStateConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorBlockStateConverter {#Aspid_MVVM_StarterKit_ColorBlockStateConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Writes one authored color into the chosen states of a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Color Block State", Tooltip = "Writes one authored color into the chosen states of a ColorBlock")]
public sealed class ColorBlockStateConverter : IConverter<ColorBlock, ColorBlock>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorBlockStateConverter](Aspid.MVVM.StarterKit.ColorBlockStateConverter.md)

#### Implements

[IConverter\<ColorBlock, ColorBlock\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The states are a mask rather than a single choice, so one converter can pin several states
to the same color.

## Constructors

### ColorBlockStateConverter\(\) {#Aspid_MVVM_StarterKit_ColorBlockStateConverter__ctor}

```csharp
public ColorBlockStateConverter()
```

#### Remarks

Default: gray into [`SelectableStates.Disabled`](Aspid.MVVM.StarterKit.SelectableStates.md) alone.

### ColorBlockStateConverter\(SelectableStates, Color\) {#Aspid_MVVM_StarterKit_ColorBlockStateConverter__ctor_Aspid_MVVM_StarterKit_SelectableStates_UnityEngine_Color_}

```csharp
public ColorBlockStateConverter(SelectableStates states, Color color)
```

#### Parameters

`states` [SelectableStates](Aspid.MVVM.StarterKit.SelectableStates.md)

Which states the color is written into. The rest pass through untouched.

`color` Color

The color written into the chosen states.

## Methods

### Convert\(ColorBlock\) {#Aspid_MVVM_StarterKit_ColorBlockStateConverter_Convert_UnityEngine_UI_ColorBlock_}

Writes the authored color into the chosen states of the specified block.

```csharp
public ColorBlock Convert(ColorBlock value)
```

#### Parameters

`value` ColorBlock

The block to override.

#### Returns

 ColorBlock

The block, with the states outside the mask unchanged.

