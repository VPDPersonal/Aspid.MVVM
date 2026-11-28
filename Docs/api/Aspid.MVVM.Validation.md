---
title: "Namespace Aspid.MVVM.Validation"
sidebar_label: "Aspid.MVVM.Validation"
description: "Namespace Aspid.MVVM.Validation — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Namespace Aspid.MVVM.Validation {#Aspid_MVVM_Validation}

### Structs

 [MonoBinderPreviousId](Aspid.MVVM.Validation.MonoBinderPreviousId.md)

Record of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) field ID for detecting renames.

 [MonoBinderPreviousView](Aspid.MVVM.Validation.MonoBinderPreviousView.md)

Record of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md) view and its name for detecting a lost reference.

### Interfaces

 [IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md)

[`IBinder`](Aspid.MVVM.IBinder.md) with Editor validation of its View, field identifier and previous assignments.

### Enums

 [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

Specifies whether a [`IMonoBinderValidatable`](Aspid.MVVM.Validation.IMonoBinderValidatable.md) reset also clears previous values.
