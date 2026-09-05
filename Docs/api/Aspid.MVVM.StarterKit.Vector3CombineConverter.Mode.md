---
title: "Enum Vector3CombineConverter.Mode"
sidebar_label: "Vector3CombineConverter.Mode"
description: "Enum Vector3CombineConverter.Mode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum Vector3CombineConverter.Mode {#Aspid_MVVM_StarterKit_Vector3CombineConverter_Mode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Specifies which components to take from the first vector when combining.

```csharp
public enum Vector3CombineConverter.Mode
```


## Fields

`X = 0` 

Takes X from the bound vector; Y and Z stay at the reference vector's.



`Y = 1` 

Takes Y from the bound vector; X and Z stay at the reference vector's.



`Z = 2` 

Takes Z from the bound vector; X and Y stay at the reference vector's.



`XY = 3` 

Takes X and Y from the bound vector; Z stays at the reference vector's.



`XZ = 4` 

Takes X and Z from the bound vector; Y stays at the reference vector's.



`YZ = 5` 

Takes Y and Z from the bound vector; X stays at the reference vector's.



`XYZ = 6` 

Takes all three components from the bound vector, leaving the reference vector with no
say, only the pre- and post-converters shape the result.



