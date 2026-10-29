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

Defines a command with one parameter.

```csharp
public interface IRelayCommand<in T>
```

#### Type Parameters

`T` 

The command parameter type.

#### Extension Methods

[RelayCommandExtensions.CreateCommandWithoutParameters\<T\>\(IRelayCommand\<T\>, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmpty\<T\>\(IRelayCommand\<T\>?, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.CreateCommandWithoutParametersOrEmptyExecution\<T\>\(IRelayCommand\<T\>?, T\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0____0_), 
[RelayCommandExtensions.GetSelfOrEmpty\<T\>\(IRelayCommand\<T?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__1_Aspid_MVVM_IRelayCommand___0__), 
[RelayCommandExtensions.GetSelfOrEmptyExecution\<T\>\(IRelayCommand\<T?\>?\)](Aspid.MVVM.RelayCommandExtensions.md#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0__)

## Methods

### CanExecute\(T?\) {#Aspid_MVVM_IRelayCommand_1_CanExecute__0_}

Determines whether the command can execute with the given parameter.

```csharp
bool CanExecute(T? param)
```

#### Parameters

`param` T?

The command parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the command can execute; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### Execute\(T?\) {#Aspid_MVVM_IRelayCommand_1_Execute__0_}

Executes the command.

```csharp
void Execute(T? param)
```

#### Parameters

`param` T?

The command parameter.

### NotifyCanExecuteChanged\(\) {#Aspid_MVVM_IRelayCommand_1_NotifyCanExecuteChanged}

Raises the [`IRelayCommand<T>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged) event.

```csharp
void NotifyCanExecuteChanged()
```

### CanExecuteChanged {#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged}

Occurs when the result of [`IRelayCommand<T>.CanExecute`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecute__0_) may have changed.

```csharp
event Action<IRelayCommand<in T>> CanExecuteChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>\>

