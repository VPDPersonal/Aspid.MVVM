---
title: "Class ViewModelExtensions"
sidebar_label: "ViewModelExtensions"
description: "Class ViewModelExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewModelExtensions {#Aspid_MVVM_ViewModelExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Provides extension methods for [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static class ViewModelExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ViewModelExtensions](Aspid.MVVM.ViewModelExtensions.md)



## Fields

### DisposeViewModelMarker {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelMarker}

```csharp
public static readonly ProfilerMarker DisposeViewModelMarker
```

#### Field Value

 ProfilerMarker

## Methods

### DisposeViewModel\<T\>\(T\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModel__1___0_}

Disposes <code class="paramref">viewModel</code>.

```csharp
public static void DisposeViewModel<T>(this T viewModel) where T : class, IViewModel, IDisposable
```

#### Parameters

`viewModel` T

The ViewModel to dispose.

#### Type Parameters

`T` 

The ViewModel type.

### DisposeViewModel\(IViewModel\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_}

Disposes <code class="paramref">viewModel</code> if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable); otherwise, does nothing.

```csharp
public static void DisposeViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to dispose.

