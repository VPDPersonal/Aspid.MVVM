---
title: "Class ColorCanExecuteHandler"
sidebar_label: "ColorCanExecuteHandler"
description: "Class ColorCanExecuteHandler — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ColorCanExecuteHandler {#Aspid_MVVM_StarterKit_ColorCanExecuteHandler}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ICanExecuteHandler`](Aspid.MVVM.StarterKit.ICanExecuteHandler.md) that switches a [`color`](https://docs.unity3d.com/ScriptReference/UI-Graphic-color.html) between two colors by the command state.

```csharp
[Serializable]
public sealed class ColorCanExecuteHandler : ICanExecuteHandler
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ColorCanExecuteHandler](Aspid.MVVM.StarterKit.ColorCanExecuteHandler.md)

#### Implements

[ICanExecuteHandler](Aspid.MVVM.StarterKit.ICanExecuteHandler.md)



## Constructors

### ColorCanExecuteHandler\(Graphic, Color, Color\) {#Aspid_MVVM_StarterKit_ColorCanExecuteHandler__ctor_UnityEngine_UI_Graphic_UnityEngine_Color_UnityEngine_Color_}

```csharp
public ColorCanExecuteHandler(Graphic graphic, Color trueColor, Color falseColor)
```

#### Parameters

`graphic` Graphic

The graphic whose color reflects the state.

`trueColor` Color

The color applied when the command can execute.

`falseColor` Color

The color applied when the command cannot execute.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">graphic</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

## Methods

### SetCanExecute\(bool\) {#Aspid_MVVM_StarterKit_ColorCanExecuteHandler_SetCanExecute_System_Boolean_}

Reflects whether the bound command can currently execute.

```csharp
public void SetCanExecute(bool canExecute)
```

#### Parameters

`canExecute` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The command's current <code>CanExecute</code> result.

