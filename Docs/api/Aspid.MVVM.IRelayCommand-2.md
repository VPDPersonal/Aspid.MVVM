---
title: "Interface IRelayCommand<T1, T2>"
sidebar_label: "IRelayCommand<T1, T2>"
description: "Interface IRelayCommand<T1, T2> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IRelayCommand\<T1, T2\> {#Aspid_MVVM_IRelayCommand_2}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

An interface for a command that can be executed with two parameters.

```csharp
public interface IRelayCommand<in T1, in T2>
```

#### Type Parameters

`T1` 

The type of the first parameter passed to the command.

`T2` 

The type of the second parameter passed to the command.

#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T1, T2\>\(IRelayCommand\<T1, T2\>, T1, T2\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__2_Aspid_MVVM_IRelayCommand___0___1____0___1_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1____0___1_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1____0___1_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1__)

## Methods

### CanExecute\(T1?, T2?\) {#Aspid_MVVM_IRelayCommand_2_CanExecute__0__1_}

Determines whether the command can be executed with the given parameters.

```csharp
bool CanExecute(T1? param1, T2? param2)
```

#### Parameters

`param1` T1?

The first parameter used to execute the command.

`param2` T2?

The second parameter used to execute the command.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T1?, T2?\) {#Aspid_MVVM_IRelayCommand_2_Execute__0__1_}

Executes the command with the given parameters.

```csharp
void Execute(T1? param1, T2? param2)
```

#### Parameters

`param1` T1?

The first parameter used to execute the command.

`param2` T2?

The second parameter used to execute the command.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_IRelayCommand_2_NotifyCanExecuteChanged}

Notifies that the execution state of the command has changed, raising the [`IRelayCommand<T1, T2>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-2.md#Aspid_MVVM_IRelayCommand_2_CanExecuteChanged) event.

```csharp
void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_IRelayCommand_2_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
event Action<IRelayCommand<in T1, in T2>> CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>\>

