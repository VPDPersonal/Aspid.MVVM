---
title: "Enum ResolveType"
sidebar_label: "ResolveType"
description: "Enum ResolveType — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Enum ResolveType {#Aspid_MVVM_StarterKit_ResolveType}

Namespace: [Aspid.MVVM.StarterKit](Aspid.MVVM.StarterKit.md)  
Assembly: Aspid.MVVM.StarterKit.dll  

Where an [`InitializeComponent<T>`](Aspid.MVVM.StarterKit.InitializeComponent-1.md) takes its instance from.

```csharp
public enum ResolveType
```


## Fields

`Component = 0` 

A [`Component`](https://docs.unity3d.com/ScriptReference/Component.html) reference.



`Reference = 1` 

A serialized plain C# instance.



`ScriptableObject = 2` 

A [`ScriptableObject`](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) reference.



`Di = 3` 

Resolved from the DI container by type name.



