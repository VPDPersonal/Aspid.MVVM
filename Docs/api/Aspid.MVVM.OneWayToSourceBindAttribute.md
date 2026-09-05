---
title: "Class OneWayToSourceBindAttribute"
sidebar_label: "OneWayToSourceBindAttribute"
description: "Class OneWayToSourceBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceBindAttribute {#Aspid_MVVM_OneWayToSourceBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to emit a bindable property locked to [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).
Cannot be applied to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> fields.

```csharp
public sealed class OneWayToSourceBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[OneWayToSourceBindAttribute](Aspid.MVVM.OneWayToSourceBindAttribute.md)


