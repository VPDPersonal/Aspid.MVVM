---
title: "Enum MonoBinderResetMode"
sidebar_label: "MonoBinderResetMode"
description: "Enum MonoBinderResetMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum MonoBinderResetMode {#Aspid_MVVM_Validation_MonoBinderResetMode}

Namespace: [Aspid.MVVM.Validation](Aspid.MVVM.Validation.md)  
Assembly: Aspid.MVVM.Unity.dll  

How far a [`IMonoBinderValidatable`](Aspid.MVVM.Validation.IMonoBinderValidatable.md) reset goes.

```csharp
public enum MonoBinderResetMode
```


## Fields

`Soft = 0` 

Clears the current value; the previous one is kept.



`Hard = 1` 

Clears the current and the previous value.



