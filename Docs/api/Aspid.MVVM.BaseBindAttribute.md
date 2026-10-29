---
title: "Class BaseBindAttribute"
sidebar_label: "BaseBindAttribute"
description: "Class BaseBindAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BaseBindAttribute {#Aspid_MVVM_BaseBindAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

[`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) shared by [`BindAttribute`](Aspid.MVVM.BindAttribute.md) and the bind attributes with a fixed mode.

```csharp
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
public abstract class BaseBindAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BaseBindAttribute](Aspid.MVVM.BaseBindAttribute.md)

#### Derived

[BindAttribute](Aspid.MVVM.BindAttribute.md), 
[OneTimeBindAttribute](Aspid.MVVM.OneTimeBindAttribute.md), 
[OneWayBindAttribute](Aspid.MVVM.OneWayBindAttribute.md), 
[OneWayToSourceBindAttribute](Aspid.MVVM.OneWayToSourceBindAttribute.md), 
[TwoWayBindAttribute](Aspid.MVVM.TwoWayBindAttribute.md)



## Remarks

The Source Generator recognizes the bind attributes by name, so a custom subclass generates nothing.

