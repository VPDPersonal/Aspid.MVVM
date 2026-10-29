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

[`IRelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.IRelayCommand-4.md) that runs a delegate, optionally gated by a predicate.

```csharp
public sealed class RelayCommand<T1, T2, T3, T4> : IRelayCommand<T1, T2, T3, T4>
```

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

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

Creates a command that can always execute.

```csharp
public RelayCommand(Action<T1?, T2?, T3?, T4?> execute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>

The action to run.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### RelayCommand\(Action\<T1?, T2?, T3?, T4?\>, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommand_4__ctor_System_Action__0__1__2__3__System_Func__0__1__2__3_System_Boolean__}

Creates a command gated by <code class="paramref">canExecute</code>.

```csharp
public RelayCommand(Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Properties

### Empty {#Aspid_MVVM_RelayCommand_4_Empty}

Gets a shared command that does nothing and whose [`RelayCommand<T1, T2, T3, T4>.CanExecute`](Aspid.MVVM.RelayCommand-4.md) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

```csharp
public static RelayCommand<T1, T2, T3, T4> Empty { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1, T2, T3, T4\>

### EmptyExecution {#Aspid_MVVM_RelayCommand_4_EmptyExecution}

Gets a shared command that does nothing and whose [`RelayCommand<T1, T2, T3, T4>.CanExecute`](Aspid.MVVM.RelayCommand-4.md) returns
<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public static RelayCommand<T1, T2, T3, T4> EmptyExecution { get; }
```

#### Property Value

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1, T2, T3, T4\>

## Methods

### CanExecute\(T1?, T2?, T3?, T4?\) {#Aspid_MVVM_RelayCommand_4_CanExecute__0__1__2__3_}

Determines whether the command can execute by calling its predicate.

```csharp
public bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4)
```

#### Parameters

`param1` T1?

The first command parameter.

`param2` T2?

The second command parameter.

`param3` T3?

The third command parameter.

`param4` T4?

The fourth command parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The result of the predicate; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> when there is none.

### Execute\(T1?, T2?, T3?, T4?\) {#Aspid_MVVM_RelayCommand_4_Execute__0__1__2__3_}

Runs the action when [`RelayCommand<T1, T2, T3, T4>.CanExecute`](Aspid.MVVM.RelayCommand-4.md) returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>.

```csharp
public void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
```

#### Parameters

`param1` T1?

The first command parameter.

`param2` T2?

The second command parameter.

`param3` T3?

The third command parameter.

`param4` T4?

The fourth command parameter.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_RelayCommand_4_NotifyCanExecuteChanged}

Raises the [`IRelayCommand<T1, T2, T3, T4>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-4.md#Aspid_MVVM_IRelayCommand_4_CanExecuteChanged) event.

```csharp
public void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_RelayCommand_4_CanExecuteChanged}

Occurs when the result of [`IRelayCommand<T1, T2, T3, T4>.CanExecute`](Aspid.MVVM.IRelayCommand-4.md) may have changed.

```csharp
public event Action<IRelayCommand<T1, T2, T3, T4>>? CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>\>?

