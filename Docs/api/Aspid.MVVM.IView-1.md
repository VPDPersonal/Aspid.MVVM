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

[`IView`](Aspid.MVVM.IView.md) that accepts a ViewModel of <code class="typeparamref">T</code>.

```csharp
public interface IView<in T> : IView where T : IViewModel
```

#### Type Parameters

`T` 

The ViewModel type the view binds to.

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

Initializes the view with <code class="paramref">viewModel</code>, binding its binders to the ViewModel's members.

```csharp
void Initialize(T viewModel)
```

#### Parameters

`viewModel` T

The ViewModel to bind.

