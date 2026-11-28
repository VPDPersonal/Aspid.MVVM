---
title: "Class ScriptableView"
sidebar_label: "ScriptableView"
description: "Class ScriptableView — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ScriptableView {#Aspid_MVVM_ScriptableView}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

[`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) for views with generated binding lifecycle methods.

```csharp
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
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

## Properties

### ViewModel {#Aspid_MVVM_ScriptableView_ViewModel}

Gets the bound ViewModel, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> before initialization and after deinitialization.

```csharp
public IViewModel ViewModel { get; protected set; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

## Methods

### Deinitialize\(\) {#Aspid_MVVM_ScriptableView_Deinitialize}

Unbinds the binders and clears [`IView.ViewModel`](Aspid.MVVM.IView.md#Aspid_MVVM_IView_ViewModel).

```csharp
public void Deinitialize()
```

#### Remarks

Does not dispose the ViewModel.

#### Exceptions

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

Initialization is in progress.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Multiple cleanup operations fail.

### DeinitializeInternal\(\) {#Aspid_MVVM_ScriptableView_DeinitializeInternal}

```csharp
protected virtual void DeinitializeInternal()
```

### Dispose\(\) {#Aspid_MVVM_ScriptableView_Dispose}

Deinitializes the view and releases its bindings without destroying any Unity objects.

```csharp
public virtual void Dispose()
```

#### Remarks

Overrides must call the base implementation.

### Initialize\(IViewModel\) {#Aspid_MVVM_ScriptableView_Initialize_Aspid_MVVM_IViewModel_}

Binds the View to the members of <code class="paramref">viewModel</code>.

```csharp
public void Initialize(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [InvalidOperationException](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception)

The View is already initialized.

### InitializeInternal\(IViewModel\) {#Aspid_MVVM_ScriptableView_InitializeInternal_Aspid_MVVM_IViewModel_}

```csharp
protected virtual void InitializeInternal(IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

### OnDestroy\(\) {#Aspid_MVVM_ScriptableView_OnDestroy}

Called when Unity destroys the component to dispose the view.

```csharp
protected virtual void OnDestroy()
```

#### Remarks

Overrides must call the base implementation.
