---
title: "Enum IndexOutOfRangeMode"
sidebar_label: "IndexOutOfRangeMode"
description: "Enum IndexOutOfRangeMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum IndexOutOfRangeMode {#Aspid_MVVM_StarterKit_IndexOutOfRangeMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

What [`IndexToValueConverter<T>`](Aspid.MVVM.StarterKit.IndexToValueConverter-1.md) does with an index outside the array.

```csharp
public enum IndexOutOfRangeMode
```


## Fields

`Clamp = 0` 

Use the nearest end of the array.



`Wrap = 1` 

Wrap around, so one past the end is the first entry.



`Fallback = 2` 

Return the fallback.



