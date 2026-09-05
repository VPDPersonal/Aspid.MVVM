---
title: "Class GraphicExtensions"
sidebar_label: "GraphicExtensions"
description: "Class GraphicExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class GraphicExtensions {#Aspid_MVVM_StarterKit_GraphicExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Per-channel access to [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html). An empty channel mask is reported as a configuration error.

```csharp
public static class GraphicExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GraphicExtensions](Aspid.MVVM.StarterKit.GraphicExtensions.md)



## Methods

### GetColorChannel\(Graphic, ColorChannels\) {#Aspid_MVVM_StarterKit_GraphicExtensions_GetColorChannel_UnityEngine_UI_Graphic_Aspid_MVVM_StarterKit_ColorChannels_}

Returns the first selected channel of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html), in the order R, G, B, A.

```csharp
public static float GetColorChannel(this Graphic graphic, ColorChannels channels)
```

#### Parameters

`graphic` Graphic

The graphic to read.

`channels` [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

The channels to choose from.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The channel value, or <code>0</code> when no channel is selected.

### SetColorChannels\(Graphic, ColorChannels, float\) {#Aspid_MVVM_StarterKit_GraphicExtensions_SetColorChannels_UnityEngine_UI_Graphic_Aspid_MVVM_StarterKit_ColorChannels_System_Single_}

Sets the selected channels of [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html) to <code class="paramref">value</code>.

```csharp
public static void SetColorChannels(this Graphic graphic, ColorChannels channels, float value)
```

#### Parameters

`graphic` Graphic

The graphic to write.

`channels` [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

The channels to write.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The channel value.

