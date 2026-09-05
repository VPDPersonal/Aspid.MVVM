---
title: "Class ScriptableView"
sidebar_label: "ScriptableView"
description: "Class ScriptableView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScriptableView {#Aspid_MVVM_ScriptableView}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Represents a base class for views in a Unity context derived from [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html).

```csharp
public abstract class ScriptableView : ScriptableObject, IView
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
ScriptableObject ← 
[ScriptableView](Aspid.MVVM.ScriptableView.md)

#### Implements

[IView](Aspid.MVVM.IView.md)


#### Extension Methods

[ViewExtensions.DeinitializeView\<ScriptableView\>\(ScriptableView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_), 
[MonoViewExtensions.DestroyView\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_), 
[MonoViewExtensions.DestroyViewAndGameObject\(IView?\)](Aspid.MVVM.MonoViewExtensions.md#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_), 
[ViewExtensions.DisposeView\(IView?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_), 
[ViewExtensions.Reinitialize\(IView?, IViewModel?\)](Aspid.MVVM.ViewExtensions.md#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_)

## Properties

### ViewModel {#Aspid_MVVM_ScriptableView_ViewModel}

Gets the associated ViewModel.
If the view is not initialized, it may return <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public IViewModel ViewModel { get; protected set; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

## Methods

### Deinitialize\(\) {#Aspid_MVVM_ScriptableView_Deinitialize}

Deinitializes the view, resetting the ViewModel property to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

```csharp
public void Deinitialize()
```

### DeinitializeInternal\(\) {#Aspid_MVVM_ScriptableView_DeinitializeInternal}

```csharp
protected virtual void DeinitializeInternal()
```

### Initialize\(IViewModel\) {#Aspid_MVVM_ScriptableView_Initialize_Aspid_MVVM_IViewModel_}

Initializes the view with the specified [`IViewModel`](Aspid.MVVM.IViewModel.md) for binding.

```csharp
public void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The [`IViewModel`](Aspid.MVVM.IViewModel.md) object used to initialize the View.

### InitializeInternal\(IViewModel\) {#Aspid_MVVM_ScriptableView_InitializeInternal_Aspid_MVVM_IViewModel_}

```csharp
protected virtual void InitializeInternal(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

