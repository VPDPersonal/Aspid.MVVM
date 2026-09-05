---
title: "Interface IViewFactoryRelease<T>"
sidebar_label: "IViewFactoryRelease<T>"
description: "Interface IViewFactoryRelease<T> — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IViewFactoryRelease\<T\> {#Aspid_MVVM_StarterKit_IViewFactoryRelease_1}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Returns views produced by a factory back to it.

```csharp
public interface IViewFactoryRelease<in T> where T : IView
```

#### Type Parameters

`T` 

The type of the view.


## Methods

### Release\(T\) {#Aspid_MVVM_StarterKit_IViewFactoryRelease_1_Release__0_}

Releases a view created by this factory.

```csharp
void Release(T view)
```

#### Parameters

`view` T

The view to release.

