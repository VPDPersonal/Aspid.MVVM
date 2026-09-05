---
title: "Class CommandBinderExtensions"
sidebar_label: "CommandBinderExtensions"
description: "Class CommandBinderExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CommandBinderExtensions {#Aspid_MVVM_StarterKit_CommandBinderExtensions}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Helpers for command binders: swapping the bound command while keeping the
[`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged) subscription, and reflecting <code>CanExecute</code> on a [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html).

```csharp
public static class CommandBinderExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CommandBinderExtensions](Aspid.MVVM.StarterKit.CommandBinderExtensions.md)



## Methods

### SetInteractable\(Selectable, InteractableMode, bool, ICanExecuteHandler, object\) {#Aspid_MVVM_StarterKit_CommandBinderExtensions_SetInteractable_UnityEngine_UI_Selectable_Aspid_MVVM_StarterKit_InteractableMode_System_Boolean_Aspid_MVVM_StarterKit_ICanExecuteHandler_System_Object_}

Reflects <code class="paramref">isInteractable</code> on <code class="paramref">target</code> according to <code class="paramref">mode</code>.

```csharp
public static void SetInteractable(this Selectable target, InteractableMode mode, bool isInteractable, ICanExecuteHandler customView, object owner)
```

#### Parameters

`target` Selectable

The [`Selectable`](https://docs.unity3d.com/ScriptReference/UI-Selectable.html) the command binder operates on.

`mode` [InteractableMode](Aspid.MVVM.StarterKit.InteractableMode.md)

What <code class="paramref">isInteractable</code> is applied to.

`isInteractable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The command's current [`IRelayCommand.CanExecute`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecute) result.

`customView` [ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)

The handler used by [`InteractableMode.Custom`](Aspid.MVVM.StarterKit.InteractableMode.md). Ignored by other modes.

`owner` [object](https://learn.microsoft.com/dotnet/api/system.object)

The binder applying the value; names the source in diagnostics.

#### Remarks

A missing reference is logged, not thrown: this runs from [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged),
and an exception would cut the notification short for other subscribers.

### UpdateCommand\(ref IRelayCommand, IRelayCommand, in Action\<IRelayCommand\>\) {#Aspid_MVVM_StarterKit_CommandBinderExtensions_UpdateCommand_Aspid_MVVM_IRelayCommand__Aspid_MVVM_IRelayCommand_System_Action_Aspid_MVVM_IRelayCommand___}

Replaces <code class="paramref">command</code> with <code class="paramref">value</code>, moving the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged)
subscription and invoking <code class="paramref">onCanExecuteChanged</code> once for the new command. No-op when both are the same instance.

```csharp
public static void UpdateCommand(ref IRelayCommand command, IRelayCommand value, in Action<IRelayCommand> onCanExecuteChanged = null)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The field holding the current command.

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand.md)

The command to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to unbind.

`onCanExecuteChanged` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand.md)\>

The handler to subscribe, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip subscription.

### UpdateCommand\<T\>\(ref IRelayCommand\<T\>, IRelayCommand\<T\>, in Action\<IRelayCommand\<T\>\>\) {#Aspid_MVVM_StarterKit_CommandBinderExtensions_UpdateCommand__1_Aspid_MVVM_IRelayCommand___0___Aspid_MVVM_IRelayCommand___0__System_Action_Aspid_MVVM_IRelayCommand___0____}

Replaces <code class="paramref">command</code> with <code class="paramref">value</code>, moving the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged)
subscription and invoking <code class="paramref">onCanExecuteChanged</code> once for the new command. No-op when both are the same instance.

```csharp
public static void UpdateCommand<T>(ref IRelayCommand<T> command, IRelayCommand<T> value, in Action<IRelayCommand<T>> onCanExecuteChanged = null)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The field holding the current command.

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>

The command to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to unbind.

`onCanExecuteChanged` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-1.md)\<T\>\>

The handler to subscribe, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip subscription.

#### Type Parameters

`T` 

The type of the command parameter.

### UpdateCommand\<T1, T2\>\(ref IRelayCommand\<T1, T2\>, IRelayCommand\<T1, T2\>, in Action\<IRelayCommand\<T1, T2\>\>\) {#Aspid_MVVM_StarterKit_CommandBinderExtensions_UpdateCommand__2_Aspid_MVVM_IRelayCommand___0___1___Aspid_MVVM_IRelayCommand___0___1__System_Action_Aspid_MVVM_IRelayCommand___0___1____}

Replaces <code class="paramref">command</code> with <code class="paramref">value</code>, moving the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged)
subscription and invoking <code class="paramref">onCanExecuteChanged</code> once for the new command. No-op when both are the same instance.

```csharp
public static void UpdateCommand<T1, T2>(ref IRelayCommand<T1, T2> command, IRelayCommand<T1, T2> value, in Action<IRelayCommand<T1, T2>> onCanExecuteChanged = null)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The field holding the current command.

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>

The command to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to unbind.

`onCanExecuteChanged` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-2.md)\<T1, T2\>\>

The handler to subscribe, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip subscription.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

### UpdateCommand\<T1, T2, T3\>\(ref IRelayCommand\<T1, T2, T3\>, IRelayCommand\<T1, T2, T3\>, in Action\<IRelayCommand\<T1, T2, T3\>\>\) {#Aspid_MVVM_StarterKit_CommandBinderExtensions_UpdateCommand__3_Aspid_MVVM_IRelayCommand___0___1___2___Aspid_MVVM_IRelayCommand___0___1___2__System_Action_Aspid_MVVM_IRelayCommand___0___1___2____}

Replaces <code class="paramref">command</code> with <code class="paramref">value</code>, moving the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged)
subscription and invoking <code class="paramref">onCanExecuteChanged</code> once for the new command. No-op when both are the same instance.

```csharp
public static void UpdateCommand<T1, T2, T3>(ref IRelayCommand<T1, T2, T3> command, IRelayCommand<T1, T2, T3> value, in Action<IRelayCommand<T1, T2, T3>> onCanExecuteChanged = null)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>

The field holding the current command.

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>

The command to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to unbind.

`onCanExecuteChanged` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-3.md)\<T1, T2, T3\>\>

The handler to subscribe, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip subscription.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

### UpdateCommand\<T1, T2, T3, T4\>\(ref IRelayCommand\<T1, T2, T3, T4\>, IRelayCommand\<T1, T2, T3, T4\>, in Action\<IRelayCommand\<T1, T2, T3, T4\>\>\) {#Aspid_MVVM_StarterKit_CommandBinderExtensions_UpdateCommand__4_Aspid_MVVM_IRelayCommand___0___1___2___3___Aspid_MVVM_IRelayCommand___0___1___2___3__System_Action_Aspid_MVVM_IRelayCommand___0___1___2___3____}

Replaces <code class="paramref">command</code> with <code class="paramref">value</code>, moving the [`IRelayCommand.CanExecuteChanged`](Aspid.MVVM.IRelayCommand.md#Aspid_MVVM_IRelayCommand_CanExecuteChanged)
subscription and invoking <code class="paramref">onCanExecuteChanged</code> once for the new command. No-op when both are the same instance.

```csharp
public static void UpdateCommand<T1, T2, T3, T4>(ref IRelayCommand<T1, T2, T3, T4> command, IRelayCommand<T1, T2, T3, T4> value, in Action<IRelayCommand<T1, T2, T3, T4>> onCanExecuteChanged = null)
```

#### Parameters

`command` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>

The field holding the current command.

`value` [IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>

The command to bind, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to unbind.

`onCanExecuteChanged` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<[IRelayCommand](Aspid.MVVM.IRelayCommand-4.md)\<T1, T2, T3, T4\>\>

The handler to subscribe, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip subscription.

#### Type Parameters

`T1` 

The type of the first command parameter.

`T2` 

The type of the second command parameter.

`T3` 

The type of the third command parameter.

`T4` 

The type of the fourth command parameter.

