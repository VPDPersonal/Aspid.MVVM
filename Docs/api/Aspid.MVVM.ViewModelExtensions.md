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

Provides extension methods for [`IViewModel`](Aspid.MVVM.IViewModel.md) providing lifecycle helpers such as disposal.

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

Disposes the ViewModel instance and returns the disposal marker.

```csharp
public static void DisposeViewModel<T>(this T viewModel) where T : class, IViewModel, IDisposable
```

#### Parameters

`viewModel` T

The ViewModel instance to dispose of.

#### Type Parameters

`T` 

The type of the ViewModel that implements [`IViewModel`](Aspid.MVVM.IViewModel.md) and [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable).

### DisposeViewModel\(IViewModel\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_}

Disposes of the ViewModel instance if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable).

```csharp
public static void DisposeViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel instance to dispose of.

