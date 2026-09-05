---
title: "Enum AxisMask"
sidebar_label: "AxisMask"
description: "Enum AxisMask — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum AxisMask {#Aspid_MVVM_StarterKit_AxisMask}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Which axes a converter writes a number into.

```csharp
[Flags]
public enum AxisMask
```


## Fields

`None = 0` 

No axis.



`X = 1` 

The X axis.



`Y = 2` 

The Y axis.



`Z = 4` 

The Z axis.



`W = 8` 

The W axis, which only a [`Vector4`](https://docs.unity3d.com/ScriptReference/Vector4.html) carries.



`All = 15` 

Every axis, so the value is uniform.



