---
title: "Enum VectorComponent"
sidebar_label: "VectorComponent"
description: "Enum VectorComponent — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum VectorComponent {#Aspid_MVVM_StarterKit_VectorComponent}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What [`VectorToFloatConverter`](Aspid.MVVM.StarterKit.VectorToFloatConverter.md) measures. A narrower vector carries fewer of them.

```csharp
public enum VectorComponent
```


## Fields

`X = 0` 

The X axis.



`Y = 1` 

The Y axis.



`Z = 2` 

The Z axis.



`Magnitude = 3` 

The length of the vector.



`SqrMagnitude = 4` 

The squared length, which needs no square root.



`Dot = 5` 

How far the vector reaches along an authored direction.



`W = 6` 

The W component, which only a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) carries.



