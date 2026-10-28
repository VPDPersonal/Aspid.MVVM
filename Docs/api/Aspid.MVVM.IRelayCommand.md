---
title: "Interface IRelayCommand"
sidebar_label: "IRelayCommand"
description: "Interface IRelayCommand — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IRelayCommand {#Aspid_MVVM_IRelayCommand}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Defines a command without parameters.

```csharp
public interface IRelayCommand
```

#### Extension Methods

[RelayCommandExtensions.GetSelfOrEmpty\(IRelayCommand?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty_Aspid_MVVM_IRelayCommand_), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\(IRelayCommand?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution_Aspid_MVVM_IRelayCommand_)

## Methods

### CanExecute\(\) {#Aspid_MVVM_IRelayCommand_CanExecute}

Determines whether the command can execute.

```csharp
bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can execute; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(\) {#Aspid_MVVM_IRelayCommand_Execute}

Executes the command.

```csharp
void Execute()
```

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_IRelayCommand_NotifyCanExecuteChanged}

Raises the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) event.

```csharp
void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_IRelayCommand_CanExecuteChanged}

Occurs when the result of [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
event Action<IRelayCommand> CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand.md)\>

