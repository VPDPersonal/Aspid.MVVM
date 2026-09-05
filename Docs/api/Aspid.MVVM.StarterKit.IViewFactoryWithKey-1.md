---
title: "Interface IViewFactoryWithKey<TView>"
sidebar_label: "IViewFactoryWithKey<TView>"
description: "Interface IViewFactoryWithKey<TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactoryWithKey\<TView\> {#Aspid_MVVM_StarterKit_IViewFactoryWithKey_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Creates views for a ViewModel identified by a key.

```csharp
public interface IViewFactoryWithKey<TView> : IViewFactoryRelease<TView> where TView : IView
```

#### Type Parameters

`TView` 

The type of the created view.

#### Implements

[IViewFactoryRelease\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)


## Methods

### Create\<TKey\>\(IViewModel?, TKey\) {#Aspid_MVVM_StarterKit_IViewFactoryWithKey_1_Create__1_Aspid_MVVM_IViewModel___0_}

Creates a view for <code class="paramref">viewModel</code>.

```csharp
TView Create<TKey>(IViewModel? viewModel, TKey key)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

`key` TKey

The key the ViewModel is stored under.

#### Returns

 TView

The created view.

#### Type Parameters

`TKey` 

The type of the key.

