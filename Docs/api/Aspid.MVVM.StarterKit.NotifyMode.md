---
title: "Enum NotifyMode"
sidebar_label: "NotifyMode"
description: "Enum NotifyMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum NotifyMode {#Aspid_MVVM_StarterKit_NotifyMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md) <br/>
Assembly: Aspid.MVVM.StarterKit.dll <br/>

Specifies when a value written by a binder raises the change event of the bound UI component.

```csharp
public enum NotifyMode
```

#### Extension Methods

[NotifyModeExtensions.ShouldNotify\(NotifyMode, bool\)](Aspid.MVVM.StarterKit.NotifyModeExtensions.md#Aspid_MVVM_StarterKit_NotifyModeExtensions_ShouldNotify_Aspid_MVVM_StarterKit_NotifyMode_System_Boolean_)

## Fields

`Always = 0`

Every write raises the event.



`AfterBind = 1`

The write made while binding is silent; later writes from the ViewModel raise the event.



`Never = 2`

No write raises the event.



## Remarks

User input raises the event in every mode.
