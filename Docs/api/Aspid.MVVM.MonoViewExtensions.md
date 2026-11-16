---
title: "Class MonoViewExtensions"
sidebar_label: "MonoViewExtensions"
description: "Class MonoViewExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoViewExtensions {#Aspid_MVVM_MonoViewExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.Unity.dll <br/>

Provides extension methods for [`IView`](Aspid.MVVM.IView.md).

```csharp
public static class MonoViewExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[MonoViewExtensions](Aspid.MVVM.MonoViewExtensions.md)



## Methods

### DestroyView\<T\>\(T?\) {#Aspid_MVVM_MonoViewExtensions_DestroyView__1___0_}

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

### DestroyView\(IView?\) {#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_}

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

### DestroyViewAndGameObject\<T\>\(T?\) {#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject__1___0_}

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

### DestroyViewAndGameObject\(IView?\) {#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_}

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
