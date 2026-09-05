---
title: "Enum ColorBlendMode"
sidebar_label: "ColorBlendMode"
description: "Enum ColorBlendMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum ColorBlendMode {#Aspid_MVVM_StarterKit_ColorBlendMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How [`ColorTintConverter`](Aspid.MVVM.StarterKit.ColorTintConverter.md) and [`ColorBlockTintConverter`](Aspid.MVVM.StarterKit.ColorBlockTintConverter.md) combine two colors.

```csharp
public enum ColorBlendMode
```


## Fields

`Multiply = 0` 

Multiply each channel, the alpha included, a tint that is not fully opaque fades the
result.



`Add = 1` 

Add the tint to each color channel and hold the sum inside 0..1, keeping the original
alpha.



`Lerp = 2` 

Move toward the tint by the configured amount, the alpha included.



`Replace = 3` 

Replace the color with the tint, keeping the original alpha.



