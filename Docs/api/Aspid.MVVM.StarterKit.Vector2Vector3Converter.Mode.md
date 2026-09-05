---
title: "Enum Vector2Vector3Converter.Mode"
sidebar_label: "Vector2Vector3Converter.Mode"
description: "Enum Vector2Vector3Converter.Mode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum Vector2Vector3Converter.Mode {#Aspid_MVVM_StarterKit_Vector2Vector3Converter_Mode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Specifies which components of the 2D vector to map to the 3D vector. The letters name the
destination axes, in the order the 2D components are read.

```csharp
public enum Vector2Vector3Converter.Mode
```


## Fields

`XY = 0` 

The 2D X goes to X and the 2D Y to Y; the constant fills Z. The mode a new converter
starts in.



`XZ = 1` 

The 2D X goes to X and the 2D Y to Z; the constant fills Y, laying a flat value on the
ground plane.



`YZ = 2` 

The 2D X goes to Y and the 2D Y to Z; the constant fills X.



`YX = 3` 

The 2D X goes to Y and the 2D Y to X; the constant fills Z.



`ZX = 4` 

The 2D X goes to Z and the 2D Y to X; the constant fills Y.



`ZY = 5` 

The 2D X goes to Z and the 2D Y to Y; the constant fills X.



## Remarks

New members are appended: Unity stores the declaration index, so inserting one would repoint serialized fields.

