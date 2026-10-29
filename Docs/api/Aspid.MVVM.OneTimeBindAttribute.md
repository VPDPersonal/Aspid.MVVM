---
title: "Class OneTimeBindAttribute"
sidebar_label: "OneTimeBindAttribute"
description: "Class OneTimeBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class OneTimeBindAttribute {#Aspid_MVVM_OneTimeBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Generates a [`BindMode.OneTime`](Aspid.MVVM.BindMode.md) bindable member for the field or property of a [`[ViewModel]`](Aspid.MVVM.ViewModelAttribute.md) class.

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

