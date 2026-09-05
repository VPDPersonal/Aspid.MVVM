---
title: "Class RawImageExtensions"
sidebar_label: "RawImageExtensions"
description: "Class RawImageExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RawImageExtensions {#Aspid_MVVM_StarterKit_RawImageExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods for [`RawImage`](https://docs.unity3d.com/ScriptReference/UI-RawImage.html) used by the raw image binders.

```csharp
public static class RawImageExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RawImageExtensions](Aspid.MVVM.StarterKit.RawImageExtensions.md)



## Methods

### SetTexture\(RawImage, Texture, bool\) {#Aspid_MVVM_StarterKit_RawImageExtensions_SetTexture_UnityEngine_UI_RawImage_UnityEngine_Texture_System_Boolean_}

Sets [`texture`](https://docs.unity3d.com/ScriptReference/UI-RawImage-texture.html) and, optionally, disables the image while the texture is
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static void SetTexture(this RawImage image, Texture texture, bool disableWhenNull)
```

#### Parameters

`image` RawImage

The image to update.

`texture` Texture

The texture to show, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to clear it.

`disableWhenNull` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether [`enabled`](https://docs.unity3d.com/ScriptReference/Behaviour-enabled.html) follows the presence of a texture.

