---
title: "Enum NumberWrapMode"
sidebar_label: "NumberWrapMode"
description: "Enum NumberWrapMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum NumberWrapMode {#Aspid_MVVM_StarterKit_NumberWrapMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How [`WrapNumberConverter`](Aspid.MVVM.StarterKit.WrapNumberConverter.md) folds a value back into its range.

```csharp
public enum NumberWrapMode
```


## Fields

`Repeat = 0` 

Past the end, start again from the beginning.



`PingPong = 1` 

Past the end, travel back toward the beginning.



