---
title: "Class TextureToSpriteRectConverter"
sidebar_label: "TextureToSpriteRectConverter"
description: "Class TextureToSpriteRectConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TextureToSpriteRectConverter {#Aspid_MVVM_StarterKit_TextureToSpriteRectConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Measures the pixel rect of a texture.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Texture/To Rect", Name = "Sprite Rect", Tooltip = "Measures the pixel rect of a texture")]
public sealed class TextureToSpriteRectConverter : IConverter<Texture?, Rect>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[TextureToSpriteRectConverter](Aspid.MVVM.StarterKit.TextureToSpriteRectConverter.md)

#### Implements

[IConverter\<Texture?, Rect\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

Typed on [`Texture`](https://docs.unity3d.com/ScriptReference/Texture.html) so a [`RenderTexture`](https://docs.unity3d.com/ScriptReference/RenderTexture.html) measures the same way; a
[`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html) field still accepts it, the input is contravariant.

## Methods

### Convert\(Texture?\) {#Aspid_MVVM_StarterKit_TextureToSpriteRectConverter_Convert_UnityEngine_Texture_}

Measures the specified texture.

```csharp
public Rect Convert(Texture? value)
```

#### Parameters

`value` Texture?

The texture to measure.

#### Returns

 Rect

A rect covering the whole texture in pixels, or [`zero`](https://docs.unity3d.com/ScriptReference/Rect-zero.html) when the texture
is missing or destroyed.

