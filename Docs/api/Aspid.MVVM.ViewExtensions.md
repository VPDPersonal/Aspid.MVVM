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



## Methods

### DeinitializeView\<T\>\(T\) {#Aspid_MVVM_ViewExtensions_DeinitializeView__1___0_}

Deinitializes <code class="paramref">view</code>.

```csharp
public static IViewModel? DeinitializeView<T>(this T view) where T : class, IView
```

#### Parameters

`view` T

The view to operate on.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none.

#### Type Parameters

`T` 

The view type.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DisposeView\<T\>\(T\) {#Aspid_MVVM_ViewExtensions_DisposeView__1___0_}

Disposes <code class="paramref">view</code>.

```csharp
public static IViewModel? DisposeView<T>(this T view) where T : class, IView, IDisposable
```

#### Parameters

`view` T

The view to operate on.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none.

#### Type Parameters

`T` 

The view type.

#### Remarks

Does not dispose the returned ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DisposeView\(IView\) {#Aspid_MVVM_ViewExtensions_DisposeView_Aspid_MVVM_IView_}

Disposes <code class="paramref">view</code> if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable); otherwise, deinitializes it.

```csharp
public static IViewModel? DisposeView(this IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The view to operate on.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none.

#### Remarks

Does not dispose the returned ViewModel.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### Reinitialize\(IView, IViewModel?\) {#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes <code class="paramref">view</code> and initializes it with <code class="paramref">newViewModel</code>.

```csharp
public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The view to operate on.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> leaves it deinitialized.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none.

#### Remarks

Does not dispose either ViewModel. If initialization fails, recovery follows the View implementation's contract.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

