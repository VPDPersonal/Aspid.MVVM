---
title: "Class SequenceCanExecuteHandler"
sidebar_label: "SequenceCanExecuteHandler"
description: "Class SequenceCanExecuteHandler — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SequenceCanExecuteHandler {#Aspid_MVVM_StarterKit_SequenceCanExecuteHandler}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md) that forwards the state to every nested handler in order.

```csharp
[Serializable]
public sealed class SequenceCanExecuteHandler : ICanExecuteHandler
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SequenceCanExecuteHandler](Aspid.MVVM.StarterKit.SequenceCanExecuteHandler.md)

#### Implements

[ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)



## Constructors

### SequenceCanExecuteHandler\(params ICanExecuteHandler?\[\]?\) {#Aspid_MVVM_StarterKit_SequenceCanExecuteHandler__ctor_Aspid_MVVM_StarterKit_ICanExecuteHandler___}

```csharp
public SequenceCanExecuteHandler(params ICanExecuteHandler?[]? handlers)
```

#### Parameters

`handlers` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)?\[\]?

The handlers that receive the state, in order.

## Methods

### SetCanExecute\(bool\) {#Aspid_MVVM_StarterKit_SequenceCanExecuteHandler_SetCanExecute_System_Boolean_}

Reflects whether the bound command can currently execute.

```csharp
public void SetCanExecute(bool canExecute)
```

#### Parameters

`canExecute` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The command's current <code>CanExecute</code> result.

