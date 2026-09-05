---
title: "Enum UpdateInputFieldEvent"
sidebar_label: "UpdateInputFieldEvent"
description: "Enum UpdateInputFieldEvent — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum UpdateInputFieldEvent {#Aspid_MVVM_StarterKit_UpdateInputFieldEvent}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Specifies which `TMP_InputField` event a binder listens to.

```csharp
public enum UpdateInputFieldEvent
```


## Fields

`OnValueChanged = 0` 

`onValueChanged`: every text change.



`OnEndEdit = 1` 

`onEndEdit`: editing finished.



`OnSubmit = 2` 

`onSubmit`: Submit pressed.



`OnSelect = 3` 

`onSelect`: the field gained focus.



`OnDeselect = 4` 

`onDeselect`: the field lost focus.



