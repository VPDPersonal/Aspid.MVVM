---
title: "Class ColorChannelsExtensions"
sidebar_label: "ColorChannelsExtensions"
description: "Class ColorChannelsExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorChannelsExtensions {#Aspid_MVVM_StarterKit_ColorChannelsExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Per-channel access to a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) by [`ColorChannels`](Aspid.MVVM.StarterKit.ColorChannels.md).

```csharp
public static class ColorChannelsExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorChannelsExtensions](Aspid.MVVM.StarterKit.ColorChannelsExtensions.md)



## Methods

### Get\(Color, ColorChannels\) {#Aspid_MVVM_StarterKit_ColorChannelsExtensions_Get_UnityEngine_Color_Aspid_MVVM_StarterKit_ColorChannels_}

Returns the first selected channel of <code class="paramref">color</code>, in the order R, G, B, A.

```csharp
public static float Get(this Color color, ColorChannels channels)
```

#### Parameters

`color` Color

The color to read.

`channels` [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

The channels to choose from.

#### Returns

 [float](https://learn.microsoft.com/dotnet/api/system.single)

The channel value, or <code>0</code> when no declared channel is selected.

### SelectsAny\(ColorChannels\) {#Aspid_MVVM_StarterKit_ColorChannelsExtensions_SelectsAny_Aspid_MVVM_StarterKit_ColorChannels_}

Indicates whether <code class="paramref">channels</code> selects at least one declared channel.

```csharp
public static bool SelectsAny(this ColorChannels channels)
```

#### Parameters

`channels` [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

The channel mask.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when a declared channel is selected; otherwise <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### With\(Color, ColorChannels, float\) {#Aspid_MVVM_StarterKit_ColorChannelsExtensions_With_UnityEngine_Color_Aspid_MVVM_StarterKit_ColorChannels_System_Single_}

Returns <code class="paramref">color</code> with every selected channel set to <code class="paramref">value</code>.

```csharp
public static Color With(this Color color, ColorChannels channels, float value)
```

#### Parameters

`color` Color

The color to copy.

`channels` [ColorChannels](Aspid.MVVM.StarterKit.ColorChannels.md)

The channels to write.

`value` [float](https://learn.microsoft.com/dotnet/api/system.single)

The channel value.

#### Returns

 Color

The color with the selected channels replaced.

