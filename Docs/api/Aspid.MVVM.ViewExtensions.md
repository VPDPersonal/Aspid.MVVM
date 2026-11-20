---
title: "Class ViewExtensions"
sidebar_label: "ViewExtensions"
description: "Class ViewExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewExtensions {#Aspid_MVVM_ViewExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Provides extension methods for [`IView`](Aspid.MVVM.IView.md).

```csharp
public static class ViewExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[ViewExtensions](Aspid.MVVM.ViewExtensions.md)



## Methods

### DeinitializeView\(IView\) {#Aspid_MVVM_ViewExtensions_DeinitializeView_Aspid_MVVM_IView_}

Deinitializes <code class="paramref">view</code>.

```csharp
public static IViewModel? DeinitializeView(this IView view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The view to operate on.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

### DeinitializeViewSafely\(IView?\) {#Aspid_MVVM_ViewExtensions_DeinitializeViewSafely_Aspid_MVVM_IView_}

Deinitializes the View, logging any exception.

```csharp
public static IViewModel? DeinitializeViewSafely(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to deinitialize. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

### DestroyView\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DestroyView__1___0_}

Disposes or deinitializes the view and destroys its Unity object while preserving any GameObject.

```csharp
public static IViewModel? DestroyView<T>(this T? view) where T : Object, IView
```

#### Parameters

`view` T?

The view to destroy; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent or the Unity object was already destroyed.

#### Type Parameters

`T`

The Unity view object.

#### Remarks

Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
Does not dispose the returned ViewModel.

### DestroyView\(IView?\) {#Aspid_MVVM_ViewExtensions_DestroyView_Aspid_MVVM_IView_}

Disposes or deinitializes the view and destroys its Unity object while preserving any GameObject.

```csharp
public static IViewModel? DestroyView(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The view to destroy; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent or the Unity object was already destroyed.

#### Remarks

Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
Does not dispose the returned ViewModel. Managed views are only disposed or deinitialized.

### DestroyViewAndGameObject\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject__1___0_}

Disposes or deinitializes the view and destroys its GameObject or the Unity object itself.

```csharp
public static IViewModel? DestroyViewAndGameObject<T>(this T? view) where T : Object, IView
```

#### Parameters

`view` T?

The view to destroy; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent or the Unity object was already destroyed.

#### Type Parameters

`T`

The Unity view object.

#### Remarks

Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
Does not dispose the returned ViewModel.

### DestroyViewAndGameObject\(IView?\) {#Aspid_MVVM_ViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_}

Disposes or deinitializes the view and destroys its GameObject or the Unity object itself.

```csharp
public static IViewModel? DestroyViewAndGameObject(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The view to destroy; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to do nothing.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent or the Unity object was already destroyed.

#### Remarks

Call on the main thread; destruction is immediate in Edit Mode and deferred in Play Mode.
Destruction is attempted even if cleanup fails; cleanup exceptions propagate to the caller.
Does not dispose the returned ViewModel. Managed views are only disposed or deinitialized.

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

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

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

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

### DisposeViewSafely\<T\>\(T?\) {#Aspid_MVVM_ViewExtensions_DisposeViewSafely__1___0_}

Disposes the View, logging any exception.

```csharp
public static IViewModel? DisposeViewSafely<T>(this T? view) where T : class, IView, IDisposable
```

#### Parameters

`view` T?

The View to dispose. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

#### Type Parameters

`T`

The disposable View type.

### DisposeViewSafely\(IView?\) {#Aspid_MVVM_ViewExtensions_DisposeViewSafely_Aspid_MVVM_IView_}

Disposes a disposable View or deinitializes it otherwise, logging any exception.

```csharp
public static IViewModel? DisposeViewSafely(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to clean up. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.

### Reinitialize\(IView, IViewModel?\) {#Aspid_MVVM_ViewExtensions_Reinitialize_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes <code class="paramref">view</code> and initializes it with <code class="paramref">newViewModel</code>.

```csharp
public static IViewModel? Reinitialize(this IView view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)

The view to operate on.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with; A missing or destroyed ViewModel leaves it deinitialized.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel the view had; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when it had none.

#### Remarks

Does not dispose either ViewModel. If initialization fails, recovery follows the View implementation's contract.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">view</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or a destroyed Unity object.

### ReinitializeSafely\(IView?, IViewModel?\) {#Aspid_MVVM_ViewExtensions_ReinitializeSafely_Aspid_MVVM_IView_Aspid_MVVM_IViewModel_}

Deinitializes the View and initializes it with <code class="paramref">newViewModel</code>, logging any exception.

```csharp
public static IViewModel? ReinitializeSafely(this IView? view, IViewModel? newViewModel)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The View to reinitialize. Skips <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> and destroyed Unity objects.

`newViewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The replacement ViewModel, A missing or destroyed ViewModel only deinitializes the View.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The previous ViewModel on success, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if absent, skipped, or failed.
