---
title: "Interface IViewFactoryWithKey<T, TView>"
sidebar_label: "IViewFactoryWithKey<T, TView>"
description: "Interface IViewFactoryWithKey<T, TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactoryWithKey\<T, TView\> {#Aspid_MVVM_StarterKit_IViewFactoryWithKey_2}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Creates views for a ViewModel identified by a key, with one extra argument.

```csharp
public interface IViewFactoryWithKey<in T, TView> : IViewFactoryRelease<TView> where TView : IView
```

#### Type Parameters

`T` 

The type of the extra argument.

`TView` 

The type of the created view.

#### Implements

[IViewFactoryRelease\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)


## Methods

### Create\<TKey\>\(IViewModel?, TKey, T?\) {#Aspid_MVVM_StarterKit_IViewFactoryWithKey_2_Create__1_Aspid_MVVM_IViewModel___0__0_}

Creates a view for <code class="paramref">viewModel</code>.

```csharp
TView Create<TKey>(IViewModel? viewModel, TKey key, T? param)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

`key` TKey

The key the ViewModel is stored under.

`param` T?

The extra argument.

#### Returns

 TView

The created view.

#### Type Parameters

`TKey` 

The type of the key.

