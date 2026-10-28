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

Provides extension methods for creating relay commands and for replacing a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command with an empty one.

```csharp
public static class RelayCommandExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelayCommandExtensions](Aspid.MVVM.RelayCommandExtensions.md)



## Methods

### CreateCommand\(Action, Func\<bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand_System_Action_System_Func_System_Boolean__}

Creates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) that runs <code class="paramref">execute</code>.

```csharp
public static RelayCommand CreateCommand(this Action execute, Func<bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

The new command.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommand\<T\>\(Action\<T?\>, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__1_System_Action___0__System_Func___0_System_Boolean__}

Creates a [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) that runs <code class="paramref">execute</code>.

```csharp
public static RelayCommand<T?> CreateCommand<T>(this Action<T?> execute, Func<T?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T?\>

The new command.

#### Type Parameters

`T` 

The command parameter type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommand\<T1, T2\>\(Action\<T1?, T2?\>, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__2_System_Action___0___1__System_Func___0___1_System_Boolean__}

Creates a [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) that runs <code class="paramref">execute</code>.

```csharp
public static RelayCommand<T1?, T2?> CreateCommand<T1, T2>(this Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1?, T2?\>

The new command.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommand\<T1, T2, T3\>\(Action\<T1?, T2?, T3?\>, Func\<T1?, T2?, T3?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__3_System_Action___0___1___2__System_Func___0___1___2_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) that runs <code class="paramref">execute</code>.

```csharp
public static RelayCommand<T1?, T2?, T3?> CreateCommand<T1, T2, T3>(this Action<T1?, T2?, T3?> execute, Func<T1?, T2?, T3?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<T1?, T2?, T3?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-4)\<T1?, T2?, T3?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-3.md)\<T1?, T2?, T3?\>

The new command.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommand\<T1, T2, T3, T4\>\(Action\<T1?, T2?, T3?, T4?\>, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommand__4_System_Action___0___1___2___3__System_Func___0___1___2___3_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) that runs <code class="paramref">execute</code>.

```csharp
public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommand<T1, T2, T3, T4>(this Action<T1?, T2?, T3?, T4?> execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>

The action to run.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

The new command.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommandOrEmpty\(Action?, Func\<bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty_System_Action_System_Func_System_Boolean__}

Creates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand CreateCommandOrEmpty(this Action? execute, Func<bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

The new command; [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommandOrEmpty\<T\>\(Action\<T?\>?, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__1_System_Action___0__System_Func___0_System_Boolean__}

Creates a [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T>.Empty`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_Empty) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T?> CreateCommandOrEmpty<T>(this Action<T?>? execute, Func<T?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T?\>

The new command; [`RelayCommand<T>.Empty`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_Empty) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The command parameter type.

### CreateCommandOrEmpty\<T1, T2\>\(Action\<T1?, T2?\>?, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__2_System_Action___0___1__System_Func___0___1_System_Boolean__}

Creates a [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T1, T2>.Empty`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_Empty) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T1?, T2?> CreateCommandOrEmpty<T1, T2>(this Action<T1?, T2?>? execute, Func<T1?, T2?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1?, T2?\>

The new command; [`RelayCommand<T1, T2>.Empty`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_Empty) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

### CreateCommandOrEmpty\<T1, T2, T3\>\(Action\<T1?, T2?, T3?\>?, Func\<T1?, T2?, T3?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__3_System_Action___0___1___2__System_Func___0___1___2_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T1, T2, T3>.Empty`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_Empty) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmpty<T1, T2, T3>(this Action<T1?, T2?, T3?>? execute, Func<T1?, T2?, T3?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<T1?, T2?, T3?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-4)\<T1?, T2?, T3?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-3.md)\<T1?, T2?, T3?\>

The new command; [`RelayCommand<T1, T2, T3>.Empty`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_Empty) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

### CreateCommandOrEmpty\<T1, T2, T3, T4\>\(Action\<T1?, T2?, T3?, T4?\>?, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmpty__4_System_Action___0___1___2___3__System_Func___0___1___2___3_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T1, T2, T3, T4>.Empty`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_Empty) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmpty<T1, T2, T3, T4>(this Action<T1?, T2?, T3?, T4?>? execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

The new command; [`RelayCommand<T1, T2, T3, T4>.Empty`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_Empty) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

### CreateCommandOrEmptyExecution\(Action?, Func\<bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution_System_Action_System_Func_System_Boolean__}

Creates a [`RelayCommand`](Aspid.MVVM.RelayCommand.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand CreateCommandOrEmptyExecution(this Action? execute, Func<bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action)?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-1)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand.md)

The new command; [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CreateCommandOrEmptyExecution\<T\>\(Action\<T?\>?, Func\<T?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__1_System_Action___0__System_Func___0_System_Boolean__}

Creates a [`RelayCommand<T>`](Aspid.MVVM.RelayCommand-1.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T>.EmptyExecution`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_EmptyExecution) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T?> CreateCommandOrEmptyExecution<T>(this Action<T?>? execute, Func<T?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<T?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-1.md)\<T?\>

The new command; [`RelayCommand<T>.EmptyExecution`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_EmptyExecution) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The command parameter type.

### CreateCommandOrEmptyExecution\<T1, T2\>\(Action\<T1?, T2?\>?, Func\<T1?, T2?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__2_System_Action___0___1__System_Func___0___1_System_Boolean__}

Creates a [`RelayCommand<T1, T2>`](Aspid.MVVM.RelayCommand-2.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T1, T2>.EmptyExecution`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_EmptyExecution) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T1?, T2?> CreateCommandOrEmptyExecution<T1, T2>(this Action<T1?, T2?>? execute, Func<T1?, T2?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<T1?, T2?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-3)\<T1?, T2?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-2.md)\<T1?, T2?\>

The new command; [`RelayCommand<T1, T2>.EmptyExecution`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_EmptyExecution) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

### CreateCommandOrEmptyExecution\<T1, T2, T3\>\(Action\<T1?, T2?, T3?\>?, Func\<T1?, T2?, T3?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__3_System_Action___0___1___2__System_Func___0___1___2_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3>`](Aspid.MVVM.RelayCommand-3.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T1, T2, T3>.EmptyExecution`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_EmptyExecution) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T1?, T2?, T3?> CreateCommandOrEmptyExecution<T1, T2, T3>(this Action<T1?, T2?, T3?>? execute, Func<T1?, T2?, T3?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<T1?, T2?, T3?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-4)\<T1?, T2?, T3?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-3.md)\<T1?, T2?, T3?\>

The new command; [`RelayCommand<T1, T2, T3>.EmptyExecution`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_EmptyExecution) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

### CreateCommandOrEmptyExecution\<T1, T2, T3, T4\>\(Action\<T1?, T2?, T3?, T4?\>?, Func\<T1?, T2?, T3?, T4?, bool\>?\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandOrEmptyExecution__4_System_Action___0___1___2___3__System_Func___0___1___2___3_System_Boolean__}

Creates a [`RelayCommand<T1, T2, T3, T4>`](Aspid.MVVM.RelayCommand-4.md) that runs <code class="paramref">execute</code>, or returns
[`RelayCommand<T1, T2, T3, T4>.EmptyExecution`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_EmptyExecution) when it is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static RelayCommand<T1?, T2?, T3?, T4?> CreateCommandOrEmptyExecution<T1, T2, T3, T4>(this Action<T1?, T2?, T3?, T4?>? execute, Func<T1?, T2?, T3?, T4?, bool>? canExecute = null)
```

#### Parameters

`execute` [Action](https://learn.microsoft.com/dotnet/api/system.action-4)\<T1?, T2?, T3?, T4?\>?

The action to run, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`canExecute` [Func](https://learn.microsoft.com/dotnet/api/system.func-5)\<T1?, T2?, T3?, T4?, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The predicate that allows execution; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to always allow it.

#### Returns

 [RelayCommand](Aspid.MVVM.RelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

The new command; [`RelayCommand<T1, T2, T3, T4>.EmptyExecution`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_EmptyExecution) when <code class="paramref">execute</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

### CreateCommandWithoutParameters\<T\>\(IRelayCommand\<T\>, T\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__1_Aspid_MVVM_IRelayCommand___0____0_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param</code>.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T>(this IRelayCommand<T> command, T param)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The command to wrap.

`param` T

The parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command.

#### Type Parameters

`T` 

The command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParameters\<T1, T2\>\(IRelayCommand\<T1, T2\>, T1, T2\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__2_Aspid_MVVM_IRelayCommand___0___1____0___1_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code> and <code class="paramref">param2</code>.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T1, T2>(this IRelayCommand<T1, T2> command, T1 param1, T2 param2)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The command to wrap.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-2.md#Aspid_MVVM_IRelayCommand_2_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParameters\<T1, T2, T3\>\(IRelayCommand\<T1, T2, T3\>, T1, T2, T3\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__3_Aspid_MVVM_IRelayCommand___0___1___2____0___1___2_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code>, <code class="paramref">param2</code> and <code class="paramref">param3</code>.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3>(this IRelayCommand<T1, T2, T3> command, T1 param1, T2 param2, T3 param3)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>

The command to wrap.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

`param3` T3

The third parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2, T3>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-3.md#Aspid_MVVM_IRelayCommand_3_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParameters\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>, T1, T2, T3, T4\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParameters__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code>, <code class="paramref">param2</code>, <code class="paramref">param3</code> and <code class="paramref">param4</code>.

```csharp
public static IRelayCommand CreateCommandWithoutParameters<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4> command, T1 param1, T2 param2, T3 param3, T4 param4)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>

The command to wrap.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

`param3` T3

The third parameter passed to <code class="paramref">command</code>.

`param4` T4

The fourth parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2, T3, T4>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-4.md#Aspid_MVVM_IRelayCommand_4_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmpty\<T\>\(IRelayCommand\<T\>?, T\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__1_Aspid_MVVM_IRelayCommand___0____0_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param</code>, or returns
[`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T>(this IRelayCommand<T>? command, T param)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param` T

The parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmpty\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1____0___1_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code> and <code class="paramref">param2</code>, or returns
[`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-2.md#Aspid_MVVM_IRelayCommand_2_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmpty\<T1, T2, T3\>\(IRelayCommand\<T1, T2, T3\>?, T1, T2, T3\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__3_Aspid_MVVM_IRelayCommand___0___1___2____0___1___2_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code>, <code class="paramref">param2</code> and <code class="paramref">param3</code>, or returns
[`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

`param3` T3

The third parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2, T3>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-3.md#Aspid_MVVM_IRelayCommand_3_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code>, <code class="paramref">param2</code>, <code class="paramref">param3</code> and <code class="paramref">param4</code>, or returns
[`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmpty<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

`param3` T3

The third parameter passed to <code class="paramref">command</code>.

`param4` T4

The fourth parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2, T3, T4>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-4.md#Aspid_MVVM_IRelayCommand_4_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmptyExecution\<T\>\(IRelayCommand\<T\>?, T\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0____0_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param</code>, or returns
[`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T>(this IRelayCommand<T>? command, T param)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param` T

The parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-1.md#Aspid_MVVM_IRelayCommand_1_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1, T2\>?, T1, T2\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1____0___1_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code> and <code class="paramref">param2</code>, or returns
[`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2>(this IRelayCommand<T1, T2>? command, T1 param1, T2 param2)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-2.md#Aspid_MVVM_IRelayCommand_2_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmptyExecution\<T1, T2, T3\>\(IRelayCommand\<T1, T2, T3\>?, T1, T2, T3\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__3_Aspid_MVVM_IRelayCommand___0___1___2____0___1___2_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code>, <code class="paramref">param2</code> and <code class="paramref">param3</code>, or returns
[`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3>(this IRelayCommand<T1, T2, T3>? command, T1 param1, T2 param2, T3 param3)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

`param3` T3

The third parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2, T3>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-3.md#Aspid_MVVM_IRelayCommand_3_CanExecuteChanged) of <code class="paramref">command</code>.

### CreateCommandWithoutParametersOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1, T2, T3, T4\>?, T1, T2, T3, T4\) {#Aspid_MVVM_RelayCommandExtensions_CreateCommandWithoutParametersOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3____0___1___2___3_}

Creates an [`IRelayCommand`](Aspid.MVVM.IRelayCommand.md) that runs <code class="paramref">command</code> with <code class="paramref">param1</code>, <code class="paramref">param2</code>, <code class="paramref">param3</code> and <code class="paramref">param4</code>, or returns
[`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public static IRelayCommand CreateCommandWithoutParametersOrEmptyExecution<T1, T2, T3, T4>(this IRelayCommand<T1, T2, T3, T4>? command, T1 param1, T2 param2, T3 param3, T4 param4)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>?

The command to wrap, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`param1` T1

The first parameter passed to <code class="paramref">command</code>.

`param2` T2

The second parameter passed to <code class="paramref">command</code>.

`param3` T3

The third parameter passed to <code class="paramref">command</code>.

`param4` T4

The fourth parameter passed to <code class="paramref">command</code>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The parameterless command; [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) when <code class="paramref">command</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

#### Remarks

The new command does not forward [`IRelayCommand<T1, T2, T3, T4>.CanExecuteChanged`](Aspid.MVVM.IRelayCommand-4.md#Aspid_MVVM_IRelayCommand_4_CanExecuteChanged) of <code class="paramref">command</code>.

### GetSelfOrEmpty\(IRelayCommand?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty_Aspid_MVVM_IRelayCommand_}

Substitutes [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand GetSelfOrEmpty(this IRelayCommand? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand.Empty`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_Empty).

### GetSelfOrEmpty\<T\>\(IRelayCommand\<T?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__1_Aspid_MVVM_IRelayCommand___0__}

Substitutes [`RelayCommand<T>.Empty`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_Empty) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T?> GetSelfOrEmpty<T>(this IRelayCommand<T?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T>.Empty`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_Empty).

#### Type Parameters

`T` 

The command parameter type.

### GetSelfOrEmpty\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__2_Aspid_MVVM_IRelayCommand___0___1__}

Substitutes [`RelayCommand<T1, T2>.Empty`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_Empty) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T1?, T2?> GetSelfOrEmpty<T1, T2>(this IRelayCommand<T1?, T2?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T1, T2>.Empty`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_Empty).

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

### GetSelfOrEmpty\<T1, T2, T3\>\(IRelayCommand\<T1?, T2?, T3?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__3_Aspid_MVVM_IRelayCommand___0___1___2__}

Substitutes [`RelayCommand<T1, T2, T3>.Empty`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_Empty) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmpty<T1, T2, T3>(this IRelayCommand<T1?, T2?, T3?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T1, T2, T3>.Empty`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_Empty).

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

### GetSelfOrEmpty\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmpty__4_Aspid_MVVM_IRelayCommand___0___1___2___3__}

Substitutes [`RelayCommand<T1, T2, T3, T4>.Empty`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_Empty) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmpty<T1, T2, T3, T4>(this IRelayCommand<T1?, T2?, T3?, T4?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T1, T2, T3, T4>.Empty`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_Empty).

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

### GetSelfOrEmptyExecution\(IRelayCommand?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution_Aspid_MVVM_IRelayCommand_}

Substitutes [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand GetSelfOrEmptyExecution(this IRelayCommand? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand.EmptyExecution`](Aspid.MVVM.RelayCommand.md#Aspid_MVVM_RelayCommand_EmptyExecution).

### GetSelfOrEmptyExecution\<T\>\(IRelayCommand\<T?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__1_Aspid_MVVM_IRelayCommand___0__}

Substitutes [`RelayCommand<T>.EmptyExecution`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_EmptyExecution) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T?> GetSelfOrEmptyExecution<T>(this IRelayCommand<T?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T>.EmptyExecution`](Aspid.MVVM.RelayCommand-1.md#Aspid_MVVM_RelayCommand_1_EmptyExecution).

#### Type Parameters

`T` 

The command parameter type.

### GetSelfOrEmptyExecution\<T1, T2\>\(IRelayCommand\<T1?, T2?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__2_Aspid_MVVM_IRelayCommand___0___1__}

Substitutes [`RelayCommand<T1, T2>.EmptyExecution`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_EmptyExecution) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T1?, T2?> GetSelfOrEmptyExecution<T1, T2>(this IRelayCommand<T1?, T2?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1?, T2?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T1, T2>.EmptyExecution`](Aspid.MVVM.RelayCommand-2.md#Aspid_MVVM_RelayCommand_2_EmptyExecution).

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

### GetSelfOrEmptyExecution\<T1, T2, T3\>\(IRelayCommand\<T1?, T2?, T3?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__3_Aspid_MVVM_IRelayCommand___0___1___2__}

Substitutes [`RelayCommand<T1, T2, T3>.EmptyExecution`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_EmptyExecution) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T1?, T2?, T3?> GetSelfOrEmptyExecution<T1, T2, T3>(this IRelayCommand<T1?, T2?, T3?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1?, T2?, T3?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T1, T2, T3>.EmptyExecution`](Aspid.MVVM.RelayCommand-3.md#Aspid_MVVM_RelayCommand_3_EmptyExecution).

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

### GetSelfOrEmptyExecution\<T1, T2, T3, T4\>\(IRelayCommand\<T1?, T2?, T3?, T4?\>?\) {#Aspid_MVVM_RelayCommandExtensions_GetSelfOrEmptyExecution__4_Aspid_MVVM_IRelayCommand___0___1___2___3__}

Substitutes [`RelayCommand<T1, T2, T3, T4>.EmptyExecution`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_EmptyExecution) for a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> command.

```csharp
public static IRelayCommand<T1?, T2?, T3?, T4?> GetSelfOrEmptyExecution<T1, T2, T3, T4>(this IRelayCommand<T1?, T2?, T3?, T4?>? command)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>?

The command, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1?, T2?, T3?, T4?\>

<code class="paramref">command</code> if it is not <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>; otherwise, [`RelayCommand<T1, T2, T3, T4>.EmptyExecution`](Aspid.MVVM.RelayCommand-4.md#Aspid_MVVM_RelayCommand_4_EmptyExecution).

#### Type Parameters

`T1` 

The first command parameter type.

`T2` 

The second command parameter type.

`T3` 

The third command parameter type.

`T4` 

The fourth command parameter type.

