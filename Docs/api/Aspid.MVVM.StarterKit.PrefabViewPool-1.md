---
title: "Class PrefabViewPool<T>"
sidebar_label: "PrefabViewPool<T>"
description: "Class PrefabViewPool<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PrefabViewPool\<T\> {#Aspid_MVVM_StarterKit_PrefabViewPool_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`PrefabViewFactory<T>`](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md) that keeps released views in an [`ObjectPool<T>`](https://docs.unity3d.com/ScriptReference/Pool-ObjectPool.html) and reuses them.

```csharp
[Serializable]
public class PrefabViewPool<T> : PrefabViewFactory<T>, IViewFactory<T>, IViewFactoryWithKey<T>, IViewFactoryRelease<T> where T : MonoBehaviour, IView
```

#### Type Parameters

`T` 

The type of the view component on the prefab.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PrefabViewFactory\<T\>](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md) ← 
[PrefabViewPool\<T\>](Aspid.MVVM.StarterKit.PrefabViewPool-1.md)

#### Implements

[IViewFactory\<T\>](Aspid.MVVM.StarterKit.IViewFactory-1.md), 
[IViewFactoryWithKey\<T\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-1.md), 
[IViewFactoryRelease\<T\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)



## Remarks

A released view is deinitialized and deactivated. A reused view is activated, repositioned and initialized again.

## Constructors

### PrefabViewPool\(\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1__ctor}

```csharp
protected PrefabViewPool()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### PrefabViewPool\(T, bool, int\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1__ctor__0_System_Boolean_System_Int32_}

```csharp
public PrefabViewPool(T prefab, bool overrideSibling = false, int siblingIndex = 0)
```

#### Parameters

`prefab` T

The prefab to instantiate.

`overrideSibling` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to place new views at <code class="paramref">siblingIndex</code> instead of last.

`siblingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The sibling index used when <code class="paramref">overrideSibling</code> is set.

### PrefabViewPool\(T, PoolSettings, bool, int\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1__ctor__0_Aspid_MVVM_StarterKit_PoolSettings_System_Boolean_System_Int32_}

```csharp
public PrefabViewPool(T prefab, PoolSettings settings, bool overrideSibling = false, int siblingIndex = 0)
```

#### Parameters

`prefab` T

The prefab to instantiate.

`settings` [PoolSettings](Aspid.MVVM.StarterKit.PoolSettings.md)

The pool size limits.

`overrideSibling` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to place new views at <code class="paramref">siblingIndex</code> instead of last.

`siblingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The sibling index used when <code class="paramref">overrideSibling</code> is set.

### PrefabViewPool\(T, Transform?, bool, int\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1__ctor__0_UnityEngine_Transform_System_Boolean_System_Int32_}

```csharp
public PrefabViewPool(T prefab, Transform? container, bool overrideSibling = false, int siblingIndex = 0)
```

#### Parameters

`prefab` T

The prefab to instantiate.

`container` Transform?

The parent of created views, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`overrideSibling` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to place new views at <code class="paramref">siblingIndex</code> instead of last.

`siblingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The sibling index used when <code class="paramref">overrideSibling</code> is set.

### PrefabViewPool\(T, Transform?, PoolSettings, bool, int\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1__ctor__0_UnityEngine_Transform_Aspid_MVVM_StarterKit_PoolSettings_System_Boolean_System_Int32_}

```csharp
public PrefabViewPool(T prefab, Transform? container, PoolSettings settings, bool overrideSibling = false, int siblingIndex = 0)
```

#### Parameters

`prefab` T

The prefab to instantiate.

`container` Transform?

The parent of created views, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the scene root.

`settings` [PoolSettings](Aspid.MVVM.StarterKit.PoolSettings.md)

The pool size limits.

`overrideSibling` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to place new views at <code class="paramref">siblingIndex</code> instead of last.

`siblingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The sibling index used when <code class="paramref">overrideSibling</code> is set.

## Methods

### Create\(IViewModel?\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1_Create_Aspid_MVVM_IViewModel_}

Takes a view from the pool, instantiating one if none is free, and initializes it with <code class="paramref">viewModel</code>.

```csharp
public override T Create(IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

#### Returns

 T

The activated view.

### OnCreate\(IViewModel?, T\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1_OnCreate_Aspid_MVVM_IViewModel__0_}

Does nothing: the pool applies [`PrefabViewFactory<T>.OnCreate`](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md) when a view is taken, not when it is instantiated.

```csharp
protected override sealed void OnCreate(IViewModel? viewModel, T view)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

Ignored.

`view` T

Ignored.

### Release\(T\) {#Aspid_MVVM_StarterKit_PrefabViewPool_1_Release__0_}

Deinitializes and deactivates the view, then returns it to the pool.

```csharp
public override void Release(T view)
```

#### Parameters

`view` T

The view to release.

