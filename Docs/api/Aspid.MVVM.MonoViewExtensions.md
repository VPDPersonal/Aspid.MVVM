---
title: "Class MonoViewExtensions"
sidebar_label: "MonoViewExtensions"
description: "Class MonoViewExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoViewExtensions {#Aspid_MVVM_MonoViewExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Provides extension methods for the [`IView`](Aspid.MVVM.IView.md) interface.

```csharp
public static class MonoViewExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MonoViewExtensions](Aspid.MVVM.MonoViewExtensions.md)



## Methods

### DestroyView\<T\>\(T?\) {#Aspid_MVVM_MonoViewExtensions_DestroyView__1___0_}

Destroys the View component and deinitializes it if it does not implement the [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) interface.
If the View implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), it calls the [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) method.
Returns the [`IViewModel`](Aspid.MVVM.IViewModel.md) instance that was bound to the View before its destruction.

```csharp
public static IViewModel? DestroyView<T>(this T? view) where T : Component, IView
```

#### Parameters

`view` T?

The instance of the View component to be destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel that was bound to the View, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if there was no ViewModel.

#### Type Parameters

`T` 

The type of View that inherits from [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) and implements the [`IView`](Aspid.MVVM.IView.md) interface.

### DestroyView\(IView?\) {#Aspid_MVVM_MonoViewExtensions_DestroyView_Aspid_MVVM_IView_}

Destroys the View component and deinitializes it if it does not implement the [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) interface.
If the View implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), it calls the [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) method.
Returns the [`IViewModel`](Aspid.MVVM.IViewModel.md) instance that was bound to the View before its destruction.

```csharp
public static IViewModel? DestroyView(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The instance of the View component to be destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel that was bound to the View, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if there was no ViewModel.

### DestroyViewAndGameObject\<T\>\(T?\) {#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject__1___0_}

Destroys the View component's GameObject and deinitializes it if it does not implement the [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) interface.
If the View implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), it calls the [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) method.
Returns the [`IViewModel`](Aspid.MVVM.IViewModel.md) instance that was bound to the View before its destruction.

```csharp
public static IViewModel? DestroyViewAndGameObject<T>(this T? view) where T : Component, IView
```

#### Parameters

`view` T?

The instance of the View component to be destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel that was bound to the View, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if there was no ViewModel.

#### Type Parameters

`T` 

The type of View that inherits from [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) and implements the [`IView`](Aspid.MVVM.IView.md) interface.

### DestroyViewAndGameObject\(IView?\) {#Aspid_MVVM_MonoViewExtensions_DestroyViewAndGameObject_Aspid_MVVM_IView_}

Destroys the View component's GameObject and deinitializes it if it does not implement the [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) interface.
If the View implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), it calls the [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) method.
Returns the [`IViewModel`](Aspid.MVVM.IViewModel.md) instance that was bound to the View before its destruction.

```csharp
public static IViewModel? DestroyViewAndGameObject(this IView? view)
```

#### Parameters

`view` [IView](Aspid.MVVM.IView.md)?

The instance of the View component to be destroyed.

#### Returns

 [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel that was bound to the View, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if there was no ViewModel.

