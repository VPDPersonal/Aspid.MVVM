---
title: "Enum StringMatchMode"
sidebar_label: "StringMatchMode"
description: "Enum StringMatchMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum StringMatchMode {#Aspid_MVVM_StarterKit_StringMatchMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How [`StringMatchToBoolConverter`](Aspid.MVVM.StarterKit.StringMatchToBoolConverter.md) compares a bound string with the authored one.

```csharp
public enum StringMatchMode
```


## Fields

`Equals = 0` 

The whole string must match.



`Contains = 1` 

The string must contain the authored text.



`StartsWith = 2` 

The string must begin with the authored text.



`EndsWith = 3` 

The string must end with the authored text.



