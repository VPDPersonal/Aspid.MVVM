---
title: "Class ViewModelAttribute"
sidebar_label: "ViewModelAttribute"
description: "Class ViewModelAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewModelAttribute {#Aspid_MVVM_ViewModelAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed marker [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) that drives the Source Generator to emit an [`IViewModel`](Aspid.MVVM.IViewModel.md)
implementation for the decorated class or struct and to analyze code blocks within the type.

```csharp
[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct, Inherited = false)]
public sealed class ViewModelAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[ViewModelAttribute](Aspid.MVVM.ViewModelAttribute.md)


