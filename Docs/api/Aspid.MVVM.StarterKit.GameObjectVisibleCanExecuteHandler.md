---
title: "Class GameObjectVisibleCanExecuteHandler"
sidebar_label: "GameObjectVisibleCanExecuteHandler"
description: "Class GameObjectVisibleCanExecuteHandler — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class GameObjectVisibleCanExecuteHandler {#Aspid_MVVM_StarterKit_GameObjectVisibleCanExecuteHandler}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md) that toggles a [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html) active by the command state.

```csharp
[Serializable]
public sealed class GameObjectVisibleCanExecuteHandler : ICanExecuteHandler
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GameObjectVisibleCanExecuteHandler](Aspid.MVVM.StarterKit.GameObjectVisibleCanExecuteHandler.md)

#### Implements

[ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)



## Constructors

### GameObjectVisibleCanExecuteHandler\(GameObject, IConverter\<bool, bool\>?\) {#Aspid_MVVM_StarterKit_GameObjectVisibleCanExecuteHandler__ctor_UnityEngine_GameObject_Aspid_MVVM_StarterKit_IConverter_System_Boolean_System_Boolean__}

```csharp
public GameObjectVisibleCanExecuteHandler(GameObject gameObject, IConverter<bool, bool>? converter = null)
```

#### Parameters

`gameObject` GameObject

The GameObject whose active state reflects the command state.

`converter` [IConverter](Aspid.MVVM.StarterKit.IConverter-2.md)\<[bool](https://learn.microsoft.com/dotnet/api/system.boolean), [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>?

The converter applied to the state, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to use it unchanged.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">gameObject</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### SetCanExecute\(bool\) {#Aspid_MVVM_StarterKit_GameObjectVisibleCanExecuteHandler_SetCanExecute_System_Boolean_}

Reflects whether the bound command can currently execute.

```csharp
public void SetCanExecute(bool canExecute)
```

#### Parameters

`canExecute` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The command's current <code>CanExecute</code> result.

