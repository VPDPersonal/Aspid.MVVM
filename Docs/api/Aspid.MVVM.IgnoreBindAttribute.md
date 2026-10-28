---
title: "Class IgnoreBindAttribute"
sidebar_label: "IgnoreBindAttribute"
description: "Class IgnoreBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class IgnoreBindAttribute {#Aspid_MVVM_IgnoreBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Excludes the field or property of a [`[View]`](Aspid.MVVM.ViewAttribute.md) class from the generated binding code.

```csharp
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
public sealed class IgnoreBindAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[IgnoreBindAttribute](Aspid.MVVM.IgnoreBindAttribute.md)



## Examples


```csharp
[IgnoreBind]
[SerializeField] private MonoBinder[] _previewBinders;
```


