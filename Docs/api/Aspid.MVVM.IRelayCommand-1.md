---
title: "Interface IRelayCommand<T>"
sidebar_label: "IRelayCommand<T>"
description: "Interface IRelayCommand<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IRelayCommand\<T\> {#Aspid_MVVM_IRelayCommand_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

An interface for a command that can be executed with a parameter.

```csharp
public interface IRelayCommand<in T>
```

#### Type Parameters

`T` 

The type of the parameter passed to the command.

#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T\>\(IRelayCommand\<T\>, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T\>\(IRelayCommand\<T\>?, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T\>\(IRelayCommand\<T\>?, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T\>\(IRelayCommand\<T?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__1_Aspid_MVVM_IRelayCommand___0__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T\>\(IRelayCommand\<T?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0__)

## Methods

### CanExecute\(T?\) {#Aspid_MVVM_IRelayCommand_1_CanExecute__0_}

Determines whether the command can be executed with the given parameter.

```csharp
bool CanExecute(T? param)
```

#### Parameters

`param` T?

The parameter used to determine whether the command can be executed.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can be executed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T?\) {#Aspid_MVVM_IRelayCommand_1_Execute__0_}

Executes the command with the given parameter.

```csharp
void Execute(T? param)
```

#### Parameters

`param` T?

The parameter used to execute the command.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_IRelayCommand_1_NotifyCanExecuteChanged}

Notifies that the execution state of the command has changed, raising the [`IRelayCommand<T>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged) event.

```csharp
void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged}

Raised when the ability to execute the command changes.

```csharp
event Action<IRelayCommand<in T>> CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>\>

