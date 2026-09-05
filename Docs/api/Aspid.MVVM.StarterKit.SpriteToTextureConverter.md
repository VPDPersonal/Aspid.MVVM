---
title: "Class SpriteToTextureConverter"
sidebar_label: "SpriteToTextureConverter"
description: "Class SpriteToTextureConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SpriteToTextureConverter {#Aspid_MVVM_StarterKit_SpriteToTextureConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Takes the texture a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html) is drawn from.

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Texture", Name = "Sprite To Texture", Tooltip = "Takes the texture a Sprite is drawn from")]
public sealed class SpriteToTextureConverter : IConverter<Sprite?, Texture?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SpriteToTextureConverter](Aspid.MVVM.StarterKit.SpriteToTextureConverter.md)

#### Implements

[IConverter\<Sprite?, Texture?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Methods

### Convert\(Sprite?\) {#Aspid_MVVM_StarterKit_SpriteToTextureConverter_Convert_UnityEngine_Sprite_}

Takes the texture of the specified sprite.

```csharp
public Texture? Convert(Sprite? value)
```

#### Parameters

`value` Sprite?

The sprite to read.

#### Returns

 Texture?

Its texture, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the sprite is missing or destroyed.

