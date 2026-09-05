---
title: "Enum SelectableStates"
sidebar_label: "SelectableStates"
description: "Enum SelectableStates — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum SelectableStates {#Aspid_MVVM_StarterKit_SelectableStates}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Which states of a [`ColorBlock`](https://docs.unity3d.com/ScriptReference/UI-ColorBlock.html) a converter writes.

```csharp
[Flags]
public enum SelectableStates
```


## Fields

`None = 0` 

No state.



`Normal = 1` 

The resting color.



`Highlighted = 2` 

The color under the pointer or the focus.



`Pressed = 4` 

The color while held down.



`Selected = 8` 

The color once chosen.



`Disabled = 16` 

The color while the control is not interactable.



`Interactive = 15` 

Every state but [`SelectableStates.Disabled`](Aspid.MVVM.StarterKit.SelectableStates.md).



`All = 31` 

Every state.



## Remarks

The five colors a [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html) keeps, as a mask.

