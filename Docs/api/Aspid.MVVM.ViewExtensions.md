---
title: "Class ViewExtensions"
sidebar_label: "ViewExtensions"
description: "Class ViewExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewExtensions {#Aspid_MVVM_ViewExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Provides extension methods for [`IView`](Aspid.MVVM.IView.md) providing helpers for reinitialization and safe disposal.

```csharp
public static class ViewExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ViewExtensions](Aspid.MVVM.ViewExtensions.md)



## Fields

### DisposeViewMarker {#Aspid_MVVM_ViewExtensions_DisposeViewMarker}

```csharp
public static readonly ProfilerMarker DisposeViewMarker
```

#### Field Value

 ProfilerMarker

## Methods

### DeinitializeView\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_}

Deinitializes the view and returns the associated [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static IViewModel? DeinitializeView<T>(this T? view) where T : IView
```

#### Parameters

`view` T?

The view to be deinitialized.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The associated [`IViewModel`](Aspid.MVVM.IViewModel.md), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if none is present.

#### Type Parameters

`T` 

The type of the view that implements [`IView`](Aspid.MVVM.IView.md).

### DisposeView\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DisposeView__1___0_}

Disposes the view and returns the associated [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static IViewModel? DisposeView<T>(this T? view) where T : IView, IDisposable
```

#### Parameters

`view` T?

The view to be disposed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The associated [`IViewModel`](Aspid.MVVM.IViewModel.md), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if none is present.

#### Type Parameters

`T` 

The type of the view that implements [`IView`](Aspid.MVVM.IView.md) and [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable).

### DisposeView\(IView?\) {#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_}

Disposes the view if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) and returns the associated [`IViewModel`](Aspid.MVVM.IViewModel.md).

```csharp
public static IViewModel? DisposeView(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The view to be disposed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The associated [`IViewModel`](Aspid.MVVM.IViewModel.md), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if none is present.

### Reinitialize\(IView?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes the view from its current ViewModel and optionally reinitializes it with a new one.

```csharp
public static IViewModel? Reinitialize(this IView? view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The view to reinitialize.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The new ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to only deinitialize.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previously associated [`IViewModel`](Aspid.MVVM.IViewModel.md), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if none was present.

