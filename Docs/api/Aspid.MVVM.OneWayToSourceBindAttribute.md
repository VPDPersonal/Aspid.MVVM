---
title: "Class OneWayToSourceBindAttribute"
sidebar_label: "OneWayToSourceBindAttribute"
description: "Class OneWayToSourceBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneWayToSourceBindAttribute {#Aspid_MVVM_OneWayToSourceBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Binds the marked field or property in [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) using [`BindMode.OneWayToSource`](Aspid.MVVM.BindMode.md).

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
