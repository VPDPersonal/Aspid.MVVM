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

[`IRelayCommand<T1, T2>`](Aspid.MVVM.IRelayCommand-2.md) that runs a delegate, optionally gated by a predicate.

```csharp
public sealed class RelayCommand<T1, T2> : IRelayCommand<T1, T2>
```

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

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

Creates a command that can always execute.

```csharp
public RelayCommand(Action<T1?, T2?> execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>

The action to run.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action\<T1?, T2?\>, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommand_2__ctor_System_Action__0__1__System_Func__0__1_System_Boolean__}

Creates a command gated by <code class="paramref">canExecute</code>.

```csharp
public RelayCommand(Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_2_Empty}

Gets a shared command that does nothing and whose [`RelayCommand<T1, T2>.CanExecute`](Aspid.MVVM.RelayCommand-2.md) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

```csharp
public static RelayCommand<T1, T2> Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1, T2\>

### EmptyExecution {#Aspid_MVVM_RelayCommand_2_EmptyExecution}

Gets a shared command that does nothing and whose [`RelayCommand<T1, T2>.CanExecute`](Aspid.MVVM.RelayCommand-2.md) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public static RelayCommand<T1, T2> EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1, T2\>

## Methods

### CanExecute\(T1?, T2?\) {#Aspid_MVVM_RelayCommand_2_CanExecute__0__1_}

Determines whether the command can execute by calling its predicate.

```csharp
public bool CanExecute(T1? param1, T2? param2)
```

#### Parameters

`param1` T1?

The first command parameter.

`param2` T2?

The second command parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result of the predicate; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when there is none.

### Execute\(T1?, T2?\) {#Aspid_MVVM_RelayCommand_2_Execute__0__1_}

Runs the action when [`RelayCommand<T1, T2>.CanExecute`](Aspid.MVVM.RelayCommand-2.md) returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public void Execute(T1? param1, T2? param2)
```

#### Parameters

`param1` T1?

The first command parameter.

`param2` T2?

The second command parameter.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_2_NotifyCanExecuteChanged}

Raises the [`IRelayCommand<T1, T2>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-2.md#Aspid_MVVM_IRelayCommand_2_CanExecuteChanged) event.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_2_CanExecuteChanged}

Occurs when the result of [`IRelayCommand<T1, T2>.CanExecute`](Aspid.MVVM.IRelayCommand-2.md) may have changed.

```csharp
public event Action<IRelayCommand<T1, T2>>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>\>?

