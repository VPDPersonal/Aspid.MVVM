---
title: "Class TwoWayBindAttribute"
sidebar_label: "TwoWayBindAttribute"
description: "Class TwoWayBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class TwoWayBindAttribute {#Aspid_MVVM_TwoWayBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md).
Cannot be applied to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields.

```csharp
public sealed class TwoWayBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[TwoWayBindAttribute](Aspid.MVVM.TwoWayBindAttribute.md)


