---
title: "Class Texture2DToSpriteConverter"
sidebar_label: "Texture2DToSpriteConverter"
description: "Class Texture2DToSpriteConverter — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class Texture2DToSpriteConverter {#Aspid_MVVM_StarterKit_Texture2DToSpriteConverter}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Wraps a [`Texture2D`](https://docs.unity3d.com/ScriptReference/Texture2D.html) in a [`Sprite`](https://docs.unity3d.com/ScriptReference/Sprite.html).

```csharp
[Serializable]
[TypeSelectorDisplay(Group = "Aspid/Texture", Name = "Texture2D To Sprite", Tooltip = "Wraps a Texture2D in a Sprite")]
public sealed class Texture2DToSpriteConverter : IConverter<Texture2D?, Sprite?>, IConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Texture2DToSpriteConverter](Aspid.MVVM.StarterKit.Texture2DToSpriteConverter.md)

#### Implements

[IConverter\<Texture2D?, Sprite?\>](Aspid.MVVM.StarterKit.IConverter-2.md), 
[IConverter](Aspid.MVVM.StarterKit.IConverter.md)


#### Extension Methods

[ConverterLogger.Log\(IConverter, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_Log_Aspid_MVVM_StarterKit_IConverter_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, string, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_String_System_String_UnityEngine_Object_), 
[ConverterLogger.LogError\(IConverter, Exception, string, Object?\)](Aspid.MVVM.StarterKit.ConverterLogger.md#Aspid_MVVM_StarterKit_ConverterLogger_LogError_Aspid_MVVM_StarterKit_IConverter_System_Exception_System_String_UnityEngine_Object_), 
[ConverterFallbackExtensions.UseFallback\<T\>\(IConverter, T, string\)](Aspid.MVVM.StarterKit.ConverterFallbackExtensions.md#Aspid_MVVM_StarterKit_ConverterFallbackExtensions_UseFallback__1_Aspid_MVVM_StarterKit_IConverter___0_System_String_)

## Remarks

The sprite is owned by the converter: it is cached against its texture, since
[`Create`](https://docs.unity3d.com/ScriptReference/Sprite-Create.html) allocates and a binder pushes on every
notification, and destroyed once the texture changes.

## Constructors

### Texture2DToSpriteConverter\(\) {#Aspid_MVVM_StarterKit_Texture2DToSpriteConverter__ctor}

```csharp
public Texture2DToSpriteConverter()
```

#### Remarks

Default: a centered pivot at 100 pixels per unit, the same as a fresh sprite import.

### Texture2DToSpriteConverter\(Vector2, float\) {#Aspid_MVVM_StarterKit_Texture2DToSpriteConverter__ctor_UnityEngine_Vector2_System_Single_}

```csharp
public Texture2DToSpriteConverter(Vector2 pivot, float pixelsPerUnit = 100)
```

#### Parameters

`pivot` Vector2

Where the sprite's pivot sits, in normalized coordinates.

`pixelsPerUnit` [float](https://learn.microsoft.com/dotnet/api/system.single)

How many texture pixels make up one world unit. A value that is not above zero is reported
as an error and 100 is used instead.

## Methods

### Convert\(Texture2D?\) {#Aspid_MVVM_StarterKit_Texture2DToSpriteConverter_Convert_UnityEngine_Texture2D_}

Wraps the specified texture in a sprite.

```csharp
public Sprite? Convert(Texture2D? value)
```

#### Parameters

`value` Texture2D?

The texture to wrap.

#### Returns

 Sprite?

A sprite covering the whole texture, reused while the texture is unchanged, or
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the texture is missing or destroyed. The previously returned
sprite is destroyed on the way, so a caller holding on to it is left with nothing.

