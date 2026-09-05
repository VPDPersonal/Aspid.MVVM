---
title: "Class ViewAttribute"
sidebar_label: "ViewAttribute"
description: "Class ViewAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ViewAttribute {#Aspid_MVVM_ViewAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed marker [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) that drives the Source Generator to emit an [`IView`](Aspid.MVVM.IView.md)
implementation for the decorated class or struct and to analyze code blocks within the type.

```csharp
[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct, Inherited = false)]
public sealed class ViewAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[ViewAttribute](Aspid.MVVM.ViewAttribute.md)



## Properties

### AutoBinderFields {#Aspid_MVVM_ViewAttribute_AutoBinderFields}

Indicates whether the Source Generator should emit binder fields for
[`IView<T>`](Aspid.MVVM.IView-1.md) bindable members that are not already declared on the View.
Defaults to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>. Set to <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> to suppress this generation
for the decorated View — useful for Views that wire binders manually or do not need an
inspector-driven layout.

```csharp
public bool AutoBinderFields { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

