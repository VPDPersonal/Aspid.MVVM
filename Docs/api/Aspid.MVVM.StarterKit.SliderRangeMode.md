---
title: "Enum SliderRangeMode"
sidebar_label: "SliderRangeMode"
description: "Enum SliderRangeMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum SliderRangeMode {#Aspid_MVVM_StarterKit_SliderRangeMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Specifies which [`Slider`](https://docs.unity3d.com/ScriptReference/UI-Slider.html) endpoints a bound [`Vector2`](https://docs.unity3d.com/ScriptReference/Vector2.html) writes.

```csharp
public enum SliderRangeMode
```


## Fields

`Min = 0` 

Only [`minValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-minValue.html), from <code>x</code>.



`Max = 1` 

Only [`maxValue`](https://docs.unity3d.com/ScriptReference/UI-Slider-maxValue.html), from <code>y</code>.



`Range = 2` 

Both endpoints.



