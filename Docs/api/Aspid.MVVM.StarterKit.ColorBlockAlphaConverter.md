---
title: "Class ColorBlockAlphaConverter"
sidebar_label: "ColorBlockAlphaConverter"
description: "Class ColorBlockAlphaConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorBlockAlphaConverter {#Aspid_MVVM_StarterKit_ColorBlockAlphaConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Changes the alpha of every color in a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Color Block Alpha", Tooltip = "Changes the alpha of every color in a ColorBlock")]
public sealed class ColorBlockAlphaConverter : IConverter<ColorBlock, ColorBlock>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorBlockAlphaConverter](Aspid.MVVM.StarterKit.ColorBlockAlphaConverter.md)

#### Implements

[IConverter\<ColorBlock, ColorBlock\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorBlockAlphaConverter\(\) {#Aspid_MVVM_StarterKit_ColorBlockAlphaConverter__ctor}

```csharp
public ColorBlockAlphaConverter()
```

#### Remarks

Default: scaling every state's alpha by one, which changes nothing.

### ColorBlockAlphaConverter\(float, AlphaMode\) {#Aspid_MVVM_StarterKit_ColorBlockAlphaConverter__ctor_System_Single_Aspid_MVVM_StarterKit_AlphaMode_}

```csharp
public ColorBlockAlphaConverter(float alpha, AlphaMode mode = AlphaMode.Multiply)
```

#### Parameters

`alpha` [float](https://learn.microsoft.com/dotnet/api/system.single)

The alpha applied to every state. The result is held to 0..1 whichever mode is used.

`mode` [AlphaMode](Aspid.MVVM.StarterKit.AlphaMode.md)

How the alpha is applied.

## Methods

### Convert\(ColorBlock\) {#Aspid_MVVM_StarterKit_ColorBlockAlphaConverter_Convert_UnityEngine_UI_ColorBlock_}

Changes the alpha of every state of the specified block.

```csharp
public ColorBlock Convert(ColorBlock value)
```

#### Parameters

`value` ColorBlock

The block to adjust.

#### Returns

 ColorBlock

The adjusted block, every alpha held to 0..1. A mode that is not a declared
[`AlphaMode`](Aspid.MVVM.StarterKit.AlphaMode.md) value reports an error and every alpha is left as it arrived.

