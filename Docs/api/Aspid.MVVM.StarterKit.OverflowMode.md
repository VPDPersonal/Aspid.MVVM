---
title: "Enum OverflowMode"
sidebar_label: "OverflowMode"
description: "Enum OverflowMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum OverflowMode {#Aspid_MVVM_StarterKit_OverflowMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What [`NumericCastConverter`](Aspid.MVVM.StarterKit.NumericCastConverter.md) does with a number the target type cannot hold.

```csharp
public enum OverflowMode
```


## Fields

`Saturate = 0` 

Return the nearest value the target type can hold, or zero for a NaN on an integer target.



`Unchecked = 1` 

Convert the way a plain cast does: an integer keeps its low bits, an out-of-range floating-point value is undefined.



`Checked = 2` 

Throw an [`OverflowException`](https://learn.microsoft.com/dotnet/api/system.overflowexception) for a value too large for the target.

Wrap the converter in [`SafeConverter<T1, T2>`](Aspid.MVVM.StarterKit.SafeConverter-2.md) to keep the throw local.

