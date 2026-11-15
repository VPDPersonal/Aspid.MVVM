---
title: "Enum BindMode"
sidebar_label: "BindMode"
description: "Enum BindMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum BindMode {#Aspid_MVVM_BindMode}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Specifies the direction of data flow between the ViewModel and the View.

```csharp
public enum BindMode
```

#### Extension Methods

[BindModeExtensions.EnsureForward\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_EnsureForward_Aspid_MVVM_BindMode_), 
[BindModeExtensions.EnsureForwardOnly\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_EnsureForwardOnly_Aspid_MVVM_BindMode_), 
[BindModeExtensions.EnsureNot\(BindMode, BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_EnsureNot_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_), 
[BindModeExtensions.EnsureReverse\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_EnsureReverse_Aspid_MVVM_BindMode_), 
[BindModeExtensions.EnsureValid\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_EnsureValid_Aspid_MVVM_BindMode_), 
[BindModeExtensions.IncludesForward\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IncludesForward_Aspid_MVVM_BindMode_), 
[BindModeExtensions.IncludesReverse\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IncludesReverse_Aspid_MVVM_BindMode_), 
[BindModeExtensions.IsForwardOnly\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IsForwardOnly_Aspid_MVVM_BindMode_), 
[BindModeExtensions.IsValid\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IsValid_Aspid_MVVM_BindMode_)

## Fields

`None = 0` 

No binding; every bindable member rejects it.



`OneWay = 1` 

Pushes every ViewModel change to the View.



`TwoWay = 2` 

Pushes ViewModel changes to the View and View changes to the ViewModel.



`OneTime = 3` 

Pushes the ViewModel value to the View once, on bind.



`OneWayToSource = 4` 

Pushes View changes to the ViewModel.



