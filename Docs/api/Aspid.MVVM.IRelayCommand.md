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

An interface for a command that can be executed without parameters.

```csharp
public interface IRelayCommand
```

#### Extension Methods

[RelayCommandExtensions.GetSelfOrEmpty\(IRelayCommand?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty_Aspid_MVVM_IRelayCommand_), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\(IRelayCommand?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution_Aspid_MVVM_IRelayCommand_)

## Methods

### CanExecute\(\) {#Aspid_MVVM_IRelayCommand_CanExecute}

Determines whether the command can be executed.

```csharp
bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(\) {#Aspid_MVVM_IRelayCommand_Execute}

Executes the command.

```csharp
void Execute()
```

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_IRelayCommand_NotifyCanExecuteChanged}

Notifies that the execution state of the command has changed, raising the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) event.

```csharp
void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_IRelayCommand_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
event Action<IRelayCommand> CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand.md)\>

