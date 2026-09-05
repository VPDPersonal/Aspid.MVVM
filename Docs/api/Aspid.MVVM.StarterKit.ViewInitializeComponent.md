---
title: "Class ViewInitializeComponent"
sidebar_label: "ViewInitializeComponent"
description: "Class ViewInitializeComponent — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewInitializeComponent {#Aspid_MVVM_StarterKit_ViewInitializeComponent}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`InitializeComponent<T>`](Aspid.MVVM.StarterKit.InitializeComponent-1.md) that resolves an [`IView`](Aspid.MVVM.IView.md).

```csharp
[Serializable]
public sealed class ViewInitializeComponent : InitializeComponent<IView>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InitializeComponent\<IView\>](Aspid.MVVM.StarterKit.InitializeComponent-1.md) ← 
[ViewInitializeComponent](Aspid.MVVM.StarterKit.ViewInitializeComponent.md)



## Methods

### GetTypeForDi\(\) {#Aspid_MVVM_StarterKit_ViewInitializeComponent_GetTypeForDi}

Gets the type requested from the DI container.

```csharp
protected override Type? GetTypeForDi()
```

#### Returns

 [Type](https://learn.microsoft.com/dotnet/api/system.type)?

The type, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none is configured.

### Validate\(\) {#Aspid_MVVM_StarterKit_ViewInitializeComponent_Validate}

Clears the references that do not belong to the chosen [`InitializeComponent<T>.ResolveType`](Aspid.MVVM.StarterKit.InitializeComponent-1.md#Aspid_MVVM_StarterKit_InitializeComponent_1_ResolveType) source.

```csharp
public override void Validate()
```

