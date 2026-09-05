---
title: "Class LineRendererExtensions"
sidebar_label: "LineRendererExtensions"
description: "Class LineRendererExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class LineRendererExtensions {#Aspid_MVVM_StarterKit_LineRendererExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Extension methods for [`LineRenderer`](https://docs.unity3d.com/ScriptReference/LineRenderer.html) used by the line renderer binders.

```csharp
public static class LineRendererExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LineRendererExtensions](Aspid.MVVM.StarterKit.LineRendererExtensions.md)



## Methods

### GetColor\(LineRenderer, LineRendererColorMode\) {#Aspid_MVVM_StarterKit_LineRendererExtensions_GetColor_UnityEngine_LineRenderer_Aspid_MVVM_StarterKit_LineRendererColorMode_}

Reads the end color selected by <code class="paramref">mode</code>; [`LineRendererColorMode.StartAndEnd`](Aspid.MVVM.StarterKit.LineRendererColorMode.md)
reads the start color.

```csharp
public static Color GetColor(this LineRenderer lineRenderer, LineRendererColorMode mode)
```

#### Parameters

`lineRenderer` LineRenderer

The renderer to read.

`mode` [LineRendererColorMode](Aspid.MVVM.StarterKit.LineRendererColorMode.md)

Which end color to read.

#### Returns

 Color

The selected color.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">mode</code> is not a known value.

### SetColor\(LineRenderer, Color, LineRendererColorMode\) {#Aspid_MVVM_StarterKit_LineRendererExtensions_SetColor_UnityEngine_LineRenderer_UnityEngine_Color_Aspid_MVVM_StarterKit_LineRendererColorMode_}

Writes [`startColor`](https://docs.unity3d.com/ScriptReference/LineRenderer-startColor.html), [`endColor`](https://docs.unity3d.com/ScriptReference/LineRenderer-endColor.html) or both.

```csharp
public static void SetColor(this LineRenderer lineRenderer, Color value, LineRendererColorMode mode)
```

#### Parameters

`lineRenderer` LineRenderer

The renderer to update.

`value` Color

The color to apply.

`mode` [LineRendererColorMode](Aspid.MVVM.StarterKit.LineRendererColorMode.md)

Which end colors <code class="paramref">value</code> writes.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">mode</code> is not a known value.

