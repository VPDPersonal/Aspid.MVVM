---
title: "Enum CultureInfoMode"
sidebar_label: "CultureInfoMode"
description: "Enum CultureInfoMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum CultureInfoMode {#Aspid_MVVM_StarterKit_CultureInfoMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Which culture a value is formatted and parsed with.

```csharp
public enum CultureInfoMode
```

#### Extension Methods

[ToCultureStringExtensions.ToCultureInfo\(CultureInfoMode\)](Aspid.MVVM.StarterKit.ToCultureStringExtensions.md#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureInfo_Aspid_MVVM_StarterKit_CultureInfoMode_)

## Fields

`CurrentCulture = 0` 

The thread's culture — what the player's machine is set to.



`CurrentUICulture = 1` 

The thread's UI culture, used for resource lookup rather than formatting.



`InvariantCulture = 2` 

Culture-independent — for anything stored, sent, or parsed back.



`InstalledUICulture = 3` 

The culture the operating system was installed with.



`DefaultThreadCurrentCulture = 4` 

The process-wide default culture, falling back to [`CultureInfoMode.CurrentCulture`](Aspid.MVVM.StarterKit.CultureInfoMode.md) while unset.



`DefaultThreadCurrentUICulture = 5` 

The process-wide default UI culture, falling back to [`CultureInfoMode.CurrentUICulture`](Aspid.MVVM.StarterKit.CultureInfoMode.md) while unset.



## Remarks

A serializable stand-in for [`CultureInfo`](https://learn.microsoft.com/dotnet/api/system.globalization.cultureinfo), which Unity cannot
serialize; [`ToCultureStringExtensions.ToCultureInfo`](Aspid.MVVM.StarterKit.ToCultureStringExtensions.md#Aspid_MVVM_StarterKit_ToCultureStringExtensions_ToCultureInfo_Aspid_MVVM_StarterKit_CultureInfoMode_) resolves it at call time.
A decimal separator is a comma in half of Europe, so a number written by one culture and parsed
by another loses its fractional part rather than failing. Append new members rather than
inserting one — the order is the serialized value.

