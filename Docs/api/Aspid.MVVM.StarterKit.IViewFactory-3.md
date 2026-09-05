---
title: "Interface IViewFactory<T1, T2, TView>"
sidebar_label: "IViewFactory<T1, T2, TView>"
description: "Interface IViewFactory<T1, T2, TView> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactory\<T1, T2, TView\> {#Aspid_MVVM_StarterKit_IViewFactory_3}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Creates views for a ViewModel with two extra arguments. Keyed creation ignores the key.

```csharp
public interface IViewFactory<in T1, in T2, TView> : IViewFactoryWithKey<T1, T2, TView>, IViewFactoryRelease<TView> where TView : IView
```

#### Type Parameters

`T1` 

The type of the first extra argument.

`T2` 

The type of the second extra argument.

`TView` 

The type of the created view.

#### Implements

[IViewFactoryWithKey\<T1, T2, TView\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-3.md), 
[IViewFactoryRelease\<TView\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)


## Methods

### Create\(IViewModel?, T1?, T2?\) {#Aspid_MVVM_StarterKit_IViewFactory_3_Create_Aspid_MVVM_IViewModel__0__1_}

Creates a view for <code class="paramref">viewModel</code>.

```csharp
TView Create(IViewModel? viewModel, T1? param1, T2? param2)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

`param1` T1?

The first extra argument.

`param2` T2?

The second extra argument.

#### Returns

 TView

The created view.

