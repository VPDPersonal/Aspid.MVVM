---
title: "Enum BoundsVector"
sidebar_label: "BoundsVector"
description: "Enum BoundsVector — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum BoundsVector {#Aspid_MVVM_StarterKit_BoundsVector}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Which vector of a bounding box [`BoundsToVectorConverter`](Aspid.MVVM.StarterKit.BoundsToVectorConverter.md) reads.

```csharp
public enum BoundsVector
```


## Fields

`Center = 0` 

The middle of the box.



`Size = 1` 

The full size of the box.



`Extents = 2` 

The half-size, which is what a radius or an offset from the middle wants.



