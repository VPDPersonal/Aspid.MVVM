---
title: "Class OneWayBindAttribute"
sidebar_label: "OneWayBindAttribute"
description: "Class OneWayBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayBindAttribute {#Aspid_MVVM_OneWayBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.OneWay`](Aspid.MVVM.BindMode.md).
On <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields the mode resolves to [`BindMode.OneTime`](Aspid.MVVM.BindMode.md), matching
[`BindAttribute`](Aspid.MVVM.BindAttribute.md) with [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public sealed class OneWayBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[OneWayBindAttribute](Aspid.MVVM.OneWayBindAttribute.md)


