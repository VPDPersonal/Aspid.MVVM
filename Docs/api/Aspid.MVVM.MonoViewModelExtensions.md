---
title: "Class MonoViewModelExtensions"
sidebar_label: "MonoViewModelExtensions"
description: "Class MonoViewModelExtensions — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class MonoViewModelExtensions {#Aspid_MVVM_MonoViewModelExtensions}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.Unity.dll  

Provides extension methods for the [`IViewModel`](Aspid.MVVM.IViewModel.md) interface.

```csharp
public static class MonoViewModelExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MonoViewModelExtensions](Aspid.MVVM.MonoViewModelExtensions.md)



## Methods

### DestroyViewModel\<T\>\(T\) {#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel__1___0_}

Destroys the ViewModel component if it does not implement the [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) interface.
If the ViewModel implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), it calls the [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) method.
Returns the [`IViewModel`](Aspid.MVVM.IViewModel.md) instance that was bound to the View before its destruction.

```csharp
public static GameObject? DestroyViewModel<T>(this T viewModel) where T : Component, IViewModel
```

#### Parameters

`viewModel` T

The instance of the ViewModel to be destroyed.

#### Returns

 GameObject?

The ViewModel [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if it is destroyed or ViewModel is not a [`Component`](https://docs.unity3d.com/ScriptReference/Component.html).

#### Type Parameters

`T` 

The type of ViewModel that inherits from [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) and implements the [`IViewModel`](Aspid.MVVM.IViewModel.md) interface.

### DestroyViewModel\(IViewModel\) {#Aspid_MVVM_MonoViewModelExtensions_DestroyViewModel_Aspid_MVVM_IViewModel_}

Destroys the ViewModel component if it does not implement the [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) interface.
If the ViewModel implements [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), it calls the [`Dispose`](https://learn.microsoft.com/dotnet/api/system.idisposable.dispose) method.
Returns the [`IViewModel`](Aspid.MVVM.IViewModel.md) instance that was bound to the View before its destruction.

```csharp
public static GameObject? DestroyViewModel(this IViewModel viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)

The instance of the ViewModel to be destroyed.

#### Returns

 GameObject?

The ViewModel [`GameObject`](https://docs.unity3d.com/ScriptReference/GameObject.html), or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if it is destroyed or ViewModel is not a [`Component`](https://docs.unity3d.com/ScriptReference/Component.html).

