---
title: "Struct ColorStop"
sidebar_label: "ColorStop"
description: "Struct ColorStop — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct ColorStop {#Aspid_MVVM_StarterKit_ColorStop}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

One color of a threshold color scale.

```csharp
[Serializable]
public struct ColorStop
```



## Constructors

### ColorStop\(float, Color\) {#Aspid_MVVM_StarterKit_ColorStop__ctor_System_Single_UnityEngine_Color_}

```csharp
public ColorStop(float threshold, Color color)
```

#### Parameters

`threshold` [float](https://learn.microsoft.com/dotnet/api/system.single)

The value at or above which this color applies.

`color` Color

The color used from the threshold up.

## Properties

### Color {#Aspid_MVVM_StarterKit_ColorStop_Color}

Gets the color used from [`ColorStop.Threshold`](Aspid.MVVM.StarterKit.ColorStop.md#Aspid_MVVM_StarterKit_ColorStop_Threshold) up.

```csharp
public readonly Color Color { get; }
```

#### Property Value

 Color

### Threshold {#Aspid_MVVM_StarterKit_ColorStop_Threshold}

Gets the value at or above which this color applies.

```csharp
public readonly float Threshold { get; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

