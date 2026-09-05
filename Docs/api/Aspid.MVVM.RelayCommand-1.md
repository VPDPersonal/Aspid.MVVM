---
title: "Class RelayCommand<T>"
sidebar_label: "RelayCommand<T>"
description: "Class RelayCommand<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommand\<T\> {#Aspid_MVVM_RelayCommand_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) implementation that wraps an [`Action<T>`](https://learn.microsoft.com/dotnet/api/system.action-1) as the execute callback
and an optional [`Func<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.func-2) predicate to gate execution against the supplied parameter.

```csharp
public sealed class RelayCommand<T> : IRelayCommand<T>
```

#### Type Parameters

`T` 

The type of the command parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelayCommand\<T\>](Aspid.MVVM.RelayCommand-1.md)

#### Implements

[IRelayCommand\<T\>](Aspid.MVVM.IRelayCommand-1.md)


#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T\>\(IRelayCommand\<T\>, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T\>\(IRelayCommand\<T\>?, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T\>\(IRelayCommand\<T\>?, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T\>\(IRelayCommand\<T?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__1_Aspid_MVVM_IRelayCommand___0__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T\>\(IRelayCommand\<T?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0__)

## Constructors

### RelayCommand\(Action\<T?\>\) {#Aspid_MVVM_RelayCommand_1__ctor_System_Action__0__}

Initializes a new instance of the [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) class, taking an action to execute the command.

```csharp
public RelayCommand(Action<T?> execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action that will be executed by the command.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action\<T?\>, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommand_1__ctor_System_Action__0__System_Func__0_System_Boolean__}

Initializes a new instance of the [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) class, taking an action to execute the command 
and a function to check whether it can be executed.

```csharp
public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action that will be executed by the command.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

A function that returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_1_Empty}

Gets an empty command that cannot be executed.

```csharp
public static RelayCommand<T> Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T\>

### EmptyExecution {#Aspid_MVVM_RelayCommand_1_EmptyExecution}

Gets an empty command that can be executed but performs no action.
Useful as a placeholder when a non-null executable command is required.

```csharp
public static RelayCommand<T> EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T\>

## Methods

### CanExecute\(T?\) {#Aspid_MVVM_RelayCommand_1_CanExecute__0_}

Determines whether the command can be executed with the specified parameter.

```csharp
public bool CanExecute(T? param)
```

#### Parameters

`param` T?

The parameter passed for checking the ability to execute the command.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T?\) {#Aspid_MVVM_RelayCommand_1_Execute__0_}

Executes the command with the specified parameter if it can be executed.

```csharp
public void Execute(T? param)
```

#### Parameters

`param` T?

The parameter passed to the command for execution.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_1_NotifyCanExecuteChanged}

Notifies that the ability to execute the command has changed.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_1_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
public event Action<IRelayCommand<T>>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>\>?

