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

### DestroyViewModel\<T\>\(T?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModel__1___0_}

Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.

```csharp
public static GameObject? DestroyViewModel<T>(this T? viewModel) where T : Object, IViewModel
```

#### Parameters

`viewModel` T?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T`

The Unity ViewModel object.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller.</p>

### DestroyViewModel\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if supported and destroys its Unity object while preserving any GameObject.

```csharp
public static GameObject? DestroyViewModel(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller. Managed ViewModels are only disposed.</p>

### DestroyViewModelAndGameObject\<T\>\(T?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject__1___0_}

Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.

```csharp
public static GameObject? DestroyViewModelAndGameObject<T>(this T? viewModel) where T : Object, IViewModel
```

#### Parameters

`viewModel` T?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Type Parameters

`T`

The Unity ViewModel object.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller.</p>

### DestroyViewModelAndGameObject\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObject_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if supported and destroys its GameObject or the Unity object itself.

```csharp
public static GameObject? DestroyViewModelAndGameObject(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The associated GameObject if still alive after the call; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Remarks

<p>Call on the main thread for Unity objects; destruction is deferred in Play Mode and immediate in Edit Mode.</p>
<p>Destruction is attempted even if disposal throws; cleanup exceptions propagate to the caller. Managed ViewModels are only disposed.</p>

### DestroyViewModelAndGameObjectSafely\<T\>\(T?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely__1___0_}

Disposes and destroys the Unity ViewModel and its GameObject, logging failures.

```csharp
public static GameObject? DestroyViewModelAndGameObjectSafely<T>(this T? viewModel) where T : Object, IViewModel
```

#### Parameters

`viewModel` T?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The surviving GameObject on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

#### Type Parameters

`T`

The Unity ViewModel object.

#### Remarks

Call on the main thread. Destruction is attempted even if disposal fails.

### DestroyViewModelAndGameObjectSafely\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelAndGameObjectSafely_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if supported, destroys its GameObject or Unity object if present, and logs failures.

```csharp
public static GameObject? DestroyViewModelAndGameObjectSafely(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The surviving GameObject on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if disposal fails.

### DestroyViewModelSafely\<T\>\(T?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely__1___0_}

Disposes and destroys the Unity ViewModel, preserving its GameObject and logging failures.

```csharp
public static GameObject? DestroyViewModelSafely<T>(this T? viewModel) where T : Object, IViewModel
```

#### Parameters

`viewModel` T?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The surviving GameObject on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

#### Type Parameters

`T`

The Unity ViewModel object.

#### Remarks

Call on the main thread. Destruction is attempted even if disposal fails.

### DestroyViewModelSafely\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DestroyViewModelSafely_Aspid_MVVM_IViewModel_}

Disposes the ViewModel if supported, destroys its Unity object if present, and logs failures.

```csharp
public static GameObject? DestroyViewModelSafely(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to destroy. Skips null and destroyed Unity objects.

#### Returns

 GameObject?

The surviving GameObject on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

#### Remarks

Call on the main thread for Unity objects. Destruction is attempted even if disposal fails.

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

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

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

<code class="paramref">viewModel</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

### DisposeViewModelSafely\<T\>\(T?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely__1___0_}

Disposes the ViewModel and logs any exception.

```csharp
public static void DisposeViewModelSafely<T>(this T? viewModel) where T : class, IViewModel, IDisposable
```

#### Parameters

`viewModel` T?

The ViewModel to dispose. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

#### Type Parameters

`T`

The disposable ViewModel type.

### DisposeViewModelSafely\(IViewModel?\) {#Aspid_MVVM_ViewModelExtensions_DisposeViewModelSafely_Aspid_MVVM_IViewModel_}

Disposes a disposable ViewModel and logs any exception.

```csharp
public static void DisposeViewModelSafely(this IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to dispose. Skips null and destroyed Unity objects.

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

<code class="paramref">viewModel</code> or <code class="paramref">view</code> is null or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both arguments are null or destroyed.

### InitializeViewSafely\(IViewModel?, IView?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView_}

Initializes the View and logs any exception.

```csharp
public static void InitializeViewSafely(this IViewModel? viewModel, IView? view)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to bind. Skips a missing or destroyed ViewModel.

`view` [IView](Aspid.MVVM.IView.md)?

The View to initialize. Skips a missing or destroyed View.

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

The ViewModel, collection, or a View is null or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both arguments are null or destroyed.

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

The ViewModel, collection, or a View is null or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both arguments are null or destroyed.

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

The ViewModel, collection, or a View is null or destroyed.

 [AggregateException](https://learn.microsoft.com/dotnet/api/system.aggregateexception)

Both arguments are null or destroyed.

### InitializeViewsSafely\(IViewModel?, params IView?\[\]?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely_Aspid_MVVM_IViewModel_Aspid_MVVM_IView___}

Initializes the Views in order, logging failures and continuing with the next View.

```csharp
public static void InitializeViewsSafely(this IViewModel? viewModel, params IView?[]? views)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to bind. Skips a missing or destroyed ViewModel.

`views` [IView](Aspid.MVVM.IView.md)?\[\]?

The Views to initialize. Skips a null collection and missing or destroyed Views.

### InitializeViewsSafely\<T\>\(IViewModel?, List\<T\>?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely__1_Aspid_MVVM_IViewModel_System_Collections_Generic_List___0__}

Initializes the Views in order, logging failures and continuing with the next View.

```csharp
public static void InitializeViewsSafely<T>(this IViewModel? viewModel, List<T>? views) where T : IView?
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to bind. Skips a missing or destroyed ViewModel.

`views` [List](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1)\<T\>?

The Views to initialize. Skips a null collection and missing or destroyed Views.

#### Type Parameters

`T`

The View type.

### InitializeViewsSafely\(IViewModel?, IEnumerable\<IView?\>?\) {#Aspid_MVVM_ViewModelExtensions_InitializeViewsSafely_Aspid_MVVM_IViewModel_System_Collections_Generic_IEnumerable_Aspid_MVVM_IView__}

Initializes the Views in order, logging failures and continuing with the next View.

```csharp
public static void InitializeViewsSafely(this IViewModel? viewModel, IEnumerable<IView?>? views)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to bind. Skips a missing or destroyed ViewModel.

`views` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[IView](Aspid.MVVM.IView.md)?\>?

The Views to initialize. Skips a null collection and missing or destroyed Views.

#### Remarks

An enumeration failure is logged and ends the batch.
