---
title: "Interface IViewFactory<T, TView>"
sidebar_label: "IViewFactory<T, TView>"
description: "Interface IViewFactory<T, TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactory\<T, TView\> {#Aspid_MVVM_StarterKit_IViewFactory_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Creates views for a ViewModel with one extra argument. Keyed creation ignores the key.

```csharp
public interface IViewFactory<in T, TView> : IViewFactoryWithKey<T, TView>, IViewFactoryRelease<TView> where TView : IView
```

#### Type Parameters

`T` 

The type of the extra argument.

`TView` 

The type of the created view.

#### Implements

[IViewFactoryWithKey\<T, TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-2.md), 
[IViewFactoryRelease\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)


## Methods

### Create\(IViewModel?, T?\) {#Aspid_MVVM_StarterKit_IViewFactory_2_Create_Aspid_MVVM_IViewModel__0_}

Creates a view for <code class="paramref">viewModel</code>.

```csharp
TView Create(IViewModel? viewModel, T? param)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

`param` T?

The extra argument.

#### Returns

 TView

The created view.

