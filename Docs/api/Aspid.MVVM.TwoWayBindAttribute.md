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

Generates a [`BindMode.TwoWay`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

```csharp
public sealed class TwoWayBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ← 
[TwoWayBindAttribute](Aspid.MVVM.TwoWayBindAttribute.md)



## Examples


```csharp
[TwoWayBind] private string _name;
```


## Remarks

Ignored on a <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/readonly">readonly</a> field and a get-only or set-only property.

