---
title: "Class RelayCommandExtensions"
sidebar_label: "RelayCommandExtensions"
description: "Class RelayCommandExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class RelayCommandExtensions {#Aspid_MVVM_RelayCommandExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Provides extension methods for [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) and its generic variants
for null-safe fallback to empty commands and for creating commands from delegates.

```csharp
public static class RelayCommandExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelayCommandExtensions](Aspid.MVVM.RelayCommandExtensions.md)



## Methods

### CreateCommand\(Action, Func\<bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand_System_Action_System_Func_System_Boolean__}

Creates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) from the specified execute and canExecute delegates.

```csharp
public static RelayCommand CreateCommand(this Action execute, Func<bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)

The action to execute.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function to determine if the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

A new [`RelayCommand`](Aspid.MVVM.RelayCommand.md) instance.

### CreateCommand\<T\>\(Action\<T?\>, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__1_System_Action___0__System_Func___0_System_Boolean__}

Creates a [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) from the specified execute and canExecute delegates.

```csharp
public static RelayCommand<T?> CreateCommand<T>(this Action<T?> execute, Func<T?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action to execute.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function to determine if the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T?\>

A new [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) instance.

#### Type Parameters

`T` 

The type of the command parameter.

### CreateCommand\<T1, T2\>\(Action\<T1?, T2?\>, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__2_System_Action___0___1__System_Func___0___1_System_Boolean__}

Creates a [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) from the specified execute and canExecute delegates.

```csharp
public static RelayCommand<T1?, T2?> CreateCommand<T1, T2>(this Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>

The action to execute.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function to determine if the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1?, T2?\>

A new [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) instance.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### CreateCommand\<T1, T2, T3\>\(Action\<T1?, T2?, T3?\>, Func\<T1?, T2?, T3?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__3_System_Action___0___1___2__System_Func___0___1___2_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) from the specified execute and canExecute delegates.

```csharp
public static RelayCommand<T1?, T2?, T3?> CreateCommand<T1, T2, T3>(this Action<T1?, T2?, T3?> execute, Func<T1?, T2?, T3?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<T1?, T2?, T3?\>

The action to execute.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-4)\<T1?, T2?, T3?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function to determine if the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-3.md)\<T1?, T2?, T3?\>

A new [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) instance.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### CreateCommand\<T1, T2, T3, T4\>\(Action\<T1?, T2?, T3?, T4?\>, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__4_System_Action___0___1___2___3__System_Func___0___1___2___3_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) from the specified execute and canExecute delegates.

```csharp
public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommand<T1, T2, T3, T4>(this Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>

The action to execute.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function to determine if the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

A new [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) instance.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### CreateCommandOrEmpty\(Action?, Func\<bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty_System_Action_System_Func_System_Boolean__}

Creates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static RelayCommand CreateCommandOrEmpty(this Action? execute, Func<bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, a non-executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function that determines whether the command can execute. Optional.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

A new [`RelayCommand`](Aspid.MVVM.RelayCommand.md) instance, or [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommandOrEmpty\<T\>\(Action\<T?\>?, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__1_System_Action___0__System_Func___0_System_Boolean__}

Creates a [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static RelayCommand<T?> CreateCommandOrEmpty<T>(this Action<T?>? execute, Func<T?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, a non-executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T?\>

A new [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) instance, or [`RelayCommand<T>.Empty`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_Empty) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The type of the command parameter.

### CreateCommandOrEmpty\<T1, T2\>\(Action\<T1?, T2?\>?, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__2_System_Action___0___1__System_Func___0___1_System_Boolean__}

Creates a [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static RelayCommand<T1?, T2?> CreateCommandOrEmpty<T1, T2>(this Action<T1?, T2?>? execute, Func<T1?, T2?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, a non-executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1?, T2?\>

A new [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) instance, or [`RelayCommand<T1, T2>.Empty`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_Empty) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### CreateCommandOrEmpty\<T1, T2, T3\>\(Action\<T1?, T2?, T3?\>?, Func\<T1?, T2?, T3?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__3_System_Action___0___1___2__System_Func___0___1___2_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmpty<T1, T2, T3>(this Action<T1?, T2?, T3?>? execute, Func<T1?, T2?, T3?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<T1?, T2?, T3?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, a non-executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-4)\<T1?, T2?, T3?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-3.md)\<T1?, T2?, T3?\>

A new [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) instance, or [`RelayCommand<T1, T2, T3>.Empty`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_Empty) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### CreateCommandOrEmpty\<T1, T2, T3, T4\>\(Action\<T1?, T2?, T3?, T4?\>?, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__4_System_Action___0___1___2___3__System_Func___0___1___2___3_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmpty<T1, T2, T3, T4>(this Action<T1?, T2?, T3?, T4?>? execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, a non-executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

A new [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) instance, or [`RelayCommand<T1, T2, T3, T4>.Empty`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_Empty) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### CreateCommandOrEmptyExecution\(Action?, Func\<bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution_System_Action_System_Func_System_Boolean__}

Creates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static RelayCommand CreateCommandOrEmptyExecution(this Action? execute, Func<bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, an executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The function that determines whether the command can execute. Optional.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

A new [`RelayCommand`](Aspid.MVVM.RelayCommand.md) instance, or [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommandOrEmptyExecution\<T\>\(Action\<T?\>?, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__1_System_Action___0__System_Func___0_System_Boolean__}

Creates a [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static RelayCommand<T?> CreateCommandOrEmptyExecution<T>(this Action<T?>? execute, Func<T?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, an executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T?\>

A new [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) instance, or [`RelayCommand<T>.EmptyExecution`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_EmptyExecution) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The type of the command parameter.

### CreateCommandOrEmptyExecution\<T1, T2\>\(Action\<T1?, T2?\>?, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__2_System_Action___0___1__System_Func___0___1_System_Boolean__}

Creates a [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static RelayCommand<T1?, T2?> CreateCommandOrEmptyExecution<T1, T2>(this Action<T1?, T2?>? execute, Func<T1?, T2?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, an executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1?, T2?\>

A new [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) instance, or [`RelayCommand<T1, T2>.EmptyExecution`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_EmptyExecution) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### CreateCommandOrEmptyExecution\<T1, T2, T3\>\(Action\<T1?, T2?, T3?\>?, Func\<T1?, T2?, T3?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__3_System_Action___0___1___2__System_Func___0___1___2_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmptyExecution<T1, T2, T3>(this Action<T1?, T2?, T3?>? execute, Func<T1?, T2?, T3?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<T1?, T2?, T3?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, an executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-4)\<T1?, T2?, T3?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-3.md)\<T1?, T2?, T3?\>

A new [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) instance, or [`RelayCommand<T1, T2, T3>.EmptyExecution`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_EmptyExecution) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### CreateCommandOrEmptyExecution\<T1, T2, T3, T4\>\(Action\<T1?, T2?, T3?, T4?\>?, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__4_System_Action___0___1___2___3__System_Func___0___1___2___3_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) using the provided delegates.
If <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmptyExecution<T1, T2, T3, T4>(this Action<T1?, T2?, T3?, T4?>? execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>?

The action to execute. If <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, an executable empty command will be returned.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

Optional function to determine whether the command can execute.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

A new [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) instance, or [`RelayCommand<T1, T2, T3, T4>.EmptyExecution`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_EmptyExecution) if <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### CreateCommandWithoutParameters\<T\>\(IRelayCommand\<T\>, T\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__1_Aspid_MVVM_IRelayCommand___0____0_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that executes the original command with the specified argument.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T>(this IRelayCommand<T> command, T param)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The source command with a parameter.

`param` T

The value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that wraps the original command with the provided parameter.

#### Type Parameters

`T` 

The type of the command parameter.

### CreateCommandWithoutParameters\<T1, T2\>\(IRelayCommand\<T1, T2\>, T1, T2\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__2_Aspid_MVVM_IRelayCommand___0___1____0___1_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that executes the original command with the specified arguments.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T1, T2>(this IRelayCommand<T1, T2> command, T1 param1, T2 param2)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The source command with two parameters.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that wraps the original command with the provided parameters.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### CreateCommandWithoutParameters\<T1, T2, T3\>\(IRelayCommand\<T1, T2, T3\>, T1, T2, T3\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__3_Aspid_MVVM_IRelayCommand___0___1___2____0___1___2_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that executes the original command with the specified arguments.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3>(this IRelayCommand<T1, T2, T3> command, T1 param1, T2 param2, T3 param3)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>

The source command with three parameters.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

`param3` T3

The third value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that wraps the original command with the provided parameters.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### CreateCommandWithoutParameters\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>, T1, T2, T3, T4\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that executes the original command with the specified arguments.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4> command, T1 param1, T2 param2, T3 param3, T4 param4)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>

The source command with four parameters.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

`param3` T3

The third value to pass to the command.

`param4` T4

The fourth value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that wraps the original command with the provided parameters.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### CreateCommandWithoutParametersOrEmpty\<T\>\(IRelayCommand\<T\>?, T\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__1_Aspid_MVVM_IRelayCommand___0____0_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameter.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T>(this IRelayCommand<T>? command, T param)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>?

The source command with a parameter, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param` T

The value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameter,
or [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The type of the command parameter.

### CreateCommandWithoutParametersOrEmpty\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1____0___1_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameters.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>?

The source command with two parameters, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameters,
or [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### CreateCommandWithoutParametersOrEmpty\<T1, T2, T3\>\(IRelayCommand\<T1, T2, T3\>?, T1, T2, T3\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__3_Aspid_MVVM_IRelayCommand___0___1___2____0___1___2_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameters.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>?

The source command with three parameters, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

`param3` T3

The third value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameters,
or [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### CreateCommandWithoutParametersOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameters.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns a non-executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>?

The source command with four parameters, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

`param3` T3

The third value to pass to the command.

`param4` T4

The fourth value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameters,
or [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### CreateCommandWithoutParametersOrEmptyExecution\<T\>\(IRelayCommand\<T\>?, T\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0____0_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameter.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T>(this IRelayCommand<T>? command, T param)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>?

The source command with a parameter, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param` T

The value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameter,
or [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The type of the command parameter.

### CreateCommandWithoutParametersOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1____0___1_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameters.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>?

The source command with two parameters, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameters,
or [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### CreateCommandWithoutParametersOrEmptyExecution\<T1, T2, T3\>\(IRelayCommand\<T1, T2, T3\>?, T1, T2, T3\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__3_Aspid_MVVM_IRelayCommand___0___1___2____0___1___2_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameters.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>?

The source command with three parameters, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

`param3` T3

The third value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameters,
or [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### CreateCommandWithoutParametersOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_}

Creates a parameterless [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that wraps the original command and uses the specified parameters.
If the original command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>, returns an executable empty command.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>?

The source command with four parameters, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first value to pass to the command.

`param2` T2

The second value to pass to the command.

`param3` T3

The third value to pass to the command.

`param4` T4

The fourth value to pass to the command.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

A parameterless command that invokes the original command with the given parameters,
or [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) if the command is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### GetSelfOrEmpty\(IRelayCommand?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty_Aspid_MVVM_IRelayCommand_}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty command.

```csharp
public static IRelayCommand GetSelfOrEmpty(this IRelayCommand? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty command.

### GetSelfOrEmpty\<T\>\(IRelayCommand\<T?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__1_Aspid_MVVM_IRelayCommand___0__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty command.

```csharp
public static IRelayCommand<T?> GetSelfOrEmpty<T>(this IRelayCommand<T?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty command.

#### Type Parameters

`T` 

The type of the command parameter.

### GetSelfOrEmpty\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty command.

```csharp
public static IRelayCommand<T1?, T2?> GetSelfOrEmpty<T1, T2>(this IRelayCommand<T1?, T2?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty command.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### GetSelfOrEmpty\<T1, T2, T3\>\(IRelayCommand\<T1?, T2?, T3?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__3_Aspid_MVVM_IRelayCommand___0___1___2__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty command.

```csharp
public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmpty<T1, T2, T3>(this IRelayCommand<T1?, T2?, T3?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty command.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### GetSelfOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty command.

```csharp
public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmpty<T1, T2, T3, T4>(this IRelayCommand<T1?, T2?, T3?, T4?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty command.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

### GetSelfOrEmptyExecution\(IRelayCommand?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution_Aspid_MVVM_IRelayCommand_}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty execution command.

```csharp
public static IRelayCommand GetSelfOrEmptyExecution(this IRelayCommand? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty execution command.

### GetSelfOrEmptyExecution\<T\>\(IRelayCommand\<T?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty execution command.

```csharp
public static IRelayCommand<T?> GetSelfOrEmptyExecution<T>(this IRelayCommand<T?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty execution command.

#### Type Parameters

`T` 

The type of the command parameter.

### GetSelfOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty execution command.

```csharp
public static IRelayCommand<T1?, T2?> GetSelfOrEmptyExecution<T1, T2>(this IRelayCommand<T1?, T2?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty execution command.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### GetSelfOrEmptyExecution\<T1, T2, T3\>\(IRelayCommand\<T1?, T2?, T3?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__3_Aspid_MVVM_IRelayCommand___0___1___2__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty execution command.

```csharp
public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmptyExecution<T1, T2, T3>(this IRelayCommand<T1?, T2?, T3?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty execution command.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### GetSelfOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3__}

Returns the command if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, returns an empty execution command.

```csharp
public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmptyExecution<T1, T2, T3, T4>(this IRelayCommand<T1?, T2?, T3?, T4?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>?

The command to check.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

The original command if not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, an empty execution command.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

