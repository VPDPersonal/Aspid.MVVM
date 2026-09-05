---
title: "Interface IViewFactoryWithKey<T1, T2, T3, TView>"
sidebar_label: "IViewFactoryWithKey<T1, T2, T3, TView>"
description: "Interface IViewFactoryWithKey<T1, T2, T3, TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactoryWithKey\<T1, T2, T3, TView\> {#Aspid_MVVM_StarterKit_IViewFactoryWithKey_4}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Creates views for a ViewModel identified by a key, with three extra arguments.

```csharp
public interface IViewFactoryWithKey<in T1, in T2, in T3, TView> : IViewFactoryRelease<TView> where TView : IView
```

#### Type Parameters

`T1` 

The type of the first extra argument.

`T2` 

The type of the second extra argument.

`T3` 

The type of the third extra argument.

`TView` 

The type of the created view.

#### Implements

[IViewFactoryRelease\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)


## Methods

### Create\<TKey\>\(IViewModel?, TKey, T1?, T2?, T3?\) {#Aspid_MVVM_StarterKit_IViewFactoryWithKey_4_Create__1_Aspid_MVVM_IViewModel___0__0__1__2_}

Creates a view for <code class="paramref">viewModel</code>.

```csharp
TView Create<TKey>(IViewModel? viewModel, TKey key, T1? param1, T2? param2, T3? param3)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

`key` TKey

The key the ViewModel is stored under.

`param1` T1?

The first extra argument.

`param2` T2?

The second extra argument.

`param3` T3?

The third extra argument.

#### Returns

 TView

The created view.

#### Type Parameters

`TKey` 

The type of the key.

