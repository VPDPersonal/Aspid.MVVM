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

[BindModeExtensions.IsNone\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IsNone_Aspid_MVVM_BindMode_), 
[BindModeExtensions.IsOne\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IsOne_Aspid_MVVM_BindMode_), 
[BindModeExtensions.IsTwo\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_IsTwo_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfMatches\(BindMode, BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfMatches_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfNone\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNone_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfNotMatches\(BindMode, BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotMatches_Aspid_MVVM_BindMode_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfNotOne\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotOne_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfNotTwo\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfNotTwo_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfOne\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfOne_Aspid_MVVM_BindMode_), 
[BindModeExtensions.ThrowExceptionIfTwo\(BindMode\)](Aspid.MVVM.BindModeExtensions.md#Aspid_MVVM_BindModeExtensions_ThrowExceptionIfTwo_Aspid_MVVM_BindMode_)

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



