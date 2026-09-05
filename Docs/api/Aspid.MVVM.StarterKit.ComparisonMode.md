---
title: "Enum ComparisonMode"
sidebar_label: "ComparisonMode"
description: "Enum ComparisonMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum ComparisonMode {#Aspid_MVVM_StarterKit_ComparisonMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How a converter compares the bound value with the one it is configured with.

```csharp
public enum ComparisonMode
```


## Fields

`Equal = 0` 

Equal, within the converter's tolerance where it has one.



`NotEqual = 1` 

Not equal, tolerance included.



`LessThan = 2` 

Below the configured value, by more than the tolerance.



`GreaterThan = 3` 

Above the configured value, by more than the tolerance.



`LessThanOrEqual = 4` 

Below the configured value, or within the tolerance of it.



`GreaterThanOrEqual = 5` 

Above the configured value, or within the tolerance of it.



