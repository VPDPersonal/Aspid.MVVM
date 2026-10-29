---
title: "Interface IView"
sidebar_label: "IView"
description: "Interface IView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IView {#Aspid_MVVM_IView}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Defines a View that binds its binders to a ViewModel.

```csharp
public interface IView
```

#### Extension Methods

[ViewExtensions.DeinitializeView\<IView\>\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_), 
[MonoViewExtensions.DestroyView\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_), 
[MonoViewExtensions.DestroyViewAndGameObject\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_), 
[ViewExtensions.DisposeView\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_), 
[ViewExtensions.Reinitialize\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Properties

### ViewModel {#Aspid_MVVM_IView_ViewModel}

Gets the ViewModel the view is initialized with; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it is not initialized.

```csharp
IViewModel? ViewModel { get; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)?

## Methods

### Deinitialize\(\) {#Aspid_MVVM_IView_Deinitialize}

Unbinds the binders and clears [`IView.ViewModel`](Aspid.MVVM.IView.md#Aspid_MVVM_IView_ViewModel).

```csharp
void Deinitialize()
```

### Initialize\(IViewModel\) {#Aspid_MVVM_IView_Initialize_Aspid_MVVM_IViewModel_}

Initializes the view with <code class="paramref">viewModel</code>, binding its binders to the ViewModel's members.

```csharp
void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

