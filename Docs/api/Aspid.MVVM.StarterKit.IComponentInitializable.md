---
title: "Interface IComponentInitializable"
sidebar_label: "IComponentInitializable"
description: "Interface IComponentInitializable — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IComponentInitializable {#Aspid_MVVM_StarterKit_IComponentInitializable}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

A View or ViewModel that needs a setup call after a [`ViewInitializerBase`](Aspid.MVVM.StarterKit.ViewInitializerBase.md) resolves it.

```csharp
public interface IComponentInitializable
```


## Methods

### Initialize\(\) {#Aspid_MVVM_StarterKit_IComponentInitializable_Initialize}

Runs the setup once the component is resolved.

```csharp
void Initialize()
```

