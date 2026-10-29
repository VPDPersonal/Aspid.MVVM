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

Generates a [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

```csharp
public sealed class OneWayToSourceBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[OneWayToSourceBindAttribute](Aspid.MVVM.OneWayToSourceBindAttribute.md)



## Examples


```csharp
[OneWayToSourceBind] private float _volume;
```


## Remarks

Ignored on a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field and a get-only property.

