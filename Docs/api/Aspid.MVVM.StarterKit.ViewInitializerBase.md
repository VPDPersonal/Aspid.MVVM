---
title: "Class ViewInitializerBase"
sidebar_label: "ViewInitializerBase"
description: "Class ViewInitializerBase — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewInitializerBase {#Aspid_MVVM_StarterKit_ViewInitializerBase}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Abstract base [`MonoBehaviour`](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) that resolves a set of views and initializes them with a ViewModel.

```csharp
public abstract class ViewInitializerBase : MonoBehaviour
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
Object ← 
Component ← 
Behaviour ← 
MonoBehaviour ← 
[ViewInitializerBase](Aspid.MVVM.StarterKit.ViewInitializerBase.md)

#### Derived

[ViewInitializer](Aspid.MVVM.StarterKit.ViewInitializer.md), 
[ViewInitializerManual](Aspid.MVVM.StarterKit.ViewInitializerManual.md)



## Properties

### IsDisposeViewOnDestroy {#Aspid_MVVM_StarterKit_ViewInitializerBase_IsDisposeViewOnDestroy}

Gets whether the views are disposed when this object is destroyed.

```csharp
protected bool IsDisposeViewOnDestroy { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsInitialized {#Aspid_MVVM_StarterKit_ViewInitializerBase_IsInitialized}

Gets whether the views are currently initialized.

```csharp
public bool IsInitialized { get; protected set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### ViewModel {#Aspid_MVVM_StarterKit_ViewInitializerBase_ViewModel}

Gets the ViewModel the views are initialized with.

```csharp
public abstract IViewModel ViewModel { get; }
```

#### Property Value

 [IViewModel](Aspid.MVVM.IViewModel.md)

### Views {#Aspid_MVVM_StarterKit_ViewInitializerBase_Views}

Gets the resolved views. Resolved once in play mode and on every call in edit mode.

```csharp
public IView[] Views { get; }
```

#### Property Value

 [IView](Aspid.MVVM.IView.md)\[\]

## Methods

### OnDestroy\(\) {#Aspid_MVVM_StarterKit_ViewInitializerBase_OnDestroy}

Disposes the already resolved views if [`ViewInitializerBase.IsDisposeViewOnDestroy`](Aspid.MVVM.StarterKit.ViewInitializerBase.md#Aspid_MVVM_StarterKit_ViewInitializerBase_IsDisposeViewOnDestroy) is set.

```csharp
protected virtual void OnDestroy()
```

### OnValidate\(\) {#Aspid_MVVM_StarterKit_ViewInitializerBase_OnValidate}

Keeps the serialized view slots consistent with their resolve mode.

```csharp
protected virtual void OnValidate()
```

### Resolve\<T\>\(InitializeComponent\<T\>\) {#Aspid_MVVM_StarterKit_ViewInitializerBase_Resolve__1_Aspid_MVVM_StarterKit_InitializeComponent___0__}

Resolves the instance of the slot, handing it the DI container first.

```csharp
protected T Resolve<T>(InitializeComponent<T> initializeComponent) where T : class
```

#### Parameters

`initializeComponent` [InitializeComponent](Aspid.MVVM.StarterKit.InitializeComponent-1.md)\<T\>

The slot to resolve.

#### Returns

 T

The resolved instance, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when the slot is empty.

#### Type Parameters

`T` 

The resolved type.

