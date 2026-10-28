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

Provides extension methods for [`IView`](Aspid.MVVM.IView.md).

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

Deinitializes <code class="paramref">view</code>.

```csharp
public static IViewModel? DeinitializeView<T>(this T? view) where T : IView
```

#### Parameters

`view` T?

The view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none or <code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The view type.

### DisposeView\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DisposeView__1___0_}

Disposes <code class="paramref">view</code>.

```csharp
public static IViewModel? DisposeView<T>(this T? view) where T : IView, IDisposable
```

#### Parameters

`view` T?

The view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none or <code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T` 

The view type.

### DisposeView\(IView?\) {#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_}

Disposes <code class="paramref">view</code> if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable); otherwise, deinitializes it.

```csharp
public static IViewModel? DisposeView(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none or <code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Reinitialize\(IView?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes <code class="paramref">view</code> and initializes it with <code class="paramref">newViewModel</code>.

```csharp
public static IViewModel? Reinitialize(this IView? view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The view, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> leaves it deinitialized.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none or <code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

