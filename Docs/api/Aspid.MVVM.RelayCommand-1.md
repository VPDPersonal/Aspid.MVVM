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

[`IRelayCommand<T>`](Aspid.MVVM.IRelayCommand-1.md) that runs a delegate, optionally gated by a predicate.

```csharp
public sealed class RelayCommand<T> : IRelayCommand<T>
```

#### Type Parameters

`T` 

The command parameter type.

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

Creates a command that can always execute.

```csharp
public RelayCommand(Action<T?> execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action to run.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action\<T?\>, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommand_1__ctor_System_Action__0__System_Func__0_System_Boolean__}

Creates a command gated by <code class="paramref">canExecute</code>.

```csharp
public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_1_Empty}

Gets a shared command that does nothing and whose [`RelayCommand<T>.CanExecute`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_CanExecute__0_) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

```csharp
public static RelayCommand<T> Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T\>

### EmptyExecution {#Aspid_MVVM_RelayCommand_1_EmptyExecution}

Gets a shared command that does nothing and whose [`RelayCommand<T>.CanExecute`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_CanExecute__0_) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public static RelayCommand<T> EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T\>

## Methods

### CanExecute\(T?\) {#Aspid_MVVM_RelayCommand_1_CanExecute__0_}

Determines whether the command can execute by calling its predicate.

```csharp
public bool CanExecute(T? param)
```

#### Parameters

`param` T?

The command parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result of the predicate; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when there is none.

### Execute\(T?\) {#Aspid_MVVM_RelayCommand_1_Execute__0_}

Runs the action when [`RelayCommand<T>.CanExecute`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_CanExecute__0_) returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public void Execute(T? param)
```

#### Parameters

`param` T?

The command parameter.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_1_NotifyCanExecuteChanged}

Raises the [`IRelayCommand<T>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged) event.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_1_CanExecuteChanged}

Occurs when the result of [`IRelayCommand<T>.CanExecute`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecute__0_) may have changed.

```csharp
public event Action<IRelayCommand<T>>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>\>?

