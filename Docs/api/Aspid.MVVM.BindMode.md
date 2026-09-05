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

Represents the binding mode that determines the direction of data flow between the ViewModel and the View.

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

No binding is applied. This is the default value.



`OneWay = 1` 

One-way binding: Updates from the ViewModel to the View are propagated.
Changes in the View do not affect the ViewModel.



`TwoWay = 2` 

Two-way binding: Updates are propagated in both directions.
Changes in the View are reflected in the ViewModel, and vice versa.



`OneTime = 3` 

One-time binding: The value is propagated from the ViewModel to the View only once.
Subsequent changes in the ViewModel do not affect the View.



`OneWayToSource = 4` 

One-way to source binding: Updates from the View to the ViewModel are propagated.
Changes in the ViewModel do not affect the View.



