---
title: "Interface IView"
sidebar_label: "IView"
description: "Interface IView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IView {#Aspid_MVVM_IView}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Defines a View that binds its binders to a ViewModel.

```csharp
public interface IView
```

#### Extension Methods

[ViewExtensions.DeinitializeView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView_Aspid_MVVM_IView_),
[ViewExtensions.DeinitializeViewSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeViewSafely_Aspid_MVVM_IView_),
[ViewExtensions.DestroyView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_),
[ViewExtensions.DestroyViewAndGameObject\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_),
[ViewExtensions.DestroyViewAndGameObjectSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObjectSafely_Aspid_MVVM_IView_),
[ViewExtensions.DestroyViewSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DestroyViewSafely_Aspid_MVVM_IView_),
[ViewExtensions.DisposeView\(IView\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_),
[ViewExtensions.DisposeViewSafely\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_),
[ViewExtensions.InitializeSafely\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_InitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_),
[ViewExtensions.Reinitialize\(IView, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_),
[ViewExtensions.ReinitializeSafely\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_ReinitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Remarks

Binder instances and collection contents stay unchanged between [`IView.Initialize`](Aspid.MVVM.IView.md#Aspid_MVVM_IView_Initialize_Aspid_MVVM_IViewModel_) and [`IView.Deinitialize`](Aspid.MVVM.IView.md#Aspid_MVVM_IView_Deinitialize).

## Properties

### ViewModel {#Aspid_MVVM_IView_ViewModel}

Gets the bound ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> before initialization and after deinitialization.

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

#### Remarks

Does not dispose the ViewModel.

### Initialize\(IViewModel\) {#Aspid_MVVM_IView_Initialize_Aspid_MVVM_IViewModel_}

Binds the View to the members of <code class="paramref">viewModel</code>.

```csharp
void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.
