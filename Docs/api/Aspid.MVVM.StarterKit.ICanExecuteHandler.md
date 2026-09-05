---
title: "Interface ICanExecuteHandler"
sidebar_label: "ICanExecuteHandler"
description: "Interface ICanExecuteHandler — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface ICanExecuteHandler {#Aspid_MVVM_StarterKit_ICanExecuteHandler}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Reacts to a command's <code>CanExecute</code> state on behalf of a command binder whose interactable mode is <code>Custom</code>.

```csharp
public interface ICanExecuteHandler
```


## Methods

### SetCanExecute\(bool\) {#Aspid_MVVM_StarterKit_ICanExecuteHandler_SetCanExecute_System_Boolean_}

Reflects whether the bound command can currently execute.

```csharp
void SetCanExecute(bool canExecute)
```

#### Parameters

`canExecute` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The command's current <code>CanExecute</code> result.

