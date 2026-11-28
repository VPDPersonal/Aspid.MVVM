---
title: "Class OneTimeBindAttribute"
sidebar_label: "OneTimeBindAttribute"
description: "Class OneTimeBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeBindAttribute {#Aspid_MVVM_OneTimeBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md) <br/>
Assembly: Aspid.MVVM.dll <br/>

Binds the marked field or property in [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) using [`BindMode.OneTime`](Aspid.MVVM.BindMode.md).

```csharp
public sealed class OneTimeBindAttribute : BaseBindAttribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ←
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ←
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md) ←
[OneTimeBindAttribute](Aspid.MVVM.OneTimeBindAttribute.md)



## Examples


```csharp
[OneTimeBind] private readonly string _id;
```


## Remarks

Ignored on a set-only property.
