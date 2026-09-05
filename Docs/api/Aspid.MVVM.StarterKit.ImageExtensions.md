---
title: "Class ImageExtensions"
sidebar_label: "ImageExtensions"
description: "Class ImageExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ImageExtensions {#Aspid_MVVM_StarterKit_ImageExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods for [`Image`](https://docs.unity3d.com/ScriptReference/UI-Image.html) used by the image binders.

```csharp
public static class ImageExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ImageExtensions](Aspid.MVVM.StarterKit.ImageExtensions.md)



## Methods

### SetSprite\(Image, Sprite, bool\) {#Aspid_MVVM_StarterKit_ImageExtensions_SetSprite_UnityEngine_UI_Image_UnityEngine_Sprite_System_Boolean_}

Sets [`sprite`](https://docs.unity3d.com/ScriptReference/UI-Image-sprite.html) and, optionally, disables the image while the sprite is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static void SetSprite(this Image image, Sprite sprite, bool disableWhenNull)
```

#### Parameters

`image` Image

The image to update.

`sprite` Sprite

The sprite to show, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear it.

`disableWhenNull` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether [`enabled`](https://docs.unity3d.com/ScriptReference/Behaviour-enabled.html) follows the presence of a sprite.

