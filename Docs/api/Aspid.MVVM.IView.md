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

Interface for initializing a View using a specified ViewModel.

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

Gets the associated ViewModel.
If the view is not initialized, it may return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
IViewModel? ViewModel { get; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)?

## Methods

### Deinitialize\(\) {#Aspid_MVVM_IView_Deinitialize}

Deinitializes the view, resetting the ViewModel property to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
void Deinitialize()
```

### Initialize\(IViewModel\) {#Aspid_MVVM_IView_Initialize_Aspid_MVVM_IViewModel_}

Initializes the view with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md) for binding.

```csharp
void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The [`IViewModel`](Aspid.MVVM.IViewModel.md) object used to initialize the View.

