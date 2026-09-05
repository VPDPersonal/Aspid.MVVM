---
title: "Class BindAlsoAttribute"
sidebar_label: "BindAlsoAttribute"
description: "Class BindAlsoAttribute — Aspid.MVVM API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class BindAlsoAttribute {#Aspid_MVVM_BindAlsoAttribute}

Namespace: [Aspid.MVVM](Aspid.MVVM.md)  
Assembly: Aspid.MVVM.dll  

Sealed [`Attribute`](https://learn.microsoft.com/dotnet/api/system.attribute) applied to fields of a type carrying [`ViewModelAttribute`](Aspid.MVVM.ViewModelAttribute.md);
directs the Source Generator to also raise the change event of the property named [`BindAlsoAttribute.PropertyName`](Aspid.MVVM.BindAlsoAttribute.md#Aspid_MVVM_BindAlsoAttribute_PropertyName)
when the decorated field changes. Requires a companion [`BindAttribute`](Aspid.MVVM.BindAttribute.md), [`OneWayBindAttribute`](Aspid.MVVM.OneWayBindAttribute.md),
[`TwoWayBindAttribute`](Aspid.MVVM.TwoWayBindAttribute.md), or [`OneWayToSourceBindAttribute`](Aspid.MVVM.OneWayToSourceBindAttribute.md) on the same field.

```csharp
[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field, AllowMultiple = true)]
public sealed class BindAlsoAttribute : Attribute
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) ← 
[BindAlsoAttribute](Aspid.MVVM.BindAlsoAttribute.md)



## Constructors

### BindAlsoAttribute\(string\) {#Aspid_MVVM_BindAlsoAttribute__ctor_System_String_}

Initializes a new instance of the [`BindAlsoAttribute`](Aspid.MVVM.BindAlsoAttribute.md) with the specified property name.

```csharp
public BindAlsoAttribute(string propertyName)
```

#### Parameters

`propertyName` [string](https://learn.microsoft.com/dotnet/api/system.string)

The name of the generated property whose change event should also be triggered.

## Properties

### PropertyName {#Aspid_MVVM_BindAlsoAttribute_PropertyName}

Gets the name of the generated property whose change event should also be triggered.

```csharp
public string PropertyName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

