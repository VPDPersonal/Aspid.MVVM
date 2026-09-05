---
title: "Interface IView<T>"
sidebar_label: "IView<T>"
description: "Interface IView<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IView\<T\> {#Aspid_MVVM_IView_1}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Generic interface for initializing a View with a strongly-typed ViewModel.

```csharp
public interface IView<in T> : IView where T : IViewModel
```

#### Type Parameters

`T` 

The specific type of the ViewModel to be used for initialization. Must implement [`IViewModel`](Aspid.MVVM.IViewModel.md).

#### Implements

[IView](Aspid.MVVM.IView.md)

#### Extension Methods

[ViewExtensions.DeinitializeView\<IView\<T\>\>\(IView\<T\>?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_), 
[MonoViewExtensions.DestroyView\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_), 
[MonoViewExtensions.DestroyViewAndGameObject\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_), 
[ViewExtensions.DisposeView\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_), 
[ViewExtensions.Reinitialize\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Methods

### Initialize\(T\) {#Aspid_MVVM_IView_1_Initialize__0_}

Initializes the view with a strongly-typed <code class="typeparamref">T</code> ViewModel.

```csharp
void Initialize(T viewModel)
```

#### Parameters

`viewModel` T

The <code class="typeparamref">T</code> ViewModel instance to initialize the View.

