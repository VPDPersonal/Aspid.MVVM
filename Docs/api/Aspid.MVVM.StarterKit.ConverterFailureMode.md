---
title: "Enum ConverterFailureMode"
sidebar_label: "ConverterFailureMode"
description: "Enum ConverterFailureMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum ConverterFailureMode {#Aspid_MVVM_StarterKit_ConverterFailureMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What a converter does with a value it cannot convert.

```csharp
public enum ConverterFailureMode
```


## Fields

`ReturnFallback = 0` 

Return the configured fallback value.



`ReturnInput = 1` 

Return the incoming value unchanged, or the fallback when it does not fit the output type.



## Remarks

The failure is always reported; the mode only decides what comes back.

