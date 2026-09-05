---
title: "Enum Vector2CombineConverter.Mode"
sidebar_label: "Vector2CombineConverter.Mode"
description: "Enum Vector2CombineConverter.Mode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum Vector2CombineConverter.Mode {#Aspid_MVVM_StarterKit_Vector2CombineConverter_Mode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Specifies which components to take from the first vector when combining.

```csharp
public enum Vector2CombineConverter.Mode
```


## Fields

`X = 0` 

Takes X from the bound vector; Y stays at the reference vector's.



`Y = 1` 

Takes Y from the bound vector; X stays at the reference vector's.



`XY = 2` 

Takes both components from the bound vector, leaving the reference vector with no
say, only the pre- and post-converters shape the result.



