---
title: "Enum InteractableMode"
sidebar_label: "InteractableMode"
description: "Enum InteractableMode — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum InteractableMode {#Aspid_MVVM_StarterKit_InteractableMode}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

How a command binder reflects the command's <code>CanExecute</code> state on its target.

```csharp
public enum InteractableMode
```


## Fields

`None = 0` 

The state is ignored.



`Visible = 1` 

The target GameObject is shown or hidden.



`Interactable = 2` 

The target's <code>interactable</code> flag follows the state.



`Custom = 3` 

The state is handed to an assigned [`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md).



