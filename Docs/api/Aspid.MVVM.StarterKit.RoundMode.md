---
title: "Enum RoundMode"
sidebar_label: "RoundMode"
description: "Enum RoundMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum RoundMode {#Aspid_MVVM_StarterKit_RoundMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How [`RoundNumberConverter`](Aspid.MVVM.StarterKit.RoundNumberConverter.md) drops the fraction.

```csharp
public enum RoundMode
```


## Fields

`Round = 0` 

To the nearest; the converter decides where an exact half goes.



`Floor = 1` 

Toward negative infinity.



`Ceil = 2` 

Toward positive infinity.



`Truncate = 3` 

Toward zero.



