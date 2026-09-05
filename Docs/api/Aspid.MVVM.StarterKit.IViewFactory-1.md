---
title: "Interface IViewFactory<TView>"
sidebar_label: "IViewFactory<TView>"
description: "Interface IViewFactory<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactory\<TView\> {#Aspid_MVVM_StarterKit_IViewFactory_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Creates views for a ViewModel. Keyed creation ignores the key.

```csharp
public interface IViewFactory<TView> : IViewFactoryWithKey<TView>, IViewFactoryRelease<TView> where TView : IView
```

#### Type Parameters

`TView` 

The type of the created view.

#### Implements

[IViewFactoryWithKey\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-1.md), 
[IViewFactoryRelease\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)


## Methods

### Create\(IViewModel?\) {#Aspid_MVVM_StarterKit_IViewFactory_1_Create_Aspid_MVVM_IViewModel_}

Creates a view for <code class="paramref">viewModel</code>.

```csharp
TView Create(IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

#### Returns

 TView

The created view.

