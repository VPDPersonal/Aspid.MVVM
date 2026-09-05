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

The last non-empty ID of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md), kept to detect a renamed View field.

 [MonoBinderPreviousView](Aspid.MVVM.Validation.MonoBinderPreviousView.md)

The last non-empty View of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md), kept with its name to detect a lost reference.

### Interfaces

 [IMonoBinderValidatable](Aspid.MVVM.Validation.IMonoBinderValidatable.md)

Editor-side view of a [`MonoBinder`](Aspid.MVVM.MonoBinder.md): the View and field ID it is wired to, with their last known values.

### Enums

 [MonoBinderResetMode](Aspid.MVVM.Validation.MonoBinderResetMode.md)

How far a [`IMonoBinderValidatable`](Aspid.MVVM.Validation.IMonoBinderValidatable.md) reset goes.

