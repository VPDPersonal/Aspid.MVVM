---
title: "Class ViewInitializerManual"
sidebar_label: "ViewInitializerManual"
description: "Class ViewInitializerManual — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewInitializerManual {#Aspid_MVVM_StarterKit_ViewInitializerManual}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`ViewInitializerBase`](Aspid.MVVM.StarterKit.ViewInitializerBase.md) that takes its ViewModel from an explicit [`ViewInitializerManual.Initialize`](Aspid.MVVM.StarterKit.ViewInitializerManual.md#Aspid_MVVM_StarterKit_ViewInitializerManual_Initialize_Aspid_MVVM_IViewModel_) call.

```csharp
[AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer Manual")]
[AddBinderContextMenu(typeof(MonoView), new string[] { }, Path = "Add View Initializers/View Initializer Manual")]
public sealed class ViewInitializerManual : ViewInitializerBase
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[ViewInitializerBase](Aspid.MVVM.StarterKit.ViewInitializerBase.md) ← 
[ViewInitializerManual](Aspid.MVVM.StarterKit.ViewInitializerManual.md)



## Properties

### ViewModel {#Aspid_MVVM_StarterKit_ViewInitializerManual_ViewModel}

Gets the ViewModel the views are initialized with.

```csharp
public override IViewModel ViewModel { get; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

## Methods

### Deinitialize\(\) {#Aspid_MVVM_StarterKit_ViewInitializerManual_Deinitialize}

Deinitializes all views. Does nothing when they are not initialized.

```csharp
public void Deinitialize()
```

### Initialize\(IViewModel\) {#Aspid_MVVM_StarterKit_ViewInitializerManual_Initialize_Aspid_MVVM_IViewModel_}

Initializes all views with <code class="paramref">viewModel</code>.

```csharp
public void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Thrown when the views are already initialized.

