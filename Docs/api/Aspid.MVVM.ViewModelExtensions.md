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

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### DisposeViewModel\(IViewModel\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModel_Aspid_MVVM_IViewModel_}

Disposes <code class="paramref">viewModel</code> if it implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable); otherwise, does nothing.

```csharp
public static void DisposeViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to dispose.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

