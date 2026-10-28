---
title: "Class ViewModelAttribute"
sidebar_label: "ViewModelAttribute"
description: "Class ViewModelAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewModelAttribute {#Aspid_MVVM_ViewModelAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Generates the [`IViewModel`](Aspid.MVVM.IViewModel.md) implementation of the class from its bind members and
[`[RelayCommand]`](Aspid.MVVM.RelayCommandAttribute.md) methods.

```csharp
[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct, Inherited = false)]
public sealed class ViewModelAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[ViewModelAttribute](Aspid.MVVM.ViewModelAttribute.md)



## Examples


```csharp
[ViewModel]
public partial class PlayerViewModel { }
```


## Remarks

The class must be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/partial-method">partial</a>; a struct is ignored.

