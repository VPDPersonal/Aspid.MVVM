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

Generates a [`BindMode.OneWay`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

```csharp
public sealed class OneWayBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[OneWayBindAttribute](Aspid.MVVM.OneWayBindAttribute.md)



## Examples


```csharp
[OneWayBind] private int _score;
```


## Remarks

Ignored on a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field and a set-only property.

