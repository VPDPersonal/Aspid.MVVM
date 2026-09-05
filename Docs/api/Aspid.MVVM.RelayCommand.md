---
title: "Class RelayCommand"
sidebar_label: "RelayCommand"
description: "Class RelayCommand — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommand {#Aspid_MVVM_RelayCommand}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) implementation that wraps an [`Action`](https://learn.microsoft.com/dotnet/api/system.action) as the execute callback
and an optional [`Func<T>`](https://learn.microsoft.com/dotnet/api/system.func-1) predicate to gate execution.

```csharp
public sealed class RelayCommand : IRelayCommand
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelayCommand](Aspid.MVVM.RelayCommand.md)

#### Implements

[IRelayCommand](Aspid.MVVM.IRelayCommand.md)


#### Extension Methods

[RelayCommandExtensions.GetSelfOrEmpty\(IRelayCommand?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty_Aspid_MVVM_IRelayCommand_), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\(IRelayCommand?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution_Aspid_MVVM_IRelayCommand_)

## Constructors

### RelayCommand\(Action\) {#Aspid_MVVM_RelayCommand__ctor_System_Action_}

Initializes a new instance of the [`RelayCommand`](Aspid.MVVM.RelayCommand.md) class, taking an action to execute the command.

```csharp
public RelayCommand(Action execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)

The action that will be executed by the command.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action, Func\<bool\>?\) {#Aspid_MVVM_RelayCommand__ctor_System_Action_System_Func_System_Boolean__}

Initializes a new instance of the [`RelayCommand`](Aspid.MVVM.RelayCommand.md) class, taking an action to execute the command 
and a function to check whether it can be executed.

```csharp
public RelayCommand(Action execute, Func<bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)

The action that will be executed by the command.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

A function that returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_Empty}

Gets an empty command that cannot be executed.

```csharp
public static RelayCommand Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

### EmptyExecution {#Aspid_MVVM_RelayCommand_EmptyExecution}

Gets an empty command that can be executed but performs no action.
Useful as a placeholder when a non-null executable command is required.

```csharp
public static RelayCommand EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

## Methods

### CanExecute\(\) {#Aspid_MVVM_RelayCommand_CanExecute}

Determines whether the command can be executed.

```csharp
public bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(\) {#Aspid_MVVM_RelayCommand_Execute}

Executes the command if it can be executed.

```csharp
public void Execute()
```

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_NotifyCanExecuteChanged}

Notifies that the ability to execute the command has changed.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
public event Action<IRelayCommand>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand.md)\>?

