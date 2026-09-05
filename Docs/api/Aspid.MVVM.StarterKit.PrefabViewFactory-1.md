---
title: "Class PrefabViewFactory<T>"
sidebar_label: "PrefabViewFactory<T>"
description: "Class PrefabViewFactory<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class PrefabViewFactory\<T\> {#Aspid_MVVM_StarterKit_PrefabViewFactory_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

[`IViewFactory<T>`](Aspid.MVVM.StarterKit.IViewFactory-1.md) that instantiates a prefab per view and destroys it on release.

```csharp
[Serializable]
public class PrefabViewFactory<T> : IViewFactory<T>, IViewFactoryWithKey<T>, IViewFactoryRelease<T> where T : MonoBehaviour, IView
```

#### Type Parameters

`T` 

The type of the view component on the prefab.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PrefabViewFactory\<T\>](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md)

#### Implements

[IViewFactory\<T\>](Aspid.MVVM.StarterKit.IViewFactory-1.md), 
[IViewFactoryWithKey\<T\>](Aspid.MVVM.StarterKit.IViewFactoryWithKey-1.md), 
[IViewFactoryRelease\<T\>](Aspid.MVVM.StarterKit.IViewFactoryRelease-1.md)



## Constructors

### PrefabViewFactory\(\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1__ctor}

```csharp
protected PrefabViewFactory()
```

#### Remarks

For deserialization only: Unity assigns the fields itself.

### PrefabViewFactory\(T, bool, int\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1__ctor__0_System_Boolean_System_Int32_}

```csharp
public PrefabViewFactory(T prefab, bool overrideSibling = false, int siblingIndex = 0)
```

#### Parameters

`prefab` T

The prefab to instantiate.

`overrideSibling` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Whether to place new views at <code class="paramref">siblingIndex</code> instead of last.

`siblingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The sibling index used when <code class="paramref">overrideSibling</code> is set.

### PrefabViewFactory\(T, Transform?, bool, int\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1__ctor__0_UnityEngine_Transform_System_Boolean_System_Int32_}

```csharp
public PrefabViewFactory(T prefab, Transform? container, bool overrideSibling = false, int siblingIndex = 0)
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

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">prefab</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

Thrown when <code class="paramref">siblingIndex</code> is negative.

## Methods

### Create\(IViewModel?\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1_Create_Aspid_MVVM_IViewModel_}

Instantiates the prefab and runs [`PrefabViewFactory<T>.OnCreate`](Aspid.MVVM.StarterKit.PrefabViewFactory-1.md) on it.

```csharp
public virtual T Create(IViewModel? viewModel)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to leave it uninitialized.

#### Returns

 T

The created view.

### OnCreate\(IViewModel?, T\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1_OnCreate_Aspid_MVVM_IViewModel__0_}

Places the view among its siblings and initializes it with <code class="paramref">viewModel</code> if one is given.

```csharp
protected virtual void OnCreate(IViewModel? viewModel, T view)
```

#### Parameters

`viewModel` [IViewModel](Aspid.MVVM.IViewModel.md)?

The ViewModel to initialize the view with, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> to skip initialization.

`view` T

The freshly instantiated view.

### Release\(T\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1_Release__0_}

Destroys the view together with its GameObject.

```csharp
public virtual void Release(T view)
```

#### Parameters

`view` T

The view to release.

### SetSibling\(T\) {#Aspid_MVVM_StarterKit_PrefabViewFactory_1_SetSibling__0_}

Moves the view to the configured sibling position.

```csharp
protected void SetSibling(T view)
```

#### Parameters

`view` T

The view to move.

