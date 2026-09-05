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

Serves as the base class for all binding-related attributes.
Derive from this class to implement custom binding attributes for use with ViewModels.
This class itself does not contain any logic and is used primarily as a marker for attribute hierarchy.
Classes that inherit from [`BaseBindAttribute`](Aspid.MVVM.BaseBindAttribute.md) must be manually added to the Source Generator 
to generate the appropriate binding logic. This process does not happen automatically.

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


