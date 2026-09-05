---
title: "Class RelayCommand<T1, T2, T3, T4>"
sidebar_label: "RelayCommand<T1, T2, T3, T4>"
description: "Class RelayCommand<T1, T2, T3, T4> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommand\<T1, T2, T3, T4\> {#Aspid_MVVM_RelayCommand_4}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) implementation that wraps an [`Action<T1, T2, T3, T4>`](https://learn.microsoft.com/dotnet/api/system.action-4) as the
execute callback and an optional [`Func<T1, T2, T3, T4, T5>`](https://learn.microsoft.com/dotnet/api/system.func-5) predicate to gate execution against the supplied parameters.

```csharp
public sealed class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
```

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.RelayCommand-4.md)

#### Implements

[IRelayCommand\<T1, T2, T3, T4\>](Aspid.MVVM.IRelayCommand-4.md)


#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>, T1, T2, T3, T4\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3__)

## Constructors

### RelayCommand\(Action\<T1?, T2?, T3?, T4?\>\) {#Aspid_MVVM_RelayCommand_4__ctor_System_Action__0__1__2__3__}

Initializes a new instance of the [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) class, taking an action to execute the command.

```csharp
public RelayCommand(Action<T1?, T2?, T3?, T4?> execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>

The action that will be executed by the command.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action\<T1?, T2?, T3?, T4?\>, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommand_4__ctor_System_Action__0__1__2__3__System_Func__0__1__2__3_System_Boolean__}

Initializes a new instance of the [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) class, taking an action to execute the command and a function to check if it can execute.

```csharp
public RelayCommand(Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>

The action that will be executed by the command.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

A function that returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can execute; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_4_Empty}

Gets an empty command that cannot be executed.

```csharp
public static RelayCommand<T1, T2, T3, T4> Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1, T2, T3, T4\>

### EmptyExecution {#Aspid_MVVM_RelayCommand_4_EmptyExecution}

Gets an empty command that can be executed but performs no action.
Useful as a placeholder when a non-null executable command is required.

```csharp
public static RelayCommand<T1, T2, T3, T4> EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1, T2, T3, T4\>

## Methods

### CanExecute\(T1?, T2?, T3?, T4?\) {#Aspid_MVVM_RelayCommand_4_CanExecute__0__1__2__3_}

Determines whether the command can be executed with the specified parameters.

```csharp
public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4)
```

#### Parameters

`param1` T1?

The first parameter passed to check if the command can execute.

`param2` T2?

The second parameter passed to check if the command can execute.

`param3` T3?

The third parameter passed to check if the command can execute.

`param4` T4?

The fourth parameter passed to check if the command can execute.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T1?, T2?, T3?, T4?\) {#Aspid_MVVM_RelayCommand_4_Execute__0__1__2__3_}

Executes the command with the specified parameters if it can be executed.

```csharp
public void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
```

#### Parameters

`param1` T1?

The first parameter passed to the command for execution.

`param2` T2?

The second parameter passed to the command for execution.

`param3` T3?

The third parameter passed to the command for execution.

`param4` T4?

The fourth parameter passed to the command for execution.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_4_NotifyCanExecuteChanged}

Notifies that the ability to execute the command has changed.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_4_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>\>?

