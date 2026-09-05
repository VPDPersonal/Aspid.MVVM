---
title: "Class ViewModelInitializeComponent"
sidebar_label: "ViewModelInitializeComponent"
description: "Class ViewModelInitializeComponent — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewModelInitializeComponent {#Aspid_MVVM_StarterKit_ViewModelInitializeComponent}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`InitializeComponent<T>`](Aspid.MVVM.StarterKit.InitializeComponent-1.md) that resolves an [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
[Serializable]
public sealed class ViewModelInitializeComponent : InitializeComponent<IViewModel>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InitializeComponent\<IViewModel\>](Aspid.MVVM.StarterKit.InitializeComponent-1.md) ← 
[ViewModelInitializeComponent](Aspid.MVVM.StarterKit.ViewModelInitializeComponent.md)



## Methods

### GetTypeForDi\(\) {#Aspid_MVVM_StarterKit_ViewModelInitializeComponent_GetTypeForDi}

Gets the type requested from the DI container.

```csharp
protected override Type? GetTypeForDi()
```

#### Returns

 [Type](https://learn.microsoft.com/dotnet/api/system.type)?

The type, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is configured.

### Validate\(\) {#Aspid_MVVM_StarterKit_ViewModelInitializeComponent_Validate}

Clears the references that do not belong to the chosen [`InitializeComponent<T>.ResolveType`](Aspid.MVVM.StarterKit.InitializeComponent-1.md#Aspid_MVVM_StarterKit_InitializeComponent_1_ResolveType) source.

```csharp
public override void Validate()
```

