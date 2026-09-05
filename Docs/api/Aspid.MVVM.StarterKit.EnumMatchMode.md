---
title: "Enum EnumMatchMode"
sidebar_label: "EnumMatchMode"
description: "Enum EnumMatchMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum EnumMatchMode {#Aspid_MVVM_StarterKit_EnumMatchMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How [`EnumMatchConverter<T>`](Aspid.MVVM.StarterKit.EnumMatchConverter-1.md) tests a bound enum value.

```csharp
public enum EnumMatchMode
```


## Fields

`Equal = 0` 

The value must equal the target.



`NotEqual = 1` 

The value must differ from the target.



`HasAllFlags = 2` 

The value must have every flag the target has.



`HasAnyFlag = 3` 

The value must have at least one flag the target has.



## Remarks

Members are appended, never inserted: the order is the serialized value.

