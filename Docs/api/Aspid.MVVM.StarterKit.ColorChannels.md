---
title: "Enum ColorChannels"
sidebar_label: "ColorChannels"
description: "Enum ColorChannels — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum ColorChannels {#Aspid_MVVM_StarterKit_ColorChannels}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Channels of a [`Color`](https://docs.unity3d.com/ScriptReference/Color.html) a binder or converter writes.

```csharp
[Flags]
public enum ColorChannels
```

#### Extension Methods

[ColorChannelsExtensions.SelectsAny\(ColorChannels\)](Aspid.MVVM.StarterKit.ColorChannelsExtensions.md#Aspid_MVVM_StarterKit_ColorChannelsExtensions_SelectsAny_Aspid_MVVM_StarterKit_ColorChannels_)

## Fields

`None = 0` 

No channel.



`R = 1` 

The red channel.



`G = 2` 

The green channel.



`B = 4` 

The blue channel.



`A = 8` 

The alpha channel.



`Rgb = 7` 

The three color channels, leaving the alpha alone.



`All = 15` 

Every channel.



