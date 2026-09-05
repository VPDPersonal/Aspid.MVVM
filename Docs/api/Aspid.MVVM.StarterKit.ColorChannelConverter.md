---
title: "Class ColorChannelConverter"
sidebar_label: "ColorChannelConverter"
description: "Class ColorChannelConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorChannelConverter {#Aspid_MVVM_StarterKit_ColorChannelConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Applies one arithmetic operation to the chosen channels of a color.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Color", Name = "Channel", Tooltip = "Applies one arithmetic operation to the chosen channels of a color")]
public sealed class ColorChannelConverter : IConverter<Color, Color>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorChannelConverter](Aspid.MVVM.StarterKit.ColorChannelConverter.md)

#### Implements

[IConverter\<Color, Color\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Constructors

### ColorChannelConverter\(\) {#Aspid_MVVM_StarterKit_ColorChannelConverter__ctor}

```csharp
public ColorChannelConverter()
```

#### Remarks

Default: a clamped multiply by white over the color channels, an identity for every
color that already sits inside 0..1.

### ColorChannelConverter\(ChannelOperation, Color, ColorChannels, bool\) {#Aspid_MVVM_StarterKit_ColorChannelConverter__ctor_Aspid_MVVM_StarterKit_ChannelOperation_UnityEngine_Color_Aspid_MVVM_StarterKit_ColorChannels_System_Boolean_}

```csharp
public ColorChannelConverter(ChannelOperation operation, Color operand, ColorChannels channels = ColorChannels.Rgb, bool clamp = true)
```

#### Parameters

`operation` [ChannelOperation](Aspid.MVVM.StarterKit.ChannelOperation.md)

What the operand does to each chosen channel.

`operand` Color

Supplies the operand for each channel.

`channels` [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

Which channels are written. The rest pass through untouched.

`clamp` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to hold every written channel inside 0..1. Clear it for HDR colors, which live
above one.

## Methods

### Convert\(Color\) {#Aspid_MVVM_StarterKit_ColorChannelConverter_Convert_UnityEngine_Color_}

Applies the operation to the chosen channels of the specified color.

```csharp
public Color Convert(Color value)
```

#### Parameters

`value` Color

The color to operate on.

#### Returns

 Color

The color, with the channels outside the mask unchanged. An operation that is not a
declared [`ChannelOperation`](Aspid.MVVM.StarterKit.ChannelOperation.md) value reports an error and leaves the written channels
unchanged too.

