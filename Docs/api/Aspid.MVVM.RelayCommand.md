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

[`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs a delegate, optionally gated by a predicate.

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

Creates a command that can always execute.

```csharp
public RelayCommand(Action execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)

The action to run.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action, Func\<bool\>?\) {#Aspid_MVVM_RelayCommand__ctor_System_Action_System_Func_System_Boolean__}

Creates a command gated by <code class="paramref">canExecute</code>.

```csharp
public RelayCommand(Action execute, Func<bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_Empty}

Gets a shared command that does nothing and whose [`RelayCommand.CanExecute`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_CanExecute) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

```csharp
public static RelayCommand Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

### EmptyExecution {#Aspid_MVVM_RelayCommand_EmptyExecution}

Gets a shared command that does nothing and whose [`RelayCommand.CanExecute`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_CanExecute) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public static RelayCommand EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

## Methods

### CanExecute\(\) {#Aspid_MVVM_RelayCommand_CanExecute}

Determines whether the command can execute by calling its predicate.

```csharp
public bool CanExecute()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result of the predicate; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when there is none.

### Execute\(\) {#Aspid_MVVM_RelayCommand_Execute}

Runs the action when [`RelayCommand.CanExecute`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_CanExecute) returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public void Execute()
```

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_NotifyCanExecuteChanged}

Raises the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) event.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_CanExecuteChanged}

Occurs when the result of [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) may have changed.

```csharp
public event Action<IRelayCommand>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand.md)\>?

