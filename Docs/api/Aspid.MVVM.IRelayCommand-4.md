---
title: "Interface IRelayCommand<T1, T2, T3, T4>"
sidebar_label: "IRelayCommand<T1, T2, T3, T4>"
description: "Interface IRelayCommand<T1, T2, T3, T4> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IRelayCommand\<T1, T2, T3, T4\> {#Aspid_MVVM_IRelayCommand_4}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Defines a command with four parameters.

```csharp
public interface IRelayCommand<in T1, in T2, in T3, in T4>
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

#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>, T1, T2, T3, T4\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3__)

## Methods

### CanExecute\(T1?, T2?, T3?, T4?\) {#Aspid_MVVM_IRelayCommand_4_CanExecute__0__1__2__3_}

Determines whether the command can execute with the given parameters.

```csharp
bool CanExecute(T1? param1, T2? param2, T3? param3, T4? param4)
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

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can execute; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T1?, T2?, T3?, T4?\) {#Aspid_MVVM_IRelayCommand_4_Execute__0__1__2__3_}

Executes the command.

```csharp
void Execute(T1? param1, T2? param2, T3? param3, T4? param4)
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

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_IRelayCommand_4_NotifyCanExecuteChanged}

Raises the [`IRelayCommand<T1, T2, T3, T4>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-4.md#Aspid_MVVM_IRelayCommand_4_CanExecuteChanged) event.

```csharp
void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_IRelayCommand_4_CanExecuteChanged}

Occurs when the result of [`IRelayCommand<T1, T2, T3, T4>.CanExecute`](Aspid.MVVM.IRelayCommand-4.md) may have changed.

```csharp
event Action<IRelayCommand<in T1, in T2, in T3, in T4>> CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>\>

