---
title: "Class RelayCommand<T1, T2>"
sidebar_label: "RelayCommand<T1, T2>"
description: "Class RelayCommand<T1, T2> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommand\<T1, T2\> {#Aspid_MVVM_RelayCommand_2}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) implementation that wraps an [`Action<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.action-2) as the execute
callback and an optional [`Func<T1, T2, T3>`](https://learn.microsoft.com/dotnet/api/system.func-3) predicate to gate execution against the supplied parameters.

```csharp
public sealed class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
```

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelayCommand\<T1, T2\>](Aspid.MVVM.RelayCommand-2.md)

#### Implements

[IRelayCommand\<T1, T2\>](Aspid.MVVM.IRelayCommand-2.md)


#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T1, T2\>\(IRelayCommand\<T1, T2\>, T1, T2\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__2_Aspid_MVVM_IRelayCommand___0___1____0___1_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1____0___1_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1____0___1_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1__)

## Constructors

### RelayCommand\(Action\<T1?, T2?\>\) {#Aspid_MVVM_RelayCommand_2__ctor_System_Action__0__1__}

Initializes a new instance of the [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) class, taking an action to execute the command.

```csharp
public RelayCommand(Action<T1?, T2?> execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>

The action that will be executed by the command.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action\<T1?, T2?\>, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommand_2__ctor_System_Action__0__1__System_Func__0__1_System_Boolean__}

Initializes a new instance of the [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) class, taking an action to execute the command 
and a function to check whether it can be executed.

```csharp
public RelayCommand(Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>

The action that will be executed by the command.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

A function that returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_2_Empty}

Gets an empty command that cannot be executed.

```csharp
public static RelayCommand<T1, T2> Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1, T2\>

### EmptyExecution {#Aspid_MVVM_RelayCommand_2_EmptyExecution}

Gets an empty command that can be executed but performs no action.
Useful as a placeholder when a non-null executable command is required.

```csharp
public static RelayCommand<T1, T2> EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1, T2\>

## Methods

### CanExecute\(T1?, T2?\) {#Aspid_MVVM_RelayCommand_2_CanExecute__0__1_}

Determines whether the command can be executed with the specified parameters.

```csharp
public bool CanExecute(T1? param1, T2? param2)
```

#### Parameters

`param1` T1?

The first parameter passed for checking the ability to execute the command.

`param2` T2?

The second parameter passed for checking the ability to execute the command.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T1?, T2?\) {#Aspid_MVVM_RelayCommand_2_Execute__0__1_}

Executes the command with the specified parameters if it can be executed.

```csharp
public void Execute(T1? param1, T2? param2)
```

#### Parameters

`param1` T1?

The first parameter passed to the command for execution.

`param2` T2?

The second parameter passed to the command for execution.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_2_NotifyCanExecuteChanged}

Notifies that the ability to execute the command has changed.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_2_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>\>?

