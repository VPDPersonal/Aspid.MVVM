---
title: "Class ViewModelExtensions"
sidebar_label: "ViewModelExtensions"
description: "Class ViewModelExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewModelExtensions {#Aspid_MVVM_ViewModelExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

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

### InitializeView\(IViewModel, IView\) {#Aspid_MVVM_ViewModelExtensions_InitializeView_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_}

Initializes <code class="paramref">view</code> with <code class="paramref">viewModel</code>.

```csharp
public static void InitializeView(this IViewModel viewModel, IView view)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

`view` [IView](Aspid.MVVM.IView.md)

The View to initialize.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code> or <code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\(IViewModel, params IView\[\]\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews_Aspid_MVVM_IViewModel_Aspid_MVVM_IView___}

Initializes each View with <code class="paramref">viewModel</code> in collection order.

```csharp
public static void InitializeViews(this IViewModel viewModel, params IView[] views)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

`views` [IView](Aspid.MVVM.IView.md)\[\]

The Views to initialize.

#### Remarks

Stops at the first exception; previously initialized Views remain initialized.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code>, <code class="paramref">views</code>, or an element is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\<T\>\(IViewModel, List\<T\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__}

Initializes each View with <code class="paramref">viewModel</code> in collection order.

```csharp
public static void InitializeViews<T>(this IViewModel viewModel, List<T> views) where T : IView
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>

The Views to initialize.

#### Type Parameters

`T`

The View type.

#### Remarks

Stops at the first exception; previously initialized Views remain initialized.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code>, <code class="paramref">views</code>, or an element is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### InitializeViews\(IViewModel, IEnumerable\<IView\>\) {#Aspid_MVVM_ViewModelExtensions_InitializeViews_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable_Aspid_MVVM_IView__}

Initializes each View with <code class="paramref">viewModel</code> in collection order.

```csharp
public static void InitializeViews(this IViewModel viewModel, IEnumerable<IView> views)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The ViewModel to bind.

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[IView](Aspid.MVVM.IView.md)\>

The Views to initialize.

#### Remarks

Stops at the first exception; previously initialized Views remain initialized.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">viewModel</code>, <code class="paramref">views</code>, or an element is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.
